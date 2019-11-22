using Microsoft.AspNetCore.Http;

namespace JLFilmApi.Infostructure
{
    public class TakingImageModel
    {
        public TakingImageModel(string type, string fileName)
        {
            Type = type;
            FileName = fileName;
        }

        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
