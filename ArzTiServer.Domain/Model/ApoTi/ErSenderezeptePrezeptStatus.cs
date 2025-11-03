using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezeptePrezeptStatus
    {
        public int IdSenderezeptePrezeptStatus { get; set; }
        public int? IdSenderezeptePrezept { get; set; }
        public long? TransaktionsNummer { get; set; }
        public string? RezeptStatus { get; set; }
        public bool? RezeptCheck { get; set; }
        /// <summary>
        /// Abfragedatum des Status zum Rezept
        /// </summary>
        public DateOnly? StatusAbfrageDatum { get; set; }
        /// <summary>
        /// Abfragezeit des Status zum Rezept
        /// </summary>
        public TimeOnly? StatusAbfrageZeit { get; set; }
        /// <summary>
        /// Interner Status - enthält immer das Prüfergebnis
        /// </summary>
        public string? AbrechenStatus { get; set; }
        /// <summary>
        /// ID mit der alle DS gekennzeichnet sind die innerhalb einer RCS Abfrage gesendet wurden
        /// </summary>
        public string? RzAbfrageId { get; set; }

        public virtual ErSenderezeptePrezept? IdSenderezeptePrezeptNavigation { get; set; }
    }
}
