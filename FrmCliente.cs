using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
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
            //Validos
            bool nomeVal = false;

            //Bloco de validação do usuário
            //Verifica se o nome é válido, sem caracteres e números
            if (semNm(nome))
            {
                nomeVal = true;
                //Nome valido prossiga
            }
            //Verifica se o número é válido
            if (nmTelValido)
            {
                //Tell valido prossiga
            }
            if (validoCPF())
            {
                //CPF valido prossiga
            }
            if (limCredValido)
            {
                //Limite de crédito válido prossiga
            }
            //Validação RG
            //if(){}

            //Bloco de validação do endereço do usuário
            //Validação do CEP
            //if(){}
            if (numResidenciaVal)
            {
                //Número da rêsidencia válido prossiga
            }
            if (semNm(cidade))
            {
                //cidade válida prossiga
            }
            if (semNm(bairro))
            {
                //bairro válido prossiga
            }
            if (semNm(rua))
            {
                //rua válida prossiga
            }

            if (nomeVal && nmTelValido && validoCPF() && limCredValido && numResidenciaVal && semNm(cidade) && semNm(bairro) && semNm(rua))
            {
                //Pode realizar update e insert, pois os dados foram verificados e são válidos
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
            reiniciarTela();
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
        }
    }
}