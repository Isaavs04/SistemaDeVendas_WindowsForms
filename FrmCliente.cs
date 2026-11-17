using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Ativ_Sistema_de_Vendas
{
    public partial class FrmCliente : Form
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public FrmCliente()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            //Habilitando e desabilitando botões, estado inicial da tela 
            btnExcluir.Enabled = false;
            btnConsultar.Enabled = true;
            btnAlterar.Enabled = false;
            btnApagar.Enabled = false;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool validoCPF()
        {
            string cpf = txtCpf.Text;
            int soma = 0, num = 10, mult = 0, resto = 0, Pdig = 0, Sdig = 0;

            //CPFs como esses são falsos mesmo passando no teste, por isso
            //esse if testa se o cpf digitado é dieferente dos inválidos
            if (cpf != "00000000000" && cpf != "111111111111" && cpf != "22222222222" && cpf != "33333333333" && cpf != "44444444444" && cpf != "55555555555" && cpf != "66666666666" && cpf != "77777777777" && cpf != "88888888888" && cpf != "99999999999")
            {
                //O bloco de código abaixo calcula o primeiro caractere verificador

                //Multiplica os dígitos do CPF digitado por numeros decrescentes 10 a 2
                //Soma essas multiplicações em uma acumuladora
                for (int i = 0; i < 9; i++)
                {
                    int digito = int.Parse(cpf[i].ToString());
                    mult = digito * num;
                    soma += mult;
                    num--;
                }
                //Testa a condição do resto da soma total, dividida por 11
                //Caso o resto seja 2 ou mais o primeiro digito é o resto - 11;
                resto = soma % 11;
                if (resto >= 2)
                {
                    Pdig = resto - 11;
                }
                //Caso seja menor que 2 e maior ou igual a 0, "0" e "1" o digito é 0
                else if (resto < 2 && resto >= 0)
                {
                    Pdig = 0;
                }

                //O bloco abaixo calcula o segundo caractere validador do CPF
                num = 11;
                for (int i = 0; i < 9; i++)
                {
                    int digito = int.Parse(cpf[i].ToString());
                    mult = digito * num;
                    soma += mult;
                    num--;
                }
                soma = soma + (resto * 2);
                resto = soma % 11;
                if (resto >= 2)
                {
                    Sdig = resto - 11;
                }
                //Caso seja menor que 2 e maior ou igual a 0, "0" e "1" o digito é 0
                else if (resto < 2 && resto >= 0)
                {
                    Sdig = 0;
                }
                //Verifica se os dígitos verificadores são iguais aos digitados
                if (cpf[9].ToString() == Pdig.ToString())
                {
                    if (cpf[10].ToString() == Sdig.ToString())
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
            return false;
        }
        private bool semNm(string nome)
        {
            bool nm = false, simb = false;
            foreach(char i in nome)
            {
                //Verificando se tem numero
                if (char.IsDigit(i))
                {
                    nm = true;
                }
                //Verificando se tem caractere especial
                if (!char.IsLetterOrDigit(i) && i != ' ')
                {
                    simb = true;
                }
            }
            if (nm || simb)
            {
                return false;
            }
            else { 
                return true; 
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            bool nmTelValido = int.TryParse(txtTelefone.Text, out int telefone);
            bool limCredValido = double.TryParse(txtLimiteCredito.Text, out double limCred);
            bool numResidenciaVal = int.TryParse(txtNum.Text, out int numResidencia);
            string nome = txtNome.Text, cidade = txtCidade.Text, rua = txtRua.Text, bairro = txtBairro.Text;

            //Verifica se todos os itens foram preenchidos antes de testar se são válidos
            // && cBoxStatus.SelectedIndex != -1
            if (txtNome.Text != "" || txtTelefone.Text != "" || txtCdCliente.Text != "" || txtCpf.Text != "" || txtRG.Text != "" || txtLimiteCredito.Text != "" || txtCEP.Text != "" || txtCidade.Text != "" || txtBairro.Text != "" || txtRua.Text != "" || txtNum.Text != "" || txtComplemento.Text != "" || cBoxUFEndereco.SelectedIndex != -1 || cBoxUFRG.SelectedIndex != -1)
            {
                //Bloco de validação do usuário
                //Verifica se o nome é válido, sem caracteres e números
                if (!semNm(nome))
                {
                    txtNome.BackColor = Color.LightPink;
                    txtNome.ForeColor = Color.Red;
                    //Checa se já tem o asterisco do erro
                    if (txtNome.Text.Contains('*'))
                        txtNome.Text = "*" + txtNome.Text;
                }
                else
                {
                    txtNome.BackColor = Color.White;
                    txtNome.ForeColor = Color.Black;
                }

                //Verifica se o número é válido
                if (!nmTelValido)
                {
                    txtTelefone.BackColor = Color.LightPink;
                    txtTelefone.ForeColor = Color.Red;
                    if (txtNome.Text.Contains('*'))
                        txtTelefone.Text = "*" + txtTelefone.Text;
                }
                else
                {
                    txtTelefone.BackColor = Color.White;
                    txtTelefone.ForeColor = Color.Black;
                }

                if (!validoCPF())
                {
                    txtCpf.BackColor = Color.LightPink;
                    txtCpf.ForeColor = Color.Red;
                    if (txtNome.Text.Contains('*'))
                        txtCpf.Text = "*" + txtCpf.Text;
                }
                else
                {
                    txtCpf.BackColor = Color.White;
                    txtCpf.ForeColor = Color.Black;
                }

                if (!limCredValido)
                {
                    txtLimiteCredito.BackColor = Color.LightPink;
                    txtLimiteCredito.ForeColor = Color.Red;
                    if (txtNome.Text.Contains('*'))
                        txtLimiteCredito.Text = "*" + txtLimiteCredito.Text;
                }
                else
                {
                    txtLimiteCredito.BackColor = Color.White;
                    txtLimiteCredito.ForeColor = Color.Black;
                }


                //Bloco de validação do endereço do usuário
                //Validação do CEP
                //if(){}
                if (!numResidenciaVal)
                {
                    txtNum.BackColor = Color.LightPink;
                    txtNum.ForeColor = Color.Red;
                    if (txtNome.Text.Contains('*'))
                        txtNum.Text = "*" + txtNum.Text;
                }
                else
                {
                    txtNum.BackColor = Color.White;
                    txtNum.ForeColor = Color.Black;
                }
                if (!semNm(cidade))
                {
                    txtCidade.BackColor = Color.LightPink;
                    txtCidade.ForeColor = Color.Red;
                    if (txtNome.Text.Contains('*'))
                        txtCidade.Text = "*" + txtCidade.Text;
                }
                else
                {
                    txtCidade.BackColor = Color.White;
                    txtCidade.ForeColor = Color.Black;
                }
                if (!semNm(bairro))
                {
                    txtBairro.BackColor = Color.LightPink;
                    txtBairro.ForeColor = Color.Red;
                    if (txtNome.Text.Contains('*'))
                        txtBairro.Text = "*" + txtBairro.Text;
                }
                else
                {
                    txtBairro.BackColor = Color.White;
                    txtBairro.ForeColor = Color.Black;
                }
                if (!semNm(rua))
                {
                    txtRua.BackColor = Color.LightPink;
                    txtRua.ForeColor = Color.Red;
                    if (txtNome.Text.Contains('*'))
                        txtRua.Text = "*" + txtRua.Text;
                }
                else
                {
                    txtRua.BackColor = Color.White;
                    txtRua.ForeColor = Color.Black;
                }

                if (semNm(nome) && nmTelValido && validoCPF() && limCredValido && numResidenciaVal && semNm(cidade) && semNm(bairro) && semNm(rua))
                {
                    //Pode realizar update e insert, pois os dados foram verificados e são válidos
                    reiniciarTela();
                }
            }
            else
            {
                MessageBox.Show("Todos os campos devem ser preenchidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFechar_Hover(object sender, EventArgs e)
        {
            BackColor = Color.Red;
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

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" || txtTelefone.Text != "" || txtCdCliente.Text != "" | txtCpf.Text != "" && txtRG.Text != "" && txtLimiteCredito.Text != "" && txtCEP.Text != "" && txtCidade.Text != "" && txtBairro.Text != "" && txtRua.Text != "" && txtNum.Text != "" && txtComplemento.Text != "" && cBoxUFEndereco.SelectedIndex != -1 && cBoxUFRG.SelectedIndex != -1)
            {
                btnApagarErros.Visible = true;
                btnApagarTudo.Visible = true;
            }
            else
            {
                MessageBox.Show("Não há campos para limpar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void reiniciarTela()
        {
            txtNome.Clear();
            txtNome.Focus();
            txtCdCliente.Clear();
            txtTelefone.Clear();
            txtCpf.Clear();
            txtRG.Clear();
            cBoxUFRG.SelectedIndex = -1;
            txtLimiteCredito.Clear();
            cBoxStatus.SelectedIndex = -1;
            txtCEP.Clear();
            cBoxUFEndereco.SelectedIndex = -1;
            txtCidade.Clear();
            txtRua.Clear();
            txtBairro.Clear();
            txtNum.Clear();
            txtComplemento.Clear();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = false;
            txtTelefone.Enabled = false;
            txtCpf.Enabled = false;
            txtRG.Enabled = false;
            txtLimiteCredito.Enabled = false;
            txtCdCliente.Enabled = false;
            cBoxUFRG.Enabled = false;
            cBoxStatus.Enabled = false;
            txtCEP.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            cBoxUFEndereco.Enabled = false;
            txtRua.Enabled = false;
            txtNum.Enabled = false;
            txtComplemento.Enabled = false;

            DialogResult Apagar = MessageBox.Show("Deseja mesmo excluir esse cadastro?", "Exclusão de cadastro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Apagar == DialogResult.Yes)
            {
                //Coloque o código de exclusão aqui
            }
            else
            {
                MessageBox.Show("O cadastro não foi excluído","Exclusão de cadastro" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Media.SystemSounds.Hand.Play();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = false;
            txtTelefone.Enabled = false;
            txtCpf.Enabled = false;
            txtRG.Enabled = false;
            txtLimiteCredito.Enabled = false;
            txtCdCliente.Enabled = true;
            cBoxUFRG.Enabled = false;
            cBoxStatus.Enabled = false;
            txtCEP.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            cBoxUFEndereco.Enabled = false;
            txtRua.Enabled = false;
            txtNum.Enabled = false;
            txtComplemento.Enabled = false;
            btnBuscar.Visible = true;
            btnSalvar.Enabled = false;

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = true;
            txtTelefone.Enabled = true;
            txtCpf.Enabled = true;
            txtRG.Enabled = true;
            txtLimiteCredito.Enabled = true;
            txtCdCliente.Enabled = true;
            cBoxUFRG.Enabled = true;
            cBoxStatus.Enabled = true;
            txtCEP.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            cBoxUFEndereco.Enabled = true;
            txtRua.Enabled = true;
            txtNum.Enabled = true;
            txtComplemento.Enabled = true;
            btnBuscar.Visible = false;
            btnSalvar.Enabled = true;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = true;
            txtTelefone.Enabled = true;
            txtCpf.Enabled = true;
            txtRG.Enabled = true;
            txtLimiteCredito.Enabled = true;
            txtCdCliente.Enabled = true;
            cBoxUFRG.Enabled = true;
            cBoxStatus.Enabled = true;
            txtCEP.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            cBoxUFEndereco.Enabled = true;
            txtRua.Enabled = true;
            txtNum.Enabled = true;
            txtComplemento.Enabled = true;
            btnBuscar.Visible = false;
            btnSalvar.Enabled = true;
            btnApagar.Enabled = true;
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

        private void btnBuscar_MouseEnter(object sender, EventArgs e)
        {
            btnBuscar.BackColor = Color.Purple;
        }

        private void btnBuscar_MouseLeave(object sender, EventArgs e)
        {
            btnBuscar.BackColor = Color.FromArgb(78, 38, 102);
        }

        private void painelArrastar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnApagarTudo_Click(object sender, EventArgs e)
        {
            reiniciarTela();
            btnApagarTudo.Visible = false;
            btnApagarErros.Visible = false;
        }

        private void btnApagarErros_Click(object sender, EventArgs e)
        {
            bool nmTelValido = int.TryParse(txtTelefone.Text, out int telefone);
            bool limCredValido = double.TryParse(txtLimiteCredito.Text, out double limCred);
            bool numResidenciaVal = int.TryParse(txtNum.Text, out int numResidencia);
            string nome = txtNome.Text, cidade = txtCidade.Text, rua = txtRua.Text, bairro = txtBairro.Text;

            if (semNm(nome) && nmTelValido && validoCPF() && limCredValido && numResidenciaVal && semNm(cidade) && semNm(bairro) && semNm(rua))
            {
                MessageBox.Show("Não há erros para apagar", "Interface sem erros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (!semNm(nome))
            {
                txtNome.Clear();
                txtNome.BackColor = Color.White;
                txtNome.ForeColor = Color.Black;
            }

            //Verifica se o número é válido
            if (!nmTelValido)
            {
                    txtTelefone.Clear();
                txtTelefone.BackColor = Color.White;
                txtTelefone.ForeColor = Color.Black;
            }

            if (!validoCPF())
            {
                txtCpf.Clear();
                txtCpf.BackColor = Color.White;
                txtCpf.ForeColor = Color.Black;
            }

            if (!limCredValido)
            {
                    txtLimiteCredito.Clear();
                txtLimiteCredito.BackColor = Color.White;
                txtLimiteCredito.ForeColor = Color.Black;
            }


            //Bloco de validação do endereço do usuário
            //Validação do CEP
            //if(){}
            if (!numResidenciaVal)
            {
                    txtNum.Clear();
                txtNum.BackColor = Color.White;
                txtNum.ForeColor = Color.Black;
            }
            if (!semNm(cidade))
            {
                    txtCidade.Clear();
                txtCidade.BackColor = Color.White;
                txtCidade.ForeColor = Color.Black;
            }
            if (!semNm(bairro))
            {
                    txtBairro.Clear();
                txtBairro.BackColor = Color.White;
                txtBairro.ForeColor = Color.Black;
            }
            if (!semNm(rua))
            {
                    txtRua.Clear();
                txtRua.BackColor = Color.White;
                txtRua.ForeColor = Color.Black;
            }

            btnApagarTudo.Visible = false;
            btnApagarErros.Visible = false;
        }
    }
}