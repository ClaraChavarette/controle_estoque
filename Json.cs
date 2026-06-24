using System;
using System.IO;
using System.Text.Json;

namespace controle_estoque;


public class Json
{

    public void SalvarEstoque(Produto produto)
    {
        string jsonString = JsonSerializer.Serialize(produto);
        File.WriteAllText("controle_estoque.json", jsonString);
        Console.WriteLine("Arquivo salvo com sucesso!");
    }

    public void Carregarstoque()
    {
        
    }
}