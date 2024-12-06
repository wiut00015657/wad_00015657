using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTrackerAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id {  get; set; }
        [Column(TypeName = "nvarchar(512)")]
        public string FullName { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        [Obsolete]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Position { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}
