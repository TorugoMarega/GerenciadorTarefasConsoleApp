using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Helpers;
using GerenciadorTarefasConsoleApp.Models;
using GerenciadorTarefasConsoleApp.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Services
{
    public class TarefaService
    {
        private readonly JsonHelper _jsonHelper;
        private readonly ITarefaRepository _repository;

        // Alterado para injeção de dependência
        public TarefaService(ITarefaRepository repository, JsonHelper jsonHelper)
        {
            _repository = repository;
            _jsonHelper = jsonHelper;
        }

        public Tarefa CriarTarefa(string titulo, string descricao)
        {
            try
            {
                LogHelper.Info($"TarefaService - Tentando criar a Tarefa: {titulo}");
                var tarefaCriada = _repository.CreateTarefa(titulo, descricao);
                LogHelper.Info($"TarefaService - Tarefa criada com sucesso: {tarefaCriada.Titulo}");
                return tarefaCriada;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao criar tarefa: {titulo}. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                throw;
            }
        }

        public void AlterarStatus(Tarefa tarefa, List<Tarefa> tarefas, StatusEnum novoStatus)
        {
            try
            {
                LogHelper.Info($"Alterando status da Tarefa: {tarefa.Id} - {tarefa.Titulo} de {tarefa.Status} para {novoStatus}");
                var tarefaExistente = tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
                if (tarefaExistente != null)
                {
                    tarefaExistente.Status = novoStatus;
                    tarefaExistente.DataConclusao = DateTime.Now;
                    _repository.SaveTarefa(tarefas);
                    LogHelper.Info($"TarefaService - Status da tarefa alterado para {novoStatus}.");
                }
                else
                {
                    LogHelper.Warn($"TarefaService - Tarefa com ID {tarefa.Id} não encontrada.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao alterar status da tarefa: {tarefa.Id}. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                throw;
            }
        }

        public List<Tarefa> CarregaListaDeTarefa()
        {
            LogHelper.Info("TarefaService - Consultando lista de tarefas");
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var tarefas = _repository.GetListaDeTarefas();
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 1000) // Se a consulta demorar mais de 1 segundo
                {
                    LogHelper.Warn($"TarefaService - Consulta de tarefas demorou mais de 1 segundo: {stopwatch.ElapsedMilliseconds}ms.");
                }
                return tarefas;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao consultar lista de tarefas. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                LogHelper.Error("TarefaService - Retornando Lista vazia");
                return new List<Tarefa>();
            }
        }

        public Tarefa BuscaTarefaPorId(int id)
        {
            LogHelper.Info($"TarefaService - Consultando tarefa {id}");
            try
            {
                var tarefa = _repository.GetTarefaById(id);
                if (tarefa != null && !string.IsNullOrWhiteSpace(tarefa.Titulo))
                {
                    return tarefa;
                }
                else
                {
                    LogHelper.Warn($"TarefaService - Nenhuma tarefa encontrada com o ID {id} ou título em branco.");
                    return null; // Pode retornar null para indicar que não foi encontrado
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao consultar tarefa. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                LogHelper.Debug("TarefaService - Retornando Tarefa vazia");
                return null; // Retornando null ao invés de um objeto vazio
            }
        }

        public List<Tarefa> BuscaTarefaPorStatus(StatusEnum status)
        {
            LogHelper.Info($"TarefaService - Consultando tarefas no status \"{EnumHelper.GetDescription(status)}\"");
            try
            {
                var tarefas = _repository.GetTarefaByStatus(status);
                if (tarefas != null && tarefas.Any())
                {
                    return tarefas;
                }
                else
                {
                    LogHelper.Warn($"TarefaService - Nenhuma tarefa encontrada com o Status \"{EnumHelper.GetDescription(status)}\"");
                    return new List<Tarefa>(); // Retornando uma lista vazia
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao consultar tarefas com status {EnumHelper.GetDescription(status)}. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                LogHelper.Debug("TarefaService - Retornando Lista vazia");
                return new List<Tarefa>();
            }
        }

        public void EditarAtributosTarefa(List<Tarefa> tarefas, Tarefa tarefa, string novoTitulo, string novaDescricao)
        {
            try
            {
                LogHelper.Info($"TarefaService - Alterando Atributos da Tarefa: {tarefa.Id} - {tarefa.Titulo}");
                var tarefaExistente = tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
                if (tarefaExistente != null)
                {
                    tarefaExistente.Titulo = novoTitulo;
                    tarefaExistente.Descricao = novaDescricao;
                    _repository.SaveTarefa(tarefas);
                    LogHelper.Info($"TarefaService - Novos Atributos. Título: {novoTitulo}. Descrição:{novaDescricao}");
                }
                else
                {
                    LogHelper.Warn($"TarefaService - Tarefa com ID {tarefa.Id} não encontrada.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao editar atributos da tarefa {tarefa.Id}. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                throw;
            }
        }

        public List<Tarefa> BuscarPorTitulo(string titulo)
        {
            LogHelper.Info($"TarefaService - Consultando tarefas com Título: \"{titulo}\"");
            try
            {
                var tarefas = _repository.GetListaDeTarefasByTitulo(titulo);
                if (tarefas != null && tarefas.Any())
                {
                    return tarefas;
                }
                else
                {
                    LogHelper.Warn($"TarefaService - Nenhuma tarefa encontrada com o Titulo \"{titulo}\"");
                    return new List<Tarefa>(); // Retornando lista vazia
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao consultar tarefas com título {titulo}. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                LogHelper.Debug("TarefaService - Retornando Lista vazia");
                return new List<Tarefa>();
            }
        }
    }
}
