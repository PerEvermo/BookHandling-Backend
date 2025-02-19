using System.ComponentModel.DataAnnotations;
using BookHandling.Validation;

namespace BookHandling.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Titel är obligatorisk.")]
        [MaxLength(100, ErrorMessage = "Titel får vara max 100 tecken lång.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ0-9 .,'-]+$", ErrorMessage = "Titel innehåller otillåtna tecken.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Författare är obligatorisk.")]
        [MaxLength(75, ErrorMessage = "Författare får vara max 75 tecken lång.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ .,'-]+$", ErrorMessage = "Författarnamn får endast innehålla bokstäver, mellanslag, punkt, apostrof och bindestreck.")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publiceringsdatum måste anges.")]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Publiceringsdatum kan inte vara i framtiden.")]
        public DateTime PublishedDate { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}