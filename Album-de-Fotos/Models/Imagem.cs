using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album_de_Fotos.Models
{
    public class Imagem
    {
        public Imagem()
        {

        }

        public int Id { get; set; }
        public string Link { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
