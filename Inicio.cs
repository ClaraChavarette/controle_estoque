using System;
using System.Threading.Tasks;

namespace controle_estoque; 

public class Inicio
{
    static async Task Main(string[] args) 
    {
        await Estoque.InicializarBancoAsync();

        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("Digite o número do que deseja realizar:");
            Console.WriteLine("1. Cadastrar Produto");
            Console.WriteLine("2. Listar Produtos");
            Console.WriteLine("3. Buscar Produto");
            Console.WriteLine("4. Atualizar Produto");
            Console.WriteLine("5. Remover Produto");
            Console.WriteLine("0. Sair do Programa");

            int itemMenu = Convert.ToInt32(Console.ReadLine());

            switch (itemMenu)
            {
                case 1:
                    Console.WriteLine("Digite as informações abaixo sobre o produto:");
                    
                    Console.WriteLine("Nome:");
                    string nome = Console.ReadLine() ?? "";
                    while (string.IsNullOrWhiteSpace(nome))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("O nome não pode ser vazio. Digite novamente: ");
                        Console.ResetColor();
                        nome = Console.ReadLine() ?? "";
                    }

                    Console.WriteLine("Quantidade:");
                    int quantidade;
                    while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Quantidade inválida! Digite um número inteiro maior que 0: ");
                        Console.ResetColor();
                    }

                    Console.WriteLine("Preço:");
                    double preco;
                    while (!double.TryParse(Console.ReadLine(), out preco) || preco <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Preço inválido! Digite um valor numérico maior que 0: ");
                        Console.ResetColor();
                    }

                    Produto novoProduto = new Produto();
                    novoProduto.Nome = nome;
                    novoProduto.Quantidade = quantidade;
                    novoProduto.Preco = preco;

                    await novoProduto.CadastrarProduto();

                    Json json = new controle_estoque.Json();
                    json.SalvarProdutoAtual(novoProduto);
                    
                    break;

                case 2:
                    Produto produtosListar = new Produto();
                    await produtosListar.ListarProdutos();
                    break;

                case 3:
                    Console.WriteLine("Digite o nome do produto que deseja buscar:");
                    string nomeProd = Console.ReadLine() ?? "";

                    Produto produtoBuscar = new Produto();
                    await produtoBuscar.BuscarProduto(nomeProd);
                    break;

                case 4:
                    Console.WriteLine("Busque o produto que deseja alterar e digite seu ID abaixo:");
                    int idProd;
                    while (!int.TryParse(Console.ReadLine(), out idProd) || idProd <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Quantidade inválida! Digite um número inteiro maior que 0: ");
                        Console.ResetColor();
                    }

                    Console.WriteLine("Novo Nome:");
                    string nomeAtual = Console.ReadLine() ?? "";
                    Console.WriteLine("Nova Quantidade:");
                    int quantidadeAtual = int.TryParse(Console.ReadLine(), out int s) ? s : 0;
                    Console.WriteLine("Novo Preço:");
                    double precoAtual = double.TryParse(Console.ReadLine(), out double r) ? r : 0;

                    Produto produtoAtualizar = new Produto();
                    await produtoAtualizar.AtualizarProduto(idProd, nomeAtual, quantidadeAtual, precoAtual);
                    break;

                case 5:
                    Console.WriteLine("Busque o produto que deseja excluir e digite seu ID abaixo:");
                    int idProdRemover = Convert.ToInt32(Console.ReadLine());

                    Produto produtoRemover = new Produto();
                    await produtoRemover.RemoverProduto(idProdRemover);
                    break;

                case 0:
                    continuar = false;
                    Console.WriteLine("Saindo do sistema... Até logo!");
                    break;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;

            }

            if (continuar)
            {
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                Console.ReadKey();
                Console.Clear(); 
            }

        }

        
    }

    
}