using JLFilmApi.Context;
using JLFilmApi.Controllers;
using JLFilmApi.DomainModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Infostructure
{
    public class SolutionBinaryResourceResolver : IBinaryResourcePathResolver
    {
        private const string ACCOUNT_IMAGE_FOLDER_NAME = @"\AccountImages\";
        private const string ACCOUNT_IMAGE_TYPE = ".png";
        private static IWebHostEnvironment myEnvironment;
        private static IConfiguration myConfiguration;
        private static string hostPath;       
        
        public SolutionBinaryResourceResolver(IWebHostEnvironment environment, IConfiguration configuration)
        {            
            myConfiguration = configuration;
            hostPath = myConfiguration.GetSection("Settings").GetSection("CurrentHostPath").Value;
            myEnvironment = environment;
        }

        public void DeleteUnusingImage(string oldAccountImage)
        {
            System.IO.File.Delete(myEnvironment.WebRootPath + ACCOUNT_IMAGE_FOLDER_NAME + oldAccountImage);
        }

        public async Task<string> Take(TakingImageModel takingModel)
        {
            if (takingModel == null) throw new ArgumentException();

            switch (takingModel.Type)
            {
                case Types.Film: return await Task.FromResult(Path.Combine(hostPath, "FilmImages",  takingModel.FileName));
                case Types.User: return await Task.FromResult(Path.Combine(hostPath,"AccountImages", takingModel.FileName));
                default: return null;
            }
        }

        public async Task<string> Upload(IFormFile file, string userLogin)
        {
            string fileName = userLogin + ACCOUNT_IMAGE_TYPE;
            if (file == null)
            {                
                return null;
            }

            if (!Directory.Exists(myEnvironment.WebRootPath + ACCOUNT_IMAGE_FOLDER_NAME))
            {
                Directory.CreateDirectory(myEnvironment.WebRootPath + ACCOUNT_IMAGE_FOLDER_NAME);
            }            

            using (FileStream fileStream = System.IO.File.Create(myEnvironment.WebRootPath + ACCOUNT_IMAGE_FOLDER_NAME + fileName))
            {
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return fileName;
            }

        }
       

    }

}
