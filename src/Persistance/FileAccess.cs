using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Media;

namespace Persistence
{
    public static class FileAccess
    {
        // Adjust path if your build folder layout is different
        private static readonly string DataPath =
            Path.Combine("..", "..", "..", "var", "data.csv");

        private static List<Media>? _cache;

        // Load media from disk (lazy, cached in memory)
        public static IReadOnlyList<Media> LoadAllMedia()
        {
            if (_cache != null)
                return _cache;

            _cache = new List<Media>();

            if (!File.Exists(DataPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataPath)!);
                File.WriteAllText(DataPath, string.Empty);
                return _cache;
            }

            foreach (var line in File.ReadAllLines(DataPath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                var type = parts[0];

                Media? media = type switch
                {
                    "EBook" => ParseEBook(parts),
                    "Movie" => ParseMovie(parts),
                    "Song" => ParseSong(parts),
                    "VideoGame" => ParseVideoGame(parts),
                    "App" => ParseApp(parts),
                    "Podcast" => ParsePodcast(parts),
                    "Image" => ParseImage(parts),
                    _ => null
                };

                if (media != null)
                    _cache.Add(media);
            }

            // default sort by type then title
            _cache = _cache
                .OrderBy(m => m.MediaType)
                .ThenBy(m => m.Title, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return _cache;
        }

        public static IEnumerable<T> GetByType<T>() where T : Media
        {
            return LoadAllMedia().OfType<T>();
        }

        public static void AddMedia(Media media)
        {
            LoadAllMedia(); // ensure cache
            _cache!.Add(media);
            SaveAllMedia();
        }

        public static bool RemoveMediaByTitle(string title)
        {
            LoadAllMedia();
            var item = _cache!
                .FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (item == null)
                return false;

            _cache.Remove(item);
            SaveAllMedia();
            return true;
        }

        // --- helpers ---

        private static void SaveAllMedia()
        {
            if (_cache == null) return;

            var lines = _cache.Select(ToCsvLine);
            File.WriteAllLines(DataPath, lines);
        }

        private static string ToCsvLine(Media media)
        {
            return media switch
            {
                EBook e => string.Join(";", "EBook",
                                       e.Title, e.Author, e.Language,
                                       e.NumPages, e.YearOfPublication, e.ISBN),

                Movie m => string.Join(";", "Movie",
                                       m.Title, m.Director, m.Genres,
                                       m.ReleaseYear, m.Language, m.Duration),

                Song s => string.Join(";", "Song",
                                      s.Title, s.Composer, s.Singer,
                                      s.Genre, s.FileType, s.Duration, s.Language),

                VideoGame v => string.Join(";", "VideoGame",
                                           v.Title, v.Genre, v.Publisher,
                                           v.ReleaseYear, v.Platforms),

                App a => string.Join(";", "App",
                                     a.Title, a.Version, a.Publisher,
                                     a.Platforms, a.FileSize),

                Podcast p => string.Join(";", "Podcast",
                                         p.Title, p.ReleaseYear, p.Hosts,
                                         p.Guests, p.EpisodeNumber, p.Language),

                ImageFile i => string.Join(";", "Image",
                                           i.Title, i.Resolution, i.FileFormat,
                                           i.FileSize, i.DateTaken),

                _ => throw new InvalidOperationException("Unknown media type.")
            };
        }

        // --- parse helpers for each type ---

        private static EBook ParseEBook(string[] parts)
        {
            // EBook;Title;Author;Language;NumPages;Year;ISBN
            return new EBook(
                title: parts[1],
                author: parts[2],
                language: parts[3],
                numPages: int.Parse(parts[4]),
                yearOfPublication: int.Parse(parts[5]),
                isbn: parts[6]);
        }

        private static Movie ParseMovie(string[] parts)
        {
            // Movie;Title;Director;Genres;ReleaseYear;Language;Duration
            return new Movie(
                title: parts[1],
                director: parts[2],
                genres: parts[3],
                releaseYear: int.Parse(parts[4]),
                language: parts[5],
                duration: int.Parse(parts[6]));
        }

        private static Song ParseSong(string[] parts)
        {
            // Song;Title;Composer;Singer;Genre;FileType;Duration;Language
            return new Song(
                title: parts[1],
                composer: parts[2],
                singer: parts[3],
                genre: parts[4],
                fileType: parts[5],
                duration: int.Parse(parts[6]),
                language: parts[7]);
        }

        private static VideoGame ParseVideoGame(string[] parts)
        {
            // VideoGame;Title;Genre;Publisher;ReleaseYear;Platforms
            return new VideoGame(
                title: parts[1],
                genre: parts[2],
                publisher: parts[3],
                releaseYear: int.Parse(parts[4]),
                platforms: parts[5]);
        }

        private static App ParseApp(string[] parts)
        {
            // App;Title;Version;Publisher;Platforms;FileSize
            return new App(
                title: parts[1],
                version: parts[2],
                publisher: parts[3],
                platforms: parts[4],
                fileSize: parts[5]);
        }

        private static Podcast ParsePodcast(string[] parts)
        {
            // Podcast;Title;ReleaseYear;Hosts;Guests;EpisodeNumber;Language
            return new Podcast(
                title: parts[1],
                releaseYear: int.Parse(parts[2]),
                hosts: parts[3],
                guests: parts[4],
                episodeNumber: int.Parse(parts[5]),
                language: parts[6]);
        }

        private static ImageFile ParseImage(string[] parts)
        {
            // Image;Title;Resolution;FileFormat;FileSize;DateTaken
            return new ImageFile(
                title: parts[1],
                resolution: parts[2],
                fileFormat: parts[3],
                fileSize: parts[4],
                dateTaken: parts[5]);
        }
    }
}
