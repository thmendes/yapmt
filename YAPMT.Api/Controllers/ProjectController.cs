using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YAPMT.Api.Mappers;
using YAPMT.Api.Models;
using YAPMT.Api.Services;

namespace YAPMT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IMapper<Project, Project> mappingService;

        public ProjectController(IProjectService projectService, IMapper<Project, Project> mappingService)
        {
            this.projectService = projectService;
            this.mappingService = mappingService;
        }

        [HttpGet("/api/project")]
        public ActionResult<List<Project>> GetProjects()
        {
            var projects = projectService.GetProjects();
            return Ok(mappingService.MapProjects(projects));
        }

        [HttpGet("/api/project/{id}")]
        public ActionResult<Project> GetProject(string id)
        {
            var project = projectService.GetProject(id);
            return Ok(mappingService.MapProject(project));
        }

        [HttpPost("/api/project")]
        public ActionResult<Project> CreateProject([FromBody] Project project)
        {
            var projectCreated = projectService.Create(project);
            return Ok(mappingService.MapProject(projectCreated));
        }

        [HttpDelete("/api/project/{projectId}")]
        public ActionResult DeleteProject(string projectId)
        {
            projectService.DeleteProject(projectId);
            return Ok();
        }

        [HttpPost("/api/project/{project}/task")]
        public ActionResult<Project> CreateTask(string project, [FromBody] ProjectTask task)
        {
            var updatedProject = projectService.CreateTask(project, task);
            return Ok(mappingService.MapProject(updatedProject));
        }

        [HttpPut("/api/project/{projectId}/task")]
        public ActionResult<Project> UpdateTask(string projectId, [FromBody] ProjectTask task)
        {
            var project = projectService.UpdateTask(projectId, task);
            return Ok(mappingService.MapProject(project));
        }
    }
}
