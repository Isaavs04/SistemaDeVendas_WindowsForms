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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();

            this.IsMdiContainer = true;

            this.menuStrip1.MdiWindowListItem = this.janelasToolStripMenuItem;

            clientesToolStripMenuItem.Click += clientesToolStripMenuItem_Click;
            categoriasToolStripMenuItem.Click += categoriasToolStripMenuItem_Click;
            produtosToolStripMenuItem.Click += produtosToolStripMenuItem_Click;
            fecharTodasToolStripMenuItem.Click += fecharTodasToolStripMenuItem_Click;
            sobreToolStripMenuItem.Click += sobreToolStripMenuItem_Click;
            sairToolStripMenuItem.Click += sairToolStripMenuItem_Click;
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void cascataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente cliente = new FrmCliente();
            cliente.MdiParent = this;
            cliente.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoria categoria = new FrmCategoria();
            categoria.MdiParent = this;
            categoria.Show();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProduto produto = new FrmProduto();
            produto.MdiParent = this;
            produto.Show();
        }

        private void fecharTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form janela in this.MdiChildren)
            {
                janela.Close();
            }
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Sistema de Vendas\n\n" +
                "Desenvolvedores:\n" +
                "Isabelle Avelino dos Santos\n" +
                "Maria Paula Toledo da Silva\n" +
                "Bruna de Oliveira Bernardes dos Santos",
                "Sobre"
            );
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}