using AutoMapper;
using IssueTrackerAPI.Models;
using IssueTrackerAPI.DTOs;

namespace IssueTrackerAPI.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<IssueDTO, Issue>().ReverseMap();
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
        }
    }
}
