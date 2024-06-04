namespace BkApOopContaCorrente
{
    public abstract class ContaBase
    {
        public int Numero { get; set; }
        public bool Special { get; set; }
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }
        public Cliente Cliente { get; set; }

        public string ResultadoOperacao { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }

        public ContaBase()
        {
            Movimentacoes = new List<Movimentacao>();
            Special = false;
        }

        public ContaBase(decimal saldo)
        {
            Movimentacoes = new List<Movimentacao>();
            Saldo = saldo;
            Special = false;
        }

        public bool Deposito(decimal valorDeposito)
        {
            Saldo += valorDeposito;

            Movimentacao movimentacao = new Movimentacao();
            movimentacao.Valor = valorDeposito;
            movimentacao.TipoMovimentacao = TipoMovimentacao.Credito;
            this.Movimentacoes.Add(movimentacao);
            return true;
        }

        public virtual bool Saque(decimal valorDeSaque)
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

        public string Extrato()
        {
            string tipoConta = Special ? "Especial" : "Comum";
            string movimentacoes = "";

            int id = 1;
            foreach (var item in Movimentacoes)
            {
                if (id > 1)
                {
                    movimentacoes += "\r\n";
                }
                movimentacoes +=
                    $@"Transação {id}
Valor: {item.Valor}
Tipo: {item.TipoMovimentacao}";
                id++;
            }
            return
 @$"####Extrato Cliente####
{Cliente}
####Conta####
Número da conta: {Numero}
Saldo: {Saldo}
Tipo de conta: {tipoConta}
Limite: {Limite}
####Movimentações####
{movimentacoes}
####Fim Extrato####";
        }

        public abstract string CorDoCartao();
    }
    public class ContaPlatinum : ContaBase
    {
        public int CreditoEspecial { get; set; }
        public bool PixMultiplos;

        public override bool Saque(decimal valorDeSaque)
        {
            return base.Saque(valorDeSaque);
        }

        public override string CorDoCartao()
        {
            return "Prata";
        }
    }

    public class ContaPlatinumPLus : ContaPlatinum
    {
        public override string CorDoCartao()
        {
            return base.CorDoCartao() + "rocho";
        }
    }

    public class ContaCorrente : ContaBase
    {
        public override string CorDoCartao()
        {
            return "Branco";
        }
    }
}
