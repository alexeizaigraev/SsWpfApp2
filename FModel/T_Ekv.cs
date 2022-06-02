namespace SsWpfApp2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Ekv
    {
        public int Id { get; set; }

        [Required]
        public string FiscalEkv { get; set; }

        [Required]
        public string StatusEkv { get; set; }
    }
}
