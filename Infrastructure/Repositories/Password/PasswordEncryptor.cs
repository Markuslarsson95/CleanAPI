namespace Infrastructure.Repositories.Password
{
    public class PasswordEncryptor : IPasswordEncryptor
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
