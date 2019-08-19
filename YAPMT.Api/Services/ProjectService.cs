using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using YAPMT.Api.Models;

namespace YAPMT.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMongoCollection<Project> _projects;


        public ProjectService(IProjectManagementConfigs configs)
        {
            var client = new MongoClient(configs.ConnectionString);
            var database = client.GetDatabase(configs.DatabaseName);

            _projects = database.GetCollection<Project>(configs.ProjectsCollectionName);
        }

        public Project Create(Project project)
        {
            project.Id = new ObjectId();
            project.CreatedAt = DateTime.Now;

            _projects.InsertOne(project);

            return project;
        }

        public Project CreateTask(string projectId, ProjectTask task)
        {
            var set = Builders<Project>.Update.Push<ProjectTask>(_ => _.Tasks, task);

            _projects.UpdateOne(_ => _.Id == new ObjectId(projectId), set);

            return _projects.Find(_ => _.Id == new ObjectId(projectId)).First();
        }

        public void DeleteProject(string projectId)
        {
            _projects.DeleteOne(_ => _.Id == new ObjectId(projectId));
        }

        public List<Project> GetProjects()
        {
            return _projects.Find(p => true).ToList();
        }

        public Project GetProject(string id)
        {
            return _projects.Find(_ => _.Id == new ObjectId(id)).First();
        }

        public Project UpdateTask(string projectdId, ProjectTask task)
        {
            var project = _projects.Find<Project>(_ => _.Id == new ObjectId(projectdId)).First();
            var currentTask = project.Tasks.FirstOrDefault(_ => _.Id == task.Id);

            if(project != null)
            {
                var pull = Builders<Project>.Update.Pull<ProjectTask>(_ => _.Tasks, currentTask);
                _projects.UpdateOne(_ => _.Id == new ObjectId(projectdId), pull);
            }

            var set = Builders<Project>.Update.Push<ProjectTask>(_ => _.Tasks, task);
            _projects.UpdateOne(_ => _.Id == new ObjectId(projectdId), set);

            return _projects.Find<Project>(_ => _.Id == new ObjectId(projectdId)).First();
        }
    }
}
