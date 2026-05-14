namespace ProjetoWeb1.Models
{
    public class LoginViewModel
    {   
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha {  get; set; } = string.Empty;
        public string Nivel { get; set; } = "Funcionario";
    }
}
