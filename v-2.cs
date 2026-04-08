using System.Linq.Expressions;
using Microsoft.Data.SqlClient;


string masterConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;";

string dbName = "Magaz";


try
{
    using (SqlConnection conn = new SqlConnection(masterConnectionString))
    {
        conn.Open();
        Console.WriteLine("Подключение к master выполнено!");

        string createDbSql = $"CREATE DATABASE [{dbName}]";
        SqlCommand cmd = new SqlCommand(createDbSql, conn);
        cmd.ExecuteNonQuery();

        Console.WriteLine($"База данных '{dbName}' создана");

        string checkSql = "SELECT DB_NAME() AS CurrentDB";
        SqlCommand checkCmd = new SqlCommand(checkSql, conn);

        conn.ChangeDatabase(dbName);
        string currentDb = checkCmd.ExecuteScalar().ToString();
        Console.WriteLine($"Текущая БД: {currentDb}");

        conn.Close();
    }



string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Magaz;Integrated Security=True;";

    using (SqlConnection conn = new SqlConnection(ConnectionString))
    {
        conn.Open();
        Console.WriteLine("Подключение к Magaz выполнено!");

        string createTableSql = @"
                CREATE TABLE Products (
                Id INT PRIMARY KEY,
                Name VARCHAR(100),
                Price DECIMAL(10,2)
            )";

        SqlCommand createCmd = new SqlCommand(createTableSql, conn);
        createCmd.ExecuteNonQuery();
        Console.WriteLine("Таблица Products создана!");

        string insertSql = @"
                INSERT INTO Products VALUES
                (1, 'Pk', 89990.00),
                (2, 'MacBook', 159000.00),
                (3, 'Play', 8000.00),
                (4, 'iPhone', 90000.00);
            ";

        SqlCommand insertCmd = new SqlCommand(insertSql, conn);
        int rows = insertCmd.ExecuteNonQuery();
        Console.WriteLine($"Добавлено {rows}");

        string readSql = "SELECT * FROM Products";
        SqlCommand readCmd = new SqlCommand(readSql, conn);

        using (SqlDataReader reader = readCmd.ExecuteReader())
        {
            Console.WriteLine("\n=== Товары в базе ===");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Price: {reader["Price"]} руб.");
            }
        }

        conn.Close();
    } }
catch (SqlException ex) {
    Console.WriteLine($"Ошибка SQL: {ex.Message}");
    Console.WriteLine($"Номер ошибки: {ex.Number}");
}
