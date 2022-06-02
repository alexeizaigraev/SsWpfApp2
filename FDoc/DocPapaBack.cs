using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class DocPapaBack : Papa
    {
        internal static void MainDocPapaBack(List<List<string>> data, string head, string fName)
        {
            string ofName = $"{DateNowDots()}_{fName}";
            //var docPath = Path.Combine(gDrivePath, "PG_BACKUP");
            var docPath = dataInPath;
            string txt = "";
            if (head != "") txt = head + "\n";
            foreach (var line in data)
            {
                txt += $"{String.Join(";", line)}\n";
            }

            txt = txt.Replace(" 0:00:00", "").Replace("null", "");


            string fout = Path.Combine("G:/Мой диск/DRM/SQL_SERVER/", ofName);

            try
            {
                File.Delete(fout);
            }
            catch { }
            TextToFile(fout, txt);


            fout = Path.Combine("G:/Мой диск/DRM/SQL_SERVER/", fName);

            try
            {
                File.Delete(fout);
            }
            catch { }
            TextToFile(fout, txt);


            fout = Path.Combine(dataInPath, fName);
            try
            {
                File.Delete(fout);
            }
            catch { }
            TextToFile(fout, txt);
            //Papa.info += $"{fout}\n";

            //info += fout;
            //Say($"\n\n{fout}");
        }
    }
}
