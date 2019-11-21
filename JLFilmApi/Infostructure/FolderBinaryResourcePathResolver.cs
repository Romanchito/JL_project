using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Infostructure
{
    public class FolderBinaryResourcePathResolver : IBinaryResourcePathResolver
    {
        public ICollection<byte> FindAndGet(string resourceName)
        {
            throw new NotImplementedException();
        }
    }
}
