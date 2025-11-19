# oop-workshop
debugging ninjas🥷🥷🥷

```mermaid
classDiagram
    class Users {
        +id : int
        +title : string
        +type : string
        --
        +previewDetails()
        +listByType()
    }

    class Borrower {
        +borrowerId : int
        --
        +listItemsByType()
        +selectItem()
        +previewItemDetails()
        +rateItem()
        +performMediaAction()
    }

    class Employee {
        +employeeId : int
        --
        +addMediaItem()
        +removeMediaItem()
        +manageCollection()
    }

    class Admin {
        +adminId : int
        --
        +manageBorrowers()
        +manageEmployees()
        +createUser()
        +updateUser()
        +deleteUser()
        +viewUser()
    }

    Users <|-- Borrower
    Users <|-- Employee
    Users <|-- Admin

    
    class Media {
        +title : string
        +language : string
        +download()
    }

    %% ----- EBOOK -----
    class EBook {
        +author : string
        +numPages : int
        +yearOfPublication : int
        +ISBN : string
        --
        +view()
        +read()
        +download()
    }

    %% ----- MOVIE -----
    class Movie {
        +director : string
        +genres : string
        +releaseYear : int
        +duration : int
        --
        +watch()
        +download()
    }

    %% ----- SONG -----
    class Song {
        +composer : string
        +singer : string
        +genre : string
        +fileType : string
        +duration : int
        --
        +play()
        +download()
    }

    %% ----- VIDEO GAME -----
    class VideoGame {
        +genre : string
        +publisher : string
        +releaseYear : int
        +platforms : string
        --
        +download()
        +play()
        +complete()
    }

    %% ----- APP -----
    class App {
        +version : string
        +publisher : string
        +platforms : string
        +fileSize : string
        --
        +download()
        +execute()
    }

    %% ----- PODCAST -----
    class Podcast {
        +releaseYear : int
        +hosts : string
        +guests : string
        +episodeNumber : int
        +language : string
        --
        +listen()
        +download()
        +complete()
    }

    %% ----- IMAGE -----
    class ImageFile {
        +resolution : string
        +fileFormat : string
        +fileSize : string
        +dateTaken : string
        --
        +display()
        +download()
    }

    %% --- INHERITANCE ---
    Media <|-- EBook
    Media <|-- Movie
    Media <|-- Song
    Media <|-- VideoGame
    Media <|-- App
    Media <|-- Podcast
    Media <|-- ImageFile

