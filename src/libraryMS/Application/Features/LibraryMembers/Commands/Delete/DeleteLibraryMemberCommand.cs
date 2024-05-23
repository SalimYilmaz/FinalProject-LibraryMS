using Application.Features.LibraryMembers.Constants;
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

namespace Application.Features.LibraryMembers.Commands.Delete;

public class DeleteLibraryMemberCommand : IRequest<DeletedLibraryMemberResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, LibraryMembersOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLibraryMembers"];

    public class DeleteLibraryMemberCommandHandler : IRequestHandler<DeleteLibraryMemberCommand, DeletedLibraryMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILibraryMemberRepository _libraryMemberRepository;
        private readonly LibraryMemberBusinessRules _libraryMemberBusinessRules;

        public DeleteLibraryMemberCommandHandler(IMapper mapper, ILibraryMemberRepository libraryMemberRepository,
                                         LibraryMemberBusinessRules libraryMemberBusinessRules)
        {
            _mapper = mapper;
            _libraryMemberRepository = libraryMemberRepository;
            _libraryMemberBusinessRules = libraryMemberBusinessRules;
        }

        public async Task<DeletedLibraryMemberResponse> Handle(DeleteLibraryMemberCommand request, CancellationToken cancellationToken)
        {
            LibraryMember? libraryMember = await _libraryMemberRepository.GetAsync(predicate: lm => lm.Id == request.Id, cancellationToken: cancellationToken);
            await _libraryMemberBusinessRules.LibraryMemberShouldExistWhenSelected(libraryMember);

            await _libraryMemberRepository.DeleteAsync(libraryMember!);

            DeletedLibraryMemberResponse response = _mapper.Map<DeletedLibraryMemberResponse>(libraryMember);
            return response;
        }
    }
}