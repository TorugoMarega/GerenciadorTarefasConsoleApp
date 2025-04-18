using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Helpers;
using GerenciadorTarefasConsoleApp.Models;
using GerenciadorTarefasConsoleApp.Repository;
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Services
{
    public class TarefaService
    {
        private readonly JsonHelper _jsonHelper = new JsonHelper();

        private readonly ITarefaRepository _repository;

        public TarefaService(ITarefaRepository repository) {
            _repository = repository;
        }
        public Tarefa CriarTarefa(string titulo, string descricao)
        {
            try
            {
                LogHelper.Info(($"TarefaService - Tentando criar a Tarefa"));
                return _repository.CreateTarefa(titulo, descricao);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao criar tarefa: {titulo}. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                throw;
            }
        }

        public void ConcluirTarefa(Tarefa tarefa)
        {
            tarefa.Status = StatusEnum.CONCLUIDA;
            tarefa.DataConclusao = DateTime.Now;
        }

        public void CancelarTarefa(Tarefa tarefa)
        {
            tarefa.Status = StatusEnum.CANCELADA;
        }

        public void EditarTarefa(Tarefa tarefa, string novoTitulo, string novaDescricao)
        {
            tarefa.Titulo = novoTitulo;
            tarefa.Descricao = novaDescricao;
        }

        public void AlterarStatus(Tarefa tarefa, List<Tarefa> tarefas,StatusEnum novoStatus) {
            LogHelper.Info($"Alterando status da Tarefa: {tarefa.Id} - {tarefa.Titulo} de {tarefa.Status} para {novoStatus}");
            tarefas.Find(t => t.Id.Equals(tarefa.Id)).Status = novoStatus;
            tarefas.Find(t => t.Id.Equals(tarefa.Id)).DataConclusao = DateTime.Now;
            _repository.SaveTarefa(tarefas);
        }

        public List<Tarefa> CarregaListaDeTarefa() {
            LogHelper.Info("TarefaService - Consultando lista de tarefas");
            try {
                return _repository.GetListaDeTarefas();
            }
            catch (Exception ex) {
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
                var result = _repository.GetTarefaById(id);

                if (result != null && !string.IsNullOrWhiteSpace(result.Titulo))
                {
                    return result;
                }
                else
                {
                    LogHelper.Warn($"TarefaService - Nenhuma tarefa encontrada com o ID {id} ou título em branco.");
                    return new Tarefa(); // retorna objeto vazio para evitar null
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao consultar tarefa. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                LogHelper.Debug("TarefaService - Retornando Tarefa vazia");
                return new Tarefa();
            }
        }

        public List<Tarefa> BuscaTarefaPorStatus(StatusEnum status)
        {
            LogHelper.Info($"TarefaService - Consultando tarefas no status \"{EnumHelper.GetDescription(status)}\"");
            try
            {
                var result = _repository.GetTarefaByStatus(status);

                if (result != null && !string.IsNullOrWhiteSpace(result.First().Titulo))
                {
                    return result;
                }
                else
                {
                    LogHelper.Warn($"TarefaService - Nenhuma tarefa encontrada com o Status \"{EnumHelper.GetDescription(status)}\"");
                    return new List<Tarefa>(); // retorna objeto vazio para evitar null
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TarefaService - Erro ao consultar tarefa. Erro: {ex.Message}. Pilha: {ex.StackTrace}");
                LogHelper.Debug("TarefaService - Retornando Tarefa vazia");
                return new List<Tarefa>();
            }
        }

    }
}
