### Atividade: Conta Corrente

Uma conta corrente possui os seguintes atributos:

- **Número:** Identificação única da conta.
- **Saldo:** Quantidade de dinheiro disponível na conta.
- **Status:** Indica se a conta é especial ou não.
- **Limite:** Limite máximo para saques.
- **Histórico de Movimentações:** Registro das transações realizadas na conta.

Uma movimentação consiste em:

- **Valor:** Quantia envolvida na transação.
- **Tipo:** Indicação se é uma movimentação de crédito ou débito.

Cada conta também contém informações do cliente:

- **Nome:** Nome do cliente dono da conta.
- **Sobrenome:** Sobrenome do cliente.
- **CPF:** Número do CPF do cliente.

Operações disponíveis para cada conta:

1. **Saque:** Retirada de dinheiro da conta. O valor do saque não pode exceder o limite de saque (limite + saldo).
2. **Depósito:** Adição de dinheiro à conta.
3. **Visualização de Saldo:** Consulta do saldo atual da conta.
4. **Visualização de Extrato:** Visualização do histórico de movimentações da conta.
5. **Transferência entre Contas:** Transferência de dinheiro entre duas contas.

Restrições:

- Uma conta corrente só pode fazer saques desde que o valor não exceda o limite de saque (limite + saldo).

Não é necessário implementar a interação com o usuário nesta descrição.

### Especificação de Requisitos usando BDD

#### Cenário: Saque dentro do limite

Dado que uma conta corrente possui saldo de R$ 1000 e limite de saque de R$ 500
Quando o cliente realiza um saque de R$ 400
Então o saldo da conta deve ser R$ 600

#### Cenário: Saque ultrapassando o saldo mas dentro do limite de saque

Dado que uma conta corrente possui saldo de R$ 1000 e limite de saque de R$ 500
Quando o cliente realiza um saque de R$ 1500
Então o saldo da conta deve ser R$ -500

#### Cenário: Saque ultrapassando o saldo e o limite de saque

Dado que uma conta corrente possui saldo de R$ 1000 e limite de saque de R$ 500
Quando o cliente tenta realizar um saque de R$ 1500
Então uma mensagem de erro deve ser exibida informando que o saque excede o saldo disponível e o limite de saque
E o saldo da conta deve permanecer inalterado
E o limite de saque deve permanecer o mesmo

#### Cenário: Depósito

Dado que uma conta corrente possui saldo de R$ 1000
Quando o cliente realiza um depósito de R$ 500
Então o saldo da conta deve ser R$ 1500

#### Cenário: Visualização de Saldo

Dado que uma conta corrente possui saldo de R$ 1000
Quando o cliente consulta o saldo da conta
Então o saldo retornado deve ser R$ 1000

#### Cenário: Transferência entre Contas

Dado que uma conta corrente possui saldo de R$ 1000
E existe outra conta corrente com saldo de R$ 500
Quando o cliente transfere R$ 300 da primeira conta para a segunda conta
Então o saldo da primeira conta deve ser R$ 700
E o saldo da segunda conta deve ser R$ 800
