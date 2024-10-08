using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Conta conta1 = new Conta("João", "Corrente", "123456");
        Conta conta2 = new Conta("Maria", "Poupança", "654321");

        bool sair = false;

        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("Bem-vindo ao Caixa Eletrônico!");
            Console.Write("Digite sua senha: ");
            string senha = Console.ReadLine();

            if (!conta1.Autenticar(senha))
            {
                Console.WriteLine("Senha incorreta. Tente novamente.");
                continue;
            }

            Console.WriteLine("1. Saque");
            Console.WriteLine("2. Depósito");
            Console.WriteLine("3. Extrato");
            Console.WriteLine("4. Transferência");
            Console.WriteLine("5. Aplicação Financeira");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Informe o valor do saque: ");
                    double valorSaque = double.Parse(Console.ReadLine());
                    conta1.Sacar(valorSaque);
                    break;
                case 2:
                    Console.Write("Informe o valor do depósito: ");
                    double valorDeposito = double.Parse(Console.ReadLine());
                    conta1.Depositar(valorDeposito);
                    break;
                case 3:
                    conta1.ExibirExtrato();
                    conta1.SalvarExtratoEmArquivo();
                    break;
                case 4:
                    Console.Write("Informe o valor da transferência: ");
                    double valorTransferencia = double.Parse(Console.ReadLine());
                    conta1.Transferir(conta2, valorTransferencia);
                    break;
                case 5:
                    Console.WriteLine("Escolha uma aplicação: ");
                    Console.WriteLine("1. Poupança");
                    Console.WriteLine("2. CDB");
                    int opcaoAplicacao = int.Parse(Console.ReadLine());
                    conta1.Aplicar(opcaoAplicacao);
                    break;
                case 6:
                    sair = true;
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (!sair)
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}

class Conta
{
    private string titular;
    private string tipoConta;
    private double saldo;
    private string senha;
    private double limite;

    public Conta(string titular, string tipoConta, string senha)
    {
        this.titular = titular;
        this.tipoConta = tipoConta;
        this.saldo = 0;
        this.senha = senha;
        this.limite = 1000; // Definindo um limite de saque padrão
    }

    public bool Autenticar(string senha)
    {
        return this.senha == senha;
    }

    public void Sacar(double valor)
    {
        if (valor > saldo + limite)
        {
            Console.WriteLine("Saldo insuficiente.");
        }
        else if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo -= valor;
            Console.WriteLine($"Saque de {valor:C} realizado com sucesso.");
        }
    }

    public void Depositar(double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo += valor;
            Console.WriteLine($"Depósito de {valor:C} realizado com sucesso.");
        }
    }

    public void ExibirExtrato()
    {
        Console.WriteLine($"Titular: {titular}");
        Console.WriteLine($"Tipo de Conta: {tipoConta}");
        Console.WriteLine($"Saldo Atual: {saldo:C}");
    }

    public void Transferir(Conta destino, double valor)
    {
        double taxa = 0.05; // Taxa de 5 centavos
        double totalTransferencia = valor + taxa;

        if (totalTransferencia > saldo)
        {
            Console.WriteLine("Saldo insuficiente para transferência.");
        }
        else if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo -= totalTransferencia;
            destino.Depositar(valor);
            Console.WriteLine($"Transferência de {valor:C} para {destino.titular} realizada com sucesso.");
        }
    }

    public void Aplicar(int tipo)
    {
        double valorAplicacao;
        Console.Write("Informe o valor para aplicar: ");
        valorAplicacao = double.Parse(Console.ReadLine());

        if (valorAplicacao > saldo)
        {
            Console.WriteLine("Saldo insuficiente para aplicação.");
            return;
        }

        saldo -= valorAplicacao;

        if (tipo == 1)
        {
            Console.WriteLine($"Valor de {valorAplicacao:C} aplicado na Poupança.");
            // Lógica para Poupança
        }
        else if (tipo == 2)
        {
            Console.WriteLine($"Valor de {valorAplicacao:C} aplicado em CDB.");
            // Lógica para CDB
        }
        else
        {
            Console.WriteLine("Opção de aplicação inválida.");
        }
    }

    public void SalvarExtratoEmArquivo()
    {
        string nomeArquivo = $"{titular}_extrato.txt";
        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            writer.WriteLine($"Titular: {titular}");
            writer.WriteLine($"Tipo de Conta: {tipoConta}");
            writer.WriteLine($"Saldo Atual: {saldo:C}");
        }
        Console.WriteLine($"Extrato salvo em arquivo: {nomeArquivo}");
    }
}
