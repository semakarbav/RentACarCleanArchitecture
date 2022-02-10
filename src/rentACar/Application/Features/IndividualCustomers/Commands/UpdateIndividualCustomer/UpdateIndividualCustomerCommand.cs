using Application.Features.IndividualCustomers.Rules;
using Application.Features.Users.Dtos;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Commands.UpdateIndividualCustomer
{
    public class UpdateIndividualCustomerCommand: IRequest<LoginUserDto>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public class UpdateIndividualCustomerCommandHandler : IRequestHandler<UpdateIndividualCustomerCommand, LoginUserDto>
        {
            IIndividualCustomerRepository _individualRepository;
            IMapper _mapper;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;
            IAuthService _authService;

            public UpdateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualRepository, IMapper mapper, IndividualCustomerBusinessRules individualCustomerBusinessRules, IAuthService authService)
            {
                _individualRepository = individualRepository;
                _mapper = mapper;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
                _authService = authService;
            }

            public async Task<LoginUserDto> Handle(UpdateIndividualCustomerCommand request, CancellationToken cancellationToken)
            {
                var individualCustpmerToUpdate = await _individualRepository.GetAsync(c => c.Id == request.Id);
                if (individualCustpmerToUpdate == null)
                {
                    throw new BusinessException("Bireysel Müşteri bulunamadı");
                }

                individualCustpmerToUpdate = _mapper.Map(request, individualCustpmerToUpdate);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                individualCustpmerToUpdate.PasswordSalt = passwordSalt;
                individualCustpmerToUpdate.PasswordHash = passwordHash;
                

                var createdIndCustomer = await _individualRepository
                    .UpdateAsync(individualCustpmerToUpdate);


                var accessToken = await _authService.CreateAccessToken(createdIndCustomer);

                return new LoginUserDto
                {
                    AccessToken = accessToken
                };
            }
        }
    }
}
