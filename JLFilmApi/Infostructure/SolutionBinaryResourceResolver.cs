using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JLFilmApi.Infostructure
{
    public class SolutionBinaryResourceResolver : IBinaryResourcePathResolver
    {
        private static IWebHostEnvironment myEnvironment;       
        public SolutionBinaryResourceResolver(IWebHostEnvironment environment)
        {
            myEnvironment = environment;
        }

        public async Task<string> Take(TakingImageModel takingModel)
        {
           if (takingModel == null) return null;
           
            if(takingModel.Type == "Film")
            {
                return await Task.FromResult(Path.Combine("FilmImages", takingModel.FileName));
            }

            if (takingModel.Type == "User")
            {
                return await Task.FromResult(Path.Combine("AccountImages", takingModel.FileName));
            }

            return null;
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

       
    }
      
}
