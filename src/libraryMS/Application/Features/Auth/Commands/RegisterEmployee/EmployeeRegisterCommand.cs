using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Constants;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Features.Employees.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Services.AuthService;
using Application.Services.Employees;
using Application.Services.OperationClaims;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.RegisterEmployee;
public class EmployeeRegisterCommand : IRequest<RegisteredResponse>, ITransactionalRequest
{
    public UserForRegisterDto<EmployeeForRegisterDto> EmployeeForRegisterDto { get; set; }
    public string IpAddress { get; set; }
    public EmployeeRegisterCommand()
    {
        EmployeeForRegisterDto = null!;
        IpAddress = null!;
    }

    public EmployeeRegisterCommand(UserForRegisterDto<EmployeeForRegisterDto> employeeRegisterDto, string ipAddress)
    {
        EmployeeForRegisterDto = employeeRegisterDto;
        this.IpAddress = ipAddress;
    }

    public class EmployeeRegisterCommandHandler : IRequestHandler<EmployeeRegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMapper _mapper;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IEmployeeService _employeeService;
        private readonly EmployeeBusinessRules _employeeBusinessRules;
        public EmployeeRegisterCommandHandler(
            IUserRepository userRepository,
            IAuthService authService,
            AuthBusinessRules authBusinessRules, IMapper mapper, IEmployeeService employeeService,
            IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService,
            EmployeeBusinessRules employeeBusinessRules, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mapper = mapper;
            _employeeService = employeeService;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
            _employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(EmployeeRegisterCommand request, CancellationToken cancellationToken)
        {

            await _authBusinessRules.UserEmailShouldBeNotExists(request.EmployeeForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.EmployeeForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User newUser =
                new()
                {
                    Email = request.EmployeeForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            User createdUser = await _userRepository.AddAsync(newUser);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );

            Domain.Entities.Employee employee = _mapper.Map<Domain.Entities.Employee>(request.EmployeeForRegisterDto.RegisterForDto);
            
            employee.UserId = createdUser.Id;
            
            await _employeeBusinessRules.CheckIfPhoneNumberExists(employee.PhoneNumber, cancellationToken);
            
            await _employeeBusinessRules.CheckIfNationalIdentityExists(employee.NationalIdentity, cancellationToken);
            
            await _employeeService.AddAsync(employee);

            OperationClaim? operationClaim = await _operationClaimService.GetAsync(oc => oc.Name.Equals(AuthOperationClaims.GeneralEmployee));

            UserOperationClaim userOperationClaim = new UserOperationClaim()
            {
                UserId = createdUser.Id,
                OperationClaimId = operationClaim!.Id
            };
            await _userOperationClaimService.AddAsync(userOperationClaim);

            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };

            return registeredResponse;

        }
    }
}
