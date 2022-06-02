using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class Ekv : Papa
    {
        private static List<List<string>> data = GetMyData();

        internal static void MainEkv()
        {
            DbEkv.RefreshEkvFromFile();


            int countComon = 0;
            int countComonActiv = 0;
            int countComonNoactiv = 0;
            string outText = "партнёр;aктивные;заблокированные;всего\n";


            List<String> partners = DbDepMeth.GetLisPartner();
            foreach (var partner in partners)
            {
                int countPartnerActiv = SumActiv(partner);
                countComonActiv += countPartnerActiv;

                int countPartnerNoactiv = SumNoActiv(partner);
                countComonNoactiv += countPartnerNoactiv;

                if (countPartnerActiv == 0 && countPartnerNoactiv == 0)
                    continue;

                int countPartnerComon = SumComon(partner);
                countComon += countPartnerComon;

                string outLine = $"{partner};{countPartnerActiv};{countPartnerNoactiv};{countPartnerComon}";
                outText += outLine + "\n";
            }


            outText += "________________\nвсего;активные;заблокированные\n";
            outText += $"{countComon};{countComonActiv};{countComonNoactiv}\n";

            info += outText;

            outText += "\n________________\n";
            outText += "партнёр;терминал;отделение;ЗН;ФН;статус;область;город;адрес\n";


            foreach (var line in data)
            {
                outText += String.Join(";", line) + "\n";
            }

            string ofName = Path.Combine(dataOutPath, "DOC");
            ofName = Path.Combine(ofName, "Ekv.csv");

            TextToFile(ofName, outText);

        }

        private static int SumComon(string partner)
        {
            int sum = 0;
            foreach (var line in data)
            {
                if (partner == line[0])
                    sum += 1;
            }

            return sum;
        }


        private static int SumNoActiv(string partner)
        {
            int sum = 0;
            foreach (var line in data)
            {
                if (partner == line[0] && line[5] != "Активний")
                    sum += 1;
            }
            return sum;
        }


        private static int SumActiv(string partner)
        {
            int sum = 0;
            foreach(var line in data)
            {
                if (partner == line[0] && line[5] == "Активний")
                        sum += 1;
            }

            return sum;
        }

        private static List<List<string>> GetMyData()
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new MyDataContext())
            {
                var department = context.departments;
                var terminal = context.terminals;
                var ekv = context.ekvs;
                var w = department.ToList();

                var lingVar = from dep in department
                              join term in terminal on dep.DepartmentDep equals term.DepartmentTerm
                              join ek in ekv on term.FiscalNumberTerm equals ek.FiscalEkv
                              orderby term.TermialTerm
                              select
                              new
                              {
                                  partner = dep.PartnerDep,
                                  term = term.TermialTerm,
                                  dep = term.DepartmentTerm,
                                  serial = term.SerialNumberTerm,
                                  fiscal = term.FiscalNumberTerm,
                                  status = ek.StatusEkv,
                                  region = dep.RegionDep,
                                  city = dep.CityDep,
                                  addr = dep.AddressDep,                                  
                              };

                foreach (var line in lingVar)
                {
                    List<string> lineList = new List<string>();
                    lineList.Add(line.partner);
                    lineList.Add(line.term);
                    lineList.Add(line.dep);
                    lineList.Add(line.serial);
                    lineList.Add(line.fiscal);
                    lineList.Add(line.status);
                    lineList.Add(line.region);
                    lineList.Add(line.city);
                    lineList.Add(line.addr);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }

    }
}
