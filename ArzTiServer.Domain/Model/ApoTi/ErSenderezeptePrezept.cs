using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezeptePrezept
    {
        public ErSenderezeptePrezept()
        {
            ErSenderezeptePrezeptDatens = new HashSet<ErSenderezeptePrezeptDaten>();
            ErSenderezeptePrezeptPcharges = new HashSet<ErSenderezeptePrezeptPcharge>();
            ErSenderezeptePrezeptStatuses = new HashSet<ErSenderezeptePrezeptStatus>();
            ErSenderezeptePrezeptStatusinfos = new HashSet<ErSenderezeptePrezeptStatusinfo>();
        }

        public int IdSenderezeptePrezept { get; set; }
        public int IdSenderezepteHeader { get; set; }
        public string RzLieferId { get; set; } = null!;
        public string? RzDatum { get; set; }
        public DateOnly? RzLieferDatum { get; set; }
        public TimeOnly? RzLieferZeit { get; set; }
        public string? AvsId { get; set; }
        /// <summary>
        /// aPeriode
        /// </summary>
        public string? AbrechnungsPeriode { get; set; }
        /// <summary>
        /// Nur STANDARDREZEPT, BTM, SPRECHSTUNDENBEDARF
        /// </summary>
        public string? RezeptTyp { get; set; }
        /// <summary>
        /// transaktionsNummer
        /// </summary>
        public long? TransaktionsNummer { get; set; }
        /// <summary>
        /// kArt
        /// </summary>
        public string? AbdaKassentyp { get; set; }
        /// <summary>
        /// apoIk
        /// </summary>
        public long? ApoIkNr { get; set; }
        /// <summary>
        /// rTyp
        /// </summary>
        public string? KkTyp { get; set; }
        public decimal? GesamtesBrutto { get; set; }
        public decimal? Zuzahlung { get; set; }
        /// <summary>
        /// pPosition1-&gt;pzn
        /// </summary>
        public long? Ppos1Pzn { get; set; }
        /// <summary>
        /// pPosition1-&gt;faktor
        /// </summary>
        public int? Ppos1Faktor { get; set; }
        /// <summary>
        /// pPosition1-&gt;taxe
        /// </summary>
        public decimal? Ppos1Taxe { get; set; }
        /// <summary>
        /// abDatum
        /// </summary>
        public DateOnly? Abgabedatum { get; set; }
        /// <summary>
        /// erstellungsZeitpunkt
        /// </summary>
        public string? ErstellungsZeitpunkt { get; set; }
        /// <summary>
        /// hashCode
        /// </summary>
        public string? HashCode { get; set; }
        /// <summary>
        /// vrtrgsArztNr - Betriebsstättennummer oder Praxis-Nummer
        /// </summary>
        public long? BetriebsstaettenNr { get; set; }
        /// <summary>
        /// laNr - Lebenslange Arzt-Nummer
        /// </summary>
        public long? ArztNr { get; set; }
        /// <summary>
        /// verDat - Ausstellungsdatum - Verordnungsdatum
        /// </summary>
        public DateOnly? Verordnungsdatum { get; set; }
        /// <summary>
        /// kkIk Krankenkassen-Ik der Versicherung
        /// </summary>
        public long? KkIkNr { get; set; }
        /// <summary>
        /// vrsNr
        /// </summary>
        public string? VersichertenNummer { get; set; }
        /// <summary>
        /// kName
        /// </summary>
        public string? KkName { get; set; }
        /// <summary>
        /// vStat
        /// </summary>
        public string? VersichertenStatus { get; set; }
        /// <summary>
        /// vGeb
        /// </summary>
        public DateOnly? VersichertenGeburtsdatum { get; set; }
        /// <summary>
        /// bvg
        /// </summary>
        public short? KzBvg { get; set; }
        /// <summary>
        /// sprStBedarf
        /// </summary>
        public short? KzSprechstundenbedarf { get; set; }
        /// <summary>
        /// gebFrei
        /// </summary>
        public short? KzGebuehrenFrei { get; set; }
        /// <summary>
        /// noctu
        /// </summary>
        public short? KzNoctu { get; set; }
        public string? Bediener { get; set; }
        /// <summary>
        /// arbPlatz
        /// </summary>
        public string? Arbeitsplatz { get; set; }
        /// <summary>
        /// sonstige
        /// </summary>
        public short? KzSonstige { get; set; }
        /// <summary>
        /// vkGueltigBis
        /// </summary>
        public string? VersichertenkarteGueltigBis { get; set; }

        public virtual ErSenderezepteHeader IdSenderezepteHeaderNavigation { get; set; } = null!;
        public virtual ICollection<ErSenderezeptePrezeptDaten> ErSenderezeptePrezeptDatens { get; set; }
        public virtual ICollection<ErSenderezeptePrezeptPcharge> ErSenderezeptePrezeptPcharges { get; set; }
        public virtual ICollection<ErSenderezeptePrezeptStatus> ErSenderezeptePrezeptStatuses { get; set; }
        public virtual ICollection<ErSenderezeptePrezeptStatusinfo> ErSenderezeptePrezeptStatusinfos { get; set; }
    }
}
