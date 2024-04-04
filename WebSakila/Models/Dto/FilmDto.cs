using System.ComponentModel.DataAnnotations;

namespace WebSakila.Models.Dto
{
    public class FilmDto
    {
        [Key]
        public int FilmId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? ReleaseYear { get; set; }

        public byte LanguageId { get; set; }

        public byte? OriginalLanguageId { get; set; }

        public byte RentalDuration { get; set; }

        public decimal RentalRate { get; set; }

        public decimal ReplacementCost { get; set; }

        public string? Rating { get; set; }

        public string? SpecialFeatures { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}
