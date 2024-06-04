namespace BkApOopContaCorrente
{
    public class Banco
    {
        public List<ContaCorrente> ContasCorrente { get; set; }

        public Banco()
        {
            ContasCorrente = new List<ContaCorrente>();
        }

        public void AdicionarConta(ContaCorrente conta)
        {
            conta.Numero = ContasCorrente.Count + 1;
            ContasCorrente.Add(conta);
        }

        public void Transferir(int numeroContaOrigem, int numeroContaDestino, decimal valor)
        {
            ContaCorrente contaOrigem = null;
            ContaCorrente contaDestino = null;
            foreach (var conta in ContasCorrente)
            {
                if (conta.Numero == numeroContaOrigem)
                {
                    contaOrigem = conta;
                }
                if (conta.Numero == numeroContaDestino)
                {
                    contaDestino = conta;
                }
            }
            if (contaOrigem.Saque(valor))
            {
                contaDestino.Deposito(valor);
            }
        }
    }
}
