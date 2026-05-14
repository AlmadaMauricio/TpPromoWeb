
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class ImagenNegocio
    {
        private AccesoDatos AccesoDatos;

        public ImagenNegocio()
        {
            AccesoDatos = new AccesoDatos();
        }

        public List<Imagen> Listar()
        {
            List<Imagen> lista = new List<Imagen>();

            try
            {
                AccesoDatos.setearConsulta("select Id, IdArticulo,ImagenUrl from IMAGENES");
                AccesoDatos.ejecutarLectura();

                while (AccesoDatos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)AccesoDatos.Lector["Id"];
                    aux.IdArticulo = (int)AccesoDatos.Lector["IdArticulo"];

                    aux.ImagenUrl = (string)AccesoDatos.Lector["ImagenUrl"];

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

        public void Agregar(Imagen nuevo)
        {
            try
            {
                AccesoDatos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                AccesoDatos.setearParametro("@IdArticulo", nuevo.IdArticulo);
                AccesoDatos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                AccesoDatos.ejecutarAccion();
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

        public List<Imagen> ObtenerImagenesPorArticuloId(int id)
        {
            try
            {
                List<Imagen> imagenes = new List<Imagen>();

                AccesoDatos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @id");
                AccesoDatos.setearParametro("@id", id);
                AccesoDatos.ejecutarLectura();

                while (AccesoDatos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)AccesoDatos.Lector["Id"];
                    aux.IdArticulo = (int)AccesoDatos.Lector["IdArticulo"];
                    aux.ImagenUrl = (string)AccesoDatos.Lector["ImagenUrl"];

                    imagenes.Add(aux);
                }

                return imagenes;
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

        public void EliminarPorArticuloId(int idArticulo)
        {
            try
            {
                AccesoDatos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @idArticulo");
                AccesoDatos.setearParametro("@idArticulo", idArticulo);
                AccesoDatos.ejecutarAccion();
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
    }
}