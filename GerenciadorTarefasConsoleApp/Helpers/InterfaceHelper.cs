using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Models;
using GerenciadorTarefasConsoleApp.Repository;
using GerenciadorTarefasConsoleApp.Services;
using System;
using System.Collections.Generic;

namespace GerenciadorTarefasConsoleApp.Helpers
{
    class InterfaceHelper
    {
        private static TarefaService _service;

        public static void Start()
        {
            LogHelper.Info("INICIANDO APLICAÇÃO");
            var jsonHelper = new JsonHelper();
            jsonHelper.CreateIfNotExists();
            _service = new TarefaService(new TarefaRepositoryImpl(), jsonHelper); // Agora passando o JsonHelper também
            Run();
        }

        private static void Run()
        {
            while (true)
            {
                ShowMenu();
                if (!HandleMenuOption(ReadInt("Digite a opção desejada: "))) break;
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("===============================================");
            Console.WriteLine("            SISTEMA DE TAREFAS");
            Console.WriteLine("===============================================");
            Console.WriteLine("1 - Cadastrar Tarefa");
            Console.WriteLine("2 - Listar Tarefas");
            Console.WriteLine("3 - Editar Tarefa");
            Console.WriteLine("4 - Buscar Tarefa por ID");
            Console.WriteLine("5 - Buscar Tarefa por Status");
            Console.WriteLine("6 - Buscar Tarefa por Título");
            Console.WriteLine("0 - Encerrar programa");
            Console.WriteLine("===============================================");
        }

        private static bool HandleMenuOption(int op)
        {
            LogHelper.Info($"MENU - O usuário escolheu a opção: {op}");
            Console.WriteLine();
            switch (op)
            {
                case 1: ViewCriaTarefa(); break;
                case 2: ViewExibeListaDeTarefas(); break;
                case 3: ViewEditaTarefaMenu(); break;
                case 4: ViewBuscaTarefaPorId(); break;
                case 5: ViewBuscaTarefaPorStatus(); break;
                case 6: ViewBuscaTarefaPorTitulo(); break;
                case 0:
                    PararPrograma();
                    return false;
                default:
                    Console.WriteLine("Opção inválida.\n");
                    break;
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return true;
        }

        private static void PararPrograma()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("          Encerrando aplicação...");
            Console.WriteLine("===============================================");
            LogHelper.Info("Aplicação encerrada pelo usuário");
        }

        private static void ViewCriaTarefa()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("           Cadastro de Nova Tarefa");
            Console.WriteLine("===============================================");
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();
            Console.Write("Digite a descrição da tarefa: ");
            string descricao = Console.ReadLine();

            try
            {
                _service.CriarTarefa(titulo, descricao);
                Console.WriteLine("\nTarefa criada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar tarefa: {ex.Message}");
            }
        }

        private static void ViewExibeListaDeTarefas()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("           Lista de Tarefas");
            Console.WriteLine("===============================================");
            var lista = _service.CarregaListaDeTarefa();
            if (lista.Count > 0)
            {
                lista.ForEach(ViewExibeTarefa);
            }
            else Console.WriteLine("\nLista de tarefas vazia.");
        }

        private static void ViewExibeTarefa(Tarefa tarefa)
        {
            Console.WriteLine($"\nID: {tarefa.Id}");
            Console.WriteLine($"Título: {tarefa.Titulo}");
            Console.WriteLine($"Descrição: {tarefa.Descricao}");
            Console.WriteLine($"Data de Criação: {tarefa.DataCriacao}");
            Console.WriteLine($"Data de Conclusão: {tarefa.DataConclusao}");
            Console.WriteLine($"Status: {EnumHelper.GetDescription(tarefa.Status)}");
            Console.WriteLine("-----------------------------------------------\n");
        }

        private static void ViewBuscaTarefaPorId()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("        Busca de Tarefa por ID");
            Console.WriteLine("===============================================");
            int id = ReadInt("Digite o ID da tarefa: ");
            var tarefa = _service.BuscaTarefaPorId(id);

            if (tarefa == null)
            {
                Console.WriteLine("\nTarefa não encontrada.");
                return;
            }

