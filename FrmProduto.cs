using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ativ_Sistema_de_Vendas
{
    public partial class FrmProduto : Form
    {
    public FrmProduto()
    {
    InitializeComponent();

    btnFechar.Click += btnFechar_Click;
    
    }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
