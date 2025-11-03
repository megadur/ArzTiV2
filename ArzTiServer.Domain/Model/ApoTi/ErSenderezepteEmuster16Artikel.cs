using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteEmuster16Artikel
    {
        public int IdSenderezepteEmuster16Artikel { get; set; }
        public int? IdSenderezepteEmuster16 { get; set; }
        public long? ArzneiPzn { get; set; }
        public long? HilfsmittelNr { get; set; }
        public short? AtrikelNr { get; set; }
        public decimal? ArtikelFaktor { get; set; }
        public decimal? ArtikelTaxe { get; set; }
        public short? ArtikelAutidem { get; set; }
        public string? ArtikelMediName { get; set; }

        public virtual ErSenderezepteEmuster16? IdSenderezepteEmuster16Navigation { get; set; }
    }
}
