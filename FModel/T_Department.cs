namespace SsWpfApp2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Department
    {
        public int Id { get; set; }

        public string DepartmentDep { get; set; }

        public string RegionDep { get; set; }

        public string DistrictRegionDep { get; set; }

        public string DistrictCityDep { get; set; }

        public string CityTypeDep { get; set; }

        public string CityDep { get; set; }

        public string StreetDep { get; set; }

        public string StreetTypeDep { get; set; }

        public string HousDep { get; set; }

        public string PostIndexDep { get; set; }

        public string PartnerDep { get; set; }

        public string StatusDep { get; set; }

        public string RegisterDep { get; set; }

        public string EdrpouDep { get; set; }

        public string AddressDep { get; set; }

        public string PartnerNameDep { get; set; }

        public string IdTerminalDep { get; set; }

        public string KoatuDep { get; set; }

        public string TaxIdDep { get; set; }

        public string Koatu2Dep { get; set; }
    }
}
