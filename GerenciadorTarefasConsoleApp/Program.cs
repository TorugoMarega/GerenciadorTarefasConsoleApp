using GerenciadorTarefasConsoleApp.Services;

void showMenu(){
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
    Console.WriteLine("========================================================\n\n");
    TarefaService tarefaService = new TarefaService();
    switch (op) {
        case 1: tarefaService.CriarTarefas();
            break;
        case 2: tarefaService.ExibirListaDeTarefas();
            break;
        case 5: PararPrograma();
            break;
    }
    Console.WriteLine("========================================================");
}

void PararPrograma()
{
    Console.WriteLine("\nPressione qualquer tecla para sair...");
    Console.ReadKey();
}

showMenu();