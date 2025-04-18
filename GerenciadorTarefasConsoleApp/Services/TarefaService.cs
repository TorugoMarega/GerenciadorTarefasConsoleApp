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
                throw ex;
                return new Tarefa();
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

        public void ExibirTarefa(Tarefa tarefa)
        {
            LogHelper.Info($"Iniciando Exibição da Tarefa: {tarefa.Id} - {tarefa.Titulo}");
            //Console.WriteLine($"ID: {tarefa.Id}");
            Console.WriteLine($"Título: {tarefa.Titulo}");
            Console.WriteLine($"Descrição: {tarefa.Descricao}");
            Console.WriteLine($"Data de Criação: {tarefa.DataCriacao}");
            Console.WriteLine($"Data de Conclusão: {tarefa.DataConclusao}");
            Console.WriteLine($"Status: {tarefa.Status}");
            Console.WriteLine("-----------------------------");
        }

        public void ExcluirTarefa(Tarefa tarefa) {
            LogHelper.Info($"Excluindo a Tarefa: {tarefa.Id} - {tarefa.Titulo}");
            tarefa.Status = Enum.StatusEnum.EXCLUIDA;
            tarefa.DataConclusao = DateTime.Now;
        }

        public void CriarTarefas() {
            Console.WriteLine("Digite o título da tarefa:");
            var titulo = Console.ReadLine();
            Console.WriteLine("Digite o descrição da tarefa:");
            var descricao = Console.ReadLine();
            try {
                var novaTarefa = this.CriarTarefa(titulo, descricao);
                Console.WriteLine("Tarefa criada com sucesso!");
            }
            catch(Exception exception){
                Console.WriteLine($"TarefaService - Ocorreu um erro ao criar a Tarefa: {titulo} Erro: {exception.Message}");
                LogHelper.Error($"TarefaService - Ocorreu um erro ao criar a Tarefa: {titulo} Erro: {exception.Message}. Pilha: {exception.StackTrace}");
            }
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

        public void ExibirListaDeTarefas() {
            Console.WriteLine("========================================================");
            List<Tarefa> lista = CarregaListaDeTarefa();
            if (lista.Count > 0)
            {
                Console.WriteLine("Lista de Tarefas:");
                foreach (var tarefa in lista)
                {
                    this.ExibirTarefa(tarefa);
                }
            }
            else {
                Console.WriteLine("Lista de tarefas vazia\n");
            }
                InterfaceHelper.showMenu();
        }
    }
}
