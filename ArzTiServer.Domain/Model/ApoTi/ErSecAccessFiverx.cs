using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSecAccessFiverx
    {
        public int IdSecAccessFiverx { get; set; }
        public int IdApotheke { get; set; }
        public string LoginId { get; set; } = null!;
        public string? LoginPasswort { get; set; }
        public string LoginPasswortCrypt { get; set; } = null!;
        public bool? Freigegeben { get; set; }
        public bool? ErezUc4 { get; set; }
        public bool? ErezUc51 { get; set; }
        public bool? ErezUc52 { get; set; }
        public bool? Em16Uc61Asyn { get; set; }
        public bool? Em16Uc61Syn { get; set; }
        public bool? Em16Uc61U1 { get; set; }
        public bool? ErezUc62Asyn { get; set; }
        public bool? ErezUc62Syn { get; set; }
        public bool? ErezUc7 { get; set; }
        public int? AenIdSecUser { get; set; }
        public DateOnly? AenDatum { get; set; }
        public TimeOnly? AenZeit { get; set; }

        public virtual ErApotheke IdApothekeNavigation { get; set; } = null!;
    }
}
