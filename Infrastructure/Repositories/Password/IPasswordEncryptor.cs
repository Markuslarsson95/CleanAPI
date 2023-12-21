namespace Infrastructure.Repositories.Password
{
    public interface IPasswordEncryptor
    {
        string Encrypt(string password);
        bool Verify(string password, string hashedPassword);
    }
}
