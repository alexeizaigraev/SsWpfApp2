using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class HrDep : Papa
    {

        static List<string[]> koatuAll = FileToArr(dataInPath + "koatuall.csv");

        public static void MainHrDep()
        {
            info = "";
            string outText = "№ п/п;№ Відділення ТОВ ЕПС;Область;Район в обл.;Індекс;Тип населеного пункту;Населений пункт;Район в місті;Тип вулиці;Адреса;Номер будинку;Дата признчення керівника;модель РРО;Заводський № РРО;2;koatu1;koatu2\n";

            int count = 0;

            using (var context = new MyDataContext())
            {
                var department = context.departments;
                var terminal = context.terminals;
                var otbor = context.otbors;
                var w = department.ToList();

                var lingVar = from dep in department
                              join otb in otbor on dep.DepartmentDep equals otb.DepOtbor
                              where otb.DepOtbor == dep.DepartmentDep
                              select
                              new
                              {
                                  dep = dep.DepartmentDep,
                                  region = dep.RegionDep,
                                  disrictRegion = dep.DistrictRegionDep,
                                  potIndex = dep.PostIndexDep,
                                  cityType = dep.CityTypeDep,
                                  city = dep.CityDep,
                                  districtCity = dep.DistrictCityDep,
                                  streetType = dep.StreetTypeDep,
                                  street = dep.StreetDep,
                                  hous = dep.HousDep,
                                  adres = dep.AddressDep,
                                  koatu = dep.KoatuDep,
                                  partner = dep.PartnerDep
                              };

                foreach (var u in lingVar)
                {
                    try
                    {

                        count++;
                        string outLine = "";

                        string koatuNew = MkKoatuNew(u.city, u.districtCity, u.koatu);
                        outLine += koatuNew;
                        outText += outLine + "\n";

                        outLine = $"{u.dep};{u.region};{u.disrictRegion};{u.potIndex};{u.cityType};{u.city};{u.districtCity};{u.streetType};{u.street};{u.hous};;;{u.adres};{u.koatu};{koatuNew}";


                        SayCyan($"{u.dep} {koatuNew}");

                    }
                    catch { }
                }

                string ofName = Path.Combine(dataOutPath, "hr_new_deps.csv");
                TextToFile(ofName, outText);

            }
        }


        internal static string MkKoatuNew(string sity, string districtSity, string koatuOLd)
        {
            #region
            foreach (var koatuLine in koatuAll)
            {
                if (
                    Str1InStr2Both(koatuLine[1], koatuOLd) &&
                    (Str1InStr2(koatuLine[2], sity) || Str1InStr2(koatuLine[2], districtSity))
                    ) return koatuLine[0];
            }
            return "";
            #endregion
        }

        /*
                internal static string MkKoatu2(string koatuOld, string sity, string districtSity)
                {
                    MkSprDict();
                    return FinderHash(koatuOld, sity, districtSity);
                }
        */


        static string WhiteString(string inString)
        {
            #region #MkFioWhite
            string white = "";
            foreach (char cha in inString)
            {
                if (char.IsLetter(cha))
                {
                    char[] c = { cha };
                    string ss = new string(c);
                    white += ss;
                }
            }
            return white.ToLower();
            #endregion
        }


        static bool Str1InStr2(string str1, string str2)
        {
            #region
            bool flag = false;
            var s1 = WhiteString(str1);
            var s2 = WhiteString(str2);
            //if ((s2.IndexOf(s1) > -1) || (s2.IndexOf(s1) > -1)) flag = true;
            if ((s2.IndexOf(s1) > -1)) flag = true;
            return flag;
            #endregion
        }

        static bool Str1InStr2Both(string str1, string str2)
        {
            #region
            bool flag = false;
            var s1 = WhiteString(str1);
            var s2 = WhiteString(str2);
            if ((s2.IndexOf(s1) > -1) || (s1.IndexOf(s2) > -1)) flag = true;
            return flag;
            #endregion
        }

    }
}
