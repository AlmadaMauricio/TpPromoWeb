using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Imagen
    {
        public Imagen() { }
        public Imagen(int id, int idArticulo, string imagenUrl)
        {
            this.Id = id;
            this.IdArticulo = idArticulo;
            this.ImagenUrl = imagenUrl;
        }
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public string ImagenUrl { get; set; }
        public override string ToString()
        {
            return "URL:" + ImagenUrl;
        }
    }
}
