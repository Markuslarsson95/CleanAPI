namespace Infrastructure.Repositories.Login
{
    public interface ILoginRepository
    {
        Task<string> Login(string userName, string password);
    }
}
