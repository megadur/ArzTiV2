using ArzTi3Server.Domain.DTOs;
using ArzTi3Server.Domain.Model.ApoTi;
using ArzTi3Server.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ArzTi3Server.Tests
{
    public class PrescriptionRepositoryTests : IDisposable
    {
        private readonly ArzTiDbContext _context;
        private readonly PrescriptionRepository _repository;

        public PrescriptionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ArzTiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ArzTiDbContext(options);
            _repository = new PrescriptionRepository(_context);
        }

        [Fact]
        public async Task GetNewPrescriptionsAsync_ReturnsOnlyNonAbgerechnetPrescriptions()
        {
            // Setup header
            var header = new ErSenderezepteHeader { 
                IdSenderezepteHeader = 1,
                ApoIntNr = "APO123",
                RzLieferId = "RZ-MAIN",
                RezTyp = "eM16"
            };
            await _context.ErSenderezepteHeaders.AddAsync(header);
            
            // Setup prescriptions
            var emuster16_1 = new ErSenderezepteEmuster16 { IdSenderezepteEmuster16 = 1, RzLieferId = "RZ1", IdSenderezepteHeader = 1 };
            var emuster16_2 = new ErSenderezepteEmuster16 { IdSenderezepteEmuster16 = 2, RzLieferId = "RZ2", IdSenderezepteHeader = 1 };
            var emuster16_3 = new ErSenderezepteEmuster16 { IdSenderezepteEmuster16 = 3, RzLieferId = "RZ3", IdSenderezepteHeader = 1 };
            
            var prezept_1 = new ErSenderezeptePrezept { IdSenderezeptePrezept = 1, RzLieferId = "RZ4", IdSenderezepteHeader = 1 };
            var prezept_2 = new ErSenderezeptePrezept { IdSenderezeptePrezept = 2, RzLieferId = "RZ5", IdSenderezepteHeader = 1 };
            
            var erezept_1 = new ErSenderezepteErezept { IdSenderezepteErezept = 1, RzLieferId = "RZ6", IdSenderezepteHeader = 1 };
            var erezept_2 = new ErSenderezepteErezept { IdSenderezepteErezept = 2, RzLieferId = "RZ7", IdSenderezepteHeader = 1 };

            // Setup daten entries
            var emuster16Daten_1 = new ErSenderezepteEmuster16Daten { 
                IdSenderezepteEmuster16Daten = 101, 
                IdSenderezepteEmuster16 = 1, 
                TransferArz = false,
                XmlRequest = "<xml>data1</xml>",
                RezeptUuid = "uuid-em-1",
                IdSenderezepteEmuster16Navigation = emuster16_1
            };
            var emuster16Daten_2 = new ErSenderezepteEmuster16Daten { 
                IdSenderezepteEmuster16Daten = 102, 
                IdSenderezepteEmuster16 = 2, 
                TransferArz = false,
                XmlRequest = "<xml>data2</xml>",
                RezeptUuid = "uuid-em-2",
                IdSenderezepteEmuster16Navigation = emuster16_2
            };
            var emuster16Daten_3 = new ErSenderezepteEmuster16Daten { 
                IdSenderezepteEmuster16Daten = 103, 
                IdSenderezepteEmuster16 = 3, 
                TransferArz = false,
                XmlRequest = "<xml>data3</xml>",
                RezeptUuid = "uuid-em-3",
                IdSenderezepteEmuster16Navigation = emuster16_3
            };
            
            var prezeptDaten_1 = new ErSenderezeptePrezeptDaten { 
                IdSenderezeptePrezeptDaten = 201, 
                IdSenderezeptePrezept = 1, 
                TransferArz = false,
                XmlRequest = "<xml>data4</xml>",
                RezeptUuid = "uuid-pr-1",
                IdSenderezeptePrezeptNavigation = prezept_1
            };
            var prezeptDaten_2 = new ErSenderezeptePrezeptDaten { 
                IdSenderezeptePrezeptDaten = 202, 
                IdSenderezeptePrezept = 2, 
                TransferArz = false,
                XmlRequest = "<xml>data5</xml>",
                RezeptUuid = "uuid-pr-2",
                IdSenderezeptePrezeptNavigation = prezept_2
            };
            
            var erezeptDaten_1 = new ErSenderezepteErezeptDaten { 
                IdSenderezepteErezeptDaten = 301, 
                IdSenderezepteErezept = 1, 
                TransferArz = false,
                XmlRequest = "<xml>data6</xml>",
                RezeptUuid = "uuid-er-1",
                IdSenderezepteErezeptNavigation = erezept_1
            };
            var erezeptDaten_2 = new ErSenderezepteErezeptDaten { 
                IdSenderezepteErezeptDaten = 302, 
                IdSenderezepteErezept = 2, 
                TransferArz = false,
                XmlRequest = "<xml>data7</xml>",
                RezeptUuid = "uuid-er-2",
                IdSenderezepteErezeptNavigation = erezept_2
            };

            // Setup status
            var emuster16Status_1 = new ErSenderezepteEmuster16Status { 
                IdSenderezepteEmuster16Status = 1001, 
                IdSenderezepteEmuster16 = 1, 
                RezeptStatus = "NEU",
                IdSenderezepteEmuster16Navigation = emuster16_1
            };
            var emuster16Status_2 = new ErSenderezepteEmuster16Status { 
                IdSenderezepteEmuster16Status = 1002, 
                IdSenderezepteEmuster16 = 2, 
                RezeptStatus = "ABGERECHNET",
                IdSenderezepteEmuster16Navigation = emuster16_2
            };
            var emuster16Status_3 = new ErSenderezepteEmuster16Status { 
                IdSenderezepteEmuster16Status = 1003, 
                IdSenderezepteEmuster16 = 3, 
                RezeptStatus = "GELESEN",
                IdSenderezepteEmuster16Navigation = emuster16_3
            };
            
            var prezeptStatus_1 = new ErSenderezeptePrezeptStatus { 
                IdSenderezeptePrezeptStatus = 2001, 
                IdSenderezeptePrezept = 1, 
                RezeptStatus = "NEU",
                IdSenderezeptePrezeptNavigation = prezept_1
            };
            var prezeptStatus_2 = new ErSenderezeptePrezeptStatus { 
                IdSenderezeptePrezeptStatus = 2002, 
                IdSenderezeptePrezept = 2, 
                RezeptStatus = "ABGERECHNET",
                IdSenderezeptePrezeptNavigation = prezept_2
            };
            
            var erezeptStatus_1 = new ErSenderezepteErezeptStatus { 
                IdSenderezepteErezeptStatus = 3001, 
                IdSenderezepteErezept = 1, 
                RezeptStatus = "VERARBEITET",
                IdSenderezepteErezeptNavigation = erezept_1
            };
            var erezeptStatus_2 = new ErSenderezepteErezeptStatus { 
                IdSenderezepteErezeptStatus = 3002, 
                IdSenderezepteErezept = 2, 
                RezeptStatus = "ABGERECHNET",
                IdSenderezepteErezeptNavigation = erezept_2
            };

            // Add all entities
            await _context.SaveChangesAsync();  // Save header first
            
            await _context.ErSenderezepteEmuster16s.AddRangeAsync(new[] { emuster16_1, emuster16_2, emuster16_3 });
            await _context.ErSenderezeptePrezepts.AddRangeAsync(new[] { prezept_1, prezept_2 });
            await _context.ErSenderezepteErezepts.AddRangeAsync(new[] { erezept_1, erezept_2 });
            await _context.SaveChangesAsync();  // Save prescription entities
            
            await _context.ErSenderezepteEmuster16Datens.AddRangeAsync(new[] { emuster16Daten_1, emuster16Daten_2, emuster16Daten_3 });
            await _context.ErSenderezeptePrezeptDatens.AddRangeAsync(new[] { prezeptDaten_1, prezeptDaten_2 });
            await _context.ErSenderezepteErezeptDatens.AddRangeAsync(new[] { erezeptDaten_1, erezeptDaten_2 });
            await _context.SaveChangesAsync();  // Save daten entities
            
            await _context.ErSenderezepteEmuster16Statuses.AddRangeAsync(new[] { emuster16Status_1, emuster16Status_2, emuster16Status_3 });
            await _context.ErSenderezeptePrezeptStatuses.AddRangeAsync(new[] { prezeptStatus_1, prezeptStatus_2 });
            await _context.ErSenderezepteErezeptStatuses.AddRangeAsync(new[] { erezeptStatus_1, erezeptStatus_2 });
            await _context.SaveChangesAsync();  // Save status entities

            // Act
            var result = await _repository.GetNewPrescriptionsAsync(1, 10);
            var prescriptions = result.ToList();

            // Assert
            Assert.Equal(4, prescriptions.Count); // 2 eMuster16 (1,3), 1 P-Rezept (1), 1 E-Rezept (1) (excluding ABGERECHNET)

            // Verify no ABGERECHNET prescriptions are returned
            Assert.DoesNotContain(prescriptions, p => p.Id == 2 && p.Type == "eMuster16");
            Assert.DoesNotContain(prescriptions, p => p.Id == 2 && p.Type == "P-Rezept");
            Assert.DoesNotContain(prescriptions, p => p.Id == 2 && p.Type == "E-Rezept");
        }

        [Fact]
        public async Task GetNewPrescriptionsCountAsync_ReturnsCorrectCount()
        {
            // Setup header
            var header = new ErSenderezepteHeader { 
                IdSenderezepteHeader = 1,
                ApoIntNr = "APO123",
                RzLieferId = "RZ-MAIN",
                RezTyp = "eM16"
            };
            await _context.ErSenderezepteHeaders.AddAsync(header);
            await _context.SaveChangesAsync();
            
            // Setup prescriptions
            var emuster16_1 = new ErSenderezepteEmuster16 { IdSenderezepteEmuster16 = 1, RzLieferId = "RZ1", IdSenderezepteHeader = 1 };
            var emuster16_2 = new ErSenderezepteEmuster16 { IdSenderezepteEmuster16 = 2, RzLieferId = "RZ2", IdSenderezepteHeader = 1 };
            
            var prezept_1 = new ErSenderezeptePrezept { IdSenderezeptePrezept = 1, RzLieferId = "RZ4", IdSenderezepteHeader = 1 };
            var prezept_2 = new ErSenderezeptePrezept { IdSenderezeptePrezept = 2, RzLieferId = "RZ5", IdSenderezepteHeader = 1 };

            await _context.ErSenderezepteEmuster16s.AddRangeAsync(new[] { emuster16_1, emuster16_2 });
            await _context.ErSenderezeptePrezepts.AddRangeAsync(new[] { prezept_1, prezept_2 });
            await _context.SaveChangesAsync();

            // Setup daten entries
            var emuster16Daten_1 = new ErSenderezepteEmuster16Daten { 
                IdSenderezepteEmuster16Daten = 101, 
                IdSenderezepteEmuster16 = 1, 
                TransferArz = false,
                XmlRequest = "<xml>data1</xml>",
                RezeptUuid = "uuid-em-1",
                IdSenderezepteEmuster16Navigation = emuster16_1
            };
            var emuster16Daten_2 = new ErSenderezepteEmuster16Daten { 
                IdSenderezepteEmuster16Daten = 102, 
                IdSenderezepteEmuster16 = 2, 
                TransferArz = false,
                XmlRequest = "<xml>data2</xml>",
                RezeptUuid = "uuid-em-2",
                IdSenderezepteEmuster16Navigation = emuster16_2
            };
            
            var prezeptDaten_1 = new ErSenderezeptePrezeptDaten { 
                IdSenderezeptePrezeptDaten = 201, 
                IdSenderezeptePrezept = 1, 
                TransferArz = false,
                XmlRequest = "<xml>data4</xml>",
                RezeptUuid = "uuid-pr-1",
                IdSenderezeptePrezeptNavigation = prezept_1
            };
            var prezeptDaten_2 = new ErSenderezeptePrezeptDaten { 
                IdSenderezeptePrezeptDaten = 202, 
                IdSenderezeptePrezept = 2, 
                TransferArz = false,
                XmlRequest = "<xml>data5</xml>",
                RezeptUuid = "uuid-pr-2",
                IdSenderezeptePrezeptNavigation = prezept_2
            };

            await _context.ErSenderezepteEmuster16Datens.AddRangeAsync(new[] { emuster16Daten_1, emuster16Daten_2 });
            await _context.ErSenderezeptePrezeptDatens.AddRangeAsync(new[] { prezeptDaten_1, prezeptDaten_2 });
            await _context.SaveChangesAsync();

            // Setup status
            var emuster16Status_1 = new ErSenderezepteEmuster16Status { 
                IdSenderezepteEmuster16Status = 1001, 
                IdSenderezepteEmuster16 = 1, 
                RezeptStatus = "NEU",
                IdSenderezepteEmuster16Navigation = emuster16_1
            };
            var emuster16Status_2 = new ErSenderezepteEmuster16Status { 
                IdSenderezepteEmuster16Status = 1002, 
                IdSenderezepteEmuster16 = 2, 
                RezeptStatus = "ABGERECHNET",
                IdSenderezepteEmuster16Navigation = emuster16_2
            };
            
            var prezeptStatus_1 = new ErSenderezeptePrezeptStatus { 
                IdSenderezeptePrezeptStatus = 2001, 
                IdSenderezeptePrezept = 1, 
                RezeptStatus = "GELESEN",
                IdSenderezeptePrezeptNavigation = prezept_1
            };
            var prezeptStatus_2 = new ErSenderezeptePrezeptStatus { 
                IdSenderezeptePrezeptStatus = 2002, 
                IdSenderezeptePrezept = 2, 
                RezeptStatus = "ABGERECHNET",
                IdSenderezeptePrezeptNavigation = prezept_2
            };

            await _context.ErSenderezepteEmuster16Statuses.AddRangeAsync(new[] { emuster16Status_1, emuster16Status_2 });
            await _context.ErSenderezeptePrezeptStatuses.AddRangeAsync(new[] { prezeptStatus_1, prezeptStatus_2 });
            await _context.SaveChangesAsync();

            // Act
            var count = await _repository.GetNewPrescriptionsCountAsync();

            // Assert
            Assert.Equal(2, count); // 1 eMuster16 + 1 P-Rezept (excluding ABGERECHNET)
        }

        [Fact]
        public async Task MarkAsReadAsync_UpdatesStatusCorrectly()
        {
            // Arrange
            var header = new ErSenderezepteHeader { 
                IdSenderezepteHeader = 1,
                ApoIntNr = "APO123",
                RzLieferId = "RZ-MAIN",
                RezTyp = "eM16"
            };
            await _context.ErSenderezepteHeaders.AddAsync(header);
            await _context.SaveChangesAsync();
            
            var emuster16 = new ErSenderezepteEmuster16 { IdSenderezepteEmuster16 = 1, RzLieferId = "RZ1", IdSenderezepteHeader = 1 };
            var prezept = new ErSenderezeptePrezept { IdSenderezeptePrezept = 2, RzLieferId = "RZ2", IdSenderezepteHeader = 1 };
            
            await _context.ErSenderezepteEmuster16s.AddAsync(emuster16);
            await _context.ErSenderezeptePrezepts.AddAsync(prezept);
            await _context.SaveChangesAsync();
            
            var emuster16Daten = new ErSenderezepteEmuster16Daten {
                IdSenderezepteEmuster16Daten = 101,
                IdSenderezepteEmuster16 = 1,
                TransferArz = false,
                XmlRequest = "<xml>data1</xml>",
                RezeptUuid = "uuid-em-1",
                IdSenderezepteEmuster16Navigation = emuster16
            };
            var prezeptDaten = new ErSenderezeptePrezeptDaten {
                IdSenderezeptePrezeptDaten = 201,
                IdSenderezeptePrezept = 2,
                TransferArz = false,
                XmlRequest = "<xml>data2</xml>",
                RezeptUuid = "uuid-pr-2",
                IdSenderezeptePrezeptNavigation = prezept
            };
            
            await _context.ErSenderezepteEmuster16Datens.AddAsync(emuster16Daten);
            await _context.ErSenderezeptePrezeptDatens.AddAsync(prezeptDaten);
            await _context.SaveChangesAsync();
            
            var emuster16Status = new ErSenderezepteEmuster16Status {
                IdSenderezepteEmuster16Status = 1001,
                IdSenderezepteEmuster16 = 1,
                RezeptStatus = "NEU",
                IdSenderezepteEmuster16Navigation = emuster16
            };
            var prezeptStatus = new ErSenderezeptePrezeptStatus {
                IdSenderezeptePrezeptStatus = 2001,
                IdSenderezeptePrezept = 2,
                RezeptStatus = "VERARBEITET",
                IdSenderezeptePrezeptNavigation = prezept
            };
            
            await _context.ErSenderezepteEmuster16Statuses.AddAsync(emuster16Status);
            await _context.ErSenderezeptePrezeptStatuses.AddAsync(prezeptStatus);
            await _context.SaveChangesAsync();

            var prescriptions = new List<PrescriptionDto>
            {
                new PrescriptionDto { Type = "eMuster16", Id = 1, RezeptUuid = null, XmlRequest = null, TransferArz = false, RzLieferId = null, Muster16Id = null, TransaktionsNummer = null, ErezeptId = null, DatenId = null },
                new PrescriptionDto { Type = "P-Rezept", Id = 2, RezeptUuid = null, XmlRequest = null, TransferArz = false, RzLieferId = null, Muster16Id = null, TransaktionsNummer = null, ErezeptId = null, DatenId = null }
            };

            // Act
            await _repository.MarkAsReadAsync(prescriptions);

            // Assert
            var updatedEmuster16 = await _context.ErSenderezepteEmuster16Statuses.FirstAsync(s => s.IdSenderezepteEmuster16 == 1);
            var updatedPrezept = await _context.ErSenderezeptePrezeptStatuses.FirstAsync(s => s.IdSenderezeptePrezept == 2);

            Assert.Equal("GELESEN", updatedEmuster16.RezeptStatus);
            Assert.Equal("GELESEN", updatedPrezept.RezeptStatus);
        }

        [Fact]
        public async Task SetStatusAbgerechnetAsync_UpdatesStatusCorrectly()
        {
            // Arrange
            var header = new ErSenderezepteHeader { 
                IdSenderezepteHeader = 1,
                ApoIntNr = "APO123",
                RzLieferId = "RZ-MAIN",
                RezTyp = "eM16"
            };
            await _context.ErSenderezepteHeaders.AddAsync(header);
            await _context.SaveChangesAsync();
            
            var emuster16 = new ErSenderezepteEmuster16 { IdSenderezepteEmuster16 = 1, RzLieferId = "RZ1", IdSenderezepteHeader = 1 };
            var erezept = new ErSenderezepteErezept { IdSenderezepteErezept = 3, RzLieferId = "RZ3", IdSenderezepteHeader = 1 };
            
            await _context.ErSenderezepteEmuster16s.AddAsync(emuster16);
            await _context.ErSenderezepteErezepts.AddAsync(erezept);
            await _context.SaveChangesAsync();

            var emuster16Status = new ErSenderezepteEmuster16Status {
                IdSenderezepteEmuster16Status = 1001,
                IdSenderezepteEmuster16 = 1,
                RezeptStatus = "GELESEN",
                IdSenderezepteEmuster16Navigation = emuster16
            };
            var erezeptStatus = new ErSenderezepteErezeptStatus {
                IdSenderezepteErezeptStatus = 3001,
                IdSenderezepteErezept = 3,
                RezeptStatus = "VERARBEITET",
                IdSenderezepteErezeptNavigation = erezept
            };
            
            await _context.ErSenderezepteEmuster16Statuses.AddAsync(emuster16Status);
            await _context.ErSenderezepteErezeptStatuses.AddAsync(erezeptStatus);
            await _context.SaveChangesAsync();

            var prescriptions = new List<PrescriptionDto>
            {
                new PrescriptionDto { Type = "eMuster16", Id = 1, RezeptUuid = null, XmlRequest = null, TransferArz = false, RzLieferId = null, Muster16Id = null, TransaktionsNummer = null, ErezeptId = null, DatenId = null },
                new PrescriptionDto { Type = "E-Rezept", Id = 3, RezeptUuid = null, XmlRequest = null, TransferArz = false, RzLieferId = null, Muster16Id = null, TransaktionsNummer = null, ErezeptId = null, DatenId = null }
            };

            // Act
            await _repository.SetStatusAbgerechnetAsync(prescriptions);

            // Assert
            var updatedEmuster16 = await _context.ErSenderezepteEmuster16Statuses.FirstAsync(s => s.IdSenderezepteEmuster16 == 1);
            var updatedErezept = await _context.ErSenderezepteErezeptStatuses.FirstAsync(s => s.IdSenderezepteErezept == 3);

            Assert.Equal("ABGERECHNET", updatedEmuster16.RezeptStatus);
            Assert.Equal("ABGERECHNET", updatedErezept.RezeptStatus);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}