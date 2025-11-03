using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErApotheke
    {
        public ErApotheke()
        {
            ErSecAccessFiverxes = new HashSet<ErSecAccessFiverx>();
        }

        public int IdApotheke { get; set; }
        public string ApothekeName { get; set; } = null!;
        public string? ApothekeNameZusatz { get; set; }
        public long ApoIkNr { get; set; }
        public string? InhaberVorname { get; set; }
        public string? InhaberNachname { get; set; }
        public int? ApoIntNr { get; set; }
        public int? Plz { get; set; }
        public string? Ort { get; set; }
        public string? Strasse { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public string? Mobil { get; set; }
        public string? Fax { get; set; }
        public string? Bemerkung { get; set; }
        public string? Bundesland { get; set; }
        /// <summary>
        /// APO - Apotheke
        /// HRST - Hersteller (Dienstleister)
        /// </summary>
        public string? MandantType { get; set; }
        public short? IdLeType { get; set; }
        /// <summary>
        /// ID der Hauptapotheke wenn dies eine Filialapotheke ist
        /// </summary>
        public long? IdHauptapotheke { get; set; }
        public short? IdHtAnrede { get; set; }
        public short? Filialapotheke { get; set; }
        public bool? Gesperrt { get; set; }
        /// <summary>
        /// Security-Login - 0-Keiner, 1-Feste-IP, 2-DynDNS URLs, ...
        /// </summary>
        public int? SecLogin { get; set; }
        /// <summary>
        /// Werte zum Security-Login
        /// </summary>
        public string? SecLoginWerte { get; set; }
        /// <summary>
        /// Seurity-Login nur für Apotheken-Benutzer - F-nein, T-ja
        /// </summary>
        public bool? SecLoginNurApoUser { get; set; }
        public int? AenIdSecUser { get; set; }
        public DateOnly? AenDatum { get; set; }
        public TimeOnly? AenZeit { get; set; }

        public virtual ICollection<ErSecAccessFiverx> ErSecAccessFiverxes { get; set; }
    }
}
