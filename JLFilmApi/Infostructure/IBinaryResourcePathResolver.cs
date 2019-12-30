using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Infostructure
{
    public interface IBinaryResourcePathResolver
    {
        Task<string> Take(TakingImageModel resourceName);
        Task<string> Upload(IFormFile file, string userLogin);
        public void DeleteUnusingImage(string oldAccountImage);       
    }
}
