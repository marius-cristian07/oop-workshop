
public class Media
{
    public String title;
    public String language;

    public void download()
    {
        Console.WriteLine("Your file has been downloaded succesfully");
    }
}

public class EBook : Media
{
    public String author;
    public int numPages;
    public int yearOfPublication;
    public String ISBN;

    public void view() {
        Console.WriteLine("You are viewing the EBook");
    }

    public void read() {
        Console.WriteLine("You are reading the EBook");
    }

    public EBook(String author, int numPages, int yearOfPublication, String ISBN) {
        this.author = author;
        this.numPages = numPages;
        this.yearOfPublication = yearOfPublication;
        this.ISBN = ISBN;
    }
}

public class Movie : Media
{
    public String director;
    public String genres;
    public int releaseYear;
    public int duration;

    public void watch() {
        Console.WriteLine("You are watching the movie");
    }

    public void download() {
        Console.WriteLine("You downloaded the movie");
    }

    public Movie(String director, String genres, int releaseYear, int duration) {
        this.director = director;
        this.genres = genres;
        this.releaseYear = releaseYear;
        this.duration = duration;

    }

}

public class Song : Media
{
    public String composer;
    public String singer;
    public String genre;
    public String fileType;
    public int duration;

    public void play() {
        Console.WriteLine("The song is playing");
    }

    public void download() {
        Console.WriteLine("You downloaded the song");
    }

    public Song(string composer, string singer, string genre, string fileType, int duration) {
        this.composer = composer;
        this.singer = singer;
        this.genre = genre;
        this.fileType = fileType;
        this.duration = duration;
    }
}

public class VideoGame : Media
{
    public String genre;
    public String publisher;
    public String releaseYear;
    public String platforms;

    public void download() {
        Console.WriteLine("You downloaded the videogame");
    }

    public void play() {
        Console.WriteLine("The videogame is playing");
    }

    public void complete() {
        Console.WriteLine("You completed the videogame");
    }

    public VideoGame(string genre, string publisher, string releaseYear, string platforms) {
        this.genre = genre;
        this.publisher = publisher;
        this.releaseYear = releaseYear;
        this.platforms = platforms;
    }
}

public class App : Media
{
    public String version;
    public String publisher;
    public String platforms;
    public String fileSize;

    public void download() {
        Console.WriteLine("You downloaded the app");
    }

    public void execute() {
        Console.WriteLine("You are executing the app");
    }

    public App(string version, string publisher, string platforms, string fileSize) {
        this.version = version;
        this.publisher = publisher;
        this.platforms = platforms;
        this.fileSize = fileSize;
    }
}

public class Podcast : Media
{
    public int releaseYear;
    public String hosts;
    public String guests;
    public int episodeNumber;
    public String language;

    public void listen() {
        Console.WriteLine("You are listening to the podcast");
    }

    public void download() {
        Console.WriteLine("You downloaded the podcast");
    }

    public void complete() {
        Console.WriteLine("You completed the podcast");
    }

    public Podcast(int releaseYear, String hosts, String guests, int episodeNumber, String language) {
        this.releaseYear = releaseYear;
        this.hosts = hosts;
        this.guests = guests;
        this.episodeNumber = episodeNumber;
        this.language = language;
    }
}

public class ImageFile : Media
{
    public String resolution;
    public String fileFormat;
    public String fileSize;
    public String dateTaken;

    public void display() {
        Console.WriteLine("The ImageFile is displayed");
    }

    public void download() {
        Console.WriteLine("The ImageFile is downloaded");
    }

    public ImageFile(string resolution, string fileFormat, string fileSize, string dateTaken) {
        this.resolution = resolution;
        this.fileFormat = fileFormat;
        this.fileSize = fileSize;
        this.dateTaken = dateTaken;
    }
}

