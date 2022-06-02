using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class OtborDepAllTerm : Papa
    {
        internal static void MainOtborDepAllTerm()
        {
            string fName = Path.Combine(dataInPath, "otbor_term.csv");
            try
            {
                Process newProc = Process.Start("notepad.exe", fName);
                newProc.WaitForExit();
                newProc.Close(); // освободить выделенные ресурсы
            }
            catch { Sos("Err Open Notepad", fName); }

            List<string> outVec = new List<string>();
            List<string> keys = new List<string>();
            var count = 0;
            var arr = FileToVec(fName);

            foreach (var dep0 in arr)
            {
                if (dep0 == "") continue;
                string dep = dep0.Trim();

                if (keys.Contains(dep)) { continue; }

                foreach (List<string> unit in GetTermsOnDep(dep))
                {
                    var term = unit[0];
                    keys.Add(term);
                    outVec.Add(term);
                    count++;
                }
            }

            DbOtborMet.AddOtborSome(outVec);
            info = $"{count}";
        }

        public static List<List<string>> GetTermsOnDep(string dep)
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new MyDataContext())
            {
                var terminal = context.terminals;
                var lingVar = from term in terminal
                              where term.DepartmentTerm == dep
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
