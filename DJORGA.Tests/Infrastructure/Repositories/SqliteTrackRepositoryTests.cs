using DJORGA.Domain.Entities;
using DJORGA.Infrastructure.Persistence.EntityFramework;
using DJORGA.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DJORGA.Tests.Infrastructure.Repositories
{
    public class SqliteTrackRepositoryTests
    {
        private static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ThenGetAll_ReturnsSavedTrack()
        {
            using var ctx = CreateContext();
            var repo = new SqliteTrackRepository(ctx);
            var track = new Track { Title = "Test Track", FilePath = "/p/t.mp3", Bpm = 128 };

            await repo.AddAsync(track);
            var all = (await repo.GetAllAsync()).ToList();

            Assert.Single(all);
            Assert.Equal("Test Track", all[0].Title);
        }

        [Fact]
        public async Task AddRangeAsync_Inserts100Tracks_AllPresent()
        {
            using var ctx = CreateContext();
            var repo = new SqliteTrackRepository(ctx);
            var tracks = Enumerable.Range(1, 100)
                .Select(i => new Track { Title = $"Track {i}", FilePath = $"/p/{i}.mp3" })
                .ToList();

            await repo.AddRangeAsync(tracks);

            Assert.Equal(100, await repo.GetCountAsync());
        }

        [Fact]
        public async Task UpdateAsync_PersistsChangedTitle()
        {
            using var ctx = CreateContext();
            var repo = new SqliteTrackRepository(ctx);
            var track = new Track { Title = "Original", FilePath = "/p/t.mp3" };
            await repo.AddAsync(track);

            track.Title = "Updated";
            await repo.UpdateAsync(track);

            var loaded = await repo.GetByIdAsync(track.Id);
            Assert.Equal("Updated", loaded?.Title);
        }

        [Fact]
        public async Task AddAsync_FiresTrackAddedEvent()
        {
            using var ctx = CreateContext();
            var repo = new SqliteTrackRepository(ctx);
            Track? received = null;
            repo.TrackAdded += t => received = t;
            var track = new Track { Title = "EventTrack", FilePath = "/p/ev.mp3" };

            await repo.AddAsync(track);

            Assert.NotNull(received);
            Assert.Equal("EventTrack", received!.Title);
        }

        [Fact]
        public async Task EachTest_HasIsolatedDatabase()
        {
            // Two separate contexts → separate databases → no cross-test leakage
            using var ctx1 = CreateContext();
            using var ctx2 = CreateContext();
            var repo1 = new SqliteTrackRepository(ctx1);
            var repo2 = new SqliteTrackRepository(ctx2);

            await repo1.AddAsync(new Track { Title = "Only in DB1", FilePath = "/p/1.mp3" });

            Assert.Equal(0, await repo2.GetCountAsync());
        }
    }
}
