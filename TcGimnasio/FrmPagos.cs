using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TcGimnasio
{
    public partial class FrmPagos : DevExpress.XtraEditors.XtraForm
    {
        DataRow dr;
        string IdPlan = "";
        int Inscripcion = 0, Mensualidad = 0;
        public FrmPagos()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsDatos1.Planes' table. You can move, or remove it, as needed.
            this.planesTableAdapter.Fill(this.dsDatos1.Planes);
            // TODO: This line of code loads data into the 'dsDatos1.MDPago' table. You can move, or remove it, as needed.
            this.mDPagoTableAdapter.Fill(this.dsDatos1.MDPago);
            this.userinfoTableAdapter1.FillCliente(dsDatos1.Userinfo);
            radioButton2.Checked = true;
            dateEdit1.EditValue = DateTime.Now;
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Grabar el registro
            dr = dsDatos1.Pagos.NewPagosRow();
            //dr["Id"] = textEdit1.Text;
            DataRow drCliente = gridLookUpEdit1View.GetFocusedDataRow();
            //dr["Codigo"] = Convert.ToInt32(lookUpEdit3.GetColumnValue("Userid"));
            dr["Codigo"] =drCliente["Userid"].ToString();
            dr["Descripcion"] = textEdit2.Text;
            dr["Modo"] = lookUpEdit1.Text;
            dr["Plan"] = IdPlan;
            dr["NDocumento"] = Convert.ToInt32(textEdit3.Text);
            dr["Monto"] = Convert.ToInt32(textEdit7.Text);
            dr["Fecha"] = dateEdit1.DateTime;
            dsDatos1.Pagos.Rows.Add(dr);
            this.pagosTableAdapter1.Update(dsDatos1.Pagos);

            DateTime NewDate = dateEdit1.DateTime.AddMonths(1);
            //userinfoTableAdapter1.UpdateQueryFechaCorte(NewDate, lookUpEdit3.GetColumnValue("Userid").ToString());
            userinfoTableAdapter1.UpdateQueryFechaCorte(NewDate, drCliente["Userid"].ToString());
            
            Close();
        }

       

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {            
            ActualizarPrecio();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textEdit2.EditValue = "Pago de Mensualidad.";
            ActualizarPrecio();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textEdit2.EditValue = "Pago de Inscripción y Mensualidad.";
            ActualizarPrecio();
        }
       
        private void ActualizarPrecio()
        {
            DataRow drFila;
            Mensualidad = 0;
            Inscripcion = 0;

            IdPlan = null;

            if (lookUpEdit2.EditValue != null)
            {
                IdPlan = lookUpEdit2.EditValue.ToString();
            }else{
                DataRow drPlan = gridLookUpEdit1View.GetFocusedDataRow();

                if(drPlan != null){
                    IdPlan = drPlan["Plan"].ToString();
                    lookUpEdit2.EditValue = Convert.ToInt32(IdPlan);
                }
            }
                       
            
            drFila = dsDatos1.Planes.FindById(Convert.ToInt32(IdPlan));
            
            try
            {
                Inscripcion = (int)drFila["PrecioInsc"];
            }
            catch
            {
                Inscripcion = 0;
            }
            try
            {
                Mensualidad = (int)drFila["PrecioMens"];
            }
            catch
            {
                Mensualidad = 0;
            }



            if (radioButton2.Checked)
                textEdit7.Text = Mensualidad.ToString();
            if (radioButton1.Checked)
                textEdit7.Text = ((int)Inscripcion + (int)Mensualidad).ToString();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
             ActualizarPrecio();
        }

        
        
    }
}