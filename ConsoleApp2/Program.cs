using System;
using System.Data.SqlClient;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StationeryFirma;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Database connection successful!");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error connecting to the database: {0}", e.Message);
            }

            Console.WriteLine("Select a task:");
            Console.WriteLine("1. Display all information about stationery");
            Console.WriteLine("2. Display all types of stationery");
            Console.WriteLine("3. Display all sales managers");
            Console.WriteLine("4. Show stationery with the maximum quantity");
            Console.WriteLine("5. Show stationery with the minimum quantity");
            Console.WriteLine("6. Show stationery with the minimum unit cost");
            Console.WriteLine("7. Show stationery with the maximum unit cost");
            Console.WriteLine("8. Enter stationery data");
            Console.WriteLine("0. Exit");

            int choice = int.Parse(Console.ReadLine());
            do
            {
                switch (choice)
                {
                    case 1:
                        var cmd = new SqlCommand("SELECT * FROM [Products]", connection);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Name: {0}, Type: {1}, Quantity: {2}, Manager: {3}, Unit Cost: {4}", reader["Name"], reader["Type"], reader["Quantity"], reader["Manager"], reader["UnitCost"]);
                        }
                        reader.Close();
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT DISTINCT [Type] FROM [Products]", connection);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Type: {0}", reader["Type"]);
                        }
                        reader.Close();
                        break;
                    case 3:
                        cmd = new SqlCommand("SELECT DISTINCT [Manager] FROM [Products]", connection);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Manager: {0}", reader["Manager"]);
                        }
                        reader.Close();
                        break;
                    case 4:
                        cmd = new SqlCommand("SELECT * FROM [Products] ORDER BY [Quantity] DESC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Stationery with the maximum quantity:");
                        Console.WriteLine("Name: {0}, Type: {1}, Quantity: {2}, Manager: {3}, Unit Cost: {4}", reader["Name"], reader["Type"], reader["Quantity"], reader["Manager"], reader["UnitCost"]);
                        reader.Close();
                        break;
                    case 5:
                        cmd = new SqlCommand("SELECT * FROM [Products] ORDER BY [Quantity] ASC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Stationery with the minimum quantity:");
                        Console.WriteLine("Name: {0}, Type: {1}, Quantity: {2}, Manager: {3}, Unit Cost: {4}", reader["Name"], reader["Type"], reader["Quantity"], reader["Manager"], reader["UnitCost"]);
                        reader.Close();
                        break;
                    case 6:
                        cmd = new SqlCommand("SELECT * FROM [Products] ORDER BY [UnitCost] ASC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Stationery with the minimum unit cost:");
                        Console.WriteLine("Name: {0}, Type: {1}, Quantity: {2}, Manager: {3}, Unit Cost: {4}", reader["Name"], reader["Type"], reader["Quantity"], reader["Manager"], reader["UnitCost"]);
                        reader.Close();
                        break;
                    case 7:
                        cmd = new SqlCommand("SELECT * FROM [Products] ORDER BY [UnitCost] DESC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Stationery with the maximum unit cost:");
                        Console.WriteLine("Name: {0}, Type: {1}, Quantity: {2}, Manager: {3}, Unit Cost: {4}", reader["Name"], reader["Type"], reader["Quantity"], reader["Manager"], reader["UnitCost"]);
                        reader.Close();
                        break;
                    case 8:
                        Console.WriteLine("Enter the name of the stationery:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter the type of the stationery:");
                        string type = Console.ReadLine();

                        Console.WriteLine("Enter the quantity of the stationery:");
                        int quantity = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter the manager's name:");
                        string manager = Console.ReadLine();

                        Console.WriteLine("Enter the unit cost of the stationery:");
                        float cost = float.Parse(Console.ReadLine());

                        cmd = new SqlCommand("INSERT INTO [Products] ([Name], [Type], [Quantity], [Manager], [UnitCost]) VALUES (@name, @type, @quantity, @manager, @cost)", connection);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@manager", manager);
                        cmd.Parameters.AddWithValue("@cost", cost);
                        cmd.ExecuteNonQuery();

                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Name: {0}, Type: {1}, Quantity: {2}, Manager: {3}, Unit Cost: {4}", reader["Name"], reader["Type"], reader["Quantity"], reader["Manager"], reader["UnitCost"]);
                        }
                        reader.Close();
                        break;
                }
            } while (choice != 0);
        }
    }
}
