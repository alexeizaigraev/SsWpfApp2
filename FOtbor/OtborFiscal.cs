using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class OtborFiscal : PapaOtbor
    {
        internal static void MainOtborFiscal()
        {
            List<List<string>> list = new List<List<string>>();
            string fName = Path.Combine(dataInPath, "otbor_fiscal.csv");
            delegateQuery myDelegate = new delegateQuery(GetTermDepOnFiscal);
            MainPapaOtbor(myDelegate, fName);
        }

        public static List<List<string>> GetTermDepOnFiscal(string parStr)
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new MyDataContext())
            {
                var terminal = context.terminals;
                var lingVar = from term in terminal
                              where term.FiscalNumberTerm == parStr
                              select
                              new
                              {
                                  TermialTerm = term.TermialTerm,
                                  DepartmentTerm = term.DepartmentTerm
                              };
                foreach (var item in lingVar)
                {
                    List<string> vec = new List<string>();

                    vec.Add(item.TermialTerm);
                    vec.Add(item.DepartmentTerm);

                    outList.Add(vec);
                }
            };
            return outList;
            #endregion
        }
    }
}
