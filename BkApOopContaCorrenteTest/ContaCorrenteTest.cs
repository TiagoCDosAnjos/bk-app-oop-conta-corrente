using BkApOopContaCorrente;

namespace BkApOopContaCorrenteTest
{
    public class ContaCorrenteTest
    {

        [Fact]//Que vai ser um teste unico
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

        //#### Cenário: Transferência entre Contas
        //Dado que uma conta corrente possui saldo de R$ 1000
        //E existe outra conta corrente com saldo de R$ 500
        //Quando o cliente transfere R$ 300 da primeira conta para a segunda conta
        //Então o saldo da primeira conta deve ser R$ 700
        //E o saldo da segunda conta deve ser R$ 800
        [Fact]
        public void DeveRealizarTransferenciaEntreContas()
        {
            Banco banco = new Banco();

            ContaCorrente contaCorrenteA = new ContaCorrente(1000);

            var saldoContaAEsperadoAposTransferencia = 700;
            ContaCorrente contaCorrenteB = new ContaCorrente(500);
            var saldoContaBEsperadoAposTransferencia = 800;
            var valorTransferencia = 300;

            banco.AdicionarConta(contaCorrenteA);
            banco.AdicionarConta(contaCorrenteB);

            //Act
            banco.Transferir(1, 2, valorTransferencia);

            //Assert
            Assert.Equal(saldoContaAEsperadoAposTransferencia, contaCorrenteA.Saldo);
            Assert.Equal(saldoContaBEsperadoAposTransferencia, contaCorrenteB.Saldo);

        }

        /*
         Cenário: Visualização de Extrato
Dado que o cliente tenha uma conta corrente
E que o cliente tenha realizado várias transações na conta corrente como saque, deposito, transferencia
Quando o cliente solicitar a visualização do extrato da conta
Então o sistema deve exibir as informações do cliente contendo: Nome, Sobrenome, CPF
E o sistema deve exibir as seguintes informações da conta:
Número da conta, Saldo atual, Status (se é uma conta especial ou não), Limite de saque
E o sistema deve exibir o histórico de movimentações da conta
Valor da transação, Tipo (crédito ou débito)
         */

        [Fact]
        public void DeveVisualizarExtratoDaConta()
        {
            //Arrange
            Cliente cliente = new Cliente("George", "Hamann", "123.456.678-90");
            ContaCorrente contaCorrente = new ContaCorrente();
            contaCorrente.Cliente = cliente;
            contaCorrente.Saldo = 666;
            contaCorrente.Limite = 999;
            contaCorrente.Numero = 1;
            contaCorrente.Special = true;

            contaCorrente.Deposito(300);
            contaCorrente.Saque(300);

            //cliente.Conta 
            string extratoEsperado =
 @"####Extrato Cliente####
Nome: George
Sobrenome: Hamann
CPF: 123.456.678-90
####Conta####
Número da conta: 1
Saldo: 666
Tipo de conta: Especial
Limite: 999
####Movimentações####
Transação 1
Valor: 300
Tipo: Credito
Transação 2
Valor: 300
Tipo: Debito
####Fim Extrato####";

            //Act
            string resultadoExtrato = contaCorrente.Extrato();

            //Assert
            Assert.Equivalent(resultadoExtrato, extratoEsperado);

        }

        /*Cenário: Visualização de Saldo
Dado que uma conta corrente possui saldo de R$ 1000
Quando o cliente consulta o saldo da conta
Então o saldo retornado deve ser R$ 1000*/
        [Fact]
        public void DeveVisualizarSaldoDaConta()
        {
            //Arrange
            Cliente cliente = new Cliente("George", "Hamann", "123.456.678-90");
            ContaCorrente contaCorrente = new ContaCorrente();
            contaCorrente.Cliente = cliente;
            contaCorrente.Saldo = 666;
            contaCorrente.Limite = 999;

            decimal saldoEsperado = 666;

            //Assert
            Assert.Equal(saldoEsperado, contaCorrente.Saldo);
        }
    }
}
