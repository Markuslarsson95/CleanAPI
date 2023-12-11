using Domain.Repositories;
using MediatR;

namespace Application.Commands.Users.LoginUser
{
    public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly ILoginRepository _loginRepository;

        public LoginUserCommandHandler(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginToken = await _loginRepository.Login(request.UserLogin.UserName, request.UserLogin.Password);

            if (loginToken == null)
                return await Task.FromResult<string>(null!);

            return await Task.FromResult(loginToken);
        }
    }
}
