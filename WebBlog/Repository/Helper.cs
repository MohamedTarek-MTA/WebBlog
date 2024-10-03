using System.Security.Cryptography;
using System.Text;

namespace WebBlog.Repository
{
    public class Helper
    {
            public static bool IsJpeg(byte[] imageBytes)
            {
                return imageBytes.Length > 2 &&
                   imageBytes[0] == 0xFF &&
                   imageBytes[1] == 0xD8 &&
                   imageBytes[imageBytes.Length - 2] == 0xFF &&
                   imageBytes[imageBytes.Length - 1] == 0xD9;
            }
            public static bool IsPng(byte[] imageBytes)
            {
                return imageBytes.Length >= 8 &&
                       imageBytes[0] == 0x89 &&
                       imageBytes[1] == 0x50 &&
                       imageBytes[2] == 0x4E &&
                       imageBytes[3] == 0x47 &&
                       imageBytes[4] == 0x0D &&
                       imageBytes[5] == 0x0A &&
                       imageBytes[6] == 0x1A &&
                       imageBytes[7] == 0x0A;
            }
            public static string HashPassword(string password)
            {
                using (var sha256 = SHA256.Create())
                {

                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));


                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in hashedBytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
    }

