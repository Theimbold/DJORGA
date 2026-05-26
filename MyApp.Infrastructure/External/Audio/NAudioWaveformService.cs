using MyApp.Application.Interfaces.External;
using MyApp.Application.DTOs;
using NAudio.Wave;
using NAudio.Dsp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.External.Audio
{
    public class NAudioWaveformService : IWaveformService
    {
        private readonly string _cacheDirectory;

        public NAudioWaveformService()
        {
            _cacheDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "DJORGA", "PeaksV2"); // Versioning the cache

            if (!Directory.Exists(_cacheDirectory))
            {
                Directory.CreateDirectory(_cacheDirectory);
            }
        }

        public async Task<FrequencyPeak[]> GetPeaksAsync(string trackId, string filePath, int peakCount = 2000)
        {
            string cachePath = Path.Combine(_cacheDirectory, $"{trackId}.dat");

            if (File.Exists(cachePath))
            {
                return await LoadPeaksFromCacheAsync(cachePath);
            }

            return await Task.Run(() =>
            {
                try
                {
                    using var reader = new AudioFileReader(filePath);
                    var results = new FrequencyPeak[peakCount];
                    
                    // Filter-Setups (BiQuad)
                    var lowPass = BiQuadFilter.LowPassFilter(reader.WaveFormat.SampleRate, 250, 0.7f);   // < 250Hz (Bass)
                    var highPass = BiQuadFilter.HighPassFilter(reader.WaveFormat.SampleRate, 4000, 0.7f); // > 4kHz (Highs)
                    
                    int samplesPerPeak = (int)(reader.Length / (reader.WaveFormat.BitsPerSample / 8) / peakCount);
                    var buffer = new float[samplesPerPeak];

                    for (int i = 0; i < peakCount; i++)
                    {
                        int read = reader.Read(buffer, 0, samplesPerPeak);
                        if (read > 0)
                        {
                            float maxLow = 0, maxMid = 0, maxHigh = 0;

                            for (int s = 0; s < read; s++)
                            {
                                float sample = buffer[s];
                                
                                // Bass-Anteil
                                float low = Math.Abs(lowPass.Transform(sample));
                                // High-Anteil
                                float high = Math.Abs(highPass.Transform(sample));
                                // Mid (Rest)
                                float mid = Math.Abs(sample) - (low + high);
                                if (mid < 0) mid = 0;

                                if (low > maxLow) maxLow = low;
                                if (mid > maxMid) maxMid = mid;
                                if (high > maxHigh) maxHigh = high;
                            }

                            // Normalisierung & Gewichtung (Bass wird oft stärker visualisiert)
                            results[i] = new FrequencyPeak(maxLow, maxMid * 0.8f, maxHigh * 0.6f);
                        }
                    }

                    SavePeaksToCache(cachePath, results);
                    return results;
                }
                catch
                {
                    return Array.Empty<FrequencyPeak>();
                }
            });
        }

        private async Task<FrequencyPeak[]> LoadPeaksFromCacheAsync(string path)
        {
            var bytes = await File.ReadAllBytesAsync(path);
            int structSize = sizeof(float) * 3;
            var peaks = new FrequencyPeak[bytes.Length / structSize];
            
            using var ms = new MemoryStream(bytes);
            using var br = new BinaryReader(ms);
            for (int i = 0; i < peaks.Length; i++)
            {
                peaks[i] = new FrequencyPeak(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            }
            return peaks;
        }

        private void SavePeaksToCache(string path, FrequencyPeak[] peaks)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            foreach (var p in peaks)
            {
                bw.Write(p.Low);
                bw.Write(p.Mid);
                bw.Write(p.High);
            }
            File.WriteAllBytes(path, ms.ToArray());
        }
    }
}
