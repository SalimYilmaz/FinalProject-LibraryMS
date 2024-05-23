using Application.Features.LibraryMembers.Constants;
using Application.Features.LibraryMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LibraryMembers.Constants.LibraryMembersOperationClaims;

namespace Application.Features.LibraryMembers.Queries.GetById;

public class GetByIdLibraryMemberQuery : IRequest<GetByIdLibraryMemberResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdLibraryMemberQueryHandler : IRequestHandler<GetByIdLibraryMemberQuery, GetByIdLibraryMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILibraryMemberRepository _libraryMemberRepository;
        private readonly LibraryMemberBusinessRules _libraryMemberBusinessRules;

        public GetByIdLibraryMemberQueryHandler(IMapper mapper, ILibraryMemberRepository libraryMemberRepository, LibraryMemberBusinessRules libraryMemberBusinessRules)
        {
            _mapper = mapper;
            _libraryMemberRepository = libraryMemberRepository;
            _libraryMemberBusinessRules = libraryMemberBusinessRules;
        }

        public async Task<GetByIdLibraryMemberResponse> Handle(GetByIdLibraryMemberQuery request, CancellationToken cancellationToken)
        {
            LibraryMember? libraryMember = await _libraryMemberRepository.GetAsync(predicate: lm => lm.Id == request.Id, cancellationToken: cancellationToken);
            await _libraryMemberBusinessRules.LibraryMemberShouldExistWhenSelected(libraryMember);

            GetByIdLibraryMemberResponse response = _mapper.Map<GetByIdLibraryMemberResponse>(libraryMember);
            return response;
        }
    }
}