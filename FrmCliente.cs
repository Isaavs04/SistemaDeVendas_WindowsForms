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

namespace Ativ_Sistema_de_Vendas
{
    public partial class FrmCliente : Form
    {
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {

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
            nome = txtNome.Text;

        }
    }
}