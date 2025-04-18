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
                throw ex;
            }
        }

        public void ConcluirTarefa(Tarefa tarefa)
        {
            tarefa.Status = Enum.StatusEnum.CONCLUIDA;
            tarefa.DataConclusao = DateTime.Now;
        }

        public void CancelarTarefa(Tarefa tarefa)
        {
            tarefa.Status = Enum.StatusEnum.CANCELADA;
        }

        public void EditarTarefa(Tarefa tarefa, string novoTitulo, string novaDescricao)
        {
            tarefa.Titulo = novoTitulo;
            tarefa.Descricao = novaDescricao;
        }

        public void ExcluirTarefa(Tarefa tarefa) {
            LogHelper.Info($"Excluindo a Tarefa: {tarefa.Id} - {tarefa.Titulo}");
            tarefa.Status = Enum.StatusEnum.EXCLUIDA;
            tarefa.DataConclusao = DateTime.Now;
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
    }
}
