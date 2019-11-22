using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace JLFilmApi.Infostructure
{
    public class FolderBinaryResourceResolver : IBinaryResourcePathResolver
    {
        private const string PATH = @"C:\Users\rtretyakov\Desktop\\";
        byte[] image = null;
        public async Task<byte[]>  Take(string resourceName)
        {
            
            if (resourceName != null)
            {
                image = await File.ReadAllBytesAsync(PATH + resourceName);
            }           
            return image;
        }

        public Task<string> Upload(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
