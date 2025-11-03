using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    public partial class ErezeptFhirPackageProfile
    {
        /// <summary>
        /// Verknüpfung zur E-Rezept FHIR Package Version
        /// </summary>
        public int ErezeptFhirPackageId { get; set; }
        /// <summary>
        /// Verknüpfung zur E-Rezept FHIR Profile Version
        /// </summary>
        public int ErezeptFhirProfileId { get; set; }
        /// <summary>
        /// Verantwortliche Stelle für das FHIR-Profil - KBV|gematik|DAV|GKV|PKV|...
        /// </summary>
        public string QuellKenner { get; set; } = null!;

        public virtual ErezeptFhirPackage ErezeptFhirPackage { get; set; } = null!;
        public virtual ErezeptFhirProfile ErezeptFhirProfile { get; set; } = null!;
    }
}
