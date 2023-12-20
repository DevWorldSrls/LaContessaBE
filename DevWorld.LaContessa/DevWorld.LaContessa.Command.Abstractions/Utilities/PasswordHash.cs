using System.Security.Cryptography;

namespace DevWorld.LaContessa.Command.Abstractions.Utilities;

public class PasswordManager
{
    // Costanti per la configurazione di PBKDF2
    private const int SaltSize = 32; // Lunghezza del salt in byte
    private const int Iterations = 10000; // Numero di iterazioni PBKDF2
    private const int KeySize = 32; // Lunghezza della chiave in byte

    // Metodo per generare un salt casuale
    private static byte[] GenerateSalt()
    {
#pragma warning disable SYSLIB0023 // Type or member is obsolete
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);
            return salt;
        }
#pragma warning restore SYSLIB0023 // Type or member is obsolete
    }

    // Metodo per criptare la password
    public static string EncryptPassword(string password)
    {
        byte[] salt = GenerateSalt();

#pragma warning disable SYSLIB0041 // Type or member is obsolete
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
        {
            byte[] hash = pbkdf2.GetBytes(KeySize);

            // Combina il salt e il risultato di PBKDF2
            byte[] hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

            return Convert.ToBase64String(hashBytes);
        }
#pragma warning restore SYSLIB0041 // Type or member is obsolete
    }

    // Metodo per verificare la corrispondenza di una password decriptata
    public static bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        // Decodifica la password criptata
        byte[] hashBytes = Convert.FromBase64String(storedPassword);

        // Estrae il salt dalla password criptata
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // Calcola l'hash della password inserita con lo stesso salt
#pragma warning disable SYSLIB0041 // Type or member is obsolete
        using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations))
        {
            byte[] hash = pbkdf2.GetBytes(KeySize);

            // Confronta l'hash calcolato con quello memorizzato
            for (int i = 0; i < KeySize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
        }
#pragma warning restore SYSLIB0041 // Type or member is obsolete

        return true;
    }
}