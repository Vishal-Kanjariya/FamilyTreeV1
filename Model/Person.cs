using System.ComponentModel.DataAnnotations;

namespace FamilyTreeV1.Model
{
    public class Person
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Father Id
        /// </summary>
        public int? FatherId { get; set; }

        /// <summary>
        /// Mother Id
        /// </summary>
        public int? MotherId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 50 characters")]
        public string Surname { get; set; }

        /// <summary>
        /// BirthDate
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Identity Number
        /// </summary>
        [Required(ErrorMessage = "IdentityNumber is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "IdentityNumber must be between 2 and 50 characters")]
        public string IdentityNumber { get; set; }
    }
}
