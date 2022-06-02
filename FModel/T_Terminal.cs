namespace SsWpfApp2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Terminal
    {
        public int Id { get; set; }

        public string DepartmentTerm { get; set; }

        public string TermialTerm { get; set; }

        public string ModelTerm { get; set; }

        public string SerialNumberTerm { get; set; }

        public string DateManufactureTerm { get; set; }

        public string SoftTerm { get; set; }

        public string ProducerTerm { get; set; }

        public string RneRroTerm { get; set; }

        public string SealingTerm { get; set; }

        public string FiscalNumberTerm { get; set; }

        public string OroSerialTerm { get; set; }

        public string OroNumberTerm { get; set; }

        public string TicketSerialTerm { get; set; }

        public string Ticket1sheetTerm { get; set; }

        public string TicketNumberTerm { get; set; }

        public string SendingTerm { get; set; }

        public string BooksArhivTerm { get; set; }

        public string TicketsArhivTerm { get; set; }

        public string ToRroTerm { get; set; }

        public string OwnerRroTerm { get; set; }

        public string RegisterTerm { get; set; }

        public string FinishTerm { get; set; }
    }
}
