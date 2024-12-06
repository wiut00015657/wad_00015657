﻿namespace IssueTrackerAPI.DTOs
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }

        public int EmployeeId { get; set; }

        public EmployeeDTO? Employee { get; set; }
    }
}
