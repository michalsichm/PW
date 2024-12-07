using System.Security.Cryptography;
using personal_calendar_application.Abstractions;

namespace personal_calendar_application.Services;


public sealed class HashService : IHashService
{
    private const int Salt = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(Salt);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Validate(string password, string hashedPassword)
    {
        string[] splitHash = hashedPassword.Split("-");
        byte[] hash = Convert.FromHexString(splitHash[0]);
        byte[] salt = Convert.FromHexString(splitHash[1]);

        byte[] calculatedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, calculatedHash);
    }
}