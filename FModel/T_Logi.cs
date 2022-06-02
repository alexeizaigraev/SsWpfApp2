namespace SsWpfApp2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Logi
    {
        public int Id { get; set; }

        public string DepartmentLogi { get; set; }

        public string TermialLogi { get; set; }

        public string SerialNumberLogi { get; set; }

        public string AddressLogi { get; set; }

        public string DataLogLogi { get; set; }

        public string KindLogi { get; set; }
    }
}
