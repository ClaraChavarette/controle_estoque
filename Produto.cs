using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace controle_estoque; 

public class Produto
{
    public string Nome { get; set; } = "";
    public int Quantidade { get; set; }
    public double Preco { get; set; }

    public async Task CadastrarProduto()
    {
        if (string.IsNullOrWhiteSpace(this.Nome))
        {
            Console.WriteLine("Erro: O nome do produto não pode estar em branco.");
            return; 
        }
        if (this.Quantidade < 0 || this.Preco <= 0)
        {
            Console.WriteLine("Erro: Quantidade não pode ser negativa e o preço deve ser maior que zero.");
            return;
        }
        await Estoque.InserirProdutoAsync(this.Nome, this.Quantidade, this.Preco);
    }

    public async Task ListarProdutos()
    {
        await Estoque.ListarEstoqueAsync();
    }

    public async Task BuscarProduto(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Erro: Digite o nome de um produto para realizar a busca.");
            return;
        }
        await Estoque.BuscarProdutoAsync(nome);
    }

    public async Task AtualizarProduto(int id, string nome, int quantidade, double preco)
    {
        List<string> camposAtualizar = new List<string>();
         
        if (id <= 0)
        {
            Console.WriteLine("Erro: Digite o ID de um produto para atualiza-lo.");
            return;
        }

        if (!string.IsNullOrWhiteSpace(nome))
        {
            camposAtualizar.Add("Nome = @Nome");
        }
        if (quantidade > 0)
        {
            camposAtualizar.Add("Quantidade = @Quantidade");
        }
        if (preco > 0)
        {
            camposAtualizar.Add("Preco = @Preco");
        }

        if (camposAtualizar.Count == 0)
        {
            Console.WriteLine("Nenhuma alteração foi informada.");
            return;
        }

        string valorSet = string.Join(", ", camposAtualizar);
        string sqlFinal = $"UPDATE Estoque SET {valorSet} WHERE ID = @ID";

        await Estoque.AtualizarProdutoAsync(sqlFinal, id, nome, quantidade, preco);
    }

    public async Task RemoverProduto(int id)
    {
        if (id <= 0)
        {
            Console.WriteLine("Erro: Digite o ID de um produto para remove-lo.");
            return;
        }

        await Estoque.ExcluirProdutoAsync(id);
    }



}


