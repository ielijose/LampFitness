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
    public partial class FrmClientes : DevExpress.XtraEditors.XtraForm
    {
        DataRow dr;
        public FrmClientes(DataRow _dr)
        {
            dr = _dr;
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            textEdit1.Text = dr["UserId"].ToString();
            textEdit2.Text = dr["Name"].ToString();
            textEdit3.Text = dr["Address"].ToString();
            comboBoxEdit1.Text = dr["Sex"].ToString();
            dateEdit2.DateTime = (DateTime)dr["EmployDate"];
            textEdit4.Text = dr["Telephone"].ToString();
            textEdit5.Text = dr["Mobile"].ToString();
            textEdit6.Text = dr["IDCard"].ToString();
            textEdit7.Text = dr["Email"].ToString();
            dateEdit1.DateTime = (DateTime)dr["Brithday"];
            dateEdit3.DateTime = (DateTime)dr["FecCorte"];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Grabar el registro
            dr["UserId"] = textEdit1.Text;
            dr["Name"] = textEdit2.Text;
            dr["Address"] = textEdit3.Text;
            dr["Sex"] = comboBoxEdit1.Text;
            dr["EmployDate"] = dateEdit2.DateTime;
            dr["Telephone"] = textEdit4.Text;
            dr["Mobile"] = textEdit5.Text;
            dr["Email"] = textEdit7.Text;
            dr["Brithday"] = dateEdit1.DateTime;
            dr["FecCorte"] = dateEdit3.DateTime;
            this.userinfoTableAdapter1.Update(dr);
            Close();
        }
    }
}