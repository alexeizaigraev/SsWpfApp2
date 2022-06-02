using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class PapaOtbor : Papa
    {
        internal delegate List<List<string>> delegateQuery(string parStr);

        internal static void MainPapaOtbor(delegateQuery queryMeth, string fName, bool flagOpenNote = true)
        {
            try
            {
                Process newProc = Process.Start("notepad.exe", fName);
                newProc.WaitForExit();
                newProc.Close(); // освободить выделенные ресурсы
            }
            catch { Sos("Err Open Notepad", fName); }

            List<List<string>> outVec = new List<List<string>>();
            List<string> keys = new List<string>();
            var count = 0;
            var arr = FileToVec(fName);
            foreach (var item in arr)
            {
                if (item == "" || item == "\n") continue;
                foreach (var unit in queryMeth(item.Trim()))
                {
                    if (keys.Contains(unit[0])) { continue; }
                    else
                    {
                        List<string> vec = new List<string>();
                        vec.Add(unit[0]);
                        vec.Add(unit[1]);
                        outVec.Add(vec);
                        count++;
                        keys.Add(unit[0]);
                    }
                }

            }

            DbOtborMet.AddOtborArrTermDep(outVec);
            info = $"{count}";
        }

    }
}
