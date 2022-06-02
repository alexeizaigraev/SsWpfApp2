namespace SsWpfApp2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Otbor
    {
        public int Id { get; set; }

        [Required]
        public string TermOtbor { get; set; }

        [Required]
        public string DepOtbor { get; set; }
    }
}
