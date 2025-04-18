using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Helpers;
using GerenciadorTarefasConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Repository
{
    public class TarefaRepositoryImpl : ITarefaRepository
    {
        private readonly JsonHelper _jsonHelper;
        public TarefaRepositoryImpl()
        {
            _jsonHelper = new JsonHelper();
            _jsonHelper.CreateIfNotExists(); // Garante que o arquivo exista
        }
        public int CreateId(List<Tarefa> listaTarefas)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Criando novo ID");
            if (listaTarefas == null || !listaTarefas.Any())
                return 1;
            return listaTarefas.Max(t => t.Id) + 1;
        }
        public Tarefa GetTarefaById(int id)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando buscar tarefa por ID");
            var tarefas = _jsonHelper.ReadJson<Tarefa>();
            if (tarefas.Count > 0)
            {
                return tarefas.FirstOrDefault(t => t.Id == id);
            }
            else
            {
                return new Tarefa();
            }
        }

        public List<Tarefa> GetListaDeTarefas() {
            LogHelper.Debug("TarefaRepositoryImpl - Lendo lista de tarefas");
            return this._jsonHelper.ReadJson<Tarefa>();
        }

        public void SaveTarefa(List<Tarefa> listaTarefas)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando Salvar tarefa");
            this._jsonHelper.SaveJson(listaTarefas);
        }

        public Tarefa CreateTarefa(String titulo, String desc)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando Criar tarefa");
            Tarefa novaTarefa = new Tarefa(titulo, desc);
            var listaTarefas = GetListaDeTarefas();
            listaTarefas.Add(novaTarefa);
            novaTarefa.Id = CreateId(listaTarefas);
            SaveTarefa(listaTarefas);
            return novaTarefa;
        }

        public List<Tarefa> GetTarefaByStatus(StatusEnum status)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando buscar tarefa por ID");
            var tarefas = _jsonHelper.ReadJson<Tarefa>();
            if (tarefas.Count > 0)
            {
                return tarefas.FindAll(t => t.Status == status);
            }
            else
            {
                return new List<Tarefa>();
            }
        }
    }
}
