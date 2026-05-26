using DJORGA.Application.DTOs;
using DJORGA.Application.Interfaces.External;
using DJORGA.Application.Interfaces.Persistence;
using DJORGA.Application.UseCases.Library;
using DJORGA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DJORGA.Tests.Application.UseCases
{
    public class UpdateTrackMetadataUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidTrack_UpdatesRepoAndMetadata()
        {
            var repo = new TrackRepoSpy();
            var meta = new MetadataSpy();
            var uc = new UpdateTrackMetadataUseCase(repo, meta);

            await uc.ExecuteAsync(new Track { Title = "My Track", FilePath = "/p/t.mp3" });

            Assert.Equal(1, repo.UpdateCallCount);
            Assert.Equal(1, meta.UpdateCallCount);
        }

        [Fact]
        public async Task ExecuteAsync_NullTrack_ThrowsArgumentNullException()
        {
            var uc = new UpdateTrackMetadataUseCase(new TrackRepoSpy(), new MetadataSpy());

            await Assert.ThrowsAsync<ArgumentNullException>(() => uc.ExecuteAsync(null!));
        }

        [Fact]
        public async Task ExecuteAsync_MetadataServiceThrows_NoExceptionPropagates()
        {
            var repo = new TrackRepoSpy();
            var uc = new UpdateTrackMetadataUseCase(repo, new MetadataSpy { ShouldThrow = true });

            // exception from IMetadataService is caught internally
            await uc.ExecuteAsync(new Track { Title = "Track", FilePath = "/p/t.mp3" });

            // DB update still completed before the metadata write failed
            Assert.Equal(1, repo.UpdateCallCount);
        }

        private sealed class TrackRepoSpy : ITrackRepository
        {
            public int UpdateCallCount { get; private set; }
            public event Action<Track>? TrackAdded;
            public event Action<Track>? TrackUpdated;
            public event Action<IEnumerable<Track>>? BulkTracksAdded;

            public Task UpdateAsync(Track t)
            {
                UpdateCallCount++; TrackUpdated?.Invoke(t); return Task.CompletedTask;
            }

            public Task<Track?> GetByIdAsync(Guid id) => Task.FromResult<Track?>(null);
            public Task<IEnumerable<Track>> GetAllAsync() => Task.FromResult<IEnumerable<Track>>([]);
            public Task AddAsync(Track t) { TrackAdded?.Invoke(t); return Task.CompletedTask; }
            public Task AddRangeAsync(IEnumerable<Track> ts) { BulkTracksAdded?.Invoke(ts); return Task.CompletedTask; }
            public Task DeleteAsync(Guid id) => Task.CompletedTask;
            public Task<IEnumerable<Track>> SearchAsync(string q) => Task.FromResult<IEnumerable<Track>>([]);
            public Task<IEnumerable<Track>> GetUnanalyzedAsync() => Task.FromResult<IEnumerable<Track>>([]);
            public Task<IEnumerable<Track>> GetOrphansAsync() => Task.FromResult<IEnumerable<Track>>([]);
            public Task<int> GetCountAsync() => Task.FromResult(0);
        }

        private sealed class MetadataSpy : IMetadataService
        {
            public int UpdateCallCount { get; private set; }
            public bool ShouldThrow { get; init; }

            public Task<TrackMetadata> ExtractMetadataAsync(string filePath) =>
                Task.FromResult(new TrackMetadata());

            public Task UpdateMetadataAsync(string filePath, TrackMetadata metadata)
            {
                UpdateCallCount++;
                if (ShouldThrow) throw new IOException("Disk write failed");
                return Task.CompletedTask;
            }
        }
    }
}
