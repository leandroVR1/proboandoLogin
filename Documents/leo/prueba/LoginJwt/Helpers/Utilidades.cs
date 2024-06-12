using System.Security.Cryptography;
using System.Text;

namespace LoginJwt.Helpers
{
    public class Utilidades
    {
        public string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerificarSHA256(string textoPlano, string hashAlmacenado)
        {
            // Hash de la contraseña proporcionada
            var hashTextoPlano = EncriptarSHA256(textoPlano);

            // Comparar con el hash almacenado
            return string.Equals(hashTextoPlano, hashAlmacenado);
        }
    }
}
