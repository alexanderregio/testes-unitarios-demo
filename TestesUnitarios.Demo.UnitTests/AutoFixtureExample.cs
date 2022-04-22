using AutoFixture;
using TestesUnitarios.Demo.Services;
using Xunit;

namespace TestesUnitarios.Demo.UnitTests;

/// <summary>
/// AutoFixture é um pacote para facilitar na construção de variáveis e objetos
/// </summary>
public class AutoFixtureExample
{
    private readonly Fixture fixture;

    public AutoFixtureExample()
    {
        fixture = new Fixture();
    }

    [Fact]
    public void AutoFixtureExampleMethod()
    {
        // Sem AutoFixture
        var cliente = new Cliente
        {
            Id = 30523,
            Nome = "Alexander",
            Idade = 38
        };

        // Com AutoFixture
        var clienteFixture = fixture.Create<Cliente>();
    }
}