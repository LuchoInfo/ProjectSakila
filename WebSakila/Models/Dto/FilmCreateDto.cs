using System.ComponentModel.DataAnnotations;

namespace WebSakila.Models.Dto
{
    public class FilmCreateDto
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(15)]
        public string? Description { get; set; }

        public string? ReleaseYear { get; set; }

        [Required]
        public byte LanguageId { get; set; }

        public byte RentalDuration { get; set; }

        public decimal RentalRate { get; set; }

        public decimal ReplacementCost { get; set; }

        public string? Rating { get; set; }

        public string? SpecialFeatures { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
