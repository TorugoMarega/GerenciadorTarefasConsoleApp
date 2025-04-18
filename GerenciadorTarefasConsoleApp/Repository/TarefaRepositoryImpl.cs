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
            if (listaTarefas == null || !listaTarefas.Any())
                return 1;
            return listaTarefas.Max(t => t.Id) + 1;
        }
        public Tarefa GetTarefaById(int id)
        {
            var tarefas = _jsonHelper.ReadJson<Tarefa>();
            return tarefas.FirstOrDefault(t => t.Id == id);
        }

        public List<Tarefa> GetListaDeTarefas() {
           return this._jsonHelper.ReadJson<Tarefa>();
        }

        public void SaveTarefa(List<Tarefa> listaTarefas, Tarefa novaTarefa)
        {
            listaTarefas.Add(novaTarefa);
            this._jsonHelper.SaveJson(listaTarefas);
        }

        public Tarefa CreateTarefa(String titulo, String desc)
        {
            LogHelper.Info("TarefaRepositoryImpl - Tentando Criar tarefa");
            Tarefa novaTarefa = new Tarefa(titulo, desc);
            var listaTarefas = GetListaDeTarefas();
            novaTarefa.Id = CreateId(listaTarefas);
            SaveTarefa(listaTarefas, novaTarefa);
            return novaTarefa;
        }
    }
}
