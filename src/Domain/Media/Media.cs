using System;

namespace Domain.Media
{
    public abstract class Media
    {
        public string Title { get; }
        public string MediaType { get; }

        protected Media(string mediaType, string title)
        {
            MediaType = mediaType;
            Title = title;
        }

        public virtual void Download()
        {
            Console.WriteLine("Your file has been downloaded successfully.");
        }

        public override string ToString()
        {
            return $"{MediaType}: {Title}";
        }
    }

    public class EBook : Media
    {
        public string Author { get; }
        public string Language { get; }
        public int NumPages { get; }
        public int YearOfPublication { get; }
        public string ISBN { get; }

        public EBook(string title, string author, string language,
                     int numPages, int yearOfPublication, string isbn)
            : base("EBook", title)
        {
            Author = author;
            Language = language;
            NumPages = numPages;
            YearOfPublication = yearOfPublication;
            ISBN = isbn;
        }

        public void View()
        {
            Console.WriteLine($"You are viewing the EBook \"{Title}\".");
        }

        public void Read()
        {
            Console.WriteLine($"You are reading the EBook \"{Title}\".");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Author: {Author} | Language: {Language} | Pages: {NumPages} | Year: {YearOfPublication} | ISBN: {ISBN}";
        }
    }

    public class Movie : Media
    {
        public string Director { get; }
        public string Genres { get; }
        public int ReleaseYear { get; }
        public string Language { get; }
        public int Duration { get; }

        public Movie(string title, string director, string genres,
                     int releaseYear, string language, int duration)
            : base("Movie", title)
        {
            Director = director;
            Genres = genres;
            ReleaseYear = releaseYear;
            Language = language;
            Duration = duration;
        }

        public void Watch()
        {
            Console.WriteLine($"You are watching the movie \"{Title}\".");
        }

        public override void Download()
        {
            Console.WriteLine($"You downloaded the movie \"{Title}\".");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Director: {Director} | Genres: {Genres} | Year: {ReleaseYear} | Language: {Language} | Duration: {Duration} min";
        }
    }

    public class Song : Media
    {
        public string Composer { get; }
        public string Singer { get; }
        public string Genre { get; }
        public string FileType { get; }
        public int Duration { get; }
        public string Language { get; }

        public Song(string title, string composer, string singer,
                    string genre, string fileType, int duration, string language)
            : base("Song", title)
        {
            Composer = composer;
            Singer = singer;
            Genre = genre;
            FileType = fileType;
            Duration = duration;
            Language = language;
        }

        public void Play()
        {
            Console.WriteLine($"The song \"{Title}\" is playing.");
        }

        public override void Download()
        {
            Console.WriteLine($"You downloaded the song \"{Title}\".");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Composer: {Composer} | Singer: {Singer} | Genre: {Genre} | Type: {FileType} | Duration: {Duration} s | Language: {Language}";
        }
    }

    public class VideoGame : Media
    {
        public string Genre { get; }
        public string Publisher { get; }
        public int ReleaseYear { get; }
        public string Platforms { get; }

        public VideoGame(string title, string genre, string publisher,
                         int releaseYear, string platforms)
            : base("VideoGame", title)
        {
            Genre = genre;
            Publisher = publisher;
            ReleaseYear = releaseYear;
            Platforms = platforms;
        }

        public void Play()
        {
            Console.WriteLine($"The videogame \"{Title}\" is playing.");
        }

        public void Complete()
        {
            Console.WriteLine($"You completed the videogame \"{Title}\".");
        }

        public override void Download()
        {
            Console.WriteLine($"You downloaded the videogame \"{Title}\".");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Genre: {Genre} | Publisher: {Publisher} | Year: {ReleaseYear} | Platforms: {Platforms}";
        }
    }

    public class App : Media
    {
        public string Version { get; }
        public string Publisher { get; }
        public string Platforms { get; }
        public string FileSize { get; }

        public App(string title, string version, string publisher,
                   string platforms, string fileSize)
            : base("App", title)
        {
            Version = version;
            Publisher = publisher;
            Platforms = platforms;
            FileSize = fileSize;
        }

        public void Execute()
        {
            Console.WriteLine($"You are executing the app \"{Title}\".");
        }

        public override void Download()
        {
            Console.WriteLine($"You downloaded the app \"{Title}\".");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Version: {Version} | Publisher: {Publisher} | Platforms: {Platforms} | Size: {FileSize}";
        }
    }

    public class Podcast : Media
    {
        public int ReleaseYear { get; }
        public string Hosts { get; }
        public string Guests { get; }
        public int EpisodeNumber { get; }
        public string Language { get; }

        public Podcast(string title, int releaseYear, string hosts,
                       string guests, int episodeNumber, string language)
            : base("Podcast", title)
        {
            ReleaseYear = releaseYear;
            Hosts = hosts;
            Guests = guests;
            EpisodeNumber = episodeNumber;
            Language = language;
        }

        public void Listen()
        {
            Console.WriteLine($"You are listening to the podcast \"{Title}\".");
        }

        public void Complete()
        {
            Console.WriteLine($"You completed the podcast \"{Title}\".");
        }

        public override void Download()
        {
            Console.WriteLine($"You downloaded the podcast \"{Title}\".");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Year: {ReleaseYear} | Hosts: {Hosts} | Guests: {Guests} | Episode: {EpisodeNumber} | Language: {Language}";
        }
    }

    public class ImageFile : Media
    {
        public string Resolution { get; }
        public string FileFormat { get; }
        public string FileSize { get; }
        public string DateTaken { get; }

        public ImageFile(string title, string resolution, string fileFormat,
                         string fileSize, string dateTaken)
            : base("Image", title)
        {
            Resolution = resolution;
            FileFormat = fileFormat;
            FileSize = fileSize;
            DateTaken = dateTaken;
        }

        public void Display()
        {
            Console.WriteLine($"The image \"{Title}\" is displayed.");
        }

        public override void Download()
        {
            Console.WriteLine($"The image \"{Title}\" is downloaded.");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Resolution: {Resolution} | Format: {FileFormat} | Size: {FileSize} | Date taken: {DateTaken}";
        }
    }
}
