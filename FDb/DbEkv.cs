using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class DbEkv : Papa
    {

        internal static void RefreshEkvFromFile()
        {
            #region
            DeleteAllEkv();
            //Say("otbor add:\n");
            var data = FileToArr(Path.Combine(dataInPath, "ekv.csv"));
            var sum = 0;
            using (var context = new MyDataContext())
            {
                int countLines = -1;
                foreach (var dataLine in data)
                {
                    countLines += 1;
                    if (countLines > 0)
                    {
                        var fiscal = dataLine[0];
                        var status = dataLine[1];

                        if (fiscal == "" || status == "") continue;

                        var ekv = new T_Ekv
                        {
                            FiscalEkv = fiscal,
                            StatusEkv = status
                        };
                        context.ekvs.Add(ekv);
                        //Say($"{terminal}");
                        sum += 1;
                    }
                }
                context.SaveChanges();
            }
            SayGreen($"\nekv + {sum}\n");
            #endregion
        }


        internal static void DeleteAllEkv()
        {
            #region
            using (var context = new MyDataContext())
            {
                var ekv = context.ekvs;

                context.ekvs.RemoveRange(ekv);
                context.SaveChanges();
            }
            #endregion
        }

        public static List<List<string>> GetAllEkv()
        {
            #region
            var outList = new List<List<string>>();
            using (var context = new MyDataContext())
            {
                var ekv = context.ekvs;
                var w = ekv.ToList();
                foreach (var line in w)
                {
                    var lineVec = new List<string> { line.FiscalEkv, line.StatusEkv };
                    outList.Add(lineVec);
                }

            }
            return outList;
            #endregion
        }

        

    }
}
