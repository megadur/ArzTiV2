using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteErezeptStatusinfo
    {
        public int IdSenderezepteErezeptStatusinfo { get; set; }
        public int? IdSenderezepteErezept { get; set; }
        public string? Fcode { get; set; }
        public string? Fstatus { get; set; }
        public string? Fkommentar { get; set; }
        public string? Ftcode { get; set; }
        public short? Posnr { get; set; }
        public string? Fkurztext { get; set; }
        public string? RegelNr { get; set; }
        public string? RegelTrefferCode { get; set; }
        /// <summary>
        /// Prüf-Level: 0-nicht geprüft, 10-Import, 20-Validiert, 30-Inhalt, 40-Logik
        /// </summary>
        public short CheckLevel { get; set; }
        /// <summary>
        /// Zugehöriger Integer Wert zum fStatus - dient zur schnellen Sortierung
        /// </summary>
        public short CheckStatus { get; set; }
        public bool? Fhauptfehler { get; set; }

        public virtual ErSenderezepteErezept? IdSenderezepteErezeptNavigation { get; set; }
    }
}
