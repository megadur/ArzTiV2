using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezeptePrezeptPchargePwirkstoff
    {
        public int IdSenderezeptePrezeptPwirkstoff { get; set; }
        public int? IdSenderezeptePrezeptPcharge { get; set; }
        /// <summary>
        /// pzn
        /// </summary>
        public long? Pzn { get; set; }
        /// <summary>
        /// pPosNr
        /// </summary>
        public short? PPosNr { get; set; }
        /// <summary>
        /// faktor
        /// </summary>
        public decimal? Faktor { get; set; }
        /// <summary>
        /// faktorKennzeichen
        /// </summary>
        public string? FaktorKennzeichen { get; set; }
        /// <summary>
        /// taxe
        /// </summary>
        public decimal? Taxe { get; set; }
        /// <summary>
        /// preisKennzeichen
        /// </summary>
        public string? PreisKennzeichen { get; set; }
        /// <summary>
        /// wirkstoffName
        /// </summary>
        public string? WirkstoffName { get; set; }
        /// <summary>
        /// notiz
        /// </summary>
        public string? Notiz { get; set; }

        public virtual ErSenderezeptePrezeptPcharge? IdSenderezeptePrezeptPchargeNavigation { get; set; }
    }
}
