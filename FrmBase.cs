using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ativ_Sistema_de_Vendas

{

    public partial class FrmBase : Form

    {

        [DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]

        public static extern bool ReleaseCapture();

        public FrmBase()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void painelArrastar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }

        private void btnFechar_MouseEnter(object sender, EventArgs e)

        {
            btnFechar.BackColor = Color.Red;
        }
        private void btnFechar_MouseLeave(object sender, EventArgs e)
        {
            btnFechar.BackColor = Color.FromArgb(78, 38, 102);
        }
        private void btnMinimizar_MouseEnter(object sender, EventArgs e)
        {
            btnMinimizar.BackColor = Color.Red;
        }
        private void btnMinimizar_MouseLeave(object sender, EventArgs e)
        {
            btnMinimizar.BackColor = Color.FromArgb(78, 38, 102);
        }
        private void btnExcluir_MouseEnter(object sender, EventArgs e)
        {
            btnExcluir.BackColor = Color.Purple;
        }
        private void btnExcluir_MouseLeave(object sender, EventArgs e)
        {
            btnExcluir.BackColor = Color.FromArgb(78, 38, 102);
        }
        private void btnConsultar_MouseEnter(object sender, EventArgs e)
        {
            btnConsultar.BackColor = Color.Purple;
        }
        private void btnConsultar_MouseLeave(object sender, EventArgs e)
        {
            btnConsultar.BackColor = Color.FromArgb(78, 38, 102);
        }
        private void btnAlterar_MouseEnter(object sender, EventArgs e)
        {
            btnAlterar.BackColor = Color.Purple;
        }
        private void btnAlterar_MouseLeave(object sender, EventArgs e)
        {
            btnAlterar.BackColor = Color.FromArgb(78, 38, 102);
        }
        private void btnApagar_MouseEnter(object sender, EventArgs e)
        {
            btnApagar.BackColor = Color.Purple;
        }
        private void btnApagar_MouseLeave(object sender, EventArgs e)
        {
            btnApagar.BackColor = Color.FromArgb(78, 38, 102);
        }
        private void btnNovo_MouseEnter(object sender, EventArgs e)
        {
            btnNovo.BackColor = Color.Purple;
        }
        private void btnNovo_MouseLeave(object sender, EventArgs e)
        {
            btnNovo.BackColor = Color.FromArgb(78, 38, 102);
        }
        private void btnSalvar_MouseEnter(object sender, EventArgs e)
        {
            btnSalvar.BackColor = Color.Purple;
        }
        private void btnSalvar_MouseLeave(object sender, EventArgs e)
        {
            btnSalvar.BackColor = Color.FromArgb(78, 38, 102);
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

