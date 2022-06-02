using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class DepToFile : DocPapaBack
    {
        internal static void MainDepToFile()
        {
            var data = DbDepMeth.GetAllDep();

            string head = "department;region;district_region;district_city;city_type;city;street;street_type;hous;post_index;partner;status;register;edrpou;address;partner name;id_terminal;koatu;tax_id;koatu2";

            string fName = "departments.csv";

            MainDocPapaBack(data, head, fName);

        }




    }
}
