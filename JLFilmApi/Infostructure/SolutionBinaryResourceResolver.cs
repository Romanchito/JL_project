using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NuGet.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Infostructure
{
    public class SolutionBinaryResourceResolver : IBinaryResourcePathResolver
    {
        private static IWebHostEnvironment myEnvironment;
        private static IConfiguration myConfiguration;
        private static string hostPath;
        private Settings Settings { get; set; }
        private JLDatabaseContext jLDatabaseContext;
        public SolutionBinaryResourceResolver(IWebHostEnvironment environment, JLDatabaseContext jLDatabaseContext,
                IConfiguration configuration)
        {
            this.jLDatabaseContext = jLDatabaseContext;
            myConfiguration = configuration;
            hostPath = myConfiguration.GetSection("Settings").GetSection("CurrentHostPath").Value;
            myEnvironment = environment;
        }

        public async Task<string> Take(TakingImageModel takingModel)
        {
            if (takingModel == null) throw new ArgumentException();

            switch (takingModel.Type)
            {
                case "film": return await Task.FromResult(Path.Combine(hostPath, "FilmImages",  takingModel.FileName));
                case "user": return await Task.FromResult(Path.Combine(hostPath,"AccountImages", takingModel.FileName));
                default: return null;
            }
        }

        public async Task<string> Upload(IFormFile file, string userLogin)
        {
            if (file == null)
            {
                var user = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Login == userLogin);
                user.AccountImage = null;
                jLDatabaseContext.SaveChanges();
                return "default_user.png";
            }

            if (!Directory.Exists(myEnvironment.WebRootPath + @"\AccountImages\"))
            {
                Directory.CreateDirectory(myEnvironment.WebRootPath + @"\AccountImages\");
            }

            await UploadDataImage(file.FileName, userLogin);

            using (FileStream fileStream = System.IO.File.Create(myEnvironment.WebRootPath + @"\AccountImages\" + file.FileName))
            {
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return file.FileName;
            }

        }

        private async Task UploadDataImage(string imageName, string userLogin)
        {
            Users user = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Login == userLogin);

            if (user.AccountImage == null)
            {
                user.AccountImage = imageName;
                await jLDatabaseContext.SaveChangesAsync();
            }

            else if (user.AccountImage.Equals(imageName))
            {
                DeleteUnusingImage(imageName);
            }

            else
            {
                DeleteUnusingImage(user.AccountImage);
                user.AccountImage = imageName;
                await jLDatabaseContext.SaveChangesAsync();
            }
        }

        private void DeleteUnusingImage(string oldAccountImage)
        {
            System.IO.File.Delete($"wwwroot/{Path.Combine("AccountImages", oldAccountImage)}");
        }

    }

}
