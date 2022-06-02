using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class DocPapa : Papa
    {
        internal static void MainDocPapa(List<List<string>> data, string head, string ofName)
        {
            var docPath = Path.Combine(dataOutPath, "DOC");
            info = "";
            if (head != "") info = head + "\n";
            int count = 0;
            foreach (var line in data)
            {
                count += 1;
                info += $"{count};{String.Join(";", line)}\n";
            }
            string fout = Path.Combine(docPath, ofName);

            info = info.Replace(" 0:00:00", "").Replace("null", "");
            TextToFile(fout, info);
            info = fout;
            //Say($"\n\n{fout}");
        }
    }
}
