namespace UpcomingMovies.Models
{
    public class MovieItem : IMovieItem
    {
        public string PosterPath { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public int Id { get; set; }
        public string OriginalTitle { get; set; }
        public string OriginalLanguage { get; set; }
        public string Title { get; set; }
        public string BackdropPath { get; set; }
        public float Popularity { get; set; }
        public int VoteCount { get; set; }
        public float VoteAverage { get; set; }
        public int Number { get; set; }
        public string Genres { get; set; }
        public int Index { get; set; }
        public string MovieUrl { get; set; }
        public string PosterThumbPath { get; set; }
    }
}
