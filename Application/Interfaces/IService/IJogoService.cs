using Application.DTOs;
using Application.Input.JogoInput;

namespace Application.Interfaces.IService
{
    public interface IJogoService
    {
        IEnumerable<JogoDto> ObterTodosJogosDto();
        JogoDto ObterJogoDtoPorTitulo(string titulo);
        JogoDto ObterJogoDtoPorId(int id);
        void CadastrarJogo(JogoCadastroInput jogoCadastroInput, string nomeUsuarioLogado);
        void AlterarJogo(JogoAlteracaoInput jogoAlteracaoInput);
        void DeletarJogo(int id);
    }
}
