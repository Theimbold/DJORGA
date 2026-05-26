using DJORGA.Application.Interfaces.External;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DJORGA.Infrastructure.External.Metadata
{
    /// <summary>
    /// Implementierung des Cover-Caches unter Verwendung von SkiaSharp.
    /// </summary>
    public class LocalCoverCacheService : ICoverCacheService
    {
        private readonly string _cacheDirectory;

        public LocalCoverCacheService()
        {
            // Speicherpfad in AppData/Local
            _cacheDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "DJORGA", "Covers");

            if (!Directory.Exists(_cacheDirectory))
            {
                Directory.CreateDirectory(_cacheDirectory);
            }
        }

        public async Task<string> CacheCoverAsync(string trackId, byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return string.Empty;

            string targetPath = Path.Combine(_cacheDirectory, $"{trackId}.jpg");

            // Wenn das Cover bereits im Cache ist, Pfad zurückgeben
            if (File.Exists(targetPath)) return targetPath;

            try
            {
                // SkiaSharp Logik zum Skalieren und Speichern
                using var stream = new MemoryStream(imageData);
                using var codec = SKCodec.Create(stream);
                using var bitmap = SKBitmap.Decode(codec);

                // Zielgröße: 300x300 für gute Qualität bei geringer Dateigröße
                int targetSize = 300;
                var info = new SKImageInfo(targetSize, targetSize);
                using var resized = bitmap.Resize(info, SKFilterQuality.Medium);
                using var image = SKImage.FromBitmap(resized);
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);

                using var outputStream = File.OpenWrite(targetPath);
                data.SaveTo(outputStream);

                return targetPath;
            }
            catch (Exception ex)
            {
                // TODO: Logging
                return string.Empty;
            }
        }
    }
}
