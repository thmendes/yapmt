namespace YAPMT.Api.Models
{
    public class ProjectManagementConfigs : IProjectManagementConfigs
    {
        public string ProjectsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
