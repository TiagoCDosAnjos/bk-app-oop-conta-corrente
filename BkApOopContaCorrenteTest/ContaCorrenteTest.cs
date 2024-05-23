using BkApOopContaCorrente;

namespace BkApOopContaCorrenteTest
{
    public class ContaCorrenteTest
    {

        [Fact]
        ///Dado que uma conta corrente possui saldo de R$ 1000 e
        ///limite de saque de R$ 500 Quando o cliente realiza um saque de R$ 400 Então o saldo da conta deve ser R$ 600
        public void DeveRealizarUmSaqueComLimite()
        {
            //Arrange ( Preparar )
            var valorDeSaque = 400;
            var saldoEsperado = 600;
            TipoMovimentacao tipoMovimentacaoEsperada = TipoMovimentacao.Debito;

            ContaCorrente contaCorrente = new ContaCorrente();
            contaCorrente.Saldo = 1000;
            contaCorrente.Limite = 500;

            //Ação
            contaCorrente.Saque(valorDeSaque);

            //Asserção
            Assert.Equal(saldoEsperado, contaCorrente.Saldo);

            Movimentacao movimentacao = contaCorrente.Movimentacoes[contaCorrente.Movimentacoes.Count - 1];
            Assert.Equal(valorDeSaque, movimentacao.Valor);
            Assert.Equal(tipoMovimentacaoEsperada, movimentacao.TipoMovimentacao);
        }


        //Cenário: Saque ultrapassando o saldo mas dentro do limite de saque
        //Dado que uma conta corrente possui saldo de R$ 1000 e
        //limite de saque de R$ 500 Quando o cliente realiza um saque de R$ 1500 Então o saldo da conta deve ser R$ -500
        [Fact]
        public void DeveRealizarUmSaqueComLimiteEMaiorQueOSaldo()
        {
            //Arrange ( Preparar )
            var valorDeSaque = 1500;
            var saldoEsperado = -500;
            TipoMovimentacao tipoMovimentacaoEsperada = TipoMovimentacao.Credito;

            ContaCorrente contaCorrente = new ContaCorrente();
            contaCorrente.Saldo = 1000;
            contaCorrente.Limite = 500;

            //Ação
            contaCorrente.Saque(valorDeSaque);

            //Asserção
            Assert.Equal(saldoEsperado, contaCorrente.Saldo);
            Movimentacao movimentacao = contaCorrente.Movimentacoes[contaCorrente.Movimentacoes.Count - 1];
            Assert.Equal(valorDeSaque, movimentacao.Valor);
            Assert.Equal(tipoMovimentacaoEsperada, movimentacao.TipoMovimentacao);
        }

        //Cenário: Saque ultrapassando o saldo e o limite de saque
        //Dado que uma conta corrente possui saldo de R$ 1000 e limite de saque de R$ 500
        //Quando o cliente tenta realizar um saque de R$ 1501
        //Então uma mensagem de erro deve ser exibida informando que o
        //saque excede o saldo disponível e o limite de saque E o saldo da conta deve permanecer inalterado E o limite de saque deve permanecer o mesmo
        [Fact]
        public void NaoDeveRealizarUmSaqueComLimiteQuandoO_Valor_Saque_Excede_O_Limite_E_O_Saldo()
        {
            //Arrange ( Preparar )
            var valorDeSaque = 1501;
            var mensagemDeErroEsperada = "Saque excede o limite de saldo+limite disponível";

            ContaCorrente contaCorrente = new ContaCorrente();
            contaCorrente.Saldo = 1000;
            contaCorrente.Limite = 500;

            //Ação

            var resultadoSaque = contaCorrente.Saque(valorDeSaque);
            Assert.False(resultadoSaque);
            Assert.Equal(mensagemDeErroEsperada, contaCorrente.ResultadoOperacao);

        }

        //Cenário: Depósito
        //Dado que uma conta corrente possui saldo de R$ 1000 Quando o cliente realiza um depósito de R$ 500
        //Então o saldo da conta deve ser R$ 1500
        [Theory]
        [InlineData(500, 1500)]
        [InlineData(1000, 2000)]
        [InlineData(2000, 3000)]
        public void DevePermetirRealizarUmDeposito(decimal valorDeposito, decimal saldoEsperadoAposDeposito)
        {
            ContaCorrente contaCorrente = new ContaCorrente();
            contaCorrente.Saldo = 1000;

            //Ação
            var resultadoDeposito = contaCorrente.Deposito(valorDeposito);

            //Arrange
            Assert.True(resultadoDeposito);
            Assert.Equal(saldoEsperadoAposDeposito, contaCorrente.Saldo);


        }

        //Cenário: Visualização de Saldo
        //Dado que uma conta corrente possui saldo de R$ 1000
        //Quando o cliente consulta o saldo da conta Então o saldo retornado deve ser R$ 1000

        //[Theory]
    }
}
