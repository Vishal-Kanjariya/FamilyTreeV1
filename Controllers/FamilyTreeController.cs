using FamilyTreeV1.Data;
using FamilyTreeV1.Model;
using Microsoft.AspNetCore.Http;   
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTreeV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyTreeController : ControllerBase
    {
        /// <summary>
        /// Application DbContext object
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public FamilyTreeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Descendants by Identity Number of Person
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns> return Family list </returns>
        [HttpGet("GetDescendants")]
        public ActionResult<IEnumerable<Person>> GetDescendants(string identityNumber)
        {
            var person = _context.People.FirstOrDefault(p => p.IdentityNumber == identityNumber);

            if (person == null)
                return NotFound();

            var descendants = GetDescendantsRecursive(person.Id, 1, 10);

            return Ok(descendants);
        }

        /// <summary>
        /// Get Root Ascendant by Identity Number of Person
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns> return Family list </returns>
        [HttpGet("GetRootAscendant")]
        public ActionResult<Person> GetRootAscendant(string identityNumber)
        {
            var person = _context.People.FirstOrDefault(p => p.IdentityNumber == identityNumber);
            if (person == null)
                return NotFound();

            var rootAscendant = GetRootAscendantRecursive(person);
            return Ok(rootAscendant);
        }

        /// <summary>
        /// Get Descendants Recursive
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="currentLevel"></param>
        /// <param name="maxLevels"></param>
        /// <returns> return Family list </returns>
        private List<Person> GetDescendantsRecursive(int personId, int currentLevel, int maxLevels)
        {
            if (currentLevel > maxLevels)
                return new List<Person>();

            var descendants = _context.People.Where(p => p.FatherId == personId || p.MotherId == personId).ToList();
            var allDescendants = new List<Person>();

            foreach (var descendant in descendants)
            {
                var descendantList = GetDescendantsRecursive(descendant.Id, currentLevel + 1, maxLevels);
                allDescendants.AddRange(descendantList);
            }

            return descendants.Concat(allDescendants).ToList();
        }

        /// <summary>
        /// Get Root Ascendant Recursive
        /// </summary>
        /// <param name="person"></param>
        /// <returns> return Family list </returns>
        private Person GetRootAscendantRecursive(Person person)
        {
            if (person.FatherId == null && person.MotherId == null)
                return person;

            if (person.FatherId != null)
                return GetRootAscendantRecursive(_context.People.Find(person.FatherId));

            if (person.MotherId != null)
                return GetRootAscendantRecursive(_context.People.Find(person.MotherId));

            return null;
        }
    }
}
