using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Media;
using Persistence;

namespace Domain.User
{
    public enum UserType
    {
        Borrower = 1,
        Employee = 2,
        Admin = 3
    }

    public class User
    {
        public string Name { get; init; }
        public int Age { get; init; }
        public string SSN { get; init; }
        public UserType Type { get; init; }

        public User(string name, int age, string ssn, UserType type)
        {
            Name = name;
            Age = age;
            SSN = ssn;
            Type = type;
        }

        // Borrower + Employee + Admin: view media
        public void ViewMedia()
        {
            var media = FileAccess.LoadAllMedia();

            if (!media.Any())
            {
                Console.WriteLine("No media in the collection yet.");
                return;
            }

            Console.WriteLine("Available media:");
            foreach (var group in media.GroupBy(m => m.MediaType))
            {
                Console.WriteLine($"== {group.Key} ==");
                foreach (var item in group)
                    Console.WriteLine($" - {item.Title}");
            }
        }

        // Employee/Admin: add media
        public void AddMedia(string _ignoredTitleFromUi)
        {
            if (Type == UserType.Borrower)
            {
                Console.WriteLine("Borrowers are not allowed to add media.");
                return;
            }

            Console.WriteLine("Select media type to add:");
            Console.WriteLine("1) EBook");
            Console.WriteLine("2) Movie");
            Console.WriteLine("3) Song");
            Console.WriteLine("4) VideoGame");
            Console.WriteLine("5) App");
            Console.WriteLine("6) Podcast");
            Console.WriteLine("7) Image");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            Media? media = choice switch
            {
                "1" => CreateEBookFromInput(),
                "2" => CreateMovieFromInput(),
                "3" => CreateSongFromInput(),
                "4" => CreateVideoGameFromInput(),
                "5" => CreateAppFromInput(),
                "6" => CreatePodcastFromInput(),
                "7" => CreateImageFromInput(),
                _ => null
            };

            if (media == null)
            {
                Console.WriteLine("Cancelled or invalid input.");
                return;
            }

            FileAccess.AddMedia(media);
            Console.WriteLine($"[{Type}: {Name}] Added media: {media.Title}");
        }

        // Employee/Admin: remove media
        public void RemoveMedia(string title)
        {
            if (Type == UserType.Borrower)
            {
                Console.WriteLine("Borrowers are not allowed to remove media.");
                return;
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            bool removed = FileAccess.RemoveMediaByTitle(title);
            Console.WriteLine(removed
                ? $"[{Type}: {Name}] Removed media: {title}"
                : $"Media with title \"{title}\" not found.");
        }

        // Admin-only logic stays the same
        public void ManageUsers()
        {
            Console.WriteLine($"[Admin: {Name}] Opening user management...");
        }

        // --- helper methods to build media objects from console input ---

        private static EBook? CreateEBookFromInput()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Author: ");
            var author = Console.ReadLine();
            Console.Write("Language: ");
            var language = Console.ReadLine();
            Console.Write("Number of pages: ");
            if (!int.TryParse(Console.ReadLine(), out int pages)) return null;
            Console.Write("Year of publication: ");
            if (!int.TryParse(Console.ReadLine(), out int year)) return null;
            Console.Write("ISBN: ");
            var isbn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
                return null;

            return new EBook(title, author!, language ?? "", pages, year, isbn ?? "");
        }

        private static Movie? CreateMovieFromInput()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Director: ");
            var director = Console.ReadLine();
            Console.Write("Genres: ");
            var genres = Console.ReadLine();
            Console.Write("Release year: ");
            if (!int.TryParse(Console.ReadLine(), out int year)) return null;
            Console.Write("Language: ");
            var language = Console.ReadLine();
            Console.Write("Duration (minutes): ");
            if (!int.TryParse(Console.ReadLine(), out int duration)) return null;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(director))
                return null;

            return new Movie(title!, director!, genres ?? "", year, language ?? "", duration);
        }

        private static Song? CreateSongFromInput()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Composer: ");
            var composer = Console.ReadLine();
            Console.Write("Singer: ");
            var singer = Console.ReadLine();
            Console.Write("Genre: ");
            var genre = Console.ReadLine();
            Console.Write("File type (e.g. mp3): ");
            var fileType = Console.ReadLine();
            Console.Write("Duration (seconds): ");
            if (!int.TryParse(Console.ReadLine(), out int duration)) return null;
            Console.Write("Language: ");
            var language = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title)) return null;

            return new Song(title!, composer ?? "", singer ?? "",
                            genre ?? "", fileType ?? "", duration, language ?? "");
        }

        private static VideoGame? CreateVideoGameFromInput()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Genre: ");
            var genre = Console.ReadLine();
            Console.Write("Publisher: ");
            var publisher = Console.ReadLine();
            Console.Write("Release year: ");
            if (!int.TryParse(Console.ReadLine(), out int year)) return null;
            Console.Write("Platforms: ");
            var platforms = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title)) return null;

            return new VideoGame(title!, genre ?? "", publisher ?? "", year, platforms ?? "");
        }

        private static App? CreateAppFromInput()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Version: ");
            var version = Console.ReadLine();
            Console.Write("Publisher: ");
            var publisher = Console.ReadLine();
            Console.Write("Platforms: ");
            var platforms = Console.ReadLine();
            Console.Write("File size: ");
            var fileSize = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title)) return null;

            return new App(title!, version ?? "", publisher ?? "", platforms ?? "", fileSize ?? "");
        }

        private static Podcast? CreatePodcastFromInput()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Release year: ");
            if (!int.TryParse(Console.ReadLine(), out int year)) return null;
            Console.Write("Hosts: ");
            var hosts = Console.ReadLine();
            Console.Write("Guests: ");
            var guests = Console.ReadLine();
            Console.Write("Episode number: ");
            if (!int.TryParse(Console.ReadLine(), out int ep)) return null;
            Console.Write("Language: ");
            var language = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title)) return null;

            return new Podcast(title!, year, hosts ?? "", guests ?? "", ep, language ?? "");
        }

        private static ImageFile? CreateImageFromInput()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Resolution: ");
            var resolution = Console.ReadLine();
            Console.Write("File format: ");
            var format = Console.ReadLine();
            Console.Write("File size: ");
            var fileSize = Console.ReadLine();
            Console.Write("Date taken: ");
            var dateTaken = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title)) return null;

            return new ImageFile(title!, resolution ?? "", format ?? "", fileSize ?? "", dateTaken ?? "");
        }
    }

    public static class UserRepository
    {
        private static readonly List<User> _users = new();

        public static IEnumerable<User> GetAll() => _users;

        public static void Add(User user)
        {
            _users.Add(user);
        }

        public static User? FindByNameAndSSN(string name, string ssn)
        {
            return _users.FirstOrDefault(u =>
                u.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && u.SSN == ssn);
        }

        public static bool Remove(User user)
        {
            return _users.Remove(user);
        }
    }
}
