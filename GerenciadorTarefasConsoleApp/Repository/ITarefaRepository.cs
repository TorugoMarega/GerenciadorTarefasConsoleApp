using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Repository
{
    public interface ITarefaRepository
    {
        Tarefa GetTarefaById(int id);

        List<Tarefa> GetListaDeTarefas();

        void SaveTarefa(List<Tarefa> listaTarefas);

        int CreateId(List<Tarefa> listaTarefas);

        Tarefa CreateTarefa(String titulo, String desc);

        List<Tarefa> GetTarefaByStatus(StatusEnum status);

        List<Tarefa> GetListaDeTarefasByTitulo(string titulo);
    }
}