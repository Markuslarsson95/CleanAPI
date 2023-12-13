using Application.Queries.Users.GetAll;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Queries.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = _userRepository.GetAll();

            return userList;
        }
    }
}
