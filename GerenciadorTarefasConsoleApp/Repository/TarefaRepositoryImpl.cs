using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Helpers;
using GerenciadorTarefasConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                if (listaTarefas == null || !listaTarefas.Any())
                    return 1;
                return listaTarefas.Max(t => t.Id) + 1;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Erro ao criar ID para tarefa", ex);
                throw new Exception("Erro ao criar ID para tarefa", ex);
            }
        }

        public Tarefa GetTarefaById(int id)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando buscar tarefa por ID");
            try
            {
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
            catch (Exception ex)
            {
                LogHelper.Error("Erro ao buscar tarefa por ID", ex);
                throw new Exception("Erro ao buscar tarefa por ID", ex);
            }
        }

        public List<Tarefa> GetListaDeTarefas()
        {
            LogHelper.Debug("TarefaRepositoryImpl - Lendo lista de tarefas");
            try
            {
                return this._jsonHelper.ReadJson<Tarefa>();
            }
            catch (Exception ex)
            {
                LogHelper.Error("Erro ao ler a lista de tarefas", ex);
                throw new Exception("Erro ao ler a lista de tarefas", ex);
            }
        }

        public void SaveTarefa(List<Tarefa> listaTarefas)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando Salvar tarefa");
            try
            {
                this._jsonHelper.SaveJson(listaTarefas);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Erro ao salvar tarefas", ex);
                throw new Exception("Erro ao salvar tarefas", ex);
            }
        }

        public Tarefa CreateTarefa(string titulo, string desc)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando Criar tarefa");
            try
            {
                Tarefa novaTarefa = new Tarefa(titulo, desc);
                var listaTarefas = GetListaDeTarefas();
                listaTarefas.Add(novaTarefa);
                novaTarefa.Id = CreateId(listaTarefas);
                SaveTarefa(listaTarefas);
                return novaTarefa;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Erro ao criar nova tarefa", ex);
                throw new Exception("Erro ao criar nova tarefa", ex);
            }
        }

        public List<Tarefa> GetTarefaByStatus(StatusEnum status)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando buscar tarefa por Status");
            try
            {
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
            catch (Exception ex)
            {
                LogHelper.Error("Erro ao buscar tarefa por Status", ex);
                throw new Exception("Erro ao buscar tarefa por Status", ex);
            }
        }

        public List<Tarefa> GetListaDeTarefasByTitulo(string titulo)
        {
            LogHelper.Debug("TarefaRepositoryImpl - Tentando buscar tarefa por Titulo");
            try
            {
                var tarefas = _jsonHelper.ReadJson<Tarefa>();
                if (tarefas.Count > 0)
                {
                    return tarefas.FindAll(t => t.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    return new List<Tarefa>();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Erro ao buscar tarefa por Titulo", ex);
                throw new Exception("Erro ao buscar tarefa por Titulo", ex);
            }
        }
    }
}