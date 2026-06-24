using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

public static class Bd
{
    private static string DbPath = "estoque.db";
    private static string ConexaoString = $"Data Source={DbPath}";

    // Este método cria a tabela se ela não existir. Vamos chamar ele quando o programa iniciar.
    public static async Task InicializarBancoAsync()
    {
        await using var conn = new SqliteConnection(ConexaoString);
        await conn.OpenAsync();

        string scriptSql = @"
            CREATE TABLE IF NOT EXISTS Estoque (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome VARCHAR(225),
                Quantidade INTEGER,
                Preco DOUBLE
            );";

        await using (var cmd = new SqliteCommand(scriptSql, conn))
        {
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine("Tabela 'Estoque' criada ou verificada com sucesso!");
        }
    }

    // Método para INSERIR um produto (Assíncrono)
    public static async Task InserirProdutoAsync(string nome, int quantidade, double preco)
    {
        await using var conn = new SqliteConnection(ConexaoString);
        await conn.OpenAsync();

        string sql = "INSERT INTO Estoque (Nome, Quantidade, Preco) VALUES (@Nome, @Quantidade, @Preco)";

        await using var cmd = new SqliteCommand(sql, conn);
        // Usar parâmetros protege seu banco contra erros de digitação e invasões
        cmd.Parameters.AddWithValue("@Nome", nome);
        cmd.Parameters.AddWithValue("@Quantidade", quantidade);
        cmd.Parameters.AddWithValue("@Preco", preco);

        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine($"Produto '{nome}' inserido com sucesso!");
    }

    // Método para CONSULTAR (Listar) o estoque (Assíncrono)
    public static async Task ListarEstoqueAsync()
    {
        await using var conn = new SqliteConnection(ConexaoString);
        await conn.OpenAsync();

        string sql = "SELECT ID, Nome, Quantidade, Preco FROM Estoque";

        await using var cmd = new SqliteCommand(sql, conn);
        await using var dataReader = await cmd.ExecuteReaderAsync();

        Console.WriteLine("\n--- ITENS NO ESTOQUE (Microsoft.Data.Sqlite) ---");

        while (await dataReader.ReadAsync())
        {
            int id = Convert.ToInt32(dataReader["ID"]);
            string nome = dataReader["Nome"].ToString();
            int qtd = Convert.ToInt32(dataReader["Quantidade"]);
            double preco = Convert.ToDouble(dataReader["Preco"]);

            Console.WriteLine($"ID: {id} | Produto: {nome} | Qtd: {qtd} | Preço: R$ {preco:F2}");
        }
        Console.WriteLine("------------------------------------------------\n");
    }
}

