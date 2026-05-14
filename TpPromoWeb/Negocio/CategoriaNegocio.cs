using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        private AccesoDatos AccesoDatos;

        public CategoriaNegocio()
        {
            AccesoDatos = new AccesoDatos();
        }

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            try
            {
                AccesoDatos.setearConsulta("Select Id, Descripcion From CATEGORIAS");
                AccesoDatos.ejecutarLectura();

                while (AccesoDatos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)AccesoDatos.Lector["Id"];
                    aux.Descripcion = (string)AccesoDatos.Lector["Descripcion"];

                    lista.Add(aux);
                }


                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                AccesoDatos.cerrarConexion();
            }

        }

        public Categoria ObtenerPorId(int id)
        {
            try
            {
                Categoria categoria = new Categoria();

                AccesoDatos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS WHERE Id = @id");
                AccesoDatos.setearParametro("@id", id);
                AccesoDatos.ejecutarLectura();

                while (AccesoDatos.Lector.Read())
                {
                    categoria.Id = (int)AccesoDatos.Lector["Id"];
                    categoria.Descripcion = (string)AccesoDatos.Lector["Descripcion"];
                }

                return categoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AccesoDatos.cerrarConexion();
            }
        }
        public void Agregar(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) VALUES (@descripcion)");


                datos.setearParametro("@descripcion", nueva.Descripcion);


                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
            
        }

        public void ActualizarCategoria(Categoria categoria)
        {

            AccesoDatos.setearConsulta("UPDATE  CATEGORIAS SET Descripcion = @Descripcion WHERE Id = @Id");
            AccesoDatos.setearParametro("@Id", categoria.Id);
            AccesoDatos.setearParametro("@Descripcion", categoria.Descripcion);

            AccesoDatos.ejecutarAccion();
        }

        public void EliminarCategoria(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select Id from ARTICULOS where IdCategoria = @Id");
                datos.setearParametro("@Id", categoria.Id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    throw new Exception("No se puede eliminar la categoría porque existen artículos vinculados a ella.");
                }

                datos.cerrarConexion();

                datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @Id");
                datos.setearParametro("@Id", categoria.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
