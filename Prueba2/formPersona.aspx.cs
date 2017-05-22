using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Negocio;

namespace Prueba2
{
    public partial class formPersona : System.Web.UI.Page
    {
      public static int rutPersona;

        protected void Page_Load(object sender, EventArgs e)
        {
            gvMostrar(sender, e);
        }

        //Validar que El campo Rut, número,  soporte solo n° enteros (1 pto.)
        private bool validarEntero()
        {
            try
            {
                Convert.ToInt32(txtRut.Text);
                Convert.ToInt32(txtNumero.Text);
                return true;
            }
            catch (Exception e)
            {
                lblError.Visible = true;
                lblError.Text =
                    "Por favor, cerciórese de haber ingresado un rut solo con números y sin dígito verificador. El número de su dirección igualmente solo debe contener números";
                return false;
            }
        }

        private bool validarRut()
        {
            try
            {
                if (txtRut.Text != String.Empty)
                {
                    Convert.ToInt32(txtRut.Text);
                    return true;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Debe ingresar un Rut para hacer la búsqueda.";
                    return false;
                }
            }
            catch (Exception e)
            {
                lblError.Visible = true;
                lblError.Text =
                    "Por favor, cerciórese de haber ingresado un rut solo con números, sin puntos ni dígito verificador.";
                return false;
            }
        }

        //Validar que el campo Rut, Nombre, Apellido. Dirección, Número, Comuna no se encuentre vacío.
        private bool validarVacios()
        {
            if (txtRut.Text != String.Empty
                && txtNombre.Text != String.Empty
                && txtApellido.Text != String.Empty
                && txtCalle.Text != String.Empty
                && txtNumero.Text != String.Empty
                && txtComuna.Text != String.Empty
            )
            {
                if (validarSoloLetras(txtNombre.Text,txtApellido.Text,txtComuna.Text))
                {
                    return true;
                }
                
            }
            lblError.Visible = true;
            lblError.Text =
                "Por favor, revise haber completado toda la información.Los campos Rut, Nombre, Apellido. Dirección, Número, Comuna no pueden estar vacíos.";
            return false;

        }

        protected void gvMostrar(object sender, EventArgs e)
        {
            GridView.DataSource = AccesoLogica.ObtenerPersonas();
            GridView.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Visible = true;
            btnCancelar.Text = "CANCELAR";

            try
            {
                if (validarRut())
                {
                    if (!Page.IsValid)
                        return;
                    AccesoLogica negocio = new AccesoLogica();
                    int buscar = Int32.Parse(txtRut.Text);
                    GridView.DataSource = AccesoLogica.BuscarPersonas(buscar);                
                    GridView.DataBind();
                    
                    if (GridView != null)
                    {
                        if (GridView.Rows.Count > 0)
                        {
                            habilitarCampos();
                            txtRut.Enabled = false;
                            btnGuardar.Enabled = false;
                            btnInsertar.Enabled = false;
                            btnModificar.Enabled = true;
                            lblError.Visible = false;
                            lblSucess.Visible = true;
                            lblSucess.Text = "Se han encontrado registros asociados al rut ingresado.";                          
                       }
                        else
                        {
                            btnModificar.Enabled = false;
                            lblError.Visible = true;
                            lblError.Text = "No se han encontrado registros con el rut ingresado.";
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnBuscar.Enabled = true;
            btnCancelar.Visible = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnInsertar.Enabled = true;
            txtRut.Enabled = true;
            limpiarCampos();
            deshabilitarCampos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnBuscar.Enabled = false;
            btnCancelar.Visible = true;
            habilitarCampos();

            try
            {
                if (validarVacios())
                {
                    if (validarEntero())
                    {
                        if (!Page.IsValid)
                            return;
                        AccesoLogica negocio = new AccesoLogica();
                        int rut = Convert.ToInt32(txtRut.Text);
                        string nombre = txtNombre.Text;
                        string apellido = txtApellido.Text;
                        string calle = txtCalle.Text;
                        string comuna = txtComuna.Text;
                        int numero = Convert.ToInt32(txtNumero.Text);
                        int resultado = negocio.InsertPersona(rut,nombre,apellido,calle,numero,comuna);
                        if (resultado == 1)
                        {
                            btnCancelar.Text = "LIMPIAR";
                            btnGuardar.Enabled = false;
                            lblSucess.Visible = true;
                            lblError.Visible = false;
                            lblSucess.Text = "Nuevo Registro Agregado Satisfactoriamente.";
                        }
                        negocio = null;
                    }
                }
            }
            catch (Exception exception)
            {
                lblError.Visible = true;
                lblError.Text = "El registro a ingresar ya se encuentra en nuestras bases.";
            }
        }



        private void habilitarCampos()
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtCalle.Enabled = true;
            txtComuna.Enabled = true;
            txtNumero.Enabled = true;
        }

        private void deshabilitarCampos()
        {
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtCalle.Enabled = false;
            txtComuna.Enabled = false;
            txtNumero.Enabled = false;
        }

        private void limpiarCampos()
        {

            txtRut.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            txtCalle.Text = String.Empty;
            txtComuna.Text = String.Empty;
            txtNumero.Text = String.Empty;
            lblError.Visible = false;
            lblError.Text = " ";
            lblSucess.Visible = false;
            lblSucess.Text = " ";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            lblError.Visible = lblSucess.Visible = false;
            lblError.Text = lblSucess.Text = String.Empty;
            try
            {
                if (validarVacios())
                {
                    if (validarEntero())
                    {
                        if (!Page.IsValid)
                            return;
                        AccesoLogica negocio = new AccesoLogica();
                        int rut = Convert.ToInt32(txtRut.Text);
                        string nombre = txtNombre.Text;
                        string apellido = txtApellido.Text;
                        string calle = txtCalle.Text;
                        string comuna = txtComuna.Text;
                        int numero = Convert.ToInt32(txtNumero.Text);
                        int resultado = negocio.ModifcarPersona(rut,nombre, apellido, calle, numero, comuna);
                        if (resultado == 1)
                        {
                            btnCancelar.Text = "LIMPIAR";
                            btnGuardar.Enabled = false;
                            lblSucess.Visible = true;
                            lblError.Visible = false;
                            lblSucess.Text = "Nuevo Registro Agregado Satisfactoriamente.";
                        }
                        negocio = null;
                    }
                }
            }
            catch (Exception exception)
            {
                lblError.Visible = true;
                lblError.Text = "Ha ocurrido un error al intentar actualizar los registros.";
            }

        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            GridView.DataSource = AccesoLogica.ObtenerPersonas();
            GridView.DataBind();
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            btnGuardar.Enabled = true;
            btnInsertar.Enabled = false;
            btnCancelar.Visible = true;
        }

        protected void btnAgregarVehiculo_Click(object sender, EventArgs e)
        {
            if (validarRut())
            {
                rutPersona = Convert.ToInt32(txtRut.Text);
                Response.Redirect("~/formVehiculo.aspx");
            }
        }

        public bool validarSoloLetras(string string1, string string2, string string3)
        {
            try
            {
                if (Regex.IsMatch(string1, "^[a-zA-Z]+$") && Regex.IsMatch(string2, "^[a-zA-Z]+$") && Regex.IsMatch(string3, "^[a-zA-Z]+$"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                lblError.Visible = true;
                lblError.Text = "Nombre, Apellido y Comuna solo pueden contener letras.";
                return false;           
            }
        }
    }
}