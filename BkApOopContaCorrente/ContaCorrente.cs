

namespace BkApOopContaCorrente
{
    public enum TipoMovimentacao
    {
        Credito = 1,
        Debito = 2
    }

    public class Movimentacao
    {
        public decimal Valor { get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
    }

    public class ContaCorrente
    {
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }

        public string ResultadoOperacao { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }

        public ContaCorrente()
        {
            Movimentacoes = new List<Movimentacao>();
        }

        public bool Deposito(decimal valorDeposito)
        {
            Saldo += valorDeposito;
            return true;
        }

        public bool Saque(decimal valorDeSaque)
        {
            if (valorDeSaque > (Saldo + Limite))
            {
                ResultadoOperacao = "Saque excede o limite de saldo+limite disponível";
                return false;
            }

            Saldo -= valorDeSaque;
            Movimentacao movimentacao = new Movimentacao();
            movimentacao.Valor = valorDeSaque;
            movimentacao.TipoMovimentacao = TipoMovimentacao.Credito;
            if (Saldo > 0)
            {
                movimentacao.TipoMovimentacao = TipoMovimentacao.Debito;
            }
            this.Movimentacoes.Add(movimentacao);

            return true;
        }
    }
}
