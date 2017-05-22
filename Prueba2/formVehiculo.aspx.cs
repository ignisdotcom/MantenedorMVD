using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Prueba2
{
    public partial class formVehiculo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvMostrar(sender, e);
        }

        protected void gvMostrar(object sender, EventArgs e)
        {
            GridView.DataSource = AccesoLogica.ObtenerVehiculos();
            GridView.DataBind();
        }

        //Validar que El campo Rut del DUEÑO, número,  soporte solo el ingreso de n° enteros
        private bool validarEntero()
        {
            try
            {
                Convert.ToInt32(txtDueño.Text);
                Convert.ToInt32(txtAño.Text);
                return true;
            }
            catch (Exception e)
            {
                lblError.Visible = true;
                lblError.Text =
                    "El campo dueño debe contener un rut sin puntos ni guión. También, el campo año, solo debe contener números.";
                return false;               
            }
        }

        private bool validarDueño()
        {
            try
            {
                Convert.ToInt32(txtDueño.Text);
                return true;
            }
            catch (Exception e)
            {
                lblError.Visible = true;
                lblError.Text =
                    "El rut ingresado no es válido, recuerde que el rut debe ser escrito sin dígito verificador, sin puntos y sin guión.";
                return false;
            }
        }
        //Validar que los campos Patente, Marca, Modelo, Año, Color, Rut no se encuentren vacíos.
        private bool validarVacios()
        {
            if (txtPatente.Text != String.Empty
                && txtMarca.Text != String.Empty
                && txtModelo.Text != String.Empty
                && txtAño.Text != String.Empty
                && txtColor.Text != String.Empty
                && txtDueño.Text != String.Empty 
                )
            {
                return true;
            }
            lblError.Visible = true;
            lblError.Text =
                "Por favor, revise haber completado toda la información.Los campos Patente, Marca, Modelo, Año, Color, Rut del Dueño no pueden estar vacíos.";
            return false;
            
        }

        private bool validarPatente()
        {
            if (txtPatente.Text != String.Empty)
            {
                if (txtPatente.Text.ToString().Length == 6)
                {
                    return true;  
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text =
                        "La patente debe contar con 6 caracteres.";
                    return false;
                }
            }
            lblError.Visible = true;
            lblError.Text =
                "Para realizar una busqueda por patente, debe completar el campo.";
            return false;

        }

        protected void btnBuscarPatente_Click(object sender, EventArgs e)
        {

            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Visible = true;
            btnCancelar.Text = "CANCELAR";

            try
            {
                if (validarPatente())
                {
                    if (!Page.IsValid)
                        return;
                    AccesoLogica negocio = new AccesoLogica();
                    string buscar = txtPatente.Text;
                    GridView.DataSource = AccesoLogica.BuscarVehiculosPatente(buscar);
                    GridView.DataBind();

                    if (GridView != null)
                    {
                        if (GridView.Rows.Count > 0)
                        {
                            habilitarCampos();
                            txtPatente.Enabled = false;
                            btnGuardar.Enabled = false;
                            btnInsertar.Enabled = false;
                            btnModificar.Enabled = true;
                            lblError.Visible = false;
                            lblSucess.Visible = true;
                            lblSucess.Text = "Se han encontrado registros asociados a la patente ingresada.";
                        }
                        else
                        {
                            btnModificar.Enabled = false;
                            lblError.Visible = true;
                            lblError.Text = "No se han encontrado registros asociados a la patente ingresada.";
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

        private void limpiarCampos()
        {
            txtPatente.Text = String.Empty;
            txtMarca.Text = String.Empty;
            txtModelo.Text = String.Empty;
            txtAño.Text = String.Empty;
            txtColor.Text = String.Empty;
            txtDueño.Text = String.Empty;
            lblError.Visible = false;
            lblError.Text = " ";
            lblSucess.Visible = false;
            lblSucess.Text = " ";
        }
        private void habilitarCampos()
        {
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtAño.Enabled = true;
            txtColor.Enabled = true;
            txtDueño.Enabled = true;
        }

        private void deshabilitarCampos()
        {
            txtMarca.Enabled = false;
            txtModelo.Enabled = false;
            txtAño.Enabled = false;
            txtColor.Enabled = false;
 
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            #region Acciones Campos y botones
            btnModificar.Enabled = false;
            btnBuscarPatente.Enabled = false;
            btnBuscarDueño.Enabled = false;
            btnCancelar.Visible = true;
            habilitarCampos();
            #endregion

            try
            {
                if (validarVacios())
                {
                    if (validarEntero())
                    {
                        if (validarPatente())
                        {
                            if (!Page.IsValid)
                                return;
                            AccesoLogica negocio = new AccesoLogica();
                            int rut = Convert.ToInt32(txtDueño.Text);
                            string patente = txtPatente.Text;
                            string marca = txtMarca.Text;
                            string modelo = txtModelo.Text;
                            int annio = Convert.ToInt32(txtAño.Text);
                            string color = txtColor.Text;
                            int resultado = negocio.InsertVehiculo(patente, marca, modelo, annio, color, rut);
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
            }
            catch (Exception exception)
            {
                lblError.Visible = true;
                lblError.Text = "Ya existe un vehículo con la patente ingresada, o el rut del dueño no existe en nuestros registros.";
            }
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
                        int rut = Convert.ToInt32(txtDueño.Text);
                        string patente = txtPatente.Text;
                        string marca = txtMarca.Text;
                        string modelo = txtModelo.Text;
                        int annio = Convert.ToInt32(txtAño.Text);
                        string color = txtColor.Text;
                        int resultado = negocio.ModifcarVehiculo(patente,marca,modelo,annio,color,rut);
                        if (resultado == 1)
                        {
                            btnCancelar.Text = "LIMPIAR";
                            btnGuardar.Enabled = false;
                            lblSucess.Visible = true;
                            lblError.Visible = false;
                            lblSucess.Text = "El registro se ha modificado con éxito.";
                        }
                        negocio = null;
                    }
                }
            }
            catch (Exception exception)
            {
                lblError.Visible = true;
                lblError.Text = exception.ToString();
            }
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnBuscarDueño.Enabled = true;
            btnBuscarPatente.Enabled = true;
            btnCancelar.Visible = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnInsertar.Enabled = true;
            txtPatente.Enabled = true;
            txtDueño.Enabled = true;
            limpiarCampos();
            deshabilitarCampos();
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            btnGuardar.Enabled = true;
            btnInsertar.Enabled = false;
            btnCancelar.Visible = true;
        }

        protected void btnBuscarDueño_Click(object sender, EventArgs e)
        {
            lblError.Visible = lblSucess.Visible = false;
            lblError.Text = lblSucess.Text = String.Empty;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Visible = true;
            btnCancelar.Text = "CANCELAR";

            try
            {
                if (validarDueño())
                {
                    if (!Page.IsValid)
                        return;
                    AccesoLogica negocio = new AccesoLogica();
                    int buscar = Convert.ToInt32(txtDueño.Text);
                    GridView.DataSource = AccesoLogica.BuscarVehiculosDueño(buscar);
                    GridView.DataBind();

                    if (GridView != null)
                    {
                        if (GridView.Rows.Count > 0)
                        {
                            txtPatente.Enabled = true;
                            btnGuardar.Enabled = false;
                            btnInsertar.Enabled = false;
                            btnModificar.Enabled = true;
                            btnBuscarPatente.Enabled = true;
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

        protected void GridView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}