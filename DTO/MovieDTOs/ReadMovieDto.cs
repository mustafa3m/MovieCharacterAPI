namespace MovieCharacterAPI.DTO.MovieDTOs
{
    public class ReadMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public int FranchiseId { get; set; }
    }
}
