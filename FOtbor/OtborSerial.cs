using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class OtborSerial : PapaOtbor
    {
        internal static void MainOtborSerial()
        {
            List<List<string>> list = new List<List<string>>();
            string fName = Path.Combine(dataInPath, "otbor_serial.csv");
            delegateQuery myDelegate = new delegateQuery(GetTermDepOnSerial);
            MainPapaOtbor(myDelegate, fName);
        }

        public static List<List<string>> GetTermDepOnSerial(string parStr)
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new MyDataContext())
            {
                var terminal = context.terminals;
                var lingVar = from term in terminal
                              where term.SerialNumberTerm.IndexOf(parStr.Substring(2, parStr.Length - 2)) > -1
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