            ViewExibeTarefa(tarefa);
            if (Confirmacao("\nDeseja editar a tarefa? (S/N) "))
            {
                ViewEditaTarefa(tarefa);
            }
        }

        private static void ViewBuscaTarefaPorTitulo()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("       Busca de Tarefa por Título");
            Console.WriteLine("===============================================");
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();
            var tarefas = _service.BuscarPorTitulo(titulo);

            if (tarefas.Count == 0)
                Console.WriteLine("\nNenhuma tarefa encontrada.");
            else
                tarefas.ForEach(ViewExibeTarefa);
        }

        private static void ViewBuscaTarefaPorStatus()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("      Busca de Tarefa por Status");
            Console.WriteLine("===============================================");
            Console.WriteLine("\nStatus disponíveis:");
            foreach (StatusEnum status in Enum.GetValues(typeof(StatusEnum)))
            {
                Console.WriteLine($"{EnumHelper.GetIdInt(status)} - {EnumHelper.GetDescription(status)}");
            }

            int statusId = ReadInt("\nDigite o código do status: ");
            StatusEnum statusEnum = EnumHelper.GetIdEnum<StatusEnum>(statusId);
            var tarefas = _service.BuscaTarefaPorStatus(statusEnum);

            if (tarefas.Count == 0)
                Console.WriteLine($"\nNenhuma tarefa com status \"{EnumHelper.GetDescription(statusEnum)}\"");
            else
                tarefas.ForEach(ViewExibeTarefa);
        }

        private static void ViewEditaTarefaMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("           Edição de Tarefa");
            Console.WriteLine("===============================================");
            ViewExibeListaDeTarefas();
            int id = ReadInt("\nDigite o ID da tarefa que deseja editar: ");
            var tarefa = _service.BuscaTarefaPorId(id);
            if (tarefa != null)
            {
                ViewEditaTarefa(tarefa);
            }
            else
            {
                Console.WriteLine("\nTarefa não encontrada.");
            }
        }

        private static void ViewEditaTarefa(Tarefa tarefa)
        {
            bool continuarEditando = true;

            while (continuarEditando)
            {
                Console.WriteLine("===============================================");
                Console.WriteLine("             Edição de Tarefa");
                Console.WriteLine("===============================================");
                // Exibe os dados da tarefa antes de qualquer alteração
                ViewExibeTarefa(tarefa);
                Console.WriteLine("\nAÇÕES:");
                Console.WriteLine("0 - Alterar Status");
                Console.WriteLine("1 - Alterar Título");
                Console.WriteLine("2 - Alterar Descrição");
                Console.WriteLine("333 - Voltar ao menu");

                int acao = ReadInt("\nEscolha uma ação: ");

                switch (acao)
                {
                    case 0:
                        ViewAlteraStatusTarefa(tarefa);
                        break;
                    case 1:
                    case 2:
                        ViewAlteraAtributosTarefa(tarefa, acao);
                        break;
                    case 333:
                        continuarEditando = false;
                        break;
                    default:
                        Console.WriteLine("\nAção inválida.");
                        break;
                }

                // Atualiza a tarefa após qualquer modificação
                if (acao != 333)
                {
                    tarefa = _service.BuscaTarefaPorId(tarefa.Id);  // Recarrega a tarefa atualizada
                }
            }
        }

        private static void ViewAlteraStatusTarefa(Tarefa tarefa)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("           Alteração de Status");
            Console.WriteLine("===============================================");
            Console.WriteLine("\nStatus disponíveis:");
            foreach (StatusEnum status in Enum.GetValues(typeof(StatusEnum)))
            {
                Console.WriteLine($"{EnumHelper.GetIdInt(status)} - {EnumHelper.GetDescription(status)}");
            }

            int statusId = ReadInt("\nEscolha o novo status: ");
            if (statusId == 333) return;

            StatusEnum novoStatus = EnumHelper.GetIdEnum<StatusEnum>(statusId);
            if (Confirmacao($"\nDeseja alterar o status da tarefa para \"{EnumHelper.GetDescription(novoStatus)}\"? (S/N) "))
            {
                _service.AlterarStatus(tarefa, _service.CarregaListaDeTarefa(), novoStatus);
                Console.WriteLine("\nStatus atualizado com sucesso!");
            }
        }

        private static void ViewAlteraAtributosTarefa(Tarefa tarefa, int tipoAlteracao)
        {
            string novoTitulo = tarefa.Titulo;
            string novaDesc = tarefa.Descricao;

            Console.WriteLine("===============================================");
            Console.WriteLine("           Alteração de Atributos");
            Console.WriteLine("===============================================");

            if (tipoAlteracao == 1)
            {
                Console.Write("Digite o novo título: ");
                novoTitulo = Console.ReadLine();
            }
            else
            {
                Console.Write("Digite a nova descrição: ");
                novaDesc = Console.ReadLine();
            }

            _service.EditarAtributosTarefa(_service.CarregaListaDeTarefa(), tarefa, novoTitulo, novaDesc);
            Console.WriteLine("\nAtributo atualizado com sucesso!");
        }

        // Métodos auxiliares

        private static int ReadInt(string mensagem)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Tente novamente: ");
            }
            return valor;
        }

        private static bool Confirmacao(string mensagem)
        {
            Console.Write(mensagem);
            string entrada = Console.ReadLine()?.Trim().ToLower();

            var positivos = new[] { "s", "sim", "y", "yes" };
            var negativos = new[] { "n", "nao", "não", "no" };

            if (positivos.Contains(entrada)) return true;
            if (negativos.Contains(entrada)) return false;

            Console.WriteLine("\nEntrada inválida. Operação cancelada.");
            return false;
        }
    }
}
