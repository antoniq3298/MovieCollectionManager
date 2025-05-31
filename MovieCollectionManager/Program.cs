using System;
using System.Collections.Generic;

namespace MovieCollectionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("🎬 Movie Collection Manager");
                Console.WriteLine("1. Add Movie");
                Console.WriteLine("2. List All Movies");
                Console.WriteLine("3. Update Movie");
                Console.WriteLine("4. Delete Movie");
                Console.WriteLine("5. Mark as Watched / Favorite");
                Console.WriteLine("6. Filter by Genre");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddMovie(db);
                        break;
                    case "2":
                        ListMovies(db.GetAllMovies());
                        break;
                    case "3":
                        UpdateMovie(db);
                        break;
                    case "4":
                        DeleteMovie(db);
                        break;
                    case "5":
                        MarkMovie(db);
                        break;
                    case "6":
                        FilterMovies(db);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void AddMovie(Database db)
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Genre: ");
            var genre = Console.ReadLine();
            Console.Write("Rating (0-10): ");
            var rating = int.TryParse(Console.ReadLine(), out int r) ? r : 0;
            var movie = new Movie { Title = title, Genre = genre, Rating = r };
            db.AddMovie(movie);
            Console.WriteLine("Movie added.");
        }

        static void ListMovies(List<Movie> movies)
        {
            Console.WriteLine("\nMovies:");
            foreach (var m in movies)
            {
                Console.WriteLine($"[{m.Id}] {m.Title} | {m.Genre} | Rating: {m.Rating} | Watched: {m.IsWatched} | Favorite: {m.IsFavorite}");
            }
        }

        static void UpdateMovie(Database db)
        {
            Console.Write("Movie ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("New Title: ");
                var title = Console.ReadLine();
                Console.Write("New Genre: ");
                var genre = Console.ReadLine();
                Console.Write("New Rating: ");
                var rating = int.TryParse(Console.ReadLine(), out int r) ? r : 0;
                var movie = new Movie { Id = id, Title = title, Genre = genre, Rating = r };
                db.UpdateMovie(movie);
                Console.WriteLine("Movie updated.");
            }
        }

        static void DeleteMovie(Database db)
        {
            Console.Write("Movie ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                db.DeleteMovie(id);
                Console.WriteLine("Movie deleted.");
            }
        }

        static void MarkMovie(Database db)
        {
            Console.Write("Movie ID to mark: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Mark as Watched? (y/n): ");
                var watched = Console.ReadLine().ToLower() == "y";
                Console.Write("Mark as Favorite? (y/n): ");
                var favorite = Console.ReadLine().ToLower() == "y";

                var movie = db.GetMovieById(id);
                if (movie != null)
                {
                    movie.IsWatched = watched;
                    movie.IsFavorite = favorite;
                    db.UpdateMovie(movie);
                    Console.WriteLine("Movie marked.");
                }
                else
                {
                    Console.WriteLine("Movie not found.");
                }
            }
        }

        static void FilterMovies(Database db)
        {
            Console.Write("Genre to filter by: ");
            var genre = Console.ReadLine();
            var filtered = db.GetMoviesByGenre(genre);
            ListMovies(filtered);
        }
    }
}
