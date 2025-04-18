using GerenciadorTarefasConsoleApp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public StatusEnum Status { get; set; }
        public Tarefa(string titulo, string descricao)
        {   
            Titulo = titulo;
            Descricao = descricao;
            DataCriacao = DateTime.Now;
            Status = StatusEnum.PENDENTE;
        }

        public Tarefa() { }
    }
}
