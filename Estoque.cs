using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

public static class Estoque 
{
    private static string DbPath = "estoque.db";
    private static string ConexaoString = $"Data Source={DbPath}";

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

    public static async Task InserirProdutoAsync(string nome, int quantidade, double preco)
    {
        await using var conn = new SqliteConnection(ConexaoString);
        await conn.OpenAsync();

        string sql = "INSERT INTO Estoque (Nome, Quantidade, Preco) VALUES (@Nome, @Quantidade, @Preco)";

        await using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Nome", nome);
        cmd.Parameters.AddWithValue("@Quantidade", quantidade);
        cmd.Parameters.AddWithValue("@Preco", preco);

        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine($"Produto '{nome}' inserido com sucesso!");
    }

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


    public static async Task BuscarProdutoAsync(string nomeProduto)
    {
        await using var conn = new SqliteConnection(ConexaoString);
        await conn.OpenAsync();

        string sql = "SELECT * FROM Estoque where Nome = @Nome";

        await using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Nome", nomeProduto);

        await using var dataReader = await cmd.ExecuteReaderAsync();

        Console.WriteLine("\n--- ITEM NO ESTOQUE (Microsoft.Data.Sqlite) ---");

        bool encontrou = false;

        while (await dataReader.ReadAsync())
        {
            encontrou = true;
            int id = Convert.ToInt32(dataReader["ID"]);
            string nome = dataReader["Nome"].ToString();
            int qtd = Convert.ToInt32(dataReader["Quantidade"]);
            double preco = Convert.ToDouble(dataReader["Preco"]);

            Console.WriteLine($"ID: {id} | Produto: {nome} | Qtd: {qtd} | Preço: R$ {preco:F2}");
        }

        if (!encontrou)
        {
            Console.WriteLine("Nenhum produto encontrado com esse nome.");
        }

        Console.WriteLine("------------------------------------------------\n");
    }

    public static async Task AtualizarProdutoAsync(string sql, int id, string nome, int quantidade, double preco)
    {
        await using var conn = new SqliteConnection(ConexaoString);
        await conn.OpenAsync();
        await using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("@ID", id);

        if (sql.Contains("@Nome")) cmd.Parameters.AddWithValue("@Nome", nome);
        if (sql.Contains("@Quantidade")) cmd.Parameters.AddWithValue("@Quantidade", quantidade);
        if (sql.Contains("@Preco")) cmd.Parameters.AddWithValue("@Preco", preco);

        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine($"Produto '{nome}' atualizado com sucesso!");
    }


    public static async Task ExcluirProdutoAsync(int id)
    {
        await using var conn = new SqliteConnection(ConexaoString);
        await conn.OpenAsync();

        string sql = "DELETE FROM Estoque WHERE ID = @id";

        await using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("@id", id);

        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine($"Produto '{id}' removido com sucesso!");
    }


}

