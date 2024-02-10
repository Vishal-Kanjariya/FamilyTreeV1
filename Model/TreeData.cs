using System.ComponentModel.DataAnnotations;

namespace FamilyTreeV1.Model
{
    public class TreeData
    {
        /// <summary>
        /// key
        /// </summary>
        public int key { get; set; }

        /// <summary>
        /// parent
        /// </summary>
        public int parent { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        public string? gender { get; set; }
    }
}
