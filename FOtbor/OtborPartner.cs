using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class OtborPartner : Papa
    {
        internal static void MainOtborPartner(string partnerChoise)
        {

            List<List<string>> outVec = new List<List<string>>();
            List<string> keys = new List<string>();
            var count = 0;
            var arr = GetTermDepOnPartner(partnerChoise);
            foreach (var item in arr)
            {
                string term = item[0];
                if (term == "") continue;

                if (keys.Contains(term)) { continue; }
                else
                {
                    keys.Add(term);
                    outVec.Add(item);
                    count++;
                }

            }

            DbOtborMet.AddOtborArrTermDep(outVec);
            info = $"{count}";
        }

        public static List<List<string>> GetTermDepOnPartner(string parStr)
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new MyDataContext())
            {
                var department = context.departments;
                var terminal = context.terminals;

                var lingVar = from dep in department
                              join term in terminal on dep.DepartmentDep equals term.DepartmentTerm
                              where dep.PartnerDep == parStr
                              select
                              new
                              {
                                  terminal = term.TermialTerm,
                                  department = dep.DepartmentDep
                              };

                foreach (var item in lingVar)
                {
                    List<string> vec = new List<string>();

                    vec.Add(item.terminal);
                    vec.Add(item.department);

                    outList.Add(vec);
                }
            };
            return outList;
            #endregion
        }
    }
}
