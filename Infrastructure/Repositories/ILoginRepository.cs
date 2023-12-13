namespace Infrastructure.Repositories
{
    public interface ILoginRepository
    {
        Task<string> Login(string userName, string password);
    }
}
