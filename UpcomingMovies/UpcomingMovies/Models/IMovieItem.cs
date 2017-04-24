namespace UpcomingMovies.Models
{
    public interface IMovieItem
    {
        string PosterPath { get; }
        string PosterThumbPath { get; }
        bool Adult { get; }
        string Overview { get; }
        string ReleaseDate { get; }
        int Id { get; }
        string OriginalTitle { get; }
        string OriginalLanguage { get; }
        string Title { get; }
        string BackdropPath { get; }
        float Popularity { get; }
        int VoteCount { get; }
        float VoteAverage { get; }
        int Index { get; }
        string Genres { get; }
        string MovieUrl { get; }
    }
}
