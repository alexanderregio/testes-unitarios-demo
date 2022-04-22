namespace TestesUnitarios.Demo.Services;

public class Service
{
    public async Task<IEnumerable<Cliente>> ObterClientesAsync(bool? listaVazia)
    {
        if (listaVazia is null)
        {
            throw new ArgumentNullException(nameof(listaVazia));
        }

        var clientes = new List<Cliente>();

        if (listaVazia != true)
        {
            clientes = new List<Cliente>
            {
                new Cliente { Id = 123, Nome = "Alex", Idade = 28 },
                new Cliente { Id = 321, Nome = "Alexander", Idade = 38 }
            };
        }

        return await Task.Run(() => clientes);
    }
}