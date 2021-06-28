using CatalogoDeJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeJogos.Repositories
{
    public class DBRepository : IJogoRepository
    {
        //connection
        private readonly SqlConnection sqlConnection;

        public DBRepository(IConfiguration configuration) 
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        //Interface implementation
        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"update Jogos set nome = '{jogo.nome}', produtora = '{jogo.produtora}', preco = '{jogo.preco}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"insert Jogos (id, nome, produtora, preco) values ('{jogo.id}', '{jogo.nome}', '{jogo.produtora}', '{jogo.preco}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();
            var comando = $"select * from Jogos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read()) 
            {
                jogos.Add(new Jogo
                {
                    id = (Guid)sqlDataReader["id"],
                    nome = (string)sqlDataReader["nome"],
                    produtora = (string)sqlDataReader["produtora"],
                    preco = (double)sqlDataReader["preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;

            var comando = $"slect * from Jogos where id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlComand = new SqlCommand(comando, sqlConnection);
            SqlDataReader dataReader = await sqlComand.ExecuteReaderAsync();

            while (dataReader.Read()) 
            {
                jogo = new Jogo
                {
                    id = (Guid)dataReader["id"],
                    nome = (string)dataReader["nome"],
                    produtora = (string)dataReader["produtora"],
                    preco = (double)dataReader["preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return jogo;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos where nome = '{nome}' and produtora = '{produtora}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync();

            while (dataReader.Read()) 
            {
                jogos.Add(new Jogo
                {
                    id = (Guid)dataReader["id"],
                    nome = (string)dataReader["nome"],
                    produtora = (string)dataReader["produtora"],
                    preco = (double)dataReader["preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return jogos;
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Jogos where id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}
