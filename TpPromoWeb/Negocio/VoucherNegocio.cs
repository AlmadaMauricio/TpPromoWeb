using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VoucherNegocio
    {
        public Voucher buscarVoucher(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            datos.setearConsulta("SELECT CodigoVoucher, IdCliente, FechaCanje, IdArticulo FROM Vouchers WHERE CodigoVoucher = @codigo");
            datos.setearParametro("@codigo", codigo);
            datos.ejecutarLectura();

            try
            {
                Voucher nuevoVoucher = new Voucher();

                if (datos.Lector.Read())
                {
                    nuevoVoucher.CodigoVoucher = (string)datos.Lector["CodigoVoucher"];

                    nuevoVoucher.IdCliente = datos.Lector["IdCliente"] != DBNull.Value ? (int)datos.Lector["IdCliente"] : 0;
                    nuevoVoucher.FechaCanje = datos.Lector["FechaCanje"] != DBNull.Value ? (DateTime)datos.Lector["FechaCanje"] : DateTime.MinValue;
                    nuevoVoucher.IdArticulo = datos.Lector["IdArticulo"] != DBNull.Value ? (int)datos.Lector["IdArticulo"] : 0;

                    //nuevoVoucher.IdCliente = (int)datos.Lector["IdCliente"];         


                    return nuevoVoucher;
                }

                return null;
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
