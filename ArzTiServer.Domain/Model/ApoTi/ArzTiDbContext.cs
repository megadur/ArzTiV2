using Microsoft.EntityFrameworkCore;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi;

public partial class ArzTiDbContext : DbContext
{
    public ArzTiDbContext()
    {
    }

    public ArzTiDbContext(DbContextOptions<ArzTiDbContext> options)
        : base(options)
    {
    }
    public string GetConnectionString()
    {
        return Database.GetDbConnection().ConnectionString;
    }

    public virtual DbSet<ErApotheke> ErApothekes { get; set; } = null!;
    public virtual DbSet<ErSecAccessFiverx> ErSecAccessFiverxes { get; set; } = null!;
    public virtual DbSet<ErSenderezepteEmuster16> ErSenderezepteEmuster16s { get; set; } = null!;
    public virtual DbSet<ErSenderezepteEmuster16Artikel> ErSenderezepteEmuster16Artikels { get; set; } = null!;
    public virtual DbSet<ErSenderezepteEmuster16Daten> ErSenderezepteEmuster16Datens { get; set; } = null!;
    public virtual DbSet<ErSenderezepteEmuster16Status> ErSenderezepteEmuster16Statuses { get; set; } = null!;
    public virtual DbSet<ErSenderezepteEmuster16Statusinfo> ErSenderezepteEmuster16Statusinfos { get; set; } = null!;
    public virtual DbSet<ErSenderezepteErezept> ErSenderezepteErezepts { get; set; } = null!;
    public virtual DbSet<ErSenderezepteErezeptDaten> ErSenderezepteErezeptDatens { get; set; } = null!;
    public virtual DbSet<ErSenderezepteErezeptStatus> ErSenderezepteErezeptStatuses { get; set; } = null!;
    public virtual DbSet<ErSenderezepteErezeptStatusinfo> ErSenderezepteErezeptStatusinfos { get; set; } = null!;
    public virtual DbSet<ErSenderezepteHeader> ErSenderezepteHeaders { get; set; } = null!;
    public virtual DbSet<ErSenderezepteHeaderDaten> ErSenderezepteHeaderDatens { get; set; } = null!;
    public virtual DbSet<ErSenderezeptePrezept> ErSenderezeptePrezepts { get; set; } = null!;
    public virtual DbSet<ErSenderezeptePrezeptDaten> ErSenderezeptePrezeptDatens { get; set; } = null!;
    public virtual DbSet<ErSenderezeptePrezeptPcharge> ErSenderezeptePrezeptPcharges { get; set; } = null!;
    public virtual DbSet<ErSenderezeptePrezeptPchargePwirkstoff> ErSenderezeptePrezeptPchargePwirkstoffs { get; set; } = null!;
    public virtual DbSet<ErSenderezeptePrezeptStatus> ErSenderezeptePrezeptStatuses { get; set; } = null!;
    public virtual DbSet<ErSenderezeptePrezeptStatusinfo> ErSenderezeptePrezeptStatusinfos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            _ = optionsBuilder.UseNpgsql("Username=postgres;Password=postgres;Server=localhost;Port=5432;Database=arz-gfal;");
        }
        //_ = optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<ErApotheke>(entity =>
        {
            _ = entity.HasKey(e => e.IdApotheke)
                .HasName("apotheke_pkey");

            _ = entity.ToTable("er_apotheke");

            _ = entity.HasIndex(e => e.ApoIkNr, "idx_er_apotheke_apo_ik_nr");

            _ = entity.HasIndex(e => e.ApoIntNr, "idx_er_apotheke_apo_int_nr");

            _ = entity.Property(e => e.IdApotheke).HasColumnName("id_apotheke");

            _ = entity.Property(e => e.AenDatum).HasColumnName("aen_datum");

            _ = entity.Property(e => e.AenIdSecUser).HasColumnName("aen_id_sec_user");

            _ = entity.Property(e => e.AenZeit).HasColumnName("aen_zeit");

            _ = entity.Property(e => e.ApoIkNr).HasColumnName("apo_ik_nr");

            _ = entity.Property(e => e.ApoIntNr).HasColumnName("apo_int_nr");

            _ = entity.Property(e => e.ApothekeName)
                .HasMaxLength(255)
                .HasColumnName("apotheke_name");

            _ = entity.Property(e => e.ApothekeNameZusatz)
                .HasMaxLength(255)
                .HasColumnName("apotheke_name_zusatz");

            _ = entity.Property(e => e.Bemerkung).HasColumnName("bemerkung");

            _ = entity.Property(e => e.Bundesland)
                .HasMaxLength(255)
                .HasColumnName("bundesland");

            _ = entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");

            _ = entity.Property(e => e.Fax)
                .HasMaxLength(20)
                .HasColumnName("fax");

            _ = entity.Property(e => e.Filialapotheke)
                .HasColumnName("filialapotheke")
                .HasDefaultValueSql("0");

            _ = entity.Property(e => e.Gesperrt)
                .HasColumnName("gesperrt")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.IdHauptapotheke)
                .HasColumnName("id_hauptapotheke")
                .HasDefaultValueSql("0")
                .HasComment("ID der Hauptapotheke wenn dies eine Filialapotheke ist");

            _ = entity.Property(e => e.IdHtAnrede)
                .HasColumnName("id_ht_anrede")
                .HasDefaultValueSql("0");

            _ = entity.Property(e => e.IdLeType)
                .HasColumnName("id_le_type")
                .HasDefaultValueSql("0");

            _ = entity.Property(e => e.InhaberNachname)
                .HasMaxLength(65)
                .HasColumnName("inhaber_nachname");

            _ = entity.Property(e => e.InhaberVorname)
                .HasMaxLength(65)
                .HasColumnName("inhaber_vorname");

            _ = entity.Property(e => e.MandantType)
                .HasMaxLength(4)
                .HasColumnName("mandant_type")
                .HasDefaultValueSql("'APO'::character varying")
                .HasComment("APO - Apotheke\r\nHRST - Hersteller (Dienstleister)");

            _ = entity.Property(e => e.Mobil)
                .HasMaxLength(20)
                .HasColumnName("mobil");

            _ = entity.Property(e => e.Ort)
                .HasMaxLength(45)
                .HasColumnName("ort");

            _ = entity.Property(e => e.Plz).HasColumnName("plz");

            _ = entity.Property(e => e.SecLogin)
                .HasColumnName("sec_login")
                .HasDefaultValueSql("0")
                .HasComment("Security-Login - 0-Keiner, 1-Feste-IP, 2-DynDNS URLs, ...");

            _ = entity.Property(e => e.SecLoginNurApoUser)
                .HasColumnName("sec_login_nur_apo_user")
                .HasDefaultValueSql("false")
                .HasComment("Seurity-Login nur für Apotheken-Benutzer - F-nein, T-ja");

            _ = entity.Property(e => e.SecLoginWerte)
                .HasMaxLength(580)
                .HasColumnName("sec_login_werte")
                .HasComment("Werte zum Security-Login");

            _ = entity.Property(e => e.Strasse)
                .HasMaxLength(45)
                .HasColumnName("strasse");

            _ = entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .HasColumnName("telefon");
        });

        _ = modelBuilder.Entity<ErSecAccessFiverx>(entity =>
        {
            _ = entity.HasKey(e => e.IdSecAccessFiverx)
                .HasName("er_sec_access_fiverx_pkey");

            _ = entity.ToTable("er_sec_access_fiverx");

            _ = entity.HasIndex(e => e.LoginId, "er_sec_access_fiverx_login_id_key")
                .IsUnique();

            _ = entity.HasIndex(e => e.LoginId, "idx_er_sec_access_fiverx_login_id")
                .IsUnique();

            _ = entity.Property(e => e.IdSecAccessFiverx).HasColumnName("id_sec_access_fiverx");

            _ = entity.Property(e => e.AenDatum).HasColumnName("aen_datum");

            _ = entity.Property(e => e.AenIdSecUser).HasColumnName("aen_id_sec_user");

            _ = entity.Property(e => e.AenZeit)
                .HasColumnType("time(0) without time zone")
                .HasColumnName("aen_zeit");

            _ = entity.Property(e => e.Em16Uc61Asyn)
                .HasColumnName("em16_uc61_asyn")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.Em16Uc61Syn)
                .HasColumnName("em16_uc61_syn")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.Em16Uc61U1)
                .HasColumnName("em16_uc61_u1")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.ErezUc4)
                .HasColumnName("erez_uc4")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.ErezUc51)
                .HasColumnName("erez_uc51")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.ErezUc52)
                .HasColumnName("erez_uc52")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.ErezUc62Asyn)
                .HasColumnName("erez_uc62_asyn")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.ErezUc62Syn)
                .HasColumnName("erez_uc62_syn")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.ErezUc7)
                .HasColumnName("erez_uc7")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.Freigegeben)
                .HasColumnName("freigegeben")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.IdApotheke).HasColumnName("id_apotheke");

            _ = entity.Property(e => e.LoginId)
                .HasMaxLength(47)
                .HasColumnName("login_id");

            _ = entity.Property(e => e.LoginPasswort)
                .HasMaxLength(50)
                .HasColumnName("login_passwort");

            _ = entity.Property(e => e.LoginPasswortCrypt)
                .HasMaxLength(255)
                .HasColumnName("login_passwort_crypt");

            _ = entity.HasOne(d => d.IdApothekeNavigation)
                .WithMany(p => p.ErSecAccessFiverxes)
                .HasForeignKey(d => d.IdApotheke)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("er_sec_access_fiverx_id_apotheke_fkey");
        });

        _ = modelBuilder.Entity<ErSenderezepteEmuster16>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteEmuster16)
                .HasName("er_senderezepte_emuster16_pkey");

            _ = entity.ToTable("er_senderezepte_emuster16");

            _ = entity.HasIndex(e => e.ApoIkNr, "idx_er_sendrez_apoik");

            _ = entity.HasIndex(e => e.IdSenderezepteHeader, "idx_er_sendrez_em16_id");

            _ = entity.HasIndex(e => new { e.Muster16Id, e.RzLieferId }, "idx_er_sendrez_em16_muster16_id");

            _ = entity.HasIndex(e => new { e.RzLieferId, e.Muster16Id }, "idx_er_sendrez_em16_rz_liefer_id");

            _ = entity.Property(e => e.IdSenderezepteEmuster16).HasColumnName("id_senderezepte_emuster16");

            _ = entity.Property(e => e.AbdaKassentyp)
                .HasMaxLength(5)
                .HasColumnName("abda_kassentyp")
                .HasComment("kArt");

            _ = entity.Property(e => e.Abgabedatum)
                .HasColumnName("abgabedatum")
                .HasComment("abDatum");

            _ = entity.Property(e => e.AbrechnungsPeriode)
                .HasMaxLength(10)
                .HasColumnName("abrechnungs_periode")
                .HasComment("aPeriode");

            _ = entity.Property(e => e.ApoIkNr).HasColumnName("apo_ik_nr");

            _ = entity.Property(e => e.Arbeitsplatz)
                .HasMaxLength(50)
                .HasColumnName("arbeitsplatz")
                .HasComment("arbPlatz");

            _ = entity.Property(e => e.ArztNr)
                .HasColumnName("arzt_nr")
                .HasComment("laNr - Lebenslange Arzt-Nummer");

            _ = entity.Property(e => e.AvsId)
                .HasMaxLength(20)
                .HasColumnName("avs_id");

            _ = entity.Property(e => e.Bediener)
                .HasMaxLength(50)
                .HasColumnName("bediener");

            _ = entity.Property(e => e.BegrPflicht)
                .HasColumnName("begr_pflicht")
                .HasDefaultValueSql("0")
                .HasComment("bgrPfl");

            _ = entity.Property(e => e.BetriebsstaettenNr)
                .HasColumnName("betriebsstaetten_nr")
                .HasComment("vrtrgsArztNr - Betriebsstättennummer oder Praxis-Nummer");

            _ = entity.Property(e => e.Eigenanteil)
                .HasPrecision(11, 2)
                .HasColumnName("eigenanteil")
                .HasDefaultValueSql("0")
                .HasComment("eigBet");

            _ = entity.Property(e => e.GesamtesBrutto)
                .HasPrecision(11, 2)
                .HasColumnName("gesamtes_brutto")
                .HasDefaultValueSql("0");

            _ = entity.Property(e => e.IdSenderezepteHeader).HasColumnName("id_senderezepte_header");

            _ = entity.Property(e => e.KkIkNr)
                .HasColumnName("kk_ik_nr")
                .HasComment("Krankenkassen-Ik der Versicherung");

            _ = entity.Property(e => e.KkName)
                .HasMaxLength(50)
                .HasColumnName("kk_name")
                .HasComment("kName");

            _ = entity.Property(e => e.KkTyp)
                .HasMaxLength(5)
                .HasColumnName("kk_typ")
                .HasComment("rTyp");

            _ = entity.Property(e => e.KzArbeitsunfall)
                .HasColumnName("kz_arbeitsunfall")
                .HasDefaultValueSql("0")
                .HasComment("aUnfall");

            _ = entity.Property(e => e.KzBvg)
                .HasColumnName("kz_bvg")
                .HasDefaultValueSql("0")
                .HasComment("bvg");

            _ = entity.Property(e => e.KzGebuehrenFrei)
                .HasColumnName("kz_gebuehren_frei")
                .HasDefaultValueSql("0")
                .HasComment("gebFrei");

            _ = entity.Property(e => e.KzHilfsmittel)
                .HasColumnName("kz_hilfsmittel")
                .HasDefaultValueSql("0")
                .HasComment("hilf");

            _ = entity.Property(e => e.KzImpfstoff)
                .HasColumnName("kz_impfstoff")
                .HasDefaultValueSql("0")
                .HasComment("impf");

            _ = entity.Property(e => e.KzNoctu)
                .HasColumnName("kz_noctu")
                .HasDefaultValueSql("0")
                .HasComment("noctu");

            _ = entity.Property(e => e.KzSonstige)
                .HasColumnName("kz_sonstige")
                .HasDefaultValueSql("0")
                .HasComment("sonstige");

            _ = entity.Property(e => e.KzSprechstundenbedarf)
                .HasColumnName("kz_sprechstundenbedarf")
                .HasDefaultValueSql("0")
                .HasComment("sprStBedarf");

            _ = entity.Property(e => e.KzUnfall)
                .HasColumnName("kz_unfall")
                .HasDefaultValueSql("0")
                .HasComment("unfall");

            _ = entity.Property(e => e.Muster16Id).HasColumnName("muster16_id");

            _ = entity.Property(e => e.RezeptTyp)
                .HasMaxLength(30)
                .HasColumnName("rezept_typ")
                .HasComment("Nur STANDARDREZEPT, BTM, SPRECHSTUNDENBEDARF");

            _ = entity.Property(e => e.RzDatum)
                .HasMaxLength(30)
                .HasColumnName("rz_datum");

            _ = entity.Property(e => e.RzLieferDatum).HasColumnName("rz_liefer_datum");

            _ = entity.Property(e => e.RzLieferId)
                .HasMaxLength(23)
                .HasColumnName("rz_liefer_id");

            _ = entity.Property(e => e.RzLieferZeit).HasColumnName("rz_liefer_zeit");

            _ = entity.Property(e => e.TFachinformation)
                .HasColumnName("t_fachinformation")
                .HasDefaultValueSql("0")
                .HasComment("tFachinformation");

            _ = entity.Property(e => e.TInLabel)
                .HasColumnName("t_in_label")
                .HasDefaultValueSql("0")
                .HasComment("tInLabel");

            _ = entity.Property(e => e.TOffLabel)
                .HasColumnName("t_off_label")
                .HasDefaultValueSql("0")
                .HasComment("tOffLabel");

            _ = entity.Property(e => e.TSicherheitsbestimmung)
                .HasColumnName("t_sicherheitsbestimmung")
                .HasDefaultValueSql("0")
                .HasComment("tSicherheitsbestimmung");

            _ = entity.Property(e => e.UnfallDatum)
                .HasColumnName("unfall_datum")
                .HasComment("unfallTag");

            _ = entity.Property(e => e.Verordnungsdatum)
                .HasColumnName("verordnungsdatum")
                .HasComment("verDat - Ausstellungsdatum - Verordnungsdatum");

            _ = entity.Property(e => e.VersichertenGeburtsdatum)
                .HasColumnName("versicherten_geburtsdatum")
                .HasComment("vGeb");

            _ = entity.Property(e => e.VersichertenName)
                .HasMaxLength(50)
                .HasColumnName("versicherten_name")
                .HasComment("vName");

            _ = entity.Property(e => e.VersichertenNummer)
                .HasMaxLength(12)
                .HasColumnName("versicherten_nummer")
                .HasComment("vrsNr");

            _ = entity.Property(e => e.VersichertenOrt)
                .HasMaxLength(50)
                .HasColumnName("versicherten_ort")
                .HasComment("vOrt");

            _ = entity.Property(e => e.VersichertenPlz)
                .HasMaxLength(5)
                .HasColumnName("versicherten_plz")
                .HasComment("vPlz");

            _ = entity.Property(e => e.VersichertenStatus)
                .HasMaxLength(7)
                .HasColumnName("versicherten_status")
                .HasComment("vStat");

            _ = entity.Property(e => e.VersichertenStrasse)
                .HasMaxLength(50)
                .HasColumnName("versicherten_strasse")
                .HasComment("vStr");

            _ = entity.Property(e => e.VersichertenTitel)
                .HasMaxLength(20)
                .HasColumnName("versicherten_titel")
                .HasComment("vTitel");

            _ = entity.Property(e => e.VersichertenVorname)
                .HasMaxLength(50)
                .HasColumnName("versicherten_vorname")
                .HasComment("vVorname");

            _ = entity.Property(e => e.VersichertenkarteGueltigBis)
                .HasMaxLength(10)
                .HasColumnName("versichertenkarte_gueltig_bis")
                .HasComment("vkGueltigBis");

            _ = entity.Property(e => e.Zuzahlung)
                .HasPrecision(11, 2)
                .HasColumnName("zuzahlung")
                .HasDefaultValueSql("0");

            _ = entity.HasOne(d => d.IdSenderezepteHeaderNavigation)
                .WithMany(p => p.ErSenderezepteEmuster16s)
                .HasForeignKey(d => d.IdSenderezepteHeader)
                .HasConstraintName("er_senderezepte_emuster16_id_senderezepte_header");
        });

        _ = modelBuilder.Entity<ErSenderezepteEmuster16Artikel>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteEmuster16Artikel)
                .HasName("er_senderezepte_emuster16_artikel_pkey");

            _ = entity.ToTable("er_senderezepte_emuster16_artikel");

            _ = entity.HasIndex(e => e.IdSenderezepteEmuster16, "idx_er_sendrez_em16_artikel_id");

            _ = entity.Property(e => e.IdSenderezepteEmuster16Artikel)
                .HasColumnName("id_senderezepte_emuster16_artikel")
                .HasDefaultValueSql("nextval('er_senderezepte_emuster16_art_id_senderezepte_emuster16_art_seq'::regclass)");

            _ = entity.Property(e => e.ArtikelAutidem).HasColumnName("artikel_autidem");

            _ = entity.Property(e => e.ArtikelFaktor)
                .HasPrecision(13, 6)
                .HasColumnName("artikel_faktor");

            _ = entity.Property(e => e.ArtikelMediName)
                .HasMaxLength(130)
                .HasColumnName("artikel_medi_name");

            _ = entity.Property(e => e.ArtikelTaxe)
                .HasPrecision(11, 2)
                .HasColumnName("artikel_taxe")
                .HasDefaultValueSql("0");

            _ = entity.Property(e => e.ArzneiPzn).HasColumnName("arznei_pzn");

            _ = entity.Property(e => e.AtrikelNr).HasColumnName("atrikel_nr");

            _ = entity.Property(e => e.HilfsmittelNr).HasColumnName("hilfsmittel_nr");

            _ = entity.Property(e => e.IdSenderezepteEmuster16).HasColumnName("id_senderezepte_emuster16");

            _ = entity.HasOne(d => d.IdSenderezepteEmuster16Navigation)
                .WithMany(p => p.ErSenderezepteEmuster16Artikels)
                .HasForeignKey(d => d.IdSenderezepteEmuster16)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_emuster16_artike_id_senderezepte_emuster16_fkey");
        });

        _ = modelBuilder.Entity<ErSenderezepteEmuster16Daten>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteEmuster16Daten)
                .HasName("er_senderezepte_emuster16_daten_pkey");

            _ = entity.ToTable("er_senderezepte_emuster16_daten");

            _ = entity.HasIndex(e => e.RezeptUuid, "er_senderezepte_emuster16_daten_rezept_uuid_idx")
                .IsUnique();

            _ = entity.HasIndex(e => e.IdSenderezepteEmuster16, "idx_er_sendrez_em16_daten_id");

            _ = entity.Property(e => e.IdSenderezepteEmuster16Daten)
                .HasColumnName("id_senderezepte_emuster16_daten")
                .HasDefaultValueSql("nextval('er_senderezepte_emuster16_dat_id_senderezepte_emuster16_dat_seq'::regclass)");

            _ = entity.Property(e => e.IdSenderezepteEmuster16).HasColumnName("id_senderezepte_emuster16");

            _ = entity.Property(e => e.RezeptUuid)
                .HasMaxLength(40)
                .HasColumnName("rezept_uuid")
                .HasComment("Eindeutiger Rezept - Universal Unique Identifier (UUID)");

            _ = entity.Property(e => e.TransferArz)
                .HasColumnName("transfer_arz")
                .HasDefaultValueSql("false")
                .HasComment("Bestätigung vom ARZ - Transfer der Rezeptdaten erfolgt");

            _ = entity.Property(e => e.XmlRequest)
                .HasColumnName("xml_request")
                .HasComment("Original XML-Request der Apotheke (nur Rezept-XML) - zur Weiterleitung ins ARZ");

            _ = entity.HasOne(d => d.IdSenderezepteEmuster16Navigation)
                .WithMany(p => p.ErSenderezepteEmuster16Datens)
                .HasForeignKey(d => d.IdSenderezepteEmuster16)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_emuster16_daten_id_senderezepte_emuster16");
        });

        _ = modelBuilder.Entity<ErSenderezepteEmuster16Status>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteEmuster16Status)
                .HasName("er_senderezepte_emuster16_status_pkey");

            _ = entity.ToTable("er_senderezepte_emuster16_status");

            _ = entity.HasIndex(e => new { e.RezeptCheck, e.RezeptStatus, e.IdSenderezepteEmuster16 }, "idx_er_sendrez_em16_rezcheck_rezstatus_idsendrez");

            _ = entity.HasIndex(e => new { e.IdSenderezepteEmuster16, e.Muster16Id, e.RezeptStatus }, "idx_er_sendrez_em16_stat_idrezem16");

            _ = entity.Property(e => e.IdSenderezepteEmuster16Status)
                .HasColumnName("id_senderezepte_emuster16_status")
                .HasDefaultValueSql("nextval('er_senderezepte_emuster16_sta_id_senderezepte_emuster16_sta_seq'::regclass)");

            _ = entity.Property(e => e.AbrechenStatus)
                .HasMaxLength(20)
                .HasColumnName("abrechen_status")
                .HasDefaultValueSql("'VOR_PRUEFUNG'::character varying")
                .HasComment("Interner Status - enthält immer das Prüfergebnis");

            _ = entity.Property(e => e.IdSenderezepteEmuster16).HasColumnName("id_senderezepte_emuster16");

            _ = entity.Property(e => e.Muster16Id).HasColumnName("muster16_id");

            _ = entity.Property(e => e.RezeptCheck)
                .HasColumnName("rezept_check")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.RezeptStatus)
                .HasMaxLength(20)
                .HasColumnName("rezept_status")
                .HasDefaultValueSql("'VOR_PRUEFUNG'::character varying");

            _ = entity.Property(e => e.RzAbfrageId)
                .HasMaxLength(20)
                .HasColumnName("rz_abfrage_id")
                .HasComment("ID mit der alle DS gekennzeichnet sind die innerhalb einer RCS Abfrage gesendet wurden");

            _ = entity.Property(e => e.StatusAbfrageDatum)
                .HasColumnName("status_abfrage_datum")
                .HasComment("Abfragedatum des Status zum Rezept");

            _ = entity.Property(e => e.StatusAbfrageZeit)
                .HasColumnName("status_abfrage_zeit")
                .HasComment("Abfragezeit des Status zum Rezept");

            _ = entity.HasOne(d => d.IdSenderezepteEmuster16Navigation)
                .WithMany(p => p.ErSenderezepteEmuster16Statuses)
                .HasForeignKey(d => d.IdSenderezepteEmuster16)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_emuster16_status_id_senderezepte_emuster16");
        });

        _ = modelBuilder.Entity<ErSenderezepteEmuster16Statusinfo>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteEmuster16Statusinfo)
                .HasName("er_senderezepte_emuster16_statusinfo_pkey");

            _ = entity.ToTable("er_senderezepte_emuster16_statusinfo");

            _ = entity.HasIndex(e => e.IdSenderezepteEmuster16, "idx_er_sendrez_em16_stinfo_id");

            _ = entity.Property(e => e.IdSenderezepteEmuster16Statusinfo)
                .HasColumnName("id_senderezepte_emuster16_statusinfo")
                .HasDefaultValueSql("nextval('er_senderezepte_emuster16_sta_id_senderezepte_emuster16_st_seq1'::regclass)");

            _ = entity.Property(e => e.Fcode)
                .HasMaxLength(4)
                .HasColumnName("fcode");

            _ = entity.Property(e => e.Fhauptfehler)
                .HasColumnName("fhauptfehler")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.Fkommentar).HasColumnName("fkommentar");

            _ = entity.Property(e => e.Fkurztext).HasColumnName("fkurztext");

            _ = entity.Property(e => e.Fstatus)
                .HasMaxLength(20)
                .HasColumnName("fstatus");

            _ = entity.Property(e => e.Ftcode)
                .HasMaxLength(3)
                .HasColumnName("ftcode");

            _ = entity.Property(e => e.IdSenderezepteEmuster16).HasColumnName("id_senderezepte_emuster16");

            _ = entity.Property(e => e.Posnr).HasColumnName("posnr");

            _ = entity.Property(e => e.RegelNr)
                .HasMaxLength(10)
                .HasColumnName("regel_nr");

            _ = entity.Property(e => e.RegelTrefferCode)
                .HasMaxLength(7)
                .HasColumnName("regel_treffer_code");

            _ = entity.HasOne(d => d.IdSenderezepteEmuster16Navigation)
                .WithMany(p => p.ErSenderezepteEmuster16Statusinfos)
                .HasForeignKey(d => d.IdSenderezepteEmuster16)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_emuster16_statusinfo_id_senderezepte_emuster16");
        });

        _ = modelBuilder.Entity<ErSenderezepteErezept>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteErezept)
                .HasName("er_senderezepte_erezept_pkey");

            _ = entity.ToTable("er_senderezepte_erezept");

            _ = entity.HasIndex(e => e.IdSenderezepteHeader, "idx_er_sendrez_erez_id");

            _ = entity.Property(e => e.IdSenderezepteErezept).HasColumnName("id_senderezepte_erezept");

            _ = entity.Property(e => e.AbrechnungsPeriode)
                .HasMaxLength(10)
                .HasColumnName("abrechnungs_periode");

            _ = entity.Property(e => e.ApoIkNr).HasColumnName("apo_ik_nr");

            _ = entity.Property(e => e.AvsId)
                .HasMaxLength(20)
                .HasColumnName("avs_id");

            _ = entity.Property(e => e.ErezeptData)
                .HasColumnName("erezept_data")
                .HasComment("Base64 Codierung der eRezept Daten");

            _ = entity.Property(e => e.ErezeptEabgabeData)
                .HasColumnName("erezept_eabgabe_data")
                .HasComment("FHIR-Bundle der eAbgabe");

            _ = entity.Property(e => e.ErezeptEverordnungData)
                .HasColumnName("erezept_everordnung_data")
                .HasComment("FHIR-Bundle der eVerordnung");

            _ = entity.Property(e => e.ErezeptId)
                .HasMaxLength(255)
                .HasColumnName("erezept_id");

            _ = entity.Property(e => e.ErezeptQuittungData)
                .HasColumnName("erezept_quittung_data")
                .HasComment("FHIR-Bundle der Quittung");

            _ = entity.Property(e => e.IdSenderezepteHeader).HasColumnName("id_senderezepte_header");

            _ = entity.Property(e => e.RzDatum)
                .HasMaxLength(30)
                .HasColumnName("rz_datum");

            _ = entity.Property(e => e.RzLieferDatum).HasColumnName("rz_liefer_datum");

            _ = entity.Property(e => e.RzLieferId)
                .HasMaxLength(23)
                .HasColumnName("rz_liefer_id");

            _ = entity.Property(e => e.RzLieferZeit).HasColumnName("rz_liefer_zeit");

            _ = entity.HasOne(d => d.IdSenderezepteHeaderNavigation)
                .WithMany(p => p.ErSenderezepteErezepts)
                .HasForeignKey(d => d.IdSenderezepteHeader)
                .HasConstraintName("er_senderezepte_erezept_id_senderezepte_header");
        });

        _ = modelBuilder.Entity<ErSenderezepteErezeptDaten>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteErezeptDaten)
                .HasName("er_senderezepte_erezept_daten_pkey");

            _ = entity.ToTable("er_senderezepte_erezept_daten");

            _ = entity.HasIndex(e => e.RezeptUuid, "er_senderezepte_erezept_daten_rezept_uuid_idx")
                .IsUnique();

            _ = entity.HasIndex(e => e.IdSenderezepteErezept, "idx_er_sendrez_erez_daten_id");

            _ = entity.Property(e => e.IdSenderezepteErezeptDaten).HasColumnName("id_senderezepte_erezept_daten");

            _ = entity.Property(e => e.IdSenderezepteErezept).HasColumnName("id_senderezepte_erezept");

            _ = entity.Property(e => e.RezeptUuid)
                .HasMaxLength(40)
                .HasColumnName("rezept_uuid")
                .HasComment("Eindeutiger Rezept - Universal Unique Identifier (UUID)");

            _ = entity.Property(e => e.TransferArz)
                .HasColumnName("transfer_arz")
                .HasDefaultValueSql("false")
                .HasComment("Bestätigung vom ARZ - Transfer der Rezeptdaten erfolgt");

            _ = entity.Property(e => e.XmlRequest)
                .HasColumnName("xml_request")
                .HasComment("Original XML-Request der Apotheke (nur Rezept-XML) - zur Weiterleitung ins ARZ");

            _ = entity.HasOne(d => d.IdSenderezepteErezeptNavigation)
                .WithMany(p => p.ErSenderezepteErezeptDatens)
                .HasForeignKey(d => d.IdSenderezepteErezept)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_erezept_daten_id_senderezepte_erezept");
        });

        _ = modelBuilder.Entity<ErSenderezepteErezeptStatus>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteErezeptStatus)
                .HasName("er_senderezepte_erezept_status_pkey");

            _ = entity.ToTable("er_senderezepte_erezept_status");

            _ = entity.HasIndex(e => new { e.IdSenderezepteErezept, e.ErezeptId, e.RezeptStatus }, "idx_er_sendrez_erez_stat_idrezerez");

            _ = entity.Property(e => e.IdSenderezepteErezeptStatus)
                .HasColumnName("id_senderezepte_erezept_status")
                .HasDefaultValueSql("nextval('er_senderezepte_erezept_statu_id_senderezepte_erezept_statu_seq'::regclass)");

            _ = entity.Property(e => e.AbrechenStatus)
                .HasMaxLength(20)
                .HasColumnName("abrechen_status")
                .HasDefaultValueSql("'VOR_PRUEFUNG'::character varying")
                .HasComment("Interner Status - enthält immer das Prüfergebnis");

            _ = entity.Property(e => e.ErezeptId)
                .HasMaxLength(22)
                .HasColumnName("erezept_id");

            _ = entity.Property(e => e.IdSenderezepteErezept).HasColumnName("id_senderezepte_erezept");

            _ = entity.Property(e => e.RezeptCheck)
                .HasColumnName("rezept_check")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.RezeptCheckLevel)
                .HasColumnName("rezept_check_level")
                .HasComment("Prüf-Level: 0-nicht geprüft, 1-Import, 2-Validiert, 3-Inhalt, 4-Logik");

            _ = entity.Property(e => e.RezeptStatus)
                .HasMaxLength(20)
                .HasColumnName("rezept_status")
                .HasDefaultValueSql("'VOR_PRUEFUNG'::character varying");

            _ = entity.Property(e => e.RzAbfrageId)
                .HasMaxLength(20)
                .HasColumnName("rz_abfrage_id")
                .HasComment("ID mit der alle DS gekennzeichnet sind die innerhalb einer RCS Abfrage gesendet wurden");

            _ = entity.Property(e => e.StatusAbfrageDatum)
                .HasColumnName("status_abfrage_datum")
                .HasComment("Abfragedatum des Status zum Rezept");

            _ = entity.Property(e => e.StatusAbfrageZeit)
                .HasColumnName("status_abfrage_zeit")
                .HasComment("Abfragezeit des Status zum Rezept");

            _ = entity.HasOne(d => d.IdSenderezepteErezeptNavigation)
                .WithMany(p => p.ErSenderezepteErezeptStatuses)
                .HasForeignKey(d => d.IdSenderezepteErezept)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_erezept_status_id_senderezepte_erezept");
        });

        _ = modelBuilder.Entity<ErSenderezepteErezeptStatusinfo>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteErezeptStatusinfo)
                .HasName("er_senderezepte_erezept_statusinfo_pkey");

            _ = entity.ToTable("er_senderezepte_erezept_statusinfo");

            _ = entity.HasIndex(e => e.IdSenderezepteErezept, "idx_er_sendrez_erez_stinfo_id");

            _ = entity.Property(e => e.IdSenderezepteErezeptStatusinfo)
                .HasColumnName("id_senderezepte_erezept_statusinfo")
                .HasDefaultValueSql("nextval('er_senderezepte_erezept_statu_id_senderezepte_erezept_stat_seq1'::regclass)");

            _ = entity.Property(e => e.CheckLevel)
                .HasColumnName("check_level")
                .HasComment("Prüf-Level: 0-nicht geprüft, 10-Import, 20-Validiert, 30-Inhalt, 40-Logik");

            _ = entity.Property(e => e.CheckStatus)
                .HasColumnName("check_status")
                .HasComment("Zugehöriger Integer Wert zum fStatus - dient zur schnellen Sortierung");

            _ = entity.Property(e => e.Fcode)
                .HasMaxLength(4)
                .HasColumnName("fcode");

            _ = entity.Property(e => e.Fhauptfehler)
                .HasColumnName("fhauptfehler")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.Fkommentar).HasColumnName("fkommentar");

            _ = entity.Property(e => e.Fkurztext).HasColumnName("fkurztext");

            _ = entity.Property(e => e.Fstatus)
                .HasMaxLength(20)
                .HasColumnName("fstatus");

            _ = entity.Property(e => e.Ftcode)
                .HasMaxLength(3)
                .HasColumnName("ftcode");

            _ = entity.Property(e => e.IdSenderezepteErezept).HasColumnName("id_senderezepte_erezept");

            _ = entity.Property(e => e.Posnr).HasColumnName("posnr");

            _ = entity.Property(e => e.RegelNr)
                .HasMaxLength(10)
                .HasColumnName("regel_nr");

            _ = entity.Property(e => e.RegelTrefferCode)
                .HasMaxLength(7)
                .HasColumnName("regel_treffer_code");

            _ = entity.HasOne(d => d.IdSenderezepteErezeptNavigation)
                .WithMany(p => p.ErSenderezepteErezeptStatusinfos)
                .HasForeignKey(d => d.IdSenderezepteErezept)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_erezept_statusinfo_id_senderezepte_erezept");
        });

        _ = modelBuilder.Entity<ErSenderezepteHeader>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteHeader)
                .HasName("er_senderezepte_header_pkey");

            _ = entity.ToTable("er_senderezepte_header");

            _ = entity.HasIndex(e => new { e.ApoIntNr, e.RzLieferId }, "idx_er_sendrez_head_apo_int_nr");

            _ = entity.HasIndex(e => new { e.RzLieferId, e.ApoIntNr }, "idx_er_sendrez_head_rz_liefer_id");

            _ = entity.Property(e => e.IdSenderezepteHeader).HasColumnName("id_senderezepte_header");

            _ = entity.Property(e => e.AbfrageDatum)
                .HasColumnName("abfrage_datum")
                .HasComment("Abfragedatum des Status zur Liefer-ID");

            _ = entity.Property(e => e.AbfrageZeit)
                .HasColumnName("abfrage_zeit")
                .HasComment("Abfragezeit des Status zur Liefer-ID");

            _ = entity.Property(e => e.ApoIkNr)
                .HasColumnName("apo_ik_nr")
                .HasDefaultValueSql("0");

            _ = entity.Property(e => e.ApoIntNr)
                .HasMaxLength(47)
                .HasColumnName("apo_int_nr");

            _ = entity.Property(e => e.ApoPassword)
                .HasMaxLength(50)
                .HasColumnName("apo_password");

            _ = entity.Property(e => e.AvsSendeId).HasColumnName("avs_sende_id");

            _ = entity.Property(e => e.CheckDatum)
                .HasColumnName("check_datum")
                .HasComment("Datum des letzten Checks der Rezepte dieser LieferID");

            _ = entity.Property(e => e.CheckEm16)
                .HasColumnName("check_em16")
                .HasDefaultValueSql("false")
                .HasComment("eMuster16 Rezepte wurden überprüft");

            _ = entity.Property(e => e.CheckErez)
                .HasColumnName("check_erez")
                .HasDefaultValueSql("false")
                .HasComment("eRezepte wurden überprüft");

            _ = entity.Property(e => e.CheckPrez)
                .HasColumnName("check_prez")
                .HasDefaultValueSql("false")
                .HasComment("pRezepte wurden überprüft");

            _ = entity.Property(e => e.CheckZeit)
                .HasColumnName("check_zeit")
                .HasComment("Zeit des letzten Checks der Rezepte dieser LieferID");

            _ = entity.Property(e => e.ImportTyp)
                .HasColumnName("import_typ")
                .HasDefaultValueSql("0")
                .HasComment("Art wie das Rezept importiert wurde - 0-WebService, 1-Formular, 2-Datei");

            _ = entity.Property(e => e.RezTyp)
                .HasMaxLength(5)
                .HasColumnName("rez_typ")
                .HasDefaultValueSql("'X'::character varying")
                .HasComment("eRez->ERezept, pRez->pRezept, eM16->eMuster16 Rezept, X->Unbekannt");

            _ = entity.Property(e => e.RezeptCheck)
                .HasColumnName("rezept_check")
                .HasDefaultValueSql("false")
                .HasComment("Rezeptprüfung durchgeführt TRUE|FALSE");

            _ = entity.Property(e => e.RezeptStatus)
                .HasMaxLength(20)
                .HasColumnName("rezept_status")
                .HasDefaultValueSql("'VOR_PRUEFUNG'::character varying");

            _ = entity.Property(e => e.ReztypEm16)
                .HasColumnName("reztyp_em16")
                .HasDefaultValueSql("false")
                .HasComment("Rezept-Typ eMuster16");

            _ = entity.Property(e => e.ReztypErez)
                .HasColumnName("reztyp_erez")
                .HasDefaultValueSql("false")
                .HasComment("Rezept-Typ eRezept");

            _ = entity.Property(e => e.ReztypPrez)
                .HasColumnName("reztyp_prez")
                .HasDefaultValueSql("false")
                .HasComment("Rezept-Typ pRezept");

            _ = entity.Property(e => e.RzLieferDatum).HasColumnName("rz_liefer_datum");

            _ = entity.Property(e => e.RzLieferId)
                .HasMaxLength(23)
                .HasColumnName("rz_liefer_id");

            _ = entity.Property(e => e.RzLieferZeit).HasColumnName("rz_liefer_zeit");

            _ = entity.Property(e => e.SoftwareHersteller)
                .HasMaxLength(200)
                .HasColumnName("software_hersteller");

            _ = entity.Property(e => e.SoftwareName)
                .HasMaxLength(200)
                .HasColumnName("software_name");

            _ = entity.Property(e => e.SoftwareVersion)
                .HasMaxLength(20)
                .HasColumnName("software_version");

            _ = entity.Property(e => e.Testdaten)
                .HasColumnName("testdaten")
                .HasDefaultValueSql("false");
        });

        _ = modelBuilder.Entity<ErSenderezepteHeaderDaten>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezepteHeaderDaten)
                .HasName("er_senderezepte_header_daten_pkey");

            _ = entity.ToTable("er_senderezepte_header_daten");

            _ = entity.HasIndex(e => e.IdSenderezepteHeader, "idx_er_sendrez_header_daten_id");

            _ = entity.Property(e => e.IdSenderezepteHeaderDaten).HasColumnName("id_senderezepte_header_daten");

            _ = entity.Property(e => e.HeaderUuid)
                .HasMaxLength(40)
                .HasColumnName("header_uuid")
                .HasComment("Eindeutige Header - Universal Unique Identifier (UUID)");

            _ = entity.Property(e => e.IdSenderezepteHeader).HasColumnName("id_senderezepte_header");

            _ = entity.Property(e => e.TransferArz)
                .HasColumnName("transfer_arz")
                .HasDefaultValueSql("false")
                .HasComment("Bestätigung vom ARZ - Transfer der Rezeptdaten erfolgt");

            _ = entity.Property(e => e.XmlRequest)
                .HasColumnName("xml_request")
                .HasComment("Kompletter Original XML-Request der Apotheke - zur Weiterleitung ins ARZ");

            _ = entity.HasOne(d => d.IdSenderezepteHeaderNavigation)
                .WithMany(p => p.ErSenderezepteHeaderDatens)
                .HasForeignKey(d => d.IdSenderezepteHeader)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_header_daten_id_senderezepte_header");
        });

        _ = modelBuilder.Entity<ErSenderezeptePrezept>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezeptePrezept)
                .HasName("er_senderezepte_prezept_pkey");

            _ = entity.ToTable("er_senderezepte_prezept");

            _ = entity.HasIndex(e => new { e.RzLieferId, e.TransaktionsNummer }, "idx_er_sendrez_prez_rz_liefer_id");

            _ = entity.HasIndex(e => new { e.TransaktionsNummer, e.RzLieferId }, "idx_er_sendrez_prez_transaktionsnummer");

            _ = entity.HasIndex(e => e.IdSenderezepteHeader, "idx_er_sendrez_prezid");

            _ = entity.Property(e => e.IdSenderezeptePrezept).HasColumnName("id_senderezepte_prezept");

            _ = entity.Property(e => e.AbdaKassentyp)
                .HasMaxLength(5)
                .HasColumnName("abda_kassentyp")
                .HasComment("kArt");

            _ = entity.Property(e => e.Abgabedatum)
                .HasColumnName("abgabedatum")
                .HasComment("abDatum");

            _ = entity.Property(e => e.AbrechnungsPeriode)
                .HasMaxLength(10)
                .HasColumnName("abrechnungs_periode")
                .HasComment("aPeriode");

            _ = entity.Property(e => e.ApoIkNr)
                .HasColumnName("apo_ik_nr")
                .HasComment("apoIk");

            _ = entity.Property(e => e.Arbeitsplatz)
                .HasMaxLength(50)
                .HasColumnName("arbeitsplatz")
                .HasComment("arbPlatz");

            _ = entity.Property(e => e.ArztNr)
                .HasColumnName("arzt_nr")
                .HasComment("laNr - Lebenslange Arzt-Nummer");

            _ = entity.Property(e => e.AvsId)
                .HasMaxLength(20)
                .HasColumnName("avs_id");

            _ = entity.Property(e => e.Bediener)
                .HasMaxLength(50)
                .HasColumnName("bediener");

            _ = entity.Property(e => e.BetriebsstaettenNr)
                .HasColumnName("betriebsstaetten_nr")
                .HasComment("vrtrgsArztNr - Betriebsstättennummer oder Praxis-Nummer");

            _ = entity.Property(e => e.ErstellungsZeitpunkt)
                .HasMaxLength(30)
                .HasColumnName("erstellungs_zeitpunkt")
                .HasComment("erstellungsZeitpunkt");

            _ = entity.Property(e => e.GesamtesBrutto)
                .HasPrecision(11, 2)
                .HasColumnName("gesamtes_brutto")
                .HasDefaultValueSql("0");

            _ = entity.Property(e => e.HashCode)
                .HasMaxLength(40)
                .HasColumnName("hash_code")
                .HasComment("hashCode");

            _ = entity.Property(e => e.IdSenderezepteHeader).HasColumnName("id_senderezepte_header");

            _ = entity.Property(e => e.KkIkNr)
                .HasColumnName("kk_ik_nr")
                .HasComment("kkIk Krankenkassen-Ik der Versicherung");

            _ = entity.Property(e => e.KkName)
                .HasMaxLength(50)
                .HasColumnName("kk_name")
                .HasComment("kName");

            _ = entity.Property(e => e.KkTyp)
                .HasMaxLength(5)
                .HasColumnName("kk_typ")
                .HasComment("rTyp");

            _ = entity.Property(e => e.KzBvg)
                .HasColumnName("kz_bvg")
                .HasDefaultValueSql("0")
                .HasComment("bvg");

            _ = entity.Property(e => e.KzGebuehrenFrei)
                .HasColumnName("kz_gebuehren_frei")
                .HasDefaultValueSql("0")
                .HasComment("gebFrei");

            _ = entity.Property(e => e.KzNoctu)
                .HasColumnName("kz_noctu")
                .HasDefaultValueSql("0")
                .HasComment("noctu");

            _ = entity.Property(e => e.KzSonstige)
                .HasColumnName("kz_sonstige")
                .HasDefaultValueSql("0")
                .HasComment("sonstige");

            _ = entity.Property(e => e.KzSprechstundenbedarf)
                .HasColumnName("kz_sprechstundenbedarf")
                .HasDefaultValueSql("0")
                .HasComment("sprStBedarf");

            _ = entity.Property(e => e.Ppos1Faktor)
                .HasColumnName("ppos1_faktor")
                .HasComment("pPosition1->faktor");

            _ = entity.Property(e => e.Ppos1Pzn)
                .HasColumnName("ppos1_pzn")
                .HasComment("pPosition1->pzn");

            _ = entity.Property(e => e.Ppos1Taxe)
                .HasPrecision(11, 2)
                .HasColumnName("ppos1_taxe")
                .HasDefaultValueSql("0")
                .HasComment("pPosition1->taxe");

            _ = entity.Property(e => e.RezeptTyp)
                .HasMaxLength(30)
                .HasColumnName("rezept_typ")
                .HasComment("Nur STANDARDREZEPT, BTM, SPRECHSTUNDENBEDARF");

            _ = entity.Property(e => e.RzDatum)
                .HasMaxLength(30)
                .HasColumnName("rz_datum");

            _ = entity.Property(e => e.RzLieferDatum).HasColumnName("rz_liefer_datum");

            _ = entity.Property(e => e.RzLieferId)
                .HasMaxLength(23)
                .HasColumnName("rz_liefer_id");

            _ = entity.Property(e => e.RzLieferZeit).HasColumnName("rz_liefer_zeit");

            _ = entity.Property(e => e.TransaktionsNummer)
                .HasColumnName("transaktions_nummer")
                .HasComment("transaktionsNummer");

            _ = entity.Property(e => e.Verordnungsdatum)
                .HasColumnName("verordnungsdatum")
                .HasComment("verDat - Ausstellungsdatum - Verordnungsdatum");

            _ = entity.Property(e => e.VersichertenGeburtsdatum)
                .HasColumnName("versicherten_geburtsdatum")
                .HasComment("vGeb");

            _ = entity.Property(e => e.VersichertenNummer)
                .HasMaxLength(12)
                .HasColumnName("versicherten_nummer")
                .HasComment("vrsNr");

            _ = entity.Property(e => e.VersichertenStatus)
                .HasMaxLength(7)
                .HasColumnName("versicherten_status")
                .HasComment("vStat");

            _ = entity.Property(e => e.VersichertenkarteGueltigBis)
                .HasMaxLength(10)
                .HasColumnName("versichertenkarte_gueltig_bis")
                .HasComment("vkGueltigBis");

            _ = entity.Property(e => e.Zuzahlung)
                .HasPrecision(11, 2)
                .HasColumnName("zuzahlung")
                .HasDefaultValueSql("0");

            _ = entity.HasOne(d => d.IdSenderezepteHeaderNavigation)
                .WithMany(p => p.ErSenderezeptePrezepts)
                .HasForeignKey(d => d.IdSenderezepteHeader)
                .HasConstraintName("er_senderezepte_prezept_id_senderezepte_header");
        });

        _ = modelBuilder.Entity<ErSenderezeptePrezeptDaten>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezeptePrezeptDaten)
                .HasName("er_senderezepte_prezept_daten_pkey");

            _ = entity.ToTable("er_senderezepte_prezept_daten");

            _ = entity.HasIndex(e => e.RezeptUuid, "er_senderezepte_prezept_daten_rezept_uuid_idx")
                .IsUnique();

            _ = entity.HasIndex(e => e.IdSenderezeptePrezept, "idx_er_sendrez_prez_daten_id");

            _ = entity.Property(e => e.IdSenderezeptePrezeptDaten).HasColumnName("id_senderezepte_prezept_daten");

            _ = entity.Property(e => e.IdSenderezeptePrezept).HasColumnName("id_senderezepte_prezept");

            _ = entity.Property(e => e.RezeptUuid)
                .HasMaxLength(40)
                .HasColumnName("rezept_uuid")
                .HasComment("Eindeutiger Rezept - Universal Unique Identifier (UUID)");

            _ = entity.Property(e => e.TransferArz)
                .HasColumnName("transfer_arz")
                .HasDefaultValueSql("false")
                .HasComment("Bestätigung vom ARZ - Transfer der Rezeptdaten erfolgt");

            _ = entity.Property(e => e.XmlRequest)
                .HasColumnName("xml_request")
                .HasComment("Original XML-Request der Apotheke (nur Rezept-XML) - zur Weiterleitung ins ARZ");

            _ = entity.HasOne(d => d.IdSenderezeptePrezeptNavigation)
                .WithMany(p => p.ErSenderezeptePrezeptDatens)
                .HasForeignKey(d => d.IdSenderezeptePrezept)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_prezept_daten_id_senderezepte_prezept");
        });

        _ = modelBuilder.Entity<ErSenderezeptePrezeptPcharge>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezeptePrezeptPcharge)
                .HasName("id_senderezepte_prez_pcharge_pkey");

            _ = entity.ToTable("er_senderezepte_prezept_pcharge");

            _ = entity.HasIndex(e => e.IdSenderezeptePrezept, "idx_er_sendrez_prez_pch_prezid");

            _ = entity.Property(e => e.IdSenderezeptePrezeptPcharge)
                .HasColumnName("id_senderezepte_prezept_pcharge")
                .HasDefaultValueSql("nextval('er_senderezepte_prezept_pchar_id_senderezepte_prezept_pchar_seq'::regclass)");

            _ = entity.Property(e => e.AnzahlApplikationen)
                .HasColumnName("anzahl_applikationen")
                .HasComment("anzahlApplikationen - Werte 1-99");

            _ = entity.Property(e => e.ChargenNr)
                .HasColumnName("chargen_nr")
                .HasComment("chargenNr - Werte 1-99");

            _ = entity.Property(e => e.HerstellerNr)
                .HasColumnName("hersteller_nr")
                .HasComment("herstellerNr - IK der Apotheke");

            _ = entity.Property(e => e.HerstellerSchluessel)
                .HasColumnName("hersteller_schluessel")
                .HasComment("herstellerSchluessel - Werte 1|2|3|4");

            _ = entity.Property(e => e.HerstellungsDatum)
                .HasMaxLength(30)
                .HasColumnName("herstellungs_datum")
                .HasComment("herstellungsDatum Format 2011-08-19T11:37:04.000");

            _ = entity.Property(e => e.IdSenderezeptePrezept).HasColumnName("id_senderezepte_prezept");

            _ = entity.HasOne(d => d.IdSenderezeptePrezeptNavigation)
                .WithMany(p => p.ErSenderezeptePrezeptPcharges)
                .HasForeignKey(d => d.IdSenderezeptePrezept)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_prezept_pcharge_id_srez_prez_fkey");
        });

        _ = modelBuilder.Entity<ErSenderezeptePrezeptPchargePwirkstoff>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezeptePrezeptPwirkstoff)
                .HasName("er_senderezepte_prezept_pcharge_pwirkstoff_pkey");

            _ = entity.ToTable("er_senderezepte_prezept_pcharge_pwirkstoff");

            _ = entity.HasIndex(e => e.IdSenderezeptePrezeptPcharge, "idx_er_sendrez_prez_pch_pwirk_pchid");

            _ = entity.Property(e => e.IdSenderezeptePrezeptPwirkstoff)
                .HasColumnName("id_senderezepte_prezept_pwirkstoff")
                .HasDefaultValueSql("nextval('er_senderezepte_prezept_pchar_id_senderezepte_prezept_pwirk_seq'::regclass)");

            _ = entity.Property(e => e.Faktor)
                .HasPrecision(13, 6)
                .HasColumnName("faktor")
                .HasComment("faktor");

            _ = entity.Property(e => e.FaktorKennzeichen)
                .HasMaxLength(2)
                .HasColumnName("faktor_kennzeichen")
                .HasComment("faktorKennzeichen");

            _ = entity.Property(e => e.IdSenderezeptePrezeptPcharge).HasColumnName("id_senderezepte_prezept_pcharge");

            _ = entity.Property(e => e.Notiz)
                .HasMaxLength(130)
                .HasColumnName("notiz")
                .HasComment("notiz");

            _ = entity.Property(e => e.PPosNr)
                .HasColumnName("p_pos_nr")
                .HasComment("pPosNr");

            _ = entity.Property(e => e.PreisKennzeichen)
                .HasMaxLength(2)
                .HasColumnName("preis_kennzeichen")
                .HasComment("preisKennzeichen");

            _ = entity.Property(e => e.Pzn)
                .HasColumnName("pzn")
                .HasComment("pzn");

            _ = entity.Property(e => e.Taxe)
                .HasPrecision(11, 2)
                .HasColumnName("taxe")
                .HasDefaultValueSql("0")
                .HasComment("taxe");

            _ = entity.Property(e => e.WirkstoffName)
                .HasMaxLength(130)
                .HasColumnName("wirkstoff_name")
                .HasComment("wirkstoffName");

            _ = entity.HasOne(d => d.IdSenderezeptePrezeptPchargeNavigation)
                .WithMany(p => p.ErSenderezeptePrezeptPchargePwirkstoffs)
                .HasForeignKey(d => d.IdSenderezeptePrezeptPcharge)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_prez_pcharge_pwirk_id_senderezepte_prez_pch_fke");
        });

        _ = modelBuilder.Entity<ErSenderezeptePrezeptStatus>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezeptePrezeptStatus)
                .HasName("er_senderezepte_prezept_status_pkey");

            _ = entity.ToTable("er_senderezepte_prezept_status");

            _ = entity.HasIndex(e => new { e.IdSenderezeptePrezept, e.TransaktionsNummer, e.RezeptStatus }, "idx_er_sendrez_prez_stat_idrezprez");

            _ = entity.Property(e => e.IdSenderezeptePrezeptStatus)
                .HasColumnName("id_senderezepte_prezept_status")
                .HasDefaultValueSql("nextval('er_senderezepte_prezept_statu_id_senderezepte_prezept_stat_seq1'::regclass)");

            _ = entity.Property(e => e.AbrechenStatus)
                .HasMaxLength(20)
                .HasColumnName("abrechen_status")
                .HasDefaultValueSql("'VOR_PRUEFUNG'::character varying")
                .HasComment("Interner Status - enthält immer das Prüfergebnis");

            _ = entity.Property(e => e.IdSenderezeptePrezept).HasColumnName("id_senderezepte_prezept");

            _ = entity.Property(e => e.RezeptCheck)
                .HasColumnName("rezept_check")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.RezeptStatus)
                .HasMaxLength(20)
                .HasColumnName("rezept_status")
                .HasDefaultValueSql("'VOR_PRUEFUNG'::character varying");

            _ = entity.Property(e => e.RzAbfrageId)
                .HasMaxLength(20)
                .HasColumnName("rz_abfrage_id")
                .HasComment("ID mit der alle DS gekennzeichnet sind die innerhalb einer RCS Abfrage gesendet wurden");

            _ = entity.Property(e => e.StatusAbfrageDatum)
                .HasColumnName("status_abfrage_datum")
                .HasComment("Abfragedatum des Status zum Rezept");

            _ = entity.Property(e => e.StatusAbfrageZeit)
                .HasColumnName("status_abfrage_zeit")
                .HasComment("Abfragezeit des Status zum Rezept");

            _ = entity.Property(e => e.TransaktionsNummer).HasColumnName("transaktions_nummer");

            _ = entity.HasOne(d => d.IdSenderezeptePrezeptNavigation)
                .WithMany(p => p.ErSenderezeptePrezeptStatuses)
                .HasForeignKey(d => d.IdSenderezeptePrezept)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_prezept_status_id_senderezepte_prezept");
        });

        _ = modelBuilder.Entity<ErSenderezeptePrezeptStatusinfo>(entity =>
        {
            _ = entity.HasKey(e => e.IdSenderezeptePrezeptStatusinfo)
                .HasName("er_senderezepte_prezept_statusinfo_pkey");

            _ = entity.ToTable("er_senderezepte_prezept_statusinfo");

            _ = entity.HasIndex(e => e.IdSenderezeptePrezept, "idx_er_sendrez_prez_stinfo_id");

            _ = entity.Property(e => e.IdSenderezeptePrezeptStatusinfo)
                .HasColumnName("id_senderezepte_prezept_statusinfo")
                .HasDefaultValueSql("nextval('er_senderezepte_prezept_statu_id_senderezepte_prezept_statu_seq'::regclass)");

            _ = entity.Property(e => e.Fcode)
                .HasMaxLength(4)
                .HasColumnName("fcode");

            _ = entity.Property(e => e.Fhauptfehler)
                .HasColumnName("fhauptfehler")
                .HasDefaultValueSql("false");

            _ = entity.Property(e => e.Fkommentar).HasColumnName("fkommentar");

            _ = entity.Property(e => e.Fkurztext).HasColumnName("fkurztext");

            _ = entity.Property(e => e.Fstatus)
                .HasMaxLength(20)
                .HasColumnName("fstatus");

            _ = entity.Property(e => e.Ftcode)
                .HasMaxLength(3)
                .HasColumnName("ftcode");

            _ = entity.Property(e => e.IdSenderezeptePrezept).HasColumnName("id_senderezepte_prezept");

            _ = entity.Property(e => e.Posnr).HasColumnName("posnr");

            _ = entity.Property(e => e.RegelNr)
                .HasMaxLength(10)
                .HasColumnName("regel_nr");

            _ = entity.Property(e => e.RegelTrefferCode)
                .HasMaxLength(7)
                .HasColumnName("regel_treffer_code");

            _ = entity.HasOne(d => d.IdSenderezeptePrezeptNavigation)
                .WithMany(p => p.ErSenderezeptePrezeptStatusinfos)
                .HasForeignKey(d => d.IdSenderezeptePrezept)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("er_senderezepte_prezept_statusinfo_id_senderezepte_prezept");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
