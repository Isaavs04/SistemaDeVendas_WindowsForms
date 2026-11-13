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
        //Guarda o nome da categoria caso a alteração seja cancelada
        private string nomeCategoriaOriginal;
        public FrmCategoria()
    {
        InitializeComponent();
        configurarFormulario();

        btnFechar.Click += btnFechar_Click;
    }

        private void configurarFormulario()
        {
            txtBuscarCategoria.Enabled = !Insercao && !Alteracao;

            txtCdCategoria.Enabled = Insercao;
            txtNomeCategoria.Enabled = Insercao || Alteracao;

            btnNovo.Enabled = !Insercao && !Alteracao;
            btnConsultar.Enabled = !Insercao && !Alteracao;

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
            if(!string.IsNullOrWhiteSpace(txtCdCategoria.Text) && !string.IsNullOrWhiteSpace(txtNomeCategoria.Text))
            {
                
                if(!int.TryParse(txtCdCategoria.Text.Trim(), out int cdCategoria)){
                    throw new Exception("Apenas números são aceitos para o código. Tente Novamente!"); 
                }

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
                categoriaCarregada = false;

                configurarFormulario();

                txtCdCategoria.Clear();
                txtNomeCategoria.Clear();
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
        }

        //UPDATE
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try { 
                if (!categoriaCarregada)
                {
                    throw new Exception("Consulte uma categoria antes de alterar!");
                }

                nomeCategoriaOriginal = txtNomeCategoria.Text;

                Insercao = false;
                Alteracao = true;

                configurarFormulario();

                txtNomeCategoria.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
}

        private void alterarCategoria()
        {
            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"UPDATE Categoria SET NomeCategoria = @NomeCategoria WHERE CdCategoria = @CdCategoria";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CdCategoria",
                        int.Parse(txtCdCategoria.Text));

                    cmd.Parameters.AddWithValue("@NomeCategoria",
                        txtNomeCategoria.Text.Trim());

                    //retorna a quantidade de linha afetadas pelo comando
                    int linhasAfetadas = cmd.ExecuteNonQuery(); 

                    if (linhasAfetadas == 0)
                    {
                        throw new Exception("Nenhuma categoria foi alterada.");
                    }
                }
            }

            MessageBox.Show("Categoria alterada com sucesso!");
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
            try
            {
                if (!categoriaCarregada)
                {
                    throw new Exception("Consulte uma categoria antes de excluí-la!");
                }

                int codigo = int.Parse(txtCdCategoria.Text);
                string nome = txtNomeCategoria.Text;

                DialogResult resposta = MessageBox.Show(
                    $"Deseja excluir a categoria {codigo} — {nome}?",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resposta == DialogResult.Yes)
                {
                    excluirCategoria(codigo);

                    txtCdCategoria.Clear();
                    txtNomeCategoria.Clear();

                    categoriaCarregada = false;

                    configurarFormulario();

                    MessageBox.Show("Categoria excluída com sucesso!");
                }
            }
            catch (SqlException ex)
            { //erro do sql que indica uma restrição de integridade
                if (ex.Number == 547)
                {
                    MessageBox.Show("Não é possível excluir esta categoria, pois existem produtos vinculados a ela.", 
                        "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                  MessageBox.Show("Erro ao excluir a categoria: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void excluirCategoria(int codigo)
        {
            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"DELETE FROM Categoria
                       WHERE CdCategoria = @CdCategoria";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@CdCategoria", SqlDbType.Int).Value = codigo;

                    //retorna a quantidade de linha afetadas pelo comando
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas == 0)
                    {
                        throw new Exception("A categoria não foi encontrada para exclusão.");
                    }
                }
            }
        }

        //Cancela alteração do btnAlteração ou do btnNovo
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Alteracao)
            {
                txtNomeCategoria.Text = nomeCategoriaOriginal;
            }
            else if (Insercao)
            {
                txtCdCategoria.Clear();
                txtNomeCategoria.Clear();
            }

            Insercao = false;
            Alteracao = false;

            configurarFormulario();
        }
    }
}
