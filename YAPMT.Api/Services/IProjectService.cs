using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YAPMT.Api.Models;

namespace YAPMT.Api.Services
{
    public interface IProjectService
    {
        List<Project> GetProjects();
        Project GetProject(string id);
        Project Create(Project project);
        void DeleteProject(string projectId);

        Project CreateTask(string projectId, ProjectTask task);
        Project UpdateTask(string projectdId, ProjectTask task);
    }
}
