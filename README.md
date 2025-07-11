# MovieCollectionManager 🎬
![Image 21 06 2025 г , 14_05_44](https://github.com/user-attachments/assets/b27b306b-b93d-4f1d-b341-cde3922e1eab)

# 🎬 MovieCollectionManager

**MovieCollectionManager** is a desktop application built with C# (.NET) for managing a personal collection of movies. It features a graphical user interface (WinForms or WPF) and allows users to add, edit, search, and delete movies with ease.

/MovieCollectionManager
├── /MovieCollectionManager.App # GUI layer (WinForms/WPF)
│ ├── Forms/Views/... # Form files or XAML views
│ ├── App.xaml / Program.cs
├── /MovieCollectionManager.Core # Core logic and models
│ ├── Models/ # Movie, Genre, Rating, etc.
│ ├── Services/ # MovieService, DataService, etc.
├── /MovieCollectionManager.Tests # Unit tests
│ ├── CoreTests/
└── README.md # This file

---

## 🧰 Technologies Used

- **.NET 6 / .NET 7**
- **C#**
- **WinForms / WPF**
- **Unit Testing** with MSTest / NUnit / xUnit
- (Optional) **Data Storage**: JSON or SQLite

---

## ✨ Features

- ✅ Add a new movie (title, director, year, genre, rating, description)
- ✅ Edit existing movie details
- ✅ Delete movies from the list
- ✅ Search by title, genre, or year
- ✅ (Optional) Save and load data locally (JSON or SQLite)

---

## 🔍 Example of `Movie` Class

```csharp
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public double Rating { get; set; }
    public string Description { get; set; }
}
🧠 Architecture Overview
Layer	Responsibility
Core	Models and business logic (Movie, Services, Search)
App	User interface layer – WinForms or WPF
Tests	Unit tests for validating logic and behavior

The project follows the Separation of Concerns principle by decoupling business logic from the UI.

🛠️ Example of MovieService

public class MovieService : IMovieService
{
    private readonly List<Movie> _movies = new();

    public void AddMovie(Movie m) => _movies.Add(m);
    public IEnumerable<Movie> GetAll() => _movies;
    public Movie GetById(int id) => _movies.FirstOrDefault(m => m.Id == id);

    public void UpdateMovie(Movie updated)
    {
        var existing = GetById(updated.Id);
        if (existing != null)
        {
            existing.Title = updated.Title;
            existing.Director = updated.Director;
            existing.Year = updated.Year;
            existing.Genre = updated.Genre;
            existing.Rating = updated.Rating;
            existing.Description = updated.Description;
        }
    }

    public void DeleteMovie(int id) => _movies.RemoveAll(m => m.Id == id);

    public IEnumerable<Movie> Search(string title = null, string genre = null)
    {
        return _movies.Where(m =>
            (string.IsNullOrEmpty(title) || m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(genre) || m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)));
    }
}
🧪 Unit Testing Example

[TestClass]
public class MovieServiceTests
{
    [TestMethod]
    public void AddMovie_ShouldIncreaseCount()
    {
        var service = new MovieService();
        var movie = new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi" };

        service.AddMovie(movie);
        Assert.AreEqual(1, service.GetAll().Count());
    }

    [TestMethod]
    public void SearchByGenre_ShouldReturnCorrectMovie()
    {
        var service = new MovieService();
        service.AddMovie(new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi" });
        service.AddMovie(new Movie { Id = 2, Title = "Titanic", Genre = "Romance" });

        var results = service.Search(genre: "Sci-Fi");
        Assert.AreEqual(1, results.Count());
        Assert.AreEqual("Inception", results.First().Title);
    }
}
📥 How to Run the Project
Clone the repository:


git clone https://github.com/antoniq3298/MovieCollectionManager.git
Open the solution file (.sln) in Visual Studio

Set MovieCollectionManager.App as the startup project

Press F5 to run

For testing:

Open the Test Explorer and run all tests

Or via terminal:


dotnet test
💾 Data Storage (Optional)
You can add persistence using:

JSON Files:

Use System.Text.Json or Newtonsoft.Json

Methods like SaveToFile() and LoadFromFile() can serialize/deserialize data

SQLite Database:

Use Microsoft.Data.Sqlite or EF Core

Create a table for movies and map the fields with Entity Framework or SQL scripts

🚧 Possible Improvements
Filtering by year, director, or rating

Undo/Redo functionality

Import/export to CSV or Excel

Connect to a public movie API (like TMDB) for metadata

🐛 Troubleshooting
Issue	Solution
Movies not refreshing in UI	Use BindingList<T> or proper data binding
Test count inconsistent	Make sure you reset service between tests
App doesn't save data	Ensure JSON/SQLite storage is implemented and paths are correct

🤝 Contributing
Fork the repo

Create a new branch (feature/my-feature)

Commit your changes

Push and open a pull request

📄 License
This project is licensed under the MIT License – see the LICENSE file for details.

📬 Contact
📧 Email: antoniqmml@mail.bg

🔗 LinkedIn: Antonia Ivanova

⭐ Star this repo if you find it useful or want to support the project!

