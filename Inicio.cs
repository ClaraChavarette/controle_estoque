
namespace controle_estoque;

public class Inicio
{
    static void Main(string[] args)
    {
        Console.WriteLine("Digite o número do que deseja realizar:");
        Console.WriteLine("1. Cadastrar Produto");
        Console.WriteLine("2. Listar Produtos");
        Console.WriteLine("3. Buscar Produto");
        Console.WriteLine("4. Atualizar Produto");
        Console.WriteLine("5. Remover Produto");

        int itemMenu = Convert.ToInt32(Console.ReadLine());

        Estoque estoque = new Estoque();

        switch (itemMenu)
        {
            case 1:
                estoque.CadastrarProduto();
                break;
            case 2:
                estoque.ListarProdutos();
                break;
            case 3:
                estoque.BuscarProduto();               
                break;
            case 4:
                estoque.AtualizarProduto();
                break;  
            case 5:
                estoque.RemoverProduto();
                break;              

        }
    }

    
}