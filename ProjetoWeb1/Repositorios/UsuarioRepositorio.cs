using Microsoft.AspNetCore.Identity.Data;
using MySql.Data.MySqlClient;
using ProjetoWeb1.Interfaces;
using ProjetoWeb1.Models;

namespace ProjetoWeb1.Repositorios
{
    //Classe que implementa a interface IUsuarioRepositorio(contrato de método)
    public class UsuarioRepositorio(IConfiguration config) : IUsuarioRepositorio // herança
    {
        //Variavel privada e somente leitura para armazenar a string de conexão
        private readonly string _connectionString = config.GetConnectionString("Conexao");

        //Método que valida se o usuario existe no banco com base em email e senha
        public LoginViewModel Validar(string email, string senha)
        {
            //Cria a conexão com o banco de dados mysql o using garante que ela seja fechada automaticamente
            using var conn = new MySqlConnection(_connectionString);
            //Abre a conexão com o banco de dados
            conn.Open();
            //Define a string do sql usando parametros (@) evita ataques sql injection
            var sql = "SELECT * FROM Usuarios WHERE Email =@email AND Senha =@senha";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new LoginViewModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Nivel = reader["Nivel"].ToString()!
                };
            }
            return null;
        }
    }
}
