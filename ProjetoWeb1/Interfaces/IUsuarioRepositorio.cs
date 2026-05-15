using ProjetoWeb1.Models;

namespace ProjetoWeb1.Interfaces
{
    public interface IUsuarioRepositorio
    {
        /*Interface fuciona como um contrato define o que uma classe deve fazer
        e quais metodos e propriedades terá, mas não diz como deve fazer*/

        LoginViewModel Validar(string email, string senha);
    }
}
