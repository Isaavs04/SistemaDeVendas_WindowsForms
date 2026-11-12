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
        //indica que o usuario clicou em NOVO
        private bool Insercao = false;
        //indica que o usuario clicou em ALTERAR
        private bool Alteracao = false;
        //indica que existe uma categoria consultada
        private bool categoriaCarregada = false;
        public FrmCategoria()
    {
        InitializeComponent();
        configurarFormulario();

        btnFechar.Click += btnFechar_Click;
    }

        private void configurarFormulario()
        {
            txtBuscarCategoria.Enabled = true;

            txtCdCategoria.Enabled = Insercao;
            txtNomeCategoria.Enabled = Insercao || Alteracao;

            btnNovo.Enabled = !Insercao && !Alteracao;
            btnSalvar.Enabled = Insercao || Alteracao;
            btnCancelar.Enabled = Insercao || Alteracao;

            btnAlterar.Enabled = categoriaCarregada && !Insercao && !Alteracao;
            btnExcluir.Enabled = categoriaCarregada && !Insercao && !Alteracao;
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
                if(!Insercao && !Alteracao)
                {
                    throw new Exception("Escolha primeiro qual operação que deseja executar!");
                }

                verificaCampos();

                if (Insercao)
                {
                  salvarCategoria();
                }
                else if (Alteracao)
                {
                    alterarCategoria();
                }

                Insercao = false;
                Alteracao = false;

                configurarFormulario();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //INSERT

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Insercao = true;
            Alteracao = false;
            categoriaCarregada = false;

            txtCdCategoria.Clear();
            txtNomeCategoria.Clear();

            configurarFormulario();

            txtCdCategoria.Focus();
        }

        private void salvarCategoria() //fazer verificação se o código é único
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

            Insercao = false;
            Alteracao = false;
            categoriaCarregada = true;

            configurarFormulario();
        }

        //UPDATE

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void alterarCategoria()
        {

        }

        //SELECT

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                consultarCategorias();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void consultarCategorias()
        {

            if (!int.TryParse(txtBuscarCategoria.Text.Trim(), out int codigo))
            {
                throw new Exception("Informe um código válido para consulta!");
            }

            Conexao conexao = new Conexao();

            using(SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"SELECT CdCategoria, NomeCategoria FROM Categoria WHERE CdCategoria = @CdCategoria";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@CdCategoria", SqlDbType.Int).Value = codigo;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtCdCategoria.Text = reader["CdCategoria"].ToString();
                            txtNomeCategoria.Text = reader["NomeCategoria"].ToString();

                            categoriaCarregada = true;
                            Insercao = false;
                            Alteracao = false;

                            configurarFormulario();
                        }
                        else
                        {
                            txtCdCategoria.Clear();
                            txtNomeCategoria.Clear();

                            categoriaCarregada = false;

                            configurarFormulario();

                            throw new Exception("Categoria não encontrada!");
                        }
                    }
                }
            }
        }

        //DELETE

        private void btnExcluir_Click(object sender, EventArgs e)
        { 

        }

        private void excluirCategoria()
        {

        }
    }
}
