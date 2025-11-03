using System;
using System.Collections.Generic;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    public partial class ErezeptFhirPackage
    {
        public int ErezeptFhirPackageId { get; set; }
        /// <summary>
        /// E-Rezept FHIR Package Version
        /// </summary>
        public string ErezeptFhirPackageVersion { get; set; } = null!;
        public string? Beschreibung { get; set; }
        public DateOnly GueltigVon { get; set; }
        public DateOnly GueltigBis { get; set; }
    }
}
