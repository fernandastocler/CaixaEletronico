using System;
using System.IO;

class Program
{
	static void Main(string[] args)
	{
		Conta conta1 = new Conta("João", "Corrente");
		Conta conta2 = new Conta("Maria", "Poupança");
		bool sair = false;
		while (!sair)
		{
			Console.Clear();
			Console.WriteLine("Bem-vindo ao Caixa Eletrônico!");
			Console.WriteLine("1. Saque");
			Console.WriteLine("2. Depósito");
			Console.WriteLine("3. Extrato");
			Console.WriteLine("4. Transferência");
			Console.WriteLine("5. Sair");
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
	public Conta(string titular, string tipoConta)
	{
		this.titular = titular;
		this.tipoConta = tipoConta;
		this.saldo = 0;
	}

	public void Sacar(double valor)
	{
		if (valor > saldo)
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

