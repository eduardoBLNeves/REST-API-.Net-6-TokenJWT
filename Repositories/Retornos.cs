namespace API.Repositories
{
    public class Retornos
    {
        public string ContaExistente() => "E-Mail já cadastrado!";
        public string senhaCurta() => "senha muito curta (mín. 8 caracteres)!";
        public string ErroInterno() => "Erro interno! Por favor, tente novamente.";
        public string DadosInvalidos() => "Dados inválidos! Por favor, tente novamente.";
        public string LoginInvalido() => "email e/ou senha inválidos!";
    }
}
