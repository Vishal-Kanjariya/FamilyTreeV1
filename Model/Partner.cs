using System.ComponentModel.DataAnnotations;

namespace FamilyTreeV1.Model
{
    public class Partner
    {
        /// <summary>
        /// Father Id
        /// </summary>
        public int? FatherId { get; set; }

        /// <summary>
        /// Mother Id
        /// </summary>
        public int? MotherId { get; set; }
    }
}
