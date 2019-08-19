using System.Collections.Generic;
using System.Linq;
using YAPMT.Api.Models;

namespace YAPMT.Api.Mappers
{
    public class ProjectMappingService : IMapper<Project, Project>
    {
        public Project MapProject(Project source)
        {
            return new Project
            {
                Id = source.Id,
                Name = source.Name,
                CreatedAt = source.CreatedAt,
                Tasks = source.Tasks.OrderBy(_ => _.DueDate).ToList()
            };
            
        }

        public List<Project> MapProjects(List<Project> source)
        {
            List<Project> projects = new List<Project>();
            foreach(var sourceElement in source)
            {
                Project project = MapProject(sourceElement);
                projects.Add(project);
            }

            return projects.OrderBy(_ => _.CreatedAt).ToList();
        }
    }
}
