using GerenciadorTarefasConsoleApp.Helpers;
using GerenciadorTarefasConsoleApp.Models;
using GerenciadorTarefasConsoleApp.Services;
using log4net;
using log4net.Config;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        
        void showMenu()
        {
            LogHelper.Info("INICIANDO APLICAÇÃO");

            JsonHelper JsonHelper = new JsonHelper();
            JsonHelper.CreateIfNotExists();

            Console.WriteLine("================== SISTEMA DE TAREFAS ==================");
            Console.WriteLine("Menu");
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Cadastrar Tarefa");
            Console.WriteLine("2 - Listar Tarefas");
            Console.WriteLine("3 - Editar Tarefa");
            Console.WriteLine("4 - Excluir Tarefa");
            Console.WriteLine("5 - Encerrar programa");
            Console.WriteLine("----------------------");
            Console.WriteLine("Digite a opção desejada: ");
            int.TryParse(Console.ReadLine(), out int op);
            LogHelper.Info($"MENU - O usuário esconlheu a opção: {op}");
            Console.WriteLine("========================================================\n\n");
            TarefaService tarefaService = new TarefaService();
            switch (op)
            {
                case 1:
                    tarefaService.CriarTarefas();
                    break;
                case 2:
                    tarefaService.ExibirListaDeTarefas();
                    break;
                case 5:
                    PararPrograma();
                    break;
            }
            Console.WriteLine("========================================================");
        }

        void PararPrograma()
        {
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
            LogHelper.Info("Aplicação está sendo encerrada pelo usuário");
        }

        

        showMenu();
    }
}