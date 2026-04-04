//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

string masterConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;";


string dbName = "InternetShop";
try
{
    using (SqlConnection conn = new SqlConnection(masterConnectionString))
    {
        conn.Open();
        Console.WriteLine("Подключение открыто!");

        string createDbSql = $"CREATE DATABASE [{dbName}]";
        SqlCommand cmd = new SqlCommand(createDbSql, conn);
        cmd.ExecuteNonQuery();
        Console.WriteLine($"База данных '{dbName}' создана!");

        string checkSql = "SELECT DB_NAME() AS CurrentDB";
        SqlCommand checkCmd = new SqlCommand(checkSql, conn);

        conn.ChangeDatabase(dbName);
        string currentDb = checkCmd.ExecuteScalar().ToString();
        Console.WriteLine($"Текущая БД: {currentDb}");
        conn.Close();
    }


    string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InternetShop;Integrated Security=True;";

    using (SqlConnection conn = new SqlConnection(ConnectionString))
    {
        conn.Open();
        Console.WriteLine("Подключение к InternetShop открыто!");

        string createTableSql = @"
            CREATE TABLE Products(
                Id INT PRIMARY KEY,
                Name VARCHAR(100),
                Price VARCHAR(90)
        )";

        SqlCommand createCmd = new SqlCommand(createTableSql, conn);
        createCmd.ExecuteNonQuery();
        Console.WriteLine("Таблица Products создана!");


        string insertSql = @"
            INSERT INTO Products VALUES 
            (1, 'Телик', '8999.0'),
            (2, 'ПК', '1599.0'),
            (3, 'Телефон', '1969.0')";

        SqlCommand insertCmd = new SqlCommand(insertSql, conn);
        int rows = insertCmd.ExecuteNonQuery();
        Console.WriteLine($"Добавлено {rows} строк");

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
