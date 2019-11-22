using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace JLFilmApi.Infostructure
{
    public class SolutionBinaryResourceResolver : IBinaryResourcePathResolver
    {
        private static IWebHostEnvironment myEnvironment;
        private byte[] image = null;

        public SolutionBinaryResourceResolver(IWebHostEnvironment environment)
        {
            myEnvironment = environment;
        }

        public async Task<string> Upload(IFormFile file)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(myEnvironment.WebRootPath + @"\AccountImages\"))
                {
                    Directory.CreateDirectory(myEnvironment.WebRootPath + @"\AccountImages\");
                }

                using (FileStream fileStream = System.IO.File.Create(myEnvironment.WebRootPath + @"\AccountImages\" + file.FileName))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return file.FileName;
                }
            }
            else
            {
                return null;
            }
        }

        Task<byte[]> IBinaryResourcePathResolver.Take(string resourceName)
        {
            throw new NotImplementedException();
        }
    }
      
}
