namespace Rabobank.Compliancy.Infra.AzdoClient.Response;

public class EnvironmentYaml
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TeamProjectReference Project { get; set; }
}