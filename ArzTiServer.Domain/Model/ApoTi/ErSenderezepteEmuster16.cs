using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteEmuster16
    {
        public ErSenderezepteEmuster16()
        {
            ErSenderezepteEmuster16Artikels = new HashSet<ErSenderezepteEmuster16Artikel>();
            ErSenderezepteEmuster16Datens = new HashSet<ErSenderezepteEmuster16Daten>();
            ErSenderezepteEmuster16Statuses = new HashSet<ErSenderezepteEmuster16Status>();
            ErSenderezepteEmuster16Statusinfos = new HashSet<ErSenderezepteEmuster16Statusinfo>();
        }

        public int IdSenderezepteEmuster16 { get; set; }
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
        public long? Muster16Id { get; set; }
        /// <summary>
        /// kArt
        /// </summary>
        public string? AbdaKassentyp { get; set; }
        public long? ApoIkNr { get; set; }
        /// <summary>
        /// rTyp
        /// </summary>
        public string? KkTyp { get; set; }
        public decimal? GesamtesBrutto { get; set; }
        public decimal? Zuzahlung { get; set; }
        /// <summary>
        /// abDatum
        /// </summary>
        public DateOnly? Abgabedatum { get; set; }
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
        /// Krankenkassen-Ik der Versicherung
        /// </summary>
        public long? KkIkNr { get; set; }
        /// <summary>
        /// vrsNr
        /// </summary>
        public string? VersichertenNummer { get; set; }
        /// <summary>
        /// vStat
        /// </summary>
        public string? VersichertenStatus { get; set; }
        /// <summary>
        /// vTitel
        /// </summary>
        public string? VersichertenTitel { get; set; }
        /// <summary>
        /// kName
        /// </summary>
        public string? KkName { get; set; }
        /// <summary>
        /// vName
        /// </summary>
        public string? VersichertenName { get; set; }
        /// <summary>
        /// vVorname
        /// </summary>
        public string? VersichertenVorname { get; set; }
        /// <summary>
        /// vStr
        /// </summary>
        public string? VersichertenStrasse { get; set; }
        /// <summary>
        /// vPlz
        /// </summary>
        public string? VersichertenPlz { get; set; }
        /// <summary>
        /// vOrt
        /// </summary>
        public string? VersichertenOrt { get; set; }
        /// <summary>
        /// vGeb
        /// </summary>
        public DateOnly? VersichertenGeburtsdatum { get; set; }
        /// <summary>
        /// bvg
        /// </summary>
        public short? KzBvg { get; set; }
        /// <summary>
        /// hilf
        /// </summary>
        public short? KzHilfsmittel { get; set; }
        /// <summary>
        /// impf
        /// </summary>
        public short? KzImpfstoff { get; set; }
        /// <summary>
        /// sprStBedarf
        /// </summary>
        public short? KzSprechstundenbedarf { get; set; }
        /// <summary>
        /// bgrPfl
        /// </summary>
        public short? BegrPflicht { get; set; }
        /// <summary>
        /// gebFrei
        /// </summary>
        public short? KzGebuehrenFrei { get; set; }
        /// <summary>
        /// noctu
        /// </summary>
        public short? KzNoctu { get; set; }
        /// <summary>
        /// unfall
        /// </summary>
        public short? KzUnfall { get; set; }
        /// <summary>
        /// unfallTag
        /// </summary>
        public DateOnly? UnfallDatum { get; set; }
        /// <summary>
        /// eigBet
        /// </summary>
        public decimal? Eigenanteil { get; set; }
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
        /// <summary>
        /// aUnfall
        /// </summary>
        public short? KzArbeitsunfall { get; set; }
        /// <summary>
        /// tSicherheitsbestimmung
        /// </summary>
        public short? TSicherheitsbestimmung { get; set; }
        /// <summary>
        /// tFachinformation
        /// </summary>
        public short? TFachinformation { get; set; }
        /// <summary>
        /// tInLabel
        /// </summary>
        public short? TInLabel { get; set; }
        /// <summary>
        /// tOffLabel
        /// </summary>
        public short? TOffLabel { get; set; }

        public virtual ErSenderezepteHeader IdSenderezepteHeaderNavigation { get; set; } = null!;
        public virtual ICollection<ErSenderezepteEmuster16Artikel> ErSenderezepteEmuster16Artikels { get; set; }
        public virtual ICollection<ErSenderezepteEmuster16Daten> ErSenderezepteEmuster16Datens { get; set; }
        public virtual ICollection<ErSenderezepteEmuster16Status> ErSenderezepteEmuster16Statuses { get; set; }
        public virtual ICollection<ErSenderezepteEmuster16Statusinfo> ErSenderezepteEmuster16Statusinfos { get; set; }
    }
}
