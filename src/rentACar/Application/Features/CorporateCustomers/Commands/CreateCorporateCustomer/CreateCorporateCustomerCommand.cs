using Application.Features.CorporateCustomers.Rules;
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

namespace Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer
{
    public class CreateCorporateCustomerCommand: IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public class CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand, LoginUserDto>
        {
            ICorporateCustomerRepository _corporateRepository;
            IMapper _mapper;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
            IAuthService _authService;

            public CreateCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateRepository, IMapper mapper, CorporateCustomerBusinessRules corporateCustomerBusinessRules, IAuthService authService)
            {
                _corporateRepository = corporateRepository;
                _mapper = mapper;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                _authService = authService;
            }

            public async Task<LoginUserDto> Handle(CreateCorporateCustomerCommand request, CancellationToken cancellationToken)
            {
                var corporateCustomerToAdd = _mapper.Map<CorporateCustomer>(request);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                corporateCustomerToAdd.PasswordSalt = passwordSalt;
                corporateCustomerToAdd.PasswordHash = passwordHash;
                corporateCustomerToAdd.Status = true;

                var createdIndCustomer = await _corporateRepository
                    .AddAsync(corporateCustomerToAdd);


                var accessToken = await _authService.CreateAccessToken(createdIndCustomer);

                return new LoginUserDto
                {
                    AccessToken = accessToken
                };
            }
        }
    }
}
