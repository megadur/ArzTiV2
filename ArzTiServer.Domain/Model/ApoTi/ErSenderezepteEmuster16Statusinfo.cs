using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteEmuster16Statusinfo
    {
        public int IdSenderezepteEmuster16Statusinfo { get; set; }
        public int? IdSenderezepteEmuster16 { get; set; }
        public string? Fcode { get; set; }
        public string? Fstatus { get; set; }
        public string? Fkommentar { get; set; }
        public string? Ftcode { get; set; }
        public short? Posnr { get; set; }
        public string? Fkurztext { get; set; }
        public string? RegelNr { get; set; }
        public string? RegelTrefferCode { get; set; }
        public bool? Fhauptfehler { get; set; }

        public virtual ErSenderezepteEmuster16? IdSenderezepteEmuster16Navigation { get; set; }
    }
}
