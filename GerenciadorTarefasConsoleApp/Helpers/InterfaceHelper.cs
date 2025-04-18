using GerenciadorTarefasConsoleApp.Models;
using GerenciadorTarefasConsoleApp.Repository;
using GerenciadorTarefasConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Helpers
{
    class InterfaceHelper
    {
        public static void Start()
        {


            LogHelper.Info("INICIANDO APLICAÇÃO");

            JsonHelper JsonHelper = new JsonHelper();
            JsonHelper.CreateIfNotExists();
        }

        public static void ShowMenu()
        {
            Console.WriteLine("================== SISTEMA DE TAREFAS ==================");
            Console.WriteLine("Menu");
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Cadastrar Tarefa");
            Console.WriteLine("2 - Listar Tarefas");
            Console.WriteLine("3 - Editar Tarefa");
            Console.WriteLine("4 - Excluir Tarefa");
            Console.WriteLine("5 - Buscar Tarefa por ID");
            Console.WriteLine("6 - Encerrar programa");
            Console.WriteLine("----------------------");
            Console.WriteLine("Digite a opção desejada: ");
            int.TryParse(Console.ReadLine(), out int op);
            LogHelper.Info($"MENU - O usuário esconlheu a opção: {op}");
            Console.WriteLine("========================================================\n\n");

            ITarefaRepository _repository = new TarefaRepositoryImpl();
            TarefaService service = new TarefaService(_repository);
            
            switch (op)
            {
                case 1:
                    ViewCriaTarefa(ref service);
                    break;
                case 2:
                    ViewExibeListaDeTarefas(ref service);
                    break;
                case 5:
                    ViewBuscaTarefaPorId(ref service);
                    break;
                case 6:
                    PararPrograma();
                    break;
            }
            Console.WriteLine("========================================================");
        }

        public static void PararPrograma()
        {
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
            LogHelper.Info("Aplicação está sendo encerrada pelo usuário");
        }

        public static void LimparConsole() {
            Console.Clear();
        }

        public static void ViewExibeListaDeTarefas(ref TarefaService service) {
            Console.WriteLine("\n");
            LogHelper.Debug($"InterfaceHelper - Iniciando Exibição da Lista de Tarefa");
            List<Tarefa> lista = service.CarregaListaDeTarefa();
            if (lista.Count > 0)
            {
                Console.WriteLine("Lista de Tarefas:");
                foreach (var tarefa in lista)
                {
                    ViewExibeTarefa(tarefa);
                }
                Console.WriteLine("\n\n");
            }
            else
            {
                Console.WriteLine("Lista de tarefas vazia\n\n");
            }
            ShowMenu();
        }

        public static void ViewExibeListaDeTarefasEdit(ref TarefaService service)
        {
            Console.WriteLine("\n");
            LogHelper.Debug($"InterfaceHelper - Iniciando Exibição da Lista de Tarefa");
            List<Tarefa> lista = service.CarregaListaDeTarefa();
            if (lista.Count > 0)
            {
                Console.WriteLine("Lista de Tarefas:");
                foreach (var tarefa in lista)
                {
                    ViewExibeTarefa(tarefa);
                }
                Console.WriteLine("\n\n");
            }
            else
            {
                Console.WriteLine("Lista de tarefas vazia\n\n");
            }
        }

        public static void ViewExibeTarefa(Tarefa tarefa) {
            Console.WriteLine($"ID: {tarefa.Id}");
            Console.WriteLine($"Título: {tarefa.Titulo}");
            Console.WriteLine($"Descrição: {tarefa.Descricao}");
            Console.WriteLine($"Data de Criação: {tarefa.DataCriacao}");
            Console.WriteLine($"Data de Conclusão: {tarefa.DataConclusao}");
            Console.WriteLine($"Status: {tarefa.Status}");
            Console.WriteLine("-----------------------------");
        }

        public static void ViewCriaTarefa(ref TarefaService service)
        {
            Console.WriteLine("Digite o título da tarefa:");
            var titulo = Console.ReadLine();
            Console.WriteLine("Digite o descrição da tarefa:");
            var descricao = Console.ReadLine();
            try {
                service.CriarTarefa(titulo, descricao);
                Console.WriteLine("\n\nTarefa criada com sucesso!");
                Console.WriteLine("\n\n");
                ShowMenu();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao criar a Tarefa: {titulo} Erro: {ex.Message}");
                Console.WriteLine("\n\n");
                ShowMenu();
            }
        }

        public void ViewEditaTarefa() { }

        public void ViewAtualizaTarefa() { }

        public void ViewApagaTarefa(ref TarefaService service) {
            var lista = service.CarregaListaDeTarefa();
            ViewExibeListaDeTarefasEdit(ref service);

            Console.WriteLine("Digite o ID da tarefa a ser excluída: ");
            int.TryParse(Console.ReadLine(), out int id);

            var tarefa = service.BuscaTarefaPorId(id);

            ViewExibeTarefa(tarefa);

            Console.WriteLine($"Tem certeza que deseja excluir a tarefa {id} ?");

            string op = Console.ReadLine().Trim().ToLower();

            try
            {
                if (validaSimNaoEntrada(op)){
                    service.ExcluirTarefa(tarefa);
                    Console.WriteLine("\n\nTarefa excluída com sucesso!");
                }
                else
                {
                    ViewApagaTarefa(ref service);
                }
                Console.WriteLine("\n\n");
                ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao excluir a Tarefa: {tarefa.Titulo} Erro: {ex.Message}");
                Console.WriteLine("\n\n");
                ShowMenu();
            }
        }

        public static Boolean validaSimNaoEntrada(String entrada)
        {
            // Lista de respostas positivas
            var respostasSim = new List<string> { "s", "sim", "yes", "y" };

            // Lista de respostas negativas
            var respostasNao = new List<string> { "n", "nao", "não", "no" };

            if (respostasSim.Contains(entrada))
            {
                return true;
            }
            else if (respostasNao.Contains(entrada))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Entrada inválida");
                return false;
            }
        }
        public static void ViewBuscaTarefaPorId(ref TarefaService service) {

            Console.WriteLine("Digite o ID da tarefa: ");
            int.TryParse(Console.ReadLine(), out int id);

            var tarefa = service.BuscaTarefaPorId(id);

            ViewExibeTarefa(tarefa);

            Console.WriteLine($"Deseja editar a tarefa ?");

            string op = Console.ReadLine().Trim().ToLower();

            try
            {
                if (validaSimNaoEntrada(op))
                {
                    //service.ExcluirTarefa(tarefa);
                    Console.WriteLine("\n\nTarefa editada com sucesso!");
                }
                else
                {
                    Console.WriteLine("\n\nRetornando ao menu principal");
                    ShowMenu();
                }
                Console.WriteLine("\n\n");
                ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao editar a Tarefa: {tarefa.Titulo} Erro: {ex.Message}");
                Console.WriteLine("\n\n");
                ShowMenu();
            }
        }

        public void ViewBuscaTarefaPorNome() { }
    }
}
