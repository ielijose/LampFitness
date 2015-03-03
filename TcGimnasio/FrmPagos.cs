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
        string IdPlan = "", NombrePlan = "";
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
            //textEdit1.Text = dr["Id"].ToString();
            //textEdit2.Text = dr["Descripcion"].ToString();
            //textEdit3.Text = dr["NDocumento"].ToString();
            ////comboBoxEdit1.Text = dr["Sex"].ToString();
            ////dateEdit2.DateTime = (DateTime)dr["EmployDate"];
            ////textEdit4.Text = dr["Telephone"].ToString();
            ////textEdit5.Text = dr["Mobile"].ToString();
            ////textEdit6.Text = dr["IDCard"].ToString();
            //textEdit7.Text = dr["Monto"].ToString();
            //dateEdit1.DateTime = (DateTime)dr["Brithday"];
            ////dateEdit3.DateTime = (DateTime)dr["FecCorte"];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Grabar el registro
            dr = dsDatos1.Pagos.NewPagosRow();
            //dr["Id"] = textEdit1.Text;
            DataRow drCliente = gridLookUpEdit1View.GetFocusedDataRow();
            //dr["Codigo"] = Convert.ToInt32(lookUpEdit3.GetColumnValue("Userid"));
            dr["Codigo"] = Convert.ToInt32(drCliente["Userid"]);
            dr["Descripcion"] = textEdit2.Text;
            dr["Modo"] = lookUpEdit1.Text;
            dr["Plan"] = IdPlan;
            dr["NDocumento"] = Convert.ToInt32(textEdit3.Text);
            dr["Monto"] = Convert.ToInt32(textEdit7.Text);
            dr["Fecha"] = dateEdit1.DateTime;
            dsDatos1.Pagos.Rows.Add(dr);
            this.pagosTableAdapter1.Update(dsDatos1.Pagos);

            DateTime NewDate = dateEdit1.DateTime.AddDays(30);
            //userinfoTableAdapter1.UpdateQueryFechaCorte(NewDate, lookUpEdit3.GetColumnValue("Userid").ToString());
            userinfoTableAdapter1.UpdateQueryFechaCorte(NewDate, drCliente["Userid"].ToString());
            
            Close();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            Mensualidad = 0;
            Inscripcion = 0;
            DataRow drPlan = gridLookUpEdit1View.GetFocusedDataRow();
            IdPlan = drPlan["Plan"].ToString();
            planesTableAdapter.FillById(dsDatos1.Planes, Convert.ToInt32(IdPlan));
            foreach (DataRow drFila in dsDatos1.Planes.Rows)
            {
                NombrePlan = drFila["NombrePlan"].ToString();
                try
                {
                    Inscripcion = (int)drFila["PrecioInsc"];
                }
                catch {
                    Inscripcion = 0;
                }
                try
                {
                    Mensualidad = (int)drFila["PrecioMens"];
                }
                catch {
                    Mensualidad = 0;
                }
            }
            textEdit1.Text = IdPlan;
            textEdit4.Text = NombrePlan;
            if (radioButton2.Checked)
                textEdit7.Text = Mensualidad.ToString();
            if (radioButton1.Checked)
                textEdit7.Text = Inscripcion.ToString();
        }
    }
}