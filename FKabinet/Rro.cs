using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class Rro : PapaKabinet
    {


        public static void MainRro()
        {
            Papa.info = "";
            //List<List<string>> data = new List<List<string>>();
            //data = GetKabinetRro();
            var data = GetData();

            foreach (var u in data)
            {
                string koatu2 = Koatu2.MkKoatuNew(u[4], u[19], u[7]);
                if (u[2] == "") u[2] = "Київська";
                string shablon = $@"<?xml version=""1.0"" encoding=""windows-1251"" standalone=""no""?>
<DECLAR xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""J1311405.xsd"">
    <DECLARHEAD>
        <TIN>40243180</TIN>
        <C_DOC>J13</C_DOC>
        <C_DOC_SUB>114</C_DOC_SUB>
        <C_DOC_VER>5</C_DOC_VER>
        <C_DOC_TYPE>0</C_DOC_TYPE>
        <C_DOC_CNT>26</C_DOC_CNT>
        <C_REG>26</C_REG>
        <C_RAJ>50</C_RAJ>
        <PERIOD_MONTH>12</PERIOD_MONTH>
        <PERIOD_TYPE>1</PERIOD_TYPE>
        <PERIOD_YEAR>2021</PERIOD_YEAR>
        <C_STI_ORIG>2650</C_STI_ORIG>
        <C_DOC_STAN>1</C_DOC_STAN>
        <LINKED_DOCS xsi:nil=""true""/>
        <D_FILL>07122021</D_FILL>
        <SOFTWARE>CABINET 0.5.0</SOFTWARE>
    </DECLARHEAD>
  <DECLARBODY>
<HMN>1</HMN>
<HR>1</HR>
<HKSTI>2650</HKSTI>
<HSTI>ГОЛОВНЕ УПРАВЛІННЯ ДФС У М.КИЄВІ, ДПІ У ГОЛОСІЇВСЬКОМУ РАЙОНІ (ГОЛОСІЇВСЬКИЙ РАЙОН М.КИЄВА)</HSTI>
<HNAME>ТОВАРИСТВО З ОБМЕЖЕНОЮ ВIДПОВIДАЛЬНIСТЮ ""ЕЛЕКТРУМ ПЕЙМЕНТ СІСТЕМ""</HNAME>
<HTIN>40243180</HTIN>
<T3RXXXXG1S ROWNUM=""1"">Відділення №{u[0]}</T3RXXXXG1S>
<T3RXXXXG2 ROWNUM=""1"">{u[1]}</T3RXXXXG2>
<T3RXXXXG3S ROWNUM=""1"">{u[2]}</T3RXXXXG3S>
<T3RXXXXG4S ROWNUM=""1"">{u[3]}</T3RXXXXG4S>
<T3RXXXXG5S ROWNUM=""1"">{u[4]}</T3RXXXXG5S>
<T3RXXXXG6S ROWNUM=""1"">{u[5]}</T3RXXXXG6S>
<T3RXXXXG7S ROWNUM=""1"">{u[6]}</T3RXXXXG7S>
<T3RXXXXG8S ROWNUM=""1"" xsi:nil=""true""/>
<T3RXXXXG9S ROWNUM=""1"" xsi:nil=""true""/>
<T3RXXXXG10S ROWNUM=""1"" xsi:nil=""true""/>
<T3RXXXXG11 ROWNUM=""1"">{u[7]}</T3RXXXXG11>
<T3RXXXXG11S ROWNUM=""1"">{koatu2}</T3RXXXXG11S>
<T3RXXXXG12S ROWNUM=""1"">{u[8]}</T3RXXXXG12S>
<T3RXXXXG13 ROWNUM=""1"">487</T3RXXXXG13>
<T3RXXXXG13S ROWNUM=""1"">ГУ ДПС У ДНІПРОПЕТРОВСЬКІЙ ОБЛ.(ТЕРНІВСЬКИЙ Р-Н М.КРИВ</T3RXXXXG13S>
<R0401G1>420</R0401G1>
<R0401G1S>{u[9]}</R0401G1S>
<R0402G1S>{u[10]}</R0402G1S>
<R0403G1>1256</R0403G1>
<R0403G1S>{u[11]}</R0403G1S>
<R0404G1S>{u[12]}</R0404G1S>
<R0405G1D>{u[13].Substring(0, 10).Replace(".", "")}</R0405G1D>
<R0408G1S>{u[14]}</R0408G1S>
<R0409G1S>{u[15]}</R0409G1S>
<R0501G1S>Торгівля. Громадське харчування. Сфера послуг.</R0501G1S>
<R0601G1S>{u[16]}</R0601G1S>
<R0602G1>40</R0602G1>
<R0603G1S>{u[17]}</R0603G1S>
<R0604G1>100</R0604G1>
<R0701G1S>{u[18]}</R0701G1S>
<R0702G1S>39205324</R0702G1S>
<R0703G1S>97</R0703G1S>
<R0703G2D>01092016</R0703G2D>
<R0703G3D>31122024</R0703G3D>
<M07>1</M07>
<M05>1</M05>
<HKBOS>2903722436</HKBOS>
<HBOS>ПОЖАРСЬКИЙ ВЯЧЕСЛАВ ЮХИМОВИЧ</HBOS>
<HFILL>{DateNow()}</HFILL>
</DECLARBODY>
</DECLAR>
";


                string ofname = kabinetPath + u[0] + "_rro_" + u[10] + ".xml";
                TextToFileCP1251(ofname, shablon);
            }
            //Loger("rro");
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
                                  dep = term.DepartmentTerm,
                                  postIndex = dep.PostIndexDep,
                                  reg = dep.RegionDep,
                                  distrReg = dep.DistrictRegionDep,
                                  city = dep.CityDep,
                                  street = dep.StreetDep,
                                  hous = dep.HousDep,
                                  koatu = dep.KoatuDep,
                                  taxId = dep.TaxIdDep,
                                  model = term.ModelTerm,
                                  serial = term.SerialNumberTerm,
                                  soft = term.SoftTerm,
                                  producer = term.ProducerTerm,
                                  dateManuf = term.DateManufactureTerm,
                                  rne = term.RneRroTerm,
                                  fiscal = term.FiscalNumberTerm,
                                  oroSerial = term.OroSerialTerm,
                                  ticketSerial = term.TicketSerialTerm,
                                  toRro = term.ToRroTerm,
                                  distrCity = dep.DistrictCityDep,
                                  otborTerm = otb.TermOtbor
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.dep);
                    lineList.Add(line.postIndex);
                    lineList.Add(line.reg);
                    lineList.Add(line.distrReg);
                    lineList.Add(line.city);
                    lineList.Add(line.street);
                    lineList.Add(line.hous);
                    lineList.Add(line.koatu);
                    lineList.Add(line.taxId);
                    lineList.Add(line.model);
                    lineList.Add(line.serial);
                    lineList.Add(line.soft);
                    lineList.Add(line.producer);
                    lineList.Add(line.dateManuf);
                    lineList.Add(line.rne);
                    lineList.Add(line.fiscal);
                    lineList.Add(line.oroSerial);
                    lineList.Add(line.ticketSerial);
                    lineList.Add(line.toRro);
                    lineList.Add(line.distrCity);
                    lineList.Add(line.otborTerm);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }


    }
}
