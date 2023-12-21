using Domain.Models;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var wantedUser = _userRepository.GetById(request.Id);

            if (wantedUser == null)
                return Task.FromResult<User>(null!);

            return wantedUser;
        }
    }
}
