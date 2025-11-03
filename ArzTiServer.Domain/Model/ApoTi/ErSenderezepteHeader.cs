using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteHeader
    {
        public ErSenderezepteHeader()
        {
            ErSenderezepteEmuster16s = new HashSet<ErSenderezepteEmuster16>();
            ErSenderezepteErezepts = new HashSet<ErSenderezepteErezept>();
            ErSenderezepteHeaderDatens = new HashSet<ErSenderezepteHeaderDaten>();
            ErSenderezeptePrezepts = new HashSet<ErSenderezeptePrezept>();
        }

        public int IdSenderezepteHeader { get; set; }
        public string ApoIntNr { get; set; } = null!;
        public long? ApoIkNr { get; set; }
        public string? SoftwareHersteller { get; set; }
        public string? SoftwareName { get; set; }
        public string? SoftwareVersion { get; set; }
        public string? ApoPassword { get; set; }
        public bool? Testdaten { get; set; }
        public string RzLieferId { get; set; } = null!;
        public DateOnly? RzLieferDatum { get; set; }
        public TimeOnly? RzLieferZeit { get; set; }
        public long? AvsSendeId { get; set; }
        public string? RezeptStatus { get; set; }
        /// <summary>
        /// Rezeptprüfung durchgeführt TRUE|FALSE
        /// </summary>
        public bool? RezeptCheck { get; set; }
        /// <summary>
        /// eRez-&gt;ERezept, pRez-&gt;pRezept, eM16-&gt;eMuster16 Rezept, X-&gt;Unbekannt
        /// </summary>
        public string RezTyp { get; set; } = null!;
        /// <summary>
        /// Rezept-Typ eRezept
        /// </summary>
        public bool? ReztypErez { get; set; }
        /// <summary>
        /// Rezept-Typ eMuster16
        /// </summary>
        public bool? ReztypEm16 { get; set; }
        /// <summary>
        /// Rezept-Typ pRezept
        /// </summary>
        public bool? ReztypPrez { get; set; }
        /// <summary>
        /// Abfragedatum des Status zur Liefer-ID
        /// </summary>
        public DateOnly? AbfrageDatum { get; set; }
        /// <summary>
        /// Abfragezeit des Status zur Liefer-ID
        /// </summary>
        public TimeOnly? AbfrageZeit { get; set; }
        /// <summary>
        /// eRezepte wurden überprüft
        /// </summary>
        public bool? CheckErez { get; set; }
        /// <summary>
        /// eMuster16 Rezepte wurden überprüft
        /// </summary>
        public bool? CheckEm16 { get; set; }
        /// <summary>
        /// pRezepte wurden überprüft
        /// </summary>
        public bool? CheckPrez { get; set; }
        /// <summary>
        /// Art wie das Rezept importiert wurde - 0-WebService, 1-Formular, 2-Datei
        /// </summary>
        public short? ImportTyp { get; set; }
        /// <summary>
        /// Datum des letzten Checks der Rezepte dieser LieferID
        /// </summary>
        public DateOnly? CheckDatum { get; set; }
        /// <summary>
        /// Zeit des letzten Checks der Rezepte dieser LieferID
        /// </summary>
        public TimeOnly? CheckZeit { get; set; }

        public virtual ICollection<ErSenderezepteEmuster16> ErSenderezepteEmuster16s { get; set; }
        public virtual ICollection<ErSenderezepteErezept> ErSenderezepteErezepts { get; set; }
        public virtual ICollection<ErSenderezepteHeaderDaten> ErSenderezepteHeaderDatens { get; set; }
        public virtual ICollection<ErSenderezeptePrezept> ErSenderezeptePrezepts { get; set; }
    }
}
