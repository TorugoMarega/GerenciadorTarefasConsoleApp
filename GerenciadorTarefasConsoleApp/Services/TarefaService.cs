using GerenciadorTarefasConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Services
{
    public class TarefaService
    {
        public Tarefa CriarTarefa(string titulo, string descricao)
        {
            Tarefa novaTarefa = new Tarefa(titulo, descricao);
            return novaTarefa;
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
            //Console.WriteLine($"ID: {tarefa.Id}");
            Console.WriteLine($"Título: {tarefa.Titulo}");
            Console.WriteLine($"Descrição: {tarefa.Descricao}");
            Console.WriteLine($"Data de Criação: {tarefa.DataCriacao}");
            Console.WriteLine($"Data de Conclusão: {tarefa.DataConclusao}");
            Console.WriteLine($"Status: {tarefa.Status}");
            Console.WriteLine("-----------------------------");
        }

        public void ExcluirTarefa(Tarefa tarefa) {
            tarefa.Status = Enum.StatusEnum.EXCLUIDA;
            tarefa.DataConclusao = DateTime.Now;
        }

        public void CriarTarefas() {
            Console.WriteLine("Digite o título da tarefa:");
            var titulo = Console.ReadLine();
            Console.WriteLine("Digite o descriçãO da tarefa:");
            var descricao = Console.ReadLine();
            var novaTarefa = this.CriarTarefa(titulo, descricao);

            var list = CarregaListaDeTarefa();
            list.Add(novaTarefa);
            Console.WriteLine("Tarefa Criada Com sucesso!");
        }

        public List<Tarefa> CarregaListaDeTarefa() {
            return new List<Tarefa>();
        }

        public void ExibirListaDeTarefas() {
            Console.WriteLine("========================================================");
            List<Tarefa> lista = CarregaListaDeTarefa();
            lista.Add(this.CriarTarefa("Tarefa Exemplo", "Criação de tarefa de exemplo"));
            Console.WriteLine("Lista de Tarefas:");
            foreach (var tarefa in lista) {
                this.ExibirTarefa(tarefa);
            }
        }
    }
}
