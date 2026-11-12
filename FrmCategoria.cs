using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ativ_Sistema_de_Vendas.BancoDados;

namespace Ativ_Sistema_de_Vendas
{
    public partial class FrmCategoria : Form
    {
        private bool Insercao = false;
        public FrmCategoria()
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
            if(txtCdCategoria.Text != "" && txtNomeCategoria.Text != "")
            {
                
                if(!int.TryParse(txtCdCategoria.Text.Trim(), out int cdCategoria)){
                    throw new Exception("Apenas números são aceitos. Tente Novamente!"); 
                }

                string nmCategoria = txtNomeCategoria.Text.Trim();
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
                if(!Insercao)
                {
                    throw new Exception("Escolha primeiro qual operação que deseja executar!");
                }

                verificaCampos();
                salvarCategoria();

                Insercao = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //INSERT

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
            Insercao = true;

            txtCdCategoria.Clear();
            txtNomeCategoria.Clear();

            txtNomeCategoria.Focus();
        }

        private void salvarCategoria()
        {
            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"INSERT INTO Categoria(CdCategoria,NomeCategoria)
                                VALUES (@CdCategoria, @NomeCategoria)";

                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CdCategoria", int.Parse(txtCdCategoria.Text));

                    cmd.Parameters.AddWithValue("@NomeCategoria",
                        txtNomeCategoria.Text.Trim());

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Categoria cadastrada com sucesso!");
        }

        //UPDATE

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        //SELECT

        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        //DELETE

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }


    }
}
