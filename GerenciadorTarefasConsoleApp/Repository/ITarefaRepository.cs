using GerenciadorTarefasConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Repository
{
    interface ITarefaRepository
    {
        void FindTarefa() { }

        void GetAllTarefa() { }

        void SaveTarefa(List<Tarefa> tarefaList, Tarefa NovaTarefa) { }
    }
}