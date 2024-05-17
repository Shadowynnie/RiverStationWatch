using Microsoft.JSInterop;
using RiverStationWatch.Data.Model;
using System;
using System.Security.Cryptography;
using System.Text;

namespace RiverStationWatch.Services
{
    public class TokenGenerator
    {
        public string GenerateTokenStr()
        {
            // Definice délky tokenu (v bajtech)
            int tokenLength = 32;

            // Pole pro uchování náhodných bajtů
            byte[] randomBytes = new byte[tokenLength];

            // Generátor náhodných čísel
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                // Generování náhodných bajtů
                rng.GetBytes(randomBytes);
            }

            // Převod náhodných bajtů na řetězec
            StringBuilder builder = new StringBuilder();
            foreach (byte b in randomBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            // Vrací random token jako string
            return builder.ToString();
        }
        //===============================================================================

        public ApiToken GetToken(DateTime validity) 
        {
            ApiToken token = new ApiToken();
            token.Token = GenerateTokenStr();
            token.Validity = validity;
            return token;
        }
    }
}
