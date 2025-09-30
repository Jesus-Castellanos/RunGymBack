using Isopoh.Cryptography.Argon2;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Security; // Necesario para SecureString o si usas char[]

namespace RunGym.Utils
{
    public static class Argon2Hasher
    {
        // ... (Parámetros de Costo sin cambios) ...

        private const int Iterations = 4;
        private const int MemorySize = 65536;
        private const int DegreeOfParallelism = 2;
        private const int SaltSize = 16;

        // --- Cambio en la configuración para ser más explícito ---
        private static Argon2Config GetBaseConfig()
        {
            return new Argon2Config
            {
                TimeCost = Iterations,
                MemoryCost = MemorySize,
                Lanes = DegreeOfParallelism,
                // El Password se asignará justo antes de Hashear
            };
        }

        // ------------------------------------------------------------------

        public static string Hashear(string textoPlano)
        {
            if (string.IsNullOrEmpty(textoPlano))
            {
                throw new ArgumentNullException(nameof(textoPlano), "La contraseña en texto plano no puede ser nula o vacía.");
            }

            // 💡 MEJORA DE SEGURIDAD: Convertir a byte[] SOLO para la operación y limpiar el array después
            byte[] passwordBytes = Encoding.UTF8.GetBytes(textoPlano);

            try
            {
                byte[] saltBytes = new byte[SaltSize];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(saltBytes);
                }

                Argon2Config config = GetBaseConfig();
                config.Salt = saltBytes;
                config.Password = passwordBytes; // Asignamos los bytes aquí

                // 2. Ejecutar el Hasher y obtener la cadena serializada.
                return Argon2.Hash(config);
            }
            finally
            {
                // 🔑 LIMPIEZA CRÍTICA: Sobrescribir los bytes de la contraseña en la memoria
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
            }
        }

        // ------------------------------------------------------------------

        public static bool Verificar(string textoPlano, string hashAlmacenado)
        {
            if (string.IsNullOrEmpty(textoPlano) || string.IsNullOrEmpty(hashAlmacenado))
            {
                return false;
            }

            // 💡 MEJORA DE SEGURIDAD: Convertir a byte[] SOLO para la operación
            byte[] passwordBytes = Encoding.UTF8.GetBytes(textoPlano);
            bool verified = false;

            try
            {
                // Verificar la contraseña usando los bytes
                verified = Argon2.Verify(hashAlmacenado, passwordBytes);
            }
            catch (Exception)
            {
                // Se atrapa cualquier excepción (como hash malformado) y se devuelve false por defecto
                verified = false;
            }
            finally
            {
                // 🔑 LIMPIEZA CRÍTICA: Sobrescribir los bytes de la contraseña en la memoria
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
            }

            return verified;
        }
    }
}