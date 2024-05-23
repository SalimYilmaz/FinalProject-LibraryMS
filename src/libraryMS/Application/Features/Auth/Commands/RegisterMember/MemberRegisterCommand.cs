using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Constants;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Features.Members.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Services.AuthService;
using Application.Services.Members;
using Application.Services.OperationClaims;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.RegisterMember;

public class MemberRegisterCommand : IRequest<RegisteredResponse>, ITransactionalRequest
{
    public UserForRegisterDto<MemberForRegisterDto> MemberForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public MemberRegisterCommand()
    {
        MemberForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public MemberRegisterCommand(UserForRegisterDto<MemberForRegisterDto> memberForRegisterDto, string ipAddress)
    {
        MemberForRegisterDto = memberForRegisterDto;
        IpAddress = ipAddress;
    }

    public class MemberRegisterCommandHandler : IRequestHandler<MemberRegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly MemberBusinessRules _memberBusinessRules;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
        public MemberRegisterCommandHandler(
            IUserRepository userRepository,
            IAuthService authService,
            AuthBusinessRules authBusinessRules, IMapper mapper, IMemberService memberService,
            IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService,
            MemberBusinessRules memberBusinessRules, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mapper = mapper;
            _memberService = memberService;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
            _memberBusinessRules = memberBusinessRules;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(MemberRegisterCommand request, CancellationToken cancellationToken)
        {

            await _authBusinessRules.UserEmailShouldBeNotExists(request.MemberForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.MemberForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User newUser =
                new()
                {
                    Email = request.MemberForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            User createdUser = await _userRepository.AddAsync(newUser);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );

            Member member = _mapper.Map<Member>(request.MemberForRegisterDto.RegisterForDto);

            member.UserId = createdUser.Id;

            await _memberBusinessRules.CheckIfMemberPhoneNumberAlreadyExists(member.PhoneNumber, cancellationToken);

            await _memberBusinessRules.CheckIfMemberNationalIdentityAlreadyExists(member.NationalIdentity, cancellationToken);

            await _memberService.AddAsync(member);
            
            OperationClaim? operationClaim = await _operationClaimService.GetAsync(oc => oc.Name.Equals(AuthOperationClaims.GeneralMember));
            
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);
            
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
