using Infrastructure.Repositories.Login;
using MediatR;
using Serilog;

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
            try
            {
                Log.Information($"Logging in user: {request.UserLogin.UserName}");

                var loginToken = await _loginRepository.Login(request.UserLogin.UserName, request.UserLogin.Password);

                if (loginToken == null)
                {
                    Log.Warning($"Login failed for user: {request.UserLogin.UserName}");
                    return await Task.FromResult<string>(null!);
                }

                return await Task.FromResult(loginToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing user login");
                throw new Exception("An error occurred while processing user login", ex);
            }
        }
    }
}
