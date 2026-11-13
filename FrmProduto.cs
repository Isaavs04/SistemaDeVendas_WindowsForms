using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ativ_Sistema_de_Vendas.BancoDados;

namespace Ativ_Sistema_de_Vendas
{
    public partial class FrmProduto : Form
    {
        //indica que o usuario clicou em NOVO
        private bool Insercao = false;
        //indica que o usuario clicou em ALTERAR
        private bool Alteracao = false;
        //indica que existe um produto consultado
        private bool produtoCarregado = false;
        //Guarda o nome do produto caso a alteração seja cancelada
        private string nomeProdutoOriginal;
        //Guarda o preço do produto caso a alteração seja cancelada
        private string precoProdutoOriginal;
        //Guarda a categoria do produto caso a alteração seja cancelada
        private object categoriaProdutoOriginal;
        public FrmProduto()
        {
            InitializeComponent();
            carregarCategorias();
            configurarFormulario();

            btnFechar.Click += btnFechar_Click;
        }
        private void configurarFormulario()
        {
            txtBuscarProduto.Enabled = !Insercao && !Alteracao;

            txtCdProduto.Enabled = Insercao;
            txtNomeProduto.Enabled = Insercao || Alteracao;
            txtPrecoProduto.Enabled = Insercao || Alteracao;
            cBoxCategoria.Enabled = Insercao || Alteracao;

            btnNovo.Enabled = !Insercao && !Alteracao;
            btnConsultar.Enabled = !Insercao && !Alteracao;

            btnSalvar.Enabled = Insercao || Alteracao;
            btnCancelar.Enabled = Insercao || Alteracao;

            btnAlterar.Enabled = produtoCarregado && !Insercao && !Alteracao;
            btnExcluir.Enabled = produtoCarregado && !Insercao && !Alteracao;
        }

        private void carregarCategorias()
        {
            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"SELECT CdCategoria, NomeCategoria
                       FROM Categoria";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                {
                    DataTable tabela = new DataTable();

                    adapter.Fill(tabela);

                    cBoxCategoria.DataSource = tabela;
                    cBoxCategoria.DisplayMember = "NomeCategoria";
                    cBoxCategoria.ValueMember = "CdCategoria";
                    cBoxCategoria.SelectedIndex = -1;
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void verificaCampos()
        {
          bool camposPreenchidos = !string.IsNullOrWhiteSpace(txtCdProduto.Text) 
                            && !string.IsNullOrWhiteSpace(txtNomeProduto.Text) 
                            && !string.IsNullOrWhiteSpace(txtPrecoProduto.Text) 
                            && cBoxCategoria.SelectedIndex >= 0;

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
                if (!Insercao && !Alteracao)
                {
                    throw new Exception("Escolha primeiro qual operação que deseja executar!");
                }

                verificaCampos();

                if (Insercao)
                {
                    salvarProduto();
                }
                else if (Alteracao)
                {
                    alterarProduto();
                }

                Insercao = false;
                Alteracao = false;
                produtoCarregado = false;

                configurarFormulario();

                txtCdProduto.Clear();
                txtNomeProduto.Clear();
                txtPrecoProduto.Clear();
                cBoxCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //INSERT
        private void btnNovo_Click(object sender, EventArgs e)
        {
            Insercao = true;
            Alteracao = false;
            produtoCarregado = false;

            txtCdProduto.Clear();
            txtNomeProduto.Clear();
            txtPrecoProduto.Clear();
            cBoxCategoria.SelectedIndex = -1;

            configurarFormulario();

            txtNomeProduto.Focus();
        }

        private void salvarProduto()
        {
            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"INSERT INTO Produto(CdProduto,NomeProduto,PrecoProduto,CdCategoria)
                                VALUES (@CdProduto, @NomeProduto, @PrecoProduto, @CdCategoria)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@CdProduto", SqlDbType.Int).Value =
                                   int.Parse(txtCdProduto.Text);

                    cmd.Parameters.Add("@NomeProduto", SqlDbType.VarChar, 100).Value =
                        txtNomeProduto.Text.Trim();

                    cmd.Parameters.Add("@PrecoProduto", SqlDbType.Decimal).Value =
                        decimal.Parse(txtPrecoProduto.Text);

                    cmd.Parameters["@PrecoProduto"].Precision = 10;
                    cmd.Parameters["@PrecoProduto"].Scale = 2;

                    cmd.Parameters.Add("@CdCategoria", SqlDbType.Int).Value =
                        Convert.ToInt32(cBoxCategoria.SelectedValue);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Produto cadastrado com sucesso!");
        }

        //UPDATE
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!produtoCarregado)
                {
                    throw new Exception("Consulte um produto antes de alterar!");
                }

                nomeProdutoOriginal = txtNomeProduto.Text;
                precoProdutoOriginal = txtPrecoProduto.Text;
                categoriaProdutoOriginal = cBoxCategoria.SelectedValue;

                Insercao = false;
                Alteracao = true;

                configurarFormulario();

                txtNomeProduto.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void alterarProduto()
        {
            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"UPDATE Produto SET NomeProduto = @NomeProduto, 
                            PrecoProduto = @PrecoProduto,
                            CdCategoria = @CdCategoria WHERE CdProduto = @CdProduto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@CdProduto", SqlDbType.Int).Value =
                          int.Parse(txtCdProduto.Text);

                    cmd.Parameters.Add("@NomeProduto", SqlDbType.VarChar, 100).Value =
                        txtNomeProduto.Text.Trim();

                    cmd.Parameters.Add("@PrecoProduto", SqlDbType.Decimal).Value =
                        decimal.Parse(txtPrecoProduto.Text);

                    cmd.Parameters["@PrecoProduto"].Precision = 10;
                    cmd.Parameters["@PrecoProduto"].Scale = 2;

                    cmd.Parameters.Add("@CdCategoria", SqlDbType.Int).Value =
                        Convert.ToInt32(cBoxCategoria.SelectedValue);

                    //retorna a quantidade de linha afetadas pelo comando
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas == 0)
                    {
                        throw new Exception("Nenhum produto foi alterado.");
                    }
                }
            }

