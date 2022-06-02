using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class ActPeredachi : DocPapa
    {
        internal static void MainActPeredachi()
        {
            var data = GetData();

            string head = "№ п/п;Найменування устаткування, Реєстратор контрольно-касовий електронний чорний;Серійний Номер;Відділення ТОВ \"ЕПС\";Місце розташування підприємства торгівлі-послуг;Кількість оди-ниць;Вартість з ПДВ (грн.)";

            string fName = "Act_Peredachi.csv";

            MainDocPapa(data, head, fName);
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
                              orderby term.TermialTerm
                              select
                              new
                              {
                                  model = term.ModelTerm,
                                  serial = term.SerialNumberTerm,
                                  dep = term.DepartmentTerm,
                                  adr = dep.AddressDep
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.model);
                    lineList.Add(line.serial);
                    lineList.Add(line.dep);
                    lineList.Add(line.adr);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }
    }
}
