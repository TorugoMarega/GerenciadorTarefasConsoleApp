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
        public static void start()
        {


            LogHelper.Info("INICIANDO APLICAÇÃO");

            JsonHelper JsonHelper = new JsonHelper();
            JsonHelper.CreateIfNotExists();
        }

        public static void showMenu()
        {
        ITarefaRepository repository = new TarefaRepositoryImpl();
        TarefaService tarefaService = new TarefaService(repository);

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

        public static void PararPrograma()
        {
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
            LogHelper.Info("Aplicação está sendo encerrada pelo usuário");
        }

        public static void LimparConsole() {
            Console.Clear();
        }
    }
}
