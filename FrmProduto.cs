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

        public void verificaCampos()
        {
    bool camposPreenchidos = txtCdProduto.Text != "" && txtNomeProduto.Text != "" && txtPrecoProduto.Text != "" && cBoxCategoria.Text != "";

            if (camposPreenchidos)
            {
                if (!int.TryParse(txtCdProduto.Text.Trim(), out int cdProduto))
                {
                    throw new Exception("O código deve ser um número inteiro. Tente Novamente!");
                }
                if (!decimal.TryParse(txtPrecoProduto.Text.Trim(), out decimal precoProduto))
                {
                    throw new Exception("O preço deve ser válido. Tente Novamente!"); 
                }
                if(precoProduto <= 0)
                {
                    throw new Exception("O preço deve ser maior que 0. Tente Novamente!");
                }

                string nmProduto = txtNomeProduto.Text.Trim();
                string ctgProduto = cBoxCategoria.Text;

                MessageBox.Show("Dados salvos com sucesso!");
            }
            else
            {
                throw new Exception("Todos os campos devem ser preenchidos!");
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                verificaCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }
    }
}
