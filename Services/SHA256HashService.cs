using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using WordCloud.Abstractions;

namespace WordCloud.Services
{
    //class that generates and hashes guid with salt for words id field
    public class SHA256HashService : IHashService
    {
        //method that generate and hash id
        public string Hash(string salt)
        {
            var guid = Guid.NewGuid().ToString();
            using var sha256 = SHA256.Create();
            var toHash = Encoding.UTF8.GetBytes(guid).Concat(Encoding.UTF8.GetBytes(salt)).ToArray();
            var hashed = sha256.ComputeHash(toHash);
            return Convert.ToBase64String(hashed);
        }
    }
}