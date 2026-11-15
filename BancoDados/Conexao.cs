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
