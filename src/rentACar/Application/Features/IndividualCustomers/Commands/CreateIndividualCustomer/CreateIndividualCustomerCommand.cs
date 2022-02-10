using Application.Features.IndividualCustomers.Rules;
using Application.Features.Users.Dtos;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer
{
    public class CreateIndividualCustomerCommand:IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string NationalId { get; set; }
        public class CreateIndividualCustomerCommandHandler: IRequestHandler<CreateIndividualCustomerCommand, LoginUserDto>
        {
            IIndividualCustomerRepository _individualRepository;
            IMapper _mapper;
            IAuthService _authService;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;

            public CreateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualRepository, IMapper mapper, IAuthService authService, IndividualCustomerBusinessRules individualCustomerBusinessRules)
            {
                _individualRepository = individualRepository;
                _mapper = mapper;
                _authService = authService;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
            }

            public async Task<LoginUserDto> Handle(CreateIndividualCustomerCommand request, CancellationToken cancellationToken)
            {
                var indCustomerToAdd = _mapper.Map<IndividualCustomer>(request);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                indCustomerToAdd.PasswordSalt = passwordSalt;
                indCustomerToAdd.PasswordHash = passwordHash;
                indCustomerToAdd.Status = true;

                var createdIndCustomer = await _individualRepository
                    .AddAsync(indCustomerToAdd);


                var accessToken = await _authService.CreateAccessToken(createdIndCustomer);

                return new LoginUserDto
                {
                    AccessToken = accessToken
                };
            }
        }
    }
}
