using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.Context;

namespace GerenciadorTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaContext _context;
        public TarefaController(TarefaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca a tarefa que contem o id informado.
        /// </summary>
        /// <param name="id">Informe o id da tarefa a ser pesquisada</param>
        /// <returns>A tarefa pesquisada</returns>
        /// <response code="200">Se a atualização ocorrer corretamente</response>
        /// <response code="404">Se não encontrar tarefas</response>
        [HttpGet("{id}")]
        public IActionResult Buscar(int? id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        /// <summary>
        /// Atualiza os dados da tarefa que contem o id informado.
        /// </summary>
        /// <returns>Retorna a tarefa criada</returns>
        /// <response code="200">Se a criação ocorrer corretamente</response>
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Buscar), new { id = tarefa.Id }, tarefa);
        }

        /// <summary>
        /// Atualiza os dados da tarefa que contem o id informado.
        /// </summary>
        /// <param name="id">Informe o id da tarefa a ser atualizada</param>
        /// <returns>A tarefa atualizada</returns>
        /// <response code="200">Se a atualização ocorrer corretamente</response>
        /// <response code="201">Se a pesquisa encontrar dados</response>
        /// <response code="404">Se não encontrar tarefas</response>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaExistente = _context.Tarefas.Find(id);
            if (tarefaExistente == null)
            {
                return NotFound();
            }
            tarefaExistente.Titulo = tarefa.Titulo;
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.Data = tarefa.Data;
            tarefaExistente.Status = tarefa.Status;

            _context.Update(tarefaExistente);
            _context.SaveChanges();
            return Ok(tarefaExistente);
        }

        /// <summary>
        /// Deleta a tarefa que contém o id informado.
        /// </summary>
        /// <param name="id">Informe o id da tarefa</param>
        /// <returns>Não retorna conteúdo</returns>
        /// <response code="201">Se a deleção ocorrer com sucesso</response>
        /// <response code="404">Se não encontrar tarefas</response>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaExistente = _context.Tarefas.Find(id);

            if (tarefaExistente == null)
            {
                return NotFound();
            }
            _context.Remove(tarefaExistente);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Busca a lista de tarefas.
        /// </summary>
        /// <returns>A lista de todas as tarefas</returns>
        /// <response code="201">Se a pesquisa encontrar dados</response>
        /// <response code="404">Se não encontrar tarefas</response>
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas;
            if (tarefas == null)
            {
                return NotFound();
            }
            return Ok(tarefas);
        }

        /// <summary>
        /// Busca tarefas pelo título informado.
        /// </summary>
        /// <param name="titulo">Informe o título a ser pesquisado</param>
        /// <returns>A lista de tarefas que contêm o título informado</returns>
        /// <response code="201">Se a pesquisa encontrar dados</response>
        /// <response code="404">Se não encontrar tarefas</response>
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _context.Tarefas.Where(t => t.Titulo.Contains(titulo)).ToList();
            if (tarefas == null)
            {
                return NotFound();
            }
            return Ok(tarefas);
        }

        /// <summary>
        /// Busca tarefas pela data informada.
        /// </summary>
        /// <param name="data">Informe a data no formato yyy-MM-dd</param>
        /// <returns>A lista de tarefas que contêm a data informada</returns>
        /// <response code="201">Se a pesquisa encontrar dados</response>
        /// <response code="404">Se não encontrar tarefas</response>
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefas = _context.Tarefas.Where(t => t.Data == data).ToList();
            if (tarefas == null)
            {
                return NotFound();
            }
            return Ok(tarefas);
        }

        /// <summary>
        /// Busca tarefas pelo status informado.
        /// </summary>
        /// <param name="status">Pendente ou Finalizado</param>
        /// <returns>A lista de tarefas que contêm o status informado</returns>
        /// <response code="201">Se a pesquisa encontrar dados</response>
        /// <response code="404">Se não encontrar tarefas</response>
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(string status)
        {
            var statusBanco = (status == "Pendente") ? EnumStatusTarefa.Pendente : EnumStatusTarefa.Finalizado;
            var tarefas = _context.Tarefas.Where(t => t.Status == statusBanco).ToList();
            if (tarefas == null)
            {
                return NotFound();
            }
            return Ok(tarefas);
        }
            
    }
}