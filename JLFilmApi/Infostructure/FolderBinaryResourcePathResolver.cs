using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace JLFilmApi.Infostructure
{
    public class FolderBinaryResourcePathResolver : IBinaryResourcePathResolver
    {
        private const string PATH = "C:\\Users\\myUser\\Desktop\\";
        public async Task<byte[]>  FindAndGet(string resourceName)
        {
            byte[] image = null;
            if (resourceName != null)
            {
                image = await File.ReadAllBytesAsync(PATH + resourceName);
            }           
            return image;
        }
    }
}
