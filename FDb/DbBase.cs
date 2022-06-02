using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    class DbBase
    {

        public static List<string> GetTermsPartner(string partner)
        {
            #region
            List<string> outList = new List<string>();
            using (var context = new MyDataContext())
            {
                var department = context.departments;
                var terminal = context.terminals;
                var w = department.ToList();

                var lingVar = from dep in department
                              where (dep.PartnerDep == partner)
                              join term in terminal on dep.DepartmentDep equals term.DepartmentTerm

                              select
                              new
                              {
                                  term = term.TermialTerm
                              };

                foreach (var line in lingVar)
                {
                    outList.Add(line.term);
                }

            }
            return outList;
            #endregion
        }





        /*
        public static List<string> GetListTerms()
        {
            #region
            List<string> outList = new List<string>();
            using (var context = new MyDataContext())
            {
                var terminal = context.terminals;
                var w = terminal.ToList();
                foreach (var line in w)
                {
                    if (outList.IndexOf(line.TerminalTerm) < 0) outList.Add(line.TerminalTerm);
                }

            }
            return outList;
            #endregion
        }


        public static List<string> GetListPartners()
        {
            #region
            List<string> outList = new List<string>();
            using (var context = new MyDataContext())
            {
                var department = context.departments;
                var w = department.ToList();
                foreach (var line in w)
                {
                    if (outList.IndexOf(line.PartnerDep) < 0
                        && line.PartnerDep != "") outList.Add(line.PartnerDep);
                }

            }
            return outList;
            #endregion
        }

        


        public static void RefreshAll()
        {
            ExternProcess.RefresFromAccessAll();
            DbTerminalMethods.RefreshTerminal();
            DbDepartmentMethods.RefreshDepartment();
            DbOtborMethods.RefreshOtborFromFile();

        }

        */
    }
}
