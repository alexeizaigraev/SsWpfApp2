using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class Activaciya : Papa
    {
        internal static void MainActivaciya()
        {

            ClearInfo();
            string head = "№ п/п;№ відділення ТОВ«ЕПС»;Адреса відділення; ЗН;ФН;Дата";

            string fName = "Activaciya.csv";

            var docPath = Path.Combine(dataOutPath, "DOC");
            docPath = Path.Combine(docPath, fName);

            info = "";
            if (head != "") info = head + "\n";
            var data = GetData();
            int count = 0;
            foreach (var line in data)
            {
                count += 1;
                info += $"{count};{String.Join(";", line)};{DateNowNormal()}\n";
            }


            info = info.Replace(" 0:00:00", "").Replace("null", "");

            infoFname = docPath;
        }

        private static List<List<string>> GetData()
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new MyDataContext())
            {
                var department = context.departments;
                var terminal = context.terminals;
                var otbor = context.otbors;
                var w = department.ToList();

                var lingVar = from dep in department
                              join term in terminal on dep.DepartmentDep equals term.DepartmentTerm
                              join otb in otbor on term.TermialTerm equals otb.TermOtbor
                              select
                              new
                              {
                                  dep = dep.DepartmentDep,
                                  addr = dep.AddressDep,
                                  serial = term.SerialNumberTerm,
                                  fiscal = term.FiscalNumberTerm
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.dep);
                    lineList.Add(line.addr);
                    lineList.Add(line.serial);
                    lineList.Add(line.fiscal);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }
    }
}
