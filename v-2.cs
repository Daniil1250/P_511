using Microsoft.Data.SqlClient;

string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InternetShop;Integrated Security=True;";

try
{
    using (SqlConnection conn = new SqlConnection(ConnectionString))
    {
        conn.Open();
        Console.WriteLine("Подключение к InternetShop открыто!");

    
        string createTableSql = @"
            IF OBJECT_ID('Products', 'U') IS NULL
            CREATE TABLE Products(
                Id INT PRIMARY KEY,
                Name VARCHAR(100),
                Price DECIMAL(10,2)
            )";

        SqlCommand createCmd = new SqlCommand(createTableSql, conn);
        createCmd.ExecuteNonQuery();
        Console.WriteLine("Таблица Products создана (или уже существует)!");

    
        string insertSql = @"
            IF NOT EXISTS (SELECT * FROM Products WHERE Id = 1)
            INSERT INTO Products VALUES 
            (1, 'Tivi', 8999.0),
            (2, 'ПК', 1599.0),
            (3, 'Телефон', 1969.0)";

        SqlCommand insertCmd = new SqlCommand(insertSql, conn);
        int rows = insertCmd.ExecuteNonQuery();

        if (rows > 0)
            Console.WriteLine($"Добавлено {rows} строк");
        else
            Console.WriteLine("Данные уже существуют");

    
        string readSql = "SELECT * FROM Products";
        SqlCommand readCmd = new SqlCommand(readSql, conn);

        using (SqlDataReader reader = readCmd.ExecuteReader())
        {
            Console.WriteLine("\n=== Товары в базе ===");
            while (reader.Read())
            {
                string name = reader.GetString(1);
                Console.WriteLine($"ID: {reader["Id"]}, Name: {name}, Price: {reader["Price"]} руб.");
            }
        }

        conn.Close();
    }
}
catch (SqlException ex)
{
    Console.WriteLine($"Ошибка SQL: {ex.Message}");
    Console.WriteLine($"Номер ошибки: {ex.Number}");
}
catch (Exception ex)
{
    Console.WriteLine($"Общая ошибка: {ex.Message}");
}
