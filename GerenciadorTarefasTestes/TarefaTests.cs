using GerenciadorTarefas.Controllers;
using GerenciadorTarefas.Context;
using GerenciadorTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GerenciadorTarefasTestes;

public class TarefasTestes
{
    private TarefaController CriarControllerComDados()
    {
        var options = new DbContextOptionsBuilder<TarefaContext>()
            .UseInMemoryDatabase(databaseName: $"TarefasTestDb_{Guid.NewGuid()}")
            .Options;

        var context = new TarefaContext(options);

        context.Tarefas.AddRange(
            new Tarefa { Id = 1, Titulo = "Estudar C#", Descricao = "Estudar xUnit", Data = DateTime.Today, Status = EnumStatusTarefa.Pendente },
            new Tarefa { Id = 2, Titulo = "Estudar Angular", Descricao = "Revisar services", Data = DateTime.Today, Status = EnumStatusTarefa.Finalizado },
            new Tarefa { Id = 3, Titulo = "Projeto", Descricao = "Implementar testes", Data = DateTime.Today.AddDays(1), Status = EnumStatusTarefa.Pendente }
        );
        context.SaveChanges();

        return new TarefaController(context);
    }

    [Fact]
    public void Buscar_IdValido_DeveRetornarOk()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.Buscar(1);

        var ok = Assert.IsType<OkObjectResult>(resultado);
        var tarefa = Assert.IsType<Tarefa>(ok.Value);
        Assert.Equal(1, tarefa.Id);
    }

    [Fact]
    public void Buscar_IdInvalido_DeveRetornarNotFound()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.Buscar(999);

        Assert.IsType<NotFoundResult>(resultado);
    }

    [Fact]
    public void Criar_TarefaValida_DeveRetornarCreated()
    {
        var controller = CriarControllerComDados();
        var novaTarefa = new Tarefa
        {
            Titulo = "Nova tarefa",
            Descricao = "Testando criação",
            Data = DateTime.Today,
            Status = EnumStatusTarefa.Pendente
        };

        var resultado = controller.Criar(novaTarefa);

        var created = Assert.IsType<CreatedAtActionResult>(resultado);
        var tarefa = Assert.IsType<Tarefa>(created.Value);
        Assert.Equal("Nova tarefa", tarefa.Titulo);
    }

    [Fact]
    public void Atualizar_IdValido_DeveRetornarOk()
    {
        var controller = CriarControllerComDados();
        var tarefaAtualizada = new Tarefa
        {
            Titulo = "Atualizado",
            Descricao = "Nova descrição",
            Data = DateTime.Today,
            Status = EnumStatusTarefa.Finalizado
        };

        var resultado = controller.Atualizar(1, tarefaAtualizada);

        var ok = Assert.IsType<OkObjectResult>(resultado);
        var tarefa = Assert.IsType<Tarefa>(ok.Value);
        Assert.Equal("Atualizado", tarefa.Titulo);
        Assert.Equal(EnumStatusTarefa.Finalizado, tarefa.Status);
    }

    [Fact]
    public void Atualizar_IdInvalido_DeveRetornarNotFound()
    {
        var controller = CriarControllerComDados();
        var tarefa = new Tarefa { Titulo = "Fake" };

        var resultado = controller.Atualizar(999, tarefa);

        Assert.IsType<NotFoundResult>(resultado);
    }

    [Fact]
    public void Deletar_IdValido_DeveRetornarNoContent()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.Deletar(1);

        Assert.IsType<NoContentResult>(resultado);
    }

    [Fact]
    public void Deletar_IdInvalido_DeveRetornarNotFound()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.Deletar(999);

        Assert.IsType<NotFoundResult>(resultado);
    }

    [Fact]
    public void ObterTodos_DeveRetornarLista()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.ObterTodos();

        var ok = Assert.IsType<OkObjectResult>(resultado);
        var tarefas = Assert.IsAssignableFrom<IEnumerable<Tarefa>>(ok.Value);
        Assert.NotEmpty(tarefas);
    }

    [Fact]
    public void ObterPorTitulo_DeveRetornarResultados()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.ObterPorTitulo("C#");

        var ok = Assert.IsType<OkObjectResult>(resultado);
        var tarefas = Assert.IsAssignableFrom<IEnumerable<Tarefa>>(ok.Value);
        Assert.Single(tarefas); // só 1 contém "C#"
    }

    [Fact]
    public void ObterPorData_DeveRetornarResultados()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.ObterPorData(DateTime.Today);

        var ok = Assert.IsType<OkObjectResult>(resultado);
        var tarefas = Assert.IsAssignableFrom<IEnumerable<Tarefa>>(ok.Value);
        Assert.Equal(2, tarefas.Count()); // duas tarefas têm Data = Today
    }

    [Fact]
    public void ObterPorStatus_DeveRetornarResultados()
    {
        var controller = CriarControllerComDados();

        var resultado = controller.ObterPorStatus("Pendente");

        var ok = Assert.IsType<OkObjectResult>(resultado);
        var tarefas = Assert.IsAssignableFrom<IEnumerable<Tarefa>>(ok.Value);
        Assert.Equal(2, tarefas.Count()); // duas tarefas são pendentes
    }
}
