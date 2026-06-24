namespace controle_estoque;

public class Estoque
{
    public void CadastrarProduto()
    {
        Console.WriteLine("Digite as informações abaixo sobre o produto:");

        Console.WriteLine("ID:");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Nome:");
        string nome = Console.ReadLine() ?? ""; 

        Console.WriteLine("Quantidade:");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Preço:");
        double preco = double.Parse(Console.ReadLine() ?? "0");

        Produto produto = new Produto 
        {
            Id = id, 
            Nome = nome, 
            Quantidade = quantidade,
            Preco = preco
        };

        if(id != 0 && nome != "" && quantidade != 0 && preco != 0)
        {
            controle_estoque.Json json = new controle_estoque.Json();
            json.SalvarEstoque(produto);
            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao cadastrar produto, tente novamente!");
        }
        
    }

    public void ListarProdutos()
    {
        
    }

    public void BuscarProduto()
    {
        
    }

    public void AtualizarProduto()
    {
        
    }

    public void RemoverProduto()
    {
        
    }

    public void EntradaEstoque()
    {
        
    }

    public void SaidaEstoque()
    {
        
    }

    public void RelatorioEstoque()
    {
        
    }


    
}

