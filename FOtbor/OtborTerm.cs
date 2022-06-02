using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class OtborTerm : Papa
    {
        internal static void MainOtborTerm()
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
            string nos = arr[0].Substring(0, 4);
            foreach (var item in arr)
            {
                if (item == "") continue;
                string term = item.Trim();
                if (term == "") continue;

                if (keys.Contains(term)) { continue; }
                else
                {
                    if (term.Length < 7) term = nos + term;
                    keys.Add(term);
                    outVec.Add(term);
                    count++;
                }

            }

            DbOtborMet.AddOtborSome(outVec);
            info = $"{count}";
        }
    }
}
