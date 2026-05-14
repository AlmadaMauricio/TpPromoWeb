using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class MarcaNegocio
    {
        private AccesoDatos AccesoDatos;

        public MarcaNegocio()
        {
            AccesoDatos = new AccesoDatos();
        }   

        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            
            AccesoDatos AccesoDatos = new AccesoDatos();
            try
            {
                AccesoDatos.setearConsulta("select Id, Descripcion From MARCAS");
                AccesoDatos.ejecutarLectura();

                while (AccesoDatos.Lector.Read())
                {
                    Marca aux = new Marca();
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

        public Marca ObtenerPorId(int id)
        {
            Marca marca = new Marca();
            
            try
            {
                AccesoDatos.setearConsulta("SELECT Id, Descripcion FROM MARCAS WHERE Id = @id");
                AccesoDatos.setearParametro("@id", id);
                AccesoDatos.ejecutarLectura();
                
                while (AccesoDatos.Lector.Read())
                {
                    marca.Id = (int)AccesoDatos.Lector["Id"];
                    marca.Descripcion = (string)AccesoDatos.Lector["Descripcion"];
                }

                return marca;
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

       /* public Marca CrearMarca(Marca marca)

        {
            return new Marca();

        }
        */
    

        public void eliminar(Marca marca) 
        {
            try
            {// antes de eliminar me fijo si hay un articulo que esta haciendo uso de esa marca
                AccesoDatos.setearConsulta("select * from ARTICULOS where IdMarca = @Id");
                AccesoDatos.setearParametro("@id", marca.Id);
                AccesoDatos.ejecutarLectura();

                if (AccesoDatos.Lector.Read())

                {//cuando ve que una marca esta siendo usada mandara mensaje de error y este error lo manda a la funcion de frmAdministrar marcas

                    throw new Exception("NO SE PUEDE ELIMINAR PORQUE LA MARCA ESTA SIENDO USADA");

                }
                AccesoDatos.cerrarConexion();

                AccesoDatos.setearConsulta("DELETE FROM MARCAS WHERE Id = @Id");
                AccesoDatos.setearParametro("@Id", marca.Id);
                AccesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                AccesoDatos.cerrarConexion();
            }


        }

        public void ActualizarMarca(Marca marca)
        {
            
            AccesoDatos.setearConsulta("UPDATE  MARCAS SET Descripcion = @Descripcion WHERE Id = @Id");
            AccesoDatos.setearParametro("@Id", marca.Id);
            AccesoDatos.setearParametro("@Descripcion", marca.Descripcion);

            AccesoDatos.ejecutarAccion();
        } 
        public List<Marca> BuscarMarca(string criterio)
        {

            return new List<Marca>();
        }


        public void Agregar (Marca nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("INSERT INTO MARCAS (Descripcion) VALUES (@descripcion)");


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
           

        
        
        
   
    }
}


