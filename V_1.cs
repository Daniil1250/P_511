using Microsoft.Data.SqlClient;

string masterConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;";
string dbName = "InternetShop";

try
{
    using (SqlConnection conn = new SqlConnection(masterConnectionString))
    {
        conn.Open();
        Console.WriteLine("Подключение к master открыто!");

        string createDbSql = $@"
            IF DB_ID('{dbName}') IS NULL
            BEGIN
                CREATE DATABASE [{dbName}];
                PRINT 'База данных создана';
            END
            ELSE
                PRINT 'База данных уже существует';";

        using (SqlCommand cmd = new SqlCommand(createDbSql, conn))
        {
            cmd.ExecuteNonQuery();
        }

        conn.ChangeDatabase(dbName);
        using (SqlCommand checkCmd = new SqlCommand("SELECT DB_NAME()", conn))
        {
            string? currentDb = checkCmd.ExecuteScalar()?.ToString();
            Console.WriteLine($"Текущая БД: {currentDb}");
        }
    }

    string connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database={dbName};Integrated Security=True;";

    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        conn.Open();
        Console.WriteLine($"Подключение к {dbName} открыто!");

        string createTableSql = @"
            IF OBJECT_ID('dbo.Products', 'U') IS NULL
            BEGIN
                CREATE TABLE Products(
                    Id INT PRIMARY KEY,
                    Name NVARCHAR(100) NOT NULL,
                    Price DECIMAL(10,2) NOT NULL
                );
                PRINT 'Таблица Products создана';
            END
            ELSE
                PRINT 'Таблица Products уже существует';";

        using (SqlCommand createCmd = new SqlCommand(createTableSql, conn))
        {
            createCmd.ExecuteNonQuery();
        }

        using (SqlCommand clearCmd = new SqlCommand("DELETE FROM Products", conn))
        {
            clearCmd.ExecuteNonQuery();
        }

        string insertSql = @"
            INSERT INTO Products (Id, Name, Price) VALUES 
            (1, N'Телик', 8999.0),
            (2, N'ПК', 1599.0),
            (3, N'Телефон', 1969.0);";

        using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
        {
            int rows = insertCmd.ExecuteNonQuery();
            Console.WriteLine($"Добавлено {rows} строк");
        }

        using (SqlCommand selectCmd = new SqlCommand("SELECT Id, Name, Price FROM Products ORDER BY Id", conn))
        using (SqlDataReader reader = selectCmd.ExecuteReader())
        {
            Console.WriteLine("\nСодержимое таблицы Products:");
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                decimal price = reader.GetDecimal(2);
                Console.WriteLine($"  {id} | {name} | {price}");
            }
        }
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
