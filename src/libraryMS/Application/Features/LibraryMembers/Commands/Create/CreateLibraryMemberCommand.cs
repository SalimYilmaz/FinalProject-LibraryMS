using Application.Features.LibraryMembers.Constants;
using Application.Features.LibraryMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LibraryMembers.Constants.LibraryMembersOperationClaims;

namespace Application.Features.LibraryMembers.Commands.Create;

public class CreateLibraryMemberCommand : IRequest<CreatedLibraryMemberResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid MemberId { get; set; }
    public string[] Roles => [Admin, Write, LibraryMembersOperationClaims.Create, LibraryMembersOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLibraryMembers"];

    public class CreateLibraryMemberCommandHandler : IRequestHandler<CreateLibraryMemberCommand, CreatedLibraryMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILibraryMemberRepository _libraryMemberRepository;
        private readonly LibraryMemberBusinessRules _libraryMemberBusinessRules;

        public CreateLibraryMemberCommandHandler(IMapper mapper, ILibraryMemberRepository libraryMemberRepository,
                                         LibraryMemberBusinessRules libraryMemberBusinessRules)
        {
            _mapper = mapper;
            _libraryMemberRepository = libraryMemberRepository;
            _libraryMemberBusinessRules = libraryMemberBusinessRules;
        }

        public async Task<CreatedLibraryMemberResponse> Handle(CreateLibraryMemberCommand request, CancellationToken cancellationToken)
        {
            LibraryMember libraryMember = _mapper.Map<LibraryMember>(request);

            await _libraryMemberRepository.AddAsync(libraryMember);

            CreatedLibraryMemberResponse response = _mapper.Map<CreatedLibraryMemberResponse>(libraryMember);
            return response;
        }
    }
}