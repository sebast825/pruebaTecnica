using Microsoft.Data.SqlClient;
using System.Data;



var stringConnection = "Server=localhost ,1433; Database=DB_NAME; User Id=sa; Password=PASSWORD; TrustServerCertificate=True;";

var connection = new SqlConnection(stringConnection);
await connection.OpenAsync();

var command = new SqlCommand("getOrderByiD ", connection);

command.CommandType = CommandType.StoredProcedure;

try
{
    Console.Write("Ingrese el número de orden: ");

    if (!int.TryParse(Console.ReadLine(), out int orderId))
    {
        Console.WriteLine("El número de orden no es válido.");
        return;
    }

    command.Parameters.AddWithValue("@id", orderId);


    var reader = await command.ExecuteReaderAsync();

    Console.WriteLine($"Orden Consultada: {orderId}");
    Console.WriteLine();
    if (await reader.ReadAsync())
    {
        ShowCompanyDetails(reader);

    }
    Console.WriteLine();

    if (await reader.NextResultAsync())
    {
        await ShowOrderProductsDetails(reader);
    }

}
catch (Exception error)
{
    Console.WriteLine(error.Message);
}



static void ShowCompanyDetails(SqlDataReader reader)
{
    Console.WriteLine($"Empresa: {reader["CompanyName"]}");
    Console.WriteLine($"Dirección: {reader["Address"]}");
}

static async Task ShowOrderProductsDetails(SqlDataReader reader)
{
    Console.WriteLine(
    $"{"Producto",-30} {"Precio",10} {"Cantidad",10} {"Otras órdenes",15}"
);

    Console.WriteLine(new string('-', 70));

    while (await reader.ReadAsync())
    {
        Console.WriteLine(
            $"{reader["ProductName"],-30} " +
            $"{reader["UnitPrice"],10} " +
            $"{reader["Quantity"],10} " +
            $"{reader["CountProductoEnOrdenesDistitnas"],15}"
        );
    }
}
