using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class OtborToFile : DocPapaBack
    {

        internal static void MainOtborToFile()
        {
            var data = DbOtborMet.GetAllOtbor();

            string head = "term;dep";

            string fName = "otbor.csv";

            MainDocPapaBack(data, head, fName);

        }
    }

}