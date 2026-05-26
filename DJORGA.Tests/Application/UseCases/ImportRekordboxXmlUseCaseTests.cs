using DJORGA.Application.DTOs;
using DJORGA.Application.Interfaces.External;
using DJORGA.Application.Interfaces.Persistence;
using DJORGA.Application.UseCases.Rekordbox;
using DJORGA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DJORGA.Tests.Application.UseCases
{
    public class ImportRekordboxXmlUseCaseTests
    {
        private static ImportRekordboxXmlUseCase Build(
            IEnumerable<Track> xmlTracks,
            IEnumerable<Track>? existingTracks = null,
            bool xmlThrows = false) =>
            new(
                new StubRekordboxService { TracksToReturn = xmlTracks, ShouldThrow = xmlThrows },
                new InMemoryTrackRepository(existingTracks),
                new StubMetadataService(),
                new StubCoverCacheService());

        [Fact]
        public async Task ExecuteAsync_ValidTracks_ReturnsCorrectCount()
        {
            var tracks = new[]
            {
                new Track { Title = "T1", FilePath = "/p/1.mp3" },
                new Track { Title = "T2", FilePath = "/p/2.mp3" },
                new Track { Title = "T3", FilePath = "/p/3.mp3" }
            };

            var count = await Build(tracks).ExecuteAsync("any.xml");

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task ExecuteAsync_DuplicateFilePath_SkipsDuplicate()
        {
            var existing = new[] { new Track { Title = "Old", FilePath = "/p/1.mp3" } };
            var xmlTracks = new[]
            {
                new Track { Title = "T1", FilePath = "/p/1.mp3" }, // duplicate
                new Track { Title = "T2", FilePath = "/p/2.mp3" },
                new Track { Title = "T3", FilePath = "/p/3.mp3" }
            };

            var count = await Build(xmlTracks, existing).ExecuteAsync("any.xml");

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task ExecuteAsync_EmptyOrUnknownTitles_SkippedTracks()
        {
            var tracks = new[]
            {
                new Track { Title = "Valid Track", FilePath = "/p/1.mp3" },
                new Track { Title = "", FilePath = "/p/2.mp3" },
                new Track { Title = "Unknown", FilePath = "/p/3.mp3" }
            };

            var count = await Build(tracks).ExecuteAsync("any.xml");

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task ExecuteAsync_XmlServiceThrows_PropagatesException()
        {
            await Assert.ThrowsAsync<FileNotFoundException>(
                () => Build([], xmlThrows: true).ExecuteAsync("missing.xml"));
        }

        private sealed class StubRekordboxService : IRekordboxService
        {
            public IEnumerable<Track> TracksToReturn { get; init; } = [];
            public bool ShouldThrow { get; init; }

            public Task<IEnumerable<Track>> ParseLibraryAsync(string filePath, IProgress<double>? progress = null)
            {
                if (ShouldThrow) throw new FileNotFoundException("File not found", filePath);
                return Task.FromResult(TracksToReturn);
            }

            public Task<IEnumerable<Playlist>> ParsePlaylistsAsync(string filePath) =>
                Task.FromResult<IEnumerable<Playlist>>([]);
        }

        private sealed class InMemoryTrackRepository : ITrackRepository
        {
            private readonly List<Track> _tracks;
            public event Action<Track>? TrackAdded;
            public event Action<Track>? TrackUpdated;
            public event Action<IEnumerable<Track>>? BulkTracksAdded;

            public InMemoryTrackRepository(IEnumerable<Track>? initial = null) =>
                _tracks = initial?.ToList() ?? [];

            public Task<IEnumerable<Track>> GetAllAsync() =>
                Task.FromResult<IEnumerable<Track>>(_tracks.ToList());

            public Task<Track?> GetByIdAsync(Guid id) =>
                Task.FromResult(_tracks.FirstOrDefault(t => t.Id == id));

            public Task AddAsync(Track t)
            {
                _tracks.Add(t); TrackAdded?.Invoke(t); return Task.CompletedTask;
            }

            public Task AddRangeAsync(IEnumerable<Track> tracks)
            {
                var list = tracks.ToList();
                _tracks.AddRange(list);
                BulkTracksAdded?.Invoke(list);
                return Task.CompletedTask;
            }

            public Task UpdateAsync(Track t) { TrackUpdated?.Invoke(t); return Task.CompletedTask; }
            public Task DeleteAsync(Guid id) { _tracks.RemoveAll(t => t.Id == id); return Task.CompletedTask; }
            public Task<IEnumerable<Track>> SearchAsync(string q) => Task.FromResult<IEnumerable<Track>>([]);
            public Task<IEnumerable<Track>> GetUnanalyzedAsync() => Task.FromResult<IEnumerable<Track>>([]);
            public Task<IEnumerable<Track>> GetOrphansAsync() => Task.FromResult<IEnumerable<Track>>([]);
            public Task<int> GetCountAsync() => Task.FromResult(_tracks.Count);
        }

        private sealed class StubMetadataService : IMetadataService
        {
            public Task<TrackMetadata> ExtractMetadataAsync(string filePath) =>
                Task.FromResult(new TrackMetadata());

            public Task UpdateMetadataAsync(string filePath, TrackMetadata metadata) =>
                Task.CompletedTask;
        }

        private sealed class StubCoverCacheService : ICoverCacheService
        {
            public Task<string> CacheCoverAsync(string trackId, byte[] imageData) =>
                Task.FromResult("cache/cover.jpg");
        }
    }
}