            MessageBox.Show("Produto alterado com sucesso!");
        }

        //SELECT
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                consultarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void consultarProdutos()
        {
            if (!int.TryParse(txtBuscarProduto.Text.Trim(), out int codigo))
            {
                throw new Exception("Informe um código válido para consulta!");
            }

            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"SELECT CdProduto, NomeProduto, PrecoProduto, CdCategoria FROM Produto WHERE CdProduto = @CdProduto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@CdProduto", SqlDbType.Int).Value = codigo;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtCdProduto.Text = reader["CdProduto"].ToString();
                            txtNomeProduto.Text = reader["NomeProduto"].ToString();
                            txtPrecoProduto.Text = reader["PrecoProduto"].ToString();
                            cBoxCategoria.SelectedValue = Convert.ToInt32(reader["CdCategoria"]);

                            produtoCarregado = true;
                            Insercao = false;
                            Alteracao = false;

                            configurarFormulario();
                        }
                        else
                        {
                            txtCdProduto.Clear();
                            txtNomeProduto.Clear();
                            txtPrecoProduto.Clear();
                            cBoxCategoria.SelectedIndex = -1;

                            produtoCarregado = false;

                            configurarFormulario();

                            throw new Exception("Produto não encontrado!");
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
                if (!produtoCarregado)
                {
                    throw new Exception("Consulte um produto antes de excluí-lo!");
                }

                int codigo = int.Parse(txtCdProduto.Text);
                string nome = txtNomeProduto.Text;

                DialogResult resposta = MessageBox.Show(
                    $"Deseja excluir o produto {codigo} — {nome}?",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resposta == DialogResult.Yes)
                {
                    excluirProduto(codigo);

                    txtCdProduto.Clear();
                    txtNomeProduto.Clear();
                    txtPrecoProduto.Clear();
                    cBoxCategoria.SelectedIndex= -1;

                    produtoCarregado = false;

                    configurarFormulario();

                    MessageBox.Show("Produto excluído com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void excluirProduto(int codigo)
        {
            Conexao conexao = new Conexao();

            using (SqlConnection conn = conexao.conectar())
            {
                conn.Open();

                string sql = @"DELETE FROM Produto
                       WHERE CdProduto = @CdProduto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@CdProduto", SqlDbType.Int).Value = codigo;

                    //retorna a quantidade de linha afetadas pelo comando
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas == 0)
                    {
                        throw new Exception("O produto não foi encontrado para exclusão.");
                    }
                }
            }
        }

        //Cancela alteração do btnAlteração ou do btnNovo
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Alteracao)
            {
                txtNomeProduto.Text = nomeProdutoOriginal;
                txtPrecoProduto.Text = precoProdutoOriginal;
                cBoxCategoria.SelectedValue = categoriaProdutoOriginal;
            }
            else if (Insercao)
            {
                txtCdProduto.Clear();
                txtNomeProduto.Clear();
                txtPrecoProduto.Clear();
                cBoxCategoria.SelectedIndex = -1;

                produtoCarregado = false;
            }

            Insercao = false;
            Alteracao = false;

            configurarFormulario();
        }
    }
}
