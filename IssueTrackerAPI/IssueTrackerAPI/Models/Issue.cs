using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTrackerAPI.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(1024)")]
        public string Description { get; set; }
        
        public int Priority { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DueDate { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
