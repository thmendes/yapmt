namespace YAPMT.Api.Models
{
    public interface IProjectManagementConfigs
    {
        string ProjectsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}