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

namespace Application.Features.LibraryMembers.Commands.Update;

public class UpdateLibraryMemberCommand : IRequest<UpdatedLibraryMemberResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid LibraryId { get; set; }
    public Guid MemberId { get; set; }

    public string[] Roles => [Admin, Write, LibraryMembersOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLibraryMembers"];

    public class UpdateLibraryMemberCommandHandler : IRequestHandler<UpdateLibraryMemberCommand, UpdatedLibraryMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILibraryMemberRepository _libraryMemberRepository;
        private readonly LibraryMemberBusinessRules _libraryMemberBusinessRules;

        public UpdateLibraryMemberCommandHandler(IMapper mapper, ILibraryMemberRepository libraryMemberRepository,
                                         LibraryMemberBusinessRules libraryMemberBusinessRules)
        {
            _mapper = mapper;
            _libraryMemberRepository = libraryMemberRepository;
            _libraryMemberBusinessRules = libraryMemberBusinessRules;
        }

        public async Task<UpdatedLibraryMemberResponse> Handle(UpdateLibraryMemberCommand request, CancellationToken cancellationToken)
        {
            LibraryMember? libraryMember = await _libraryMemberRepository.GetAsync(predicate: lm => lm.Id == request.Id, cancellationToken: cancellationToken);
            await _libraryMemberBusinessRules.LibraryMemberShouldExistWhenSelected(libraryMember);
            libraryMember = _mapper.Map(request, libraryMember);

            await _libraryMemberRepository.UpdateAsync(libraryMember!);

            UpdatedLibraryMemberResponse response = _mapper.Map<UpdatedLibraryMemberResponse>(libraryMember);
            return response;
        }
    }
}