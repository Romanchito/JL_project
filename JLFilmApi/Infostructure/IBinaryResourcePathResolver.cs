using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Infostructure
{
    public interface IBinaryResourcePathResolver
    {
        public Task<string> Take(TakingImageModel resourceName);
        public Task<string> Upload(IFormFile file);
    }
}
