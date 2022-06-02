using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class DbLogi : Papa
    {
        internal static List<List<string>> GetLogData()
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
                                  department = dep.DepartmentDep,
                                  term = term.TermialTerm,
                                  serial = term.SerialNumberTerm,
                                  adr = dep.AddressDep
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.department);
                    lineList.Add(line.term);
                    lineList.Add(line.serial);
                    lineList.Add(line.adr);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }


        private static void AddLogOne(List<string> vec, string kind)
        {
            #region
            using (var context = new MyDataContext())
            {

                var log = new T_Logi
                {
                    DepartmentLogi = vec[0],
                    TermialLogi = vec[1],
                    SerialNumberLogi = vec[2],
                    AddressLogi = vec[3],
                    DataLogLogi = DateTime.Today.ToString("dd.MM.yyyy"),
                    KindLogi = kind,
                };

                context.logis.Add(log);
                context.SaveChanges();

            }
            #endregion
        }




        public static void Loger(string kind)
        {
            #region
            Papa.info += "\nLoged " + kind + "\n";
            var arr = GetLogData();
            foreach (var vec in arr)
            {
                try
                {
                    AddLogOne(vec, kind);
                    Papa.info += $" loged {vec[0]}\n";
                }
                catch (Exception ex) { SayRed(ex.Message); }
            }


            string nau = DateTime.Today.ToString("yyyy.MM.dd");
            string fNameOld = "R:/DRM/Access/db2_be.accdb";
            string fNameNew = $"R:/DRM/BackupAccess/{nau}_db2_be.accdb";
            try
            {
                CopyOneFile(fNameOld, fNameNew);
                SayGreen(fNameNew);
            }
            catch (Exception ex) { SayRed(ex.Message + "\n"); }
            #endregion
        }

    }
}
