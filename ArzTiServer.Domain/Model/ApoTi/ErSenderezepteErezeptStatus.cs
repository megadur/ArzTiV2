using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteErezeptStatus
    {
        public int IdSenderezepteErezeptStatus { get; set; }
        public int? IdSenderezepteErezept { get; set; }
        public string? ErezeptId { get; set; }
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
        /// <summary>
        /// Prüf-Level: 0-nicht geprüft, 1-Import, 2-Validiert, 3-Inhalt, 4-Logik
        /// </summary>
        public short RezeptCheckLevel { get; set; }

        public virtual ErSenderezepteErezept? IdSenderezepteErezeptNavigation { get; set; }
    }
}
