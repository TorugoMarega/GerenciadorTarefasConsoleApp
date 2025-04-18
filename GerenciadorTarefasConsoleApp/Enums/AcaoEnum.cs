using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Enums
{
    enum AcaoEnum
    {
        [Description("Concluir")]
        CONCLUIR,
        [Description("Exlcuir")]
        EXCLUIR,
        [Description("Iniciar")]
        INICIAR,
        [Description("Pendência")]
        PENDENCIA,
    }
}
