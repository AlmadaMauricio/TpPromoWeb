using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Negocio
{
    public class ArticuloNegocio
    {
        private readonly AccesoDatos AccesoDatos;
        private readonly ImagenNegocio ImagenNegocio;
        private readonly CategoriaNegocio CategoriaNegocio;
        private readonly MarcaNegocio MarcaNegocio;

        public ArticuloNegocio()
        {
            AccesoDatos = new AccesoDatos();
            ImagenNegocio = new ImagenNegocio();
            CategoriaNegocio = new CategoriaNegocio();
            MarcaNegocio = new MarcaNegocio();
        }

        public List<Articulo> Listar()
        {
            try
            {
                AccesoDatos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.IdMarca, A.IdCategoria FROM ARTICULOS A" );
                AccesoDatos.ejecutarLectura();
                List<Articulo> articulos = MapearConsultaArticulos(AccesoDatos.Lector);
                AccesoDatos.cerrarConexion();
                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Articulo> MapearConsultaArticulos(SqlDataReader reader)
        {
            List<Articulo> articulos = new List<Articulo>();
            
            try
            {
                while (reader.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)AccesoDatos.Lector["Id"];
                    articulo.Codigo = (string)AccesoDatos.Lector["Codigo"];
                    articulo.Nombre = (string)AccesoDatos.Lector["Nombre"];
                    articulo.Descripcion = (string)AccesoDatos.Lector["Descripcion"];
                    articulo.Precio = (float)(decimal)AccesoDatos.Lector["Precio"];

                    int categoriaId = (int)AccesoDatos.Lector["IdCategoria"];
                    articulo.Categoria = CategoriaNegocio.ObtenerPorId(categoriaId);
                    if (articulo.Categoria.Id == 0) articulo.Categoria = new Categoria(articulo.Categoria.Id, "Sin Categoria registrada");

                    int marcaId = (int)AccesoDatos.Lector["IdMarca"];
                    articulo.Marca = MarcaNegocio.ObtenerPorId(marcaId);
                    if (articulo.Marca.Id == 0) articulo.Marca = new Marca { Id = marcaId, Descripcion = "Sin Marca registrada" };

                    articulo.Imagenes = ImagenNegocio.ObtenerImagenesPorArticuloId(articulo.Id);
                    articulos.Add(articulo);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> Listar(string campo, string criterio, string filtro)
        {
            if(campo == null || criterio == null || filtro == null)
            {
                return Listar();
            }

            try {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.IdMarca, A.IdCategoria FROM ARTICULOS A";
                string where = "";

                if (!string.IsNullOrEmpty(filtro))
                {
                    switch (campo)
                    {
                        case "Nombre":
                        case "Codigo":
                        case "Descripcion":
                            where = $" WHERE A.{campo} ";
                            switch (criterio)
                            {
                                case "Comienza con":
                                    where += "LIKE @filtro";
                                    filtro = filtro + "%";
                                    break;
                                case "Termina con":
                                    where += "LIKE @filtro";
                                    filtro = "%" + filtro;
                                    break;
                                case "Contiene":
                                    where += "LIKE @filtro";
                                    filtro = "%" + filtro + "%";
                                    break;
                                default:
                                    where += "= @filtro";
                                    break;
                            }
                            break;
                        case "Precio":
                            where = $" WHERE A.{campo} ";
                            switch (criterio)
                            {
                                case "Mayor que":
                                    where += "> @filtro";
                                    break;
                                case "Menor que":
                                    where += "< @filtro";
                                    break;
                                case "Igual a":
                                    where += "= @filtro";
                                    break;
                            } 
                            break;
                        default:
                            break;
                    }
                }

                consulta += where;
                AccesoDatos.setearConsulta(consulta);

                if (!string.IsNullOrEmpty(filtro))
                    AccesoDatos.setearParametro("@filtro", filtro);

                AccesoDatos.ejecutarLectura();
                List<Articulo> articulos = MapearConsultaArticulos(AccesoDatos.Lector);

                return articulos;
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

        public void Crear(Articulo articulo)
        {
            try
            {
                AccesoDatos.setearConsulta(
                    "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                    "OUTPUT INSERTED.Id VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");

                AccesoDatos.setearParametro("@Codigo", articulo.Codigo);
                AccesoDatos.setearParametro("@Nombre", articulo.Nombre);
                AccesoDatos.setearParametro("@Descripcion", articulo.Descripcion);
                AccesoDatos.setearParametro("@IdMarca", articulo.Marca.Id);
                AccesoDatos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                AccesoDatos.setearParametro("@Precio", articulo.Precio);

                int articuloId = AccesoDatos.ejecutarCreacionRetornandoId();

                articulo.Imagenes.ForEach(imagen =>
                {
                    imagen.IdArticulo = articuloId;
                    ImagenNegocio.Agregar(imagen);
                });
            }
            finally
            {
                AccesoDatos.cerrarConexion();
            }
        }

        public void Actualizar(Articulo articulo)
        {
            try
            {
                AccesoDatos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria WHERE Id = @Id");
                AccesoDatos.setearParametro("@Codigo", articulo.Codigo);
                AccesoDatos.setearParametro("@Nombre", articulo.Nombre);
                AccesoDatos.setearParametro("@Descripcion", articulo.Descripcion);
                AccesoDatos.setearParametro("@Precio", articulo.Precio);
                AccesoDatos.setearParametro("@IdMarca", articulo.Marca.Id);
                AccesoDatos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                AccesoDatos.setearParametro("@Id", articulo.Id);

                AccesoDatos.ejecutarAccion();
                
                ImagenNegocio.EliminarPorArticuloId(articulo.Id);
                articulo.Imagenes.ForEach(imagen =>
                {
                    imagen.IdArticulo = articulo.Id;
                    ImagenNegocio.Agregar(imagen);
                });
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

        public void Eliminar(Articulo articulo)
        {
            try
            {
                ImagenNegocio.EliminarPorArticuloId(articulo.Id);

                AccesoDatos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @Id");
                AccesoDatos.setearParametro("@Id", articulo.Id);
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
