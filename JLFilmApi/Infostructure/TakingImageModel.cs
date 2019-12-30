using JLFilmApi.Controllers;
using Microsoft.AspNetCore.Http;

namespace JLFilmApi.Infostructure
{
    public class TakingImageModel
    {
        public TakingImageModel(Types type, string fileName)
        {
            Type = type;
            FileName = fileName;
        }

        public string FileName { get; set; }
        public Types Type { get; set; }
    }
}
