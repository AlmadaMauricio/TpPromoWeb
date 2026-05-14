using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVoucher_Click(object sender, EventArgs e)
        {
            string voucherCode = txtVoucher.Text.Trim();
            if (string.IsNullOrEmpty(voucherCode))
            {
                lblError.Text = "¡Debe ingresar un código valido!";
                txtVoucher.BorderColor = System.Drawing.Color.Red;
            }
            else
            {
                VoucherNegocio negocio = new VoucherNegocio();
                try
                {
                    var voucher = negocio.buscarVoucher(voucherCode);

                    if (voucher != null)
                    {
                        if (voucher.IdArticulo != 0)
                        {
                            lblError.Text = "¡El voucher ya fue canjeado!";
                        }
                        else
                        {
                            Session["voucher"] = voucher;
                            Response.Redirect("CatalogoArticulos.aspx");
                        }
                    }
                    else
                    {
                        lblError.Text = "¡El código ingresado es inexistente!";
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "¡Hubo un error en la conexión con la bbdd! Error: " + ex.Message;
                }
            }
        }

        protected void txtVoucher_TextChanged(object sender, EventArgs e)
        {
            string voucherCode = txtVoucher.Text.Trim();

            if (!string.IsNullOrEmpty(voucherCode))
            {
                lblError.Text = String.Empty;
                txtVoucher.BorderColor = System.Drawing.Color.Black;
            }
        }
 
    }
}