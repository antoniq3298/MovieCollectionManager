using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MovieCollectionManager
{
    public class Database
    {
        private const string ConnectionString = "Data Source=movies.db";

        public Database()
        {
             var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            string tableCmd = @"CREATE TABLE IF NOT EXISTS Movies (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Title TEXT NOT NULL,
                                    Genre TEXT NOT NULL,
                                    Rating INTEGER,
                                    IsWatched INTEGER,
                                    IsFavorite INTEGER
                                );";

             var cmd = new SQLiteCommand(tableCmd, conn);
            cmd.ExecuteNonQuery();
        }

        public void AddMovie(Movie movie)
        {
             var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Movies (Title, Genre, Rating, IsWatched, IsFavorite)
                                VALUES (@title, @genre, @rating, @isWatched, @isFavorite);";
            cmd.Parameters.AddWithValue("@title", movie.Title);
            cmd.Parameters.AddWithValue("@genre", movie.Genre);
            cmd.Parameters.AddWithValue("@rating", movie.Rating);
            cmd.Parameters.AddWithValue("@isWatched", movie.IsWatched ? 1 : 0);
            cmd.Parameters.AddWithValue("@isFavorite", movie.IsFavorite ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();
             var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Movies";
             var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movies.Add(new Movie
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Title = reader["Title"].ToString(),
                    Genre = reader["Genre"].ToString(),
                    Rating = Convert.ToInt32(reader["Rating"]),
                    IsWatched = Convert.ToInt32(reader["IsWatched"]) == 1,
                    IsFavorite = Convert.ToInt32(reader["IsFavorite"]) == 1
                });
            }
            return movies;
        }

        public void DeleteMovie(int id)
        {
             var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Movies WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        private string _connectionString = "Data Source=movies.db";

        public List<Movie> GetMoviesByGenre(string genre)
        {
            var movies = new List<Movie>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT * FROM Movies WHERE Genre = @Genre", connection);
                command.Parameters.AddWithValue("@Genre", genre);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Genre = reader["Genre"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            IsWatched = Convert.ToBoolean(reader["IsWatched"]),
                            IsFavorite = Convert.ToBoolean(reader["IsFavorite"])
                        });
                    }
                }
            }

            return movies;
        }


        public Movie GetMovieById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT * FROM Movies WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Genre = reader["Genre"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            IsWatched = Convert.ToBoolean(reader["IsWatched"]),
                            IsFavorite = Convert.ToBoolean(reader["IsFavorite"])
                        };
                    }
                }
            }

            return null;
        }


        public void UpdateMovie(Movie movie)
        {
             var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Movies SET
                                Title = @title,
                                Genre = @genre,
                                Rating = @rating,
                                IsWatched = @isWatched,
                                IsFavorite = @isFavorite
                                WHERE Id = @id;";
            cmd.Parameters.AddWithValue("@title", movie.Title);
            cmd.Parameters.AddWithValue("@genre", movie.Genre);
            cmd.Parameters.AddWithValue("@rating", movie.Rating);
            cmd.Parameters.AddWithValue("@isWatched", movie.IsWatched ? 1 : 0);
            cmd.Parameters.AddWithValue("@isFavorite", movie.IsFavorite ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", movie.Id);
            cmd.ExecuteNonQuery();
        }
    }
}
