using Application.Queries.Users.GetAll;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IGenericRepository<User> _userRepository;

        public GetAllUsersQueryHandler(IGenericRepository<User> userRepository)
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
