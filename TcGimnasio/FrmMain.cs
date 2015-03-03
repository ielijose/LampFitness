using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;


namespace TcGimnasio
{
    public partial class FrmMain : RibbonForm
    {
        int typeAction = 0;
        DataRow drClientes = null;
        public FrmMain()
        {
            InitializeComponent();
            InitSkinGallery();
            InitGrid();

        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }
        BindingList<Person> gridDataList = new BindingList<Person>();
        void InitGrid()
        {
            this.pagosTableAdapter.FillPagos(this.dSDatos.Pagos);
            this.planesTableAdapter.Fill(this.dSDatos.Planes);
            this.mDPagoTableAdapter.Fill(this.dSDatos.MDPago);
            //BloquearClientes();

            int TotalClientes = 0;
            totalesTableAdapter1.FillTotalClientes(dSDatos.Totales);
            foreach (DataRow drTotalCliente in dSDatos.Totales.Rows)
            {
                TotalClientes = (int)drTotalCliente[0];
            }
            int TotalClientesActivos = 0;
            totalesTableAdapter1.FillByTotalClientesActivo(dSDatos.Totales);
            foreach (DataRow drTotalCliente in dSDatos.Totales.Rows)
            {
                TotalClientesActivos = (int)drTotalCliente[0];
            }
            int TotalClientesInactivos = 0;
            totalesTableAdapter1.FillByTotalClientesInactivo(dSDatos.Totales);
            foreach (DataRow drTotalCliente in dSDatos.Totales.Rows)
            {
                TotalClientesInactivos = (int)drTotalCliente[0];
            }
            int TotalClientesPagosVencidos = 0;
            totalesTableAdapter1.FillByTotalClientesVencidos(dSDatos.Totales, DateTime.Today);
            foreach (DataRow drTotalCliente in dSDatos.Totales.Rows)
            {
                TotalClientesPagosVencidos = (int)drTotalCliente[0];
            }
            int TotalClientesPorVencer = 0;
            totalesTableAdapter1.FillByTotalClientesPorVencer(dSDatos.Totales, DateTime.Today);
            foreach (DataRow drTotalCliente in dSDatos.Totales.Rows)
            {
                TotalClientesPorVencer = (int)drTotalCliente[0];
            }
            textEdit1.Text = TotalClientes.ToString();
            textEdit2.Text = TotalClientesActivos.ToString();
            textEdit3.Text = TotalClientesInactivos.ToString();
            textEdit4.Text = TotalClientesPagosVencidos.ToString();
            textEdit5.Text = TotalClientesPorVencer.ToString();
            siInfo.Caption = DateTime.Today.ToLongDateString();
        }

        private void BloquearClientes()
        {
            this.userinfoTableAdapter.FillCliente(this.dSDatos.Userinfo);
            int Dummy = 0;
            foreach (DataRow dr in dSDatos.Userinfo.Rows)
            {
                try
                {
                    if ((DateTime)dr["FecCorte"] <= DateTime.Today)
                    {
                        Dummy = 1;
                        dr["USerFlag"] = 3;
                    }
                }
                catch  (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            if (Dummy == 1)
            {
                this.userinfoTableAdapter.Update(dSDatos.Userinfo);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void activarTab(int action)
        {
            int countTab = xtraTabControl1.TabPages.Count;
            for (int i = 0; i < countTab; i++)
            {
                if (i == action)
                {
                    xtraTabControl1.TabPages[i].PageVisible = true;
                }
                else xtraTabControl1.TabPages[i].PageVisible = false;
            }
            //
            xtraTabControl1.TabPages[4].PageVisible = true;
            //xtraTabControl1.TabPages[5].PageVisible = true;
        }

        private void iNew_ItemClick(object sender, ItemClickEventArgs e)
        {

            FrmPagos fr = new FrmPagos();
            fr.ShowDialog();
            this.pagosTableAdapter.FillPagos(dSDatos.Pagos);


            switch (typeAction)
            {
                case 0:
                    typeAction = 0; //Clientes
                    break;
                case 1:
                    typeAction = 1; //Metodos de Pagos
                    break;
                case 2:
                    typeAction = 2; //Planes
                    break;
                case 3:
                    typeAction = 3; //Pagos
                    
                    break;
                case 4:
                    typeAction = 4; //Consultas
                    break;
                case 5:
                    typeAction = 5; //Inicio
                    break;
            }
        }

        private void tasksItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            typeAction = 0;
            this.userinfoTableAdapter.FillCliente(this.dSDatos.Userinfo);
            activarTab(typeAction);
        }

        private void inboxItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            typeAction = 1;
            this.mDPagoTableAdapter.Fill(this.dSDatos.MDPago);
            activarTab(typeAction);
        }

        private void outboxItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            typeAction = 2;
            this.planesTableAdapter.Fill(this.dSDatos.Planes);
            activarTab(typeAction);
        }

        private void calendarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            typeAction = 3;
            this.pagosTableAdapter.FillPagos(this.dSDatos.Pagos);
            activarTab(typeAction);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            drClientes = gridView1.GetDataRow(e.FocusedRowHandle);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmClientes fr = new FrmClientes(drClientes);
            fr.ShowDialog();
        }

        private void gridView4_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //drClientes = gridView1.GetDataRow(e.FocusedRowHandle);
        }

        private void iOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (typeAction)
            {
                case 0:

                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    typeAction = 4; //Consultas
                    break;
                case 5:
                    typeAction = 5; //Inicio
                    break;
            }
        }

        private void iSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            Validate();
            switch (typeAction)
            {
                case 0:
                    typeAction = 0; //Clientes
                    break;
                case 1:
                    mDPagoBindingSource.EndEdit();
                    this.mDPagoTableAdapter.Update(dSDatos.MDPago);
                    typeAction = 1; //Metodos de Pagos
                    break;
                case 2:
                    planesBindingSource.EndEdit();
                    this.planesTableAdapter.Update(dSDatos.Planes);
                    typeAction = 2; //Planes
                    break;
                case 3:
                    typeAction = 3; //Pagos
                    pagosBindingSource.EndEdit();
                    break;
                case 4:
                    typeAction = 4; //Consultas
                    break;
                case 5:
                    typeAction = 5; //Inicio
                    break;
            }
        }

        private void pictureEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void iFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            BloquearClientes();
        }

        private void xtraTabPage5_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}