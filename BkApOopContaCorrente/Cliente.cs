namespace BkApOopContaCorrente
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string CPF { get; set; }

        public Cliente(string nome, string sobreNome, string cPF)
        {
            Nome = nome;
            SobreNome = sobreNome;
            CPF = cPF;
        }

        public override string ToString()
        {
            return
@$"Nome: {Nome}
Sobrenome: {SobreNome}
CPF: {CPF}";
        }
    }
}
