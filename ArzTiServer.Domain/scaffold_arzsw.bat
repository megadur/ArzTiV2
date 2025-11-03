
::SET CONN="Username=dbuser;Password=pass1234;Server=opipc;Port=5432;Database=apoti_09;Integrated Security=true;Pooling=true;"
SET CONN="Username=postgres;Password=postgres;Server=localhost;Port=5432;Database=arzsw_db;Integrated Security=true;Pooling=true;"
::SET TABLES=-t er_apotheke -t er_senderezepte_emuster16 -t er_senderezepte_emuster16_artikel -t er_senderezepte_emuster16_daten -t er_senderezepte_emuster16_status -t er_senderezepte_emuster16_statusinfo -t er_senderezepte_erezept -t er_senderezepte_erezept_daten -t er_senderezepte_erezept_status -t er_senderezepte_erezept_statusinfo -t er_senderezepte_header -t er_senderezepte_header_daten -t er_senderezepte_prezept -t er_senderezepte_prezept_daten -t er_senderezepte_prezept_pcharge -t er_senderezepte_prezept_pcharge_pwirkstoff -t er_senderezepte_prezept_status -t er_senderezepte_prezept_statusinfo

dotnet ef dbcontext scaffold  %CONN% Npgsql.EntityFrameworkCore.PostgreSQL -o Model/ArzSw --context ArzSwDbContext %TABLES%  -f 