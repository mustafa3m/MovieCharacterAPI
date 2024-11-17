namespace MovieCharacterAPI.DTO.MovieDTOs
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public int FranchiseId { get; set; }

        public string Director { get; set; }
    }
}
