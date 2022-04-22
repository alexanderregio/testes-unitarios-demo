using AutoFixture;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestesUnitarios.Demo.Services;
using Xunit;

namespace TestesUnitarios.Demo.UnitTests;

/// <summary>
/// AAA pattern
/// 
/// Arrange: configuração do SUT do teste
/// Act: chamada do teste
/// Assert: verificação do resultado do teste
/// </summary>
public class ServiceTests
{
    private readonly Service service;
    private readonly Fixture fixture;

    public ServiceTests()
    {
        fixture = new Fixture();
        service = new Service();
    }

    [Fact]
    [Trait(nameof(Service.ObterClientesAsync), "Sucesso")]
    public async Task ObterClientesAsync_Sucesso()
    {
        // arrange
        var clientesExpected = fixture.CreateMany<Cliente>(2);

        // act
        var clientesActual = await service.ObterClientesAsync(false);

        // assert
        Assert.NotNull(clientesActual);
        Assert.NotEmpty(clientesActual);
        Assert.Equal(clientesExpected.Count(), clientesActual.Count());
    }

    [Fact]
    [Trait(nameof(Service.ObterClientesAsync), "ListaVazia")]
    public async Task ObterClientesAsync_ListaVazia()
    {
        // arrange
        var clientesExpected = fixture.CreateMany<Cliente>(0);

        // act
        var clientesActual = await service.ObterClientesAsync(true);

        // assert
        Assert.NotNull(clientesActual);
        Assert.Empty(clientesActual);
        Assert.Equal(clientesExpected.Count(), clientesActual.Count());
    }

    [Fact]
    [Trait(nameof(Service.ObterClientesAsync), "SucessoComFluentAssertions")]
    public async Task ObterClientesAsync_SucessoComFluentAssertions()
    {
        // arrange
        var clientesExpected = fixture.CreateMany<Cliente>(2);

        // act
        var clientesActual = await service.ObterClientesAsync(false);

        // assert
        clientesActual
            .Should() // deve
            .NotBeNullOrEmpty() // não ser nulo ou vazio
            .And.HaveCount(clientesExpected.Count()); // e deve conter a mesma quantidade esperada
                                                      //.And.BeEquivalentTo(clientesExpected); // e deve ter valores das propriedades equivalentes à lista esperada
    }

    [Fact]
    [Trait(nameof(Service.ObterClientesAsync), "ArgumentNullException")]
    public async Task ObterClientesAsync_ArgumentNullException()
    {
        // act & assert
        var argumentNullException = await Assert.ThrowsAsync<ArgumentNullException>
            (async () => await service.ObterClientesAsync(null));

        Assert.IsType<ArgumentNullException>(argumentNullException);
        Assert.Equal("Value cannot be null. (Parameter 'listaVazia')", argumentNullException.Message);
    }

    [Fact]
    [Trait(nameof(Service.ObterClientesAsync), "ListaVaziaComFluentAssertions")]
    public async Task ObterClientesAsync_ListaVaziaComFluentAssertions()
    {
        // arrange
        var clientesExpected = fixture.CreateMany<Cliente>(0);

        // act
        var clientesActual = await service.ObterClientesAsync(true);

        // assert
        Assert.NotNull(clientesActual);
        Assert.Empty(clientesActual);
        Assert.Equal(clientesExpected.Count(), clientesActual.Count());

        clientesActual
            .Should()
            .NotBeNull() // não deve ser nula
            .And.BeEmpty() // e deve ser fazia
            .And.HaveCount(clientesExpected.Count()); // e deve conter a mesma quantidade esperada
                                                      //.And.BeEquivalentTo(clientesExpected); // e deve ter valores das propriedades equivalentes à lista esperada
    }

    [Fact]
    [Trait(nameof(Service.ObterClientesAsync), "ArgumentNullExceptionComFluentAssertions")]
    public async Task ObterClientesAsync_ArgumentNullExceptionComFluentAssertions()
    {
        // act & assert
        await service.Invoking(m => m.ObterClientesAsync(null)) // ao realizar a chamada do método do método a ser testado
            .Should().ThrowAsync<ArgumentNullException>() // deve ser do tipo ArgumentNullException
            .WithMessage("Value cannot be null. (Parameter 'listaVazia')"); // e deve conter a devida mensagem
    }
}