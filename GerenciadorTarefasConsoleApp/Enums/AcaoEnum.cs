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
        [Description("Alterar Status")]
        ALTERAR_STATUS,
        [Description("Alterar Nome")]
        ALTERAR_NOME,
        [Description("Alterar Descrição")]
        ALTERAR_DESC,
    }
}
