using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class DbToGdrive : Papa
    {
        internal static void MainDbToGdrive()
        {
            DepToFile.MainDepToFile();
            TermToFile.MainTermToFile();
            OtborToFile.MainOtborToFile();
        }
    }
}
