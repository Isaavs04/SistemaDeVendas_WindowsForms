using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ativ_Sistema_de_Vendas.BancoDados
{
    public class Conexao
    {
        //Para rodar o aplicativo sem erros com banco de dados é necessário selecionar
        //a opção localhost e que o banco criado localmente na sua máquina se chame exatamnete SistemaDeVendas
        private string conexao = @"Data Source=localhost;" +
                          @"Initial Catalog=SistemaDeVendas;" +
                          @"Integrated Security=True;" +
                          @"TrustServerCertificate=True";

        public SqlConnection conectar()
        {
            return new SqlConnection(conexao);
        }
    }
}
