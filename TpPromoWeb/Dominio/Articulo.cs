using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    
    public class Articulo
    {
        public Articulo()
        {
            Imagenes = new List<Imagen>();
        }
        public int Id { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public int IdMarca { get; set; }
        [DisplayName("IdCategoría")]
        public int IdCategoria { get; set; }
        [DisplayName("Imágenes")]
        public List<Imagen> Imagenes { get; set; }
        [DisplayName("Categoría")]
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
    }
}
