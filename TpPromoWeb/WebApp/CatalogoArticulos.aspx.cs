using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class CatalogoArticulos : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos = new List<Articulo>();
        private object lblVoucher;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Voucher"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }
            Voucher voucher = Session["voucher"] as Voucher;
            lbVoucher.Text = voucher.CodigoVoucher;


            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                listaArticulos = negocio.Listar();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}