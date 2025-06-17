using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using TaskManager.Models;

namespace TaskManager.Utils;

public static class PasswordHelper
{
    private static int SaltLength = 16; // 16 B
    private static int MemorySize = 65536; // 64 MB
    private static int HashSize = 32; // 32 B
    private static int DegreeOfParallelism = 4;
    private static int Iterations = 4;

    public static PasswordHash GetPasswordHash(string password)
    {
        var salt = GenerateSalt();
        var hash = Hash(password, salt);        

        return new PasswordHash
        {
            Hash = FromBytesToString(hash),
            Salt = FromBytesToString(salt),
        };
    }

    public static bool VerifyPassword(string password, PasswordHash passwordHash)
    {
        var storedSalt = FromStringToBytes(passwordHash.Salt);
        var storedHash = FromStringToBytes(passwordHash.Hash);

        var hash = Hash(password, storedSalt);

        return hash.SequenceEqual(storedHash);
    }

    private static byte[] Hash(string password, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize = MemorySize,
            Iterations = Iterations,
        };

        return argon2.GetBytes(HashSize);
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[SaltLength];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return salt;
    }

    private static byte[] FromStringToBytes(string str)
    {
        return Convert.FromBase64String(str);
    }

    private static string FromBytesToString(byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }
}
