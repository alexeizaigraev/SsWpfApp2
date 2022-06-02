using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class Term : Papa
    {
        private static int ColDataShablon1 = 5;
        private static int ColDataShablon2 = 6;
        private static int ColDataSoft = 7;
        private static int ColDataLimit = 8;

        private static string agCod = "";


        private static List<List<string>> GetTermData()
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
                                  term = term.TermialTerm,
                                  id = dep.IdTerminalDep,
                                  city = dep.CityDep,
                                  region = dep.RegionDep,
                                  streetType = dep.StreetTypeDep,
                                  street = dep.StreetDep,
                                  hous = dep.HousDep,
                                  serial = term.SerialNumberTerm,
                                  fiscal = term.FiscalNumberTerm
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.term);
                    lineList.Add(line.id);
                    lineList.Add(line.city);
                    lineList.Add(line.region);
                    lineList.Add(line.streetType);
                    lineList.Add(line.street);
                    lineList.Add(line.hous);
                    lineList.Add(line.serial);
                    lineList.Add(line.fiscal);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }

        public static void MainTerm()
        {
            var data = GetTermData();
            outLine = "";
            outText = "";
            string outFileName = "OutTerminals.csv";

            foreach (var u in data)
            {
                string terminal = u[0];
                string idd;
                if (u[1] != "") { idd = u[1]; }
                else { idd = terminal; }

                string sity = u[2];
                string region = u[3];
                if (region == "")
                    region = sity;
                string streetType = u[4];
                string street = u[5];
                string house = u[6];

                string serial = "";
                if (u[7] != "" && u[7].IndexOf('0') > -1)
                {
                    string serial0 = u[7].Substring(2, u[7].Length - 2);
                    int startZero = -1;
                    foreach (char c in serial0)
                    {
                        if ('0' == c) { startZero += 1; }
                        else { break; }
                    }

                    serial = serial0.Substring(startZero + 1, serial0.Length - startZero - 1);

                }
                else serial = u[8];
                if (serial == "") serial = "333";

                agCod = terminal.Substring(0, 3);

                outLine = terminal + ";" +
                        idd + ";" +
                        DefAgent()["shablon1"] + ";" +
                        sity + ", " + region + ";" +
                        streetType + " " + street + ", " + house + ";" +
                        DefAgent()["shablon2"] + ";" +
                        DefAgent()["soft"] + ";" +
                        DefAgent()["limit"] + ";" +
                        serial;

                outText += outLine + "\n";
                //pBlue(outLine);

            }
            Say(outText);
            TextToFile(dataOutPath + outFileName, outText);
            //infoBig = outText;
            //infoSmall = outFileName;
        }

        private static Dictionary<string, string> DefAgent()
        {
            Dictionary<string, string> h = new Dictionary<string, string>()
                {
                    { "shablon1", "" },
                    { "shablon2", ""},
                    { "soft", "" },
                    { "limit", "" },
                };

            List<string[]> a = FileToArr(myDataPath);
            foreach (string[] vec in a)
            {
                if (vec[0].IndexOf(agCod) > -1)
                {
                    h["shablon1"] = vec[ColDataShablon1];
                    h["shablon2"] = vec[ColDataShablon2];
                    h["soft"] = vec[ColDataSoft];
                    h["limit"] = vec[ColDataLimit];
                    break;
                }
            }
            if ("shablon1" == h["shablon1"])
                Sos("Незнакомый агент", agCod);

            return h;
        }
    }
}
