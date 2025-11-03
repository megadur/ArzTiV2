namespace ArzTi3Server.DTOs
{
    public class ApothekeDto
    {
        public int IdApotheke { get; set; }
        public  string ApothekeName { get; set; }
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
        public string? MandantType { get; set; }
        public short? IdLeType { get; set; }
        public long? IdHauptapotheke { get; set; }
        public short? IdHtAnrede { get; set; }
        public short? Filialapotheke { get; set; }
        public bool? Gesperrt { get; set; }
        public int? SecLogin { get; set; }
        public string? SecLoginWerte { get; set; }
        public bool? SecLoginNurApoUser { get; set; }
        public int? AenIdSecUser { get; set; }
        public DateOnly? AenDatum { get; set; }
        public TimeOnly? AenZeit { get; set; }
    }

    public class CreateApothekeRequest
    {
        public string ApothekeName { get; set; }
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
        public string? MandantType { get; set; }
        public short? IdLeType { get; set; }
        public long? IdHauptapotheke { get; set; }
        public short? IdHtAnrede { get; set; }
        public short? Filialapotheke { get; set; }
        public bool? Gesperrt { get; set; }
        public int? SecLogin { get; set; }
        public string? SecLoginWerte { get; set; }
        public bool? SecLoginNurApoUser { get; set; }
    }

    public class UpdateApothekeRequest
    {
        public string ApothekeName { get; set; }
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
        public string? MandantType { get; set; }
        public short? IdLeType { get; set; }
        public long? IdHauptapotheke { get; set; }
        public short? IdHtAnrede { get; set; }
        public short? Filialapotheke { get; set; }
        public bool? Gesperrt { get; set; }
        public int? SecLogin { get; set; }
        public string? SecLoginWerte { get; set; }
        public bool? SecLoginNurApoUser { get; set; }
    }

    public class ApothekeResponse
    {
        public  IEnumerable<ApothekeDto> Apotheken { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
    }
}