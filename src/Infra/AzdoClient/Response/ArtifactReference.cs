namespace Rabobank.Compliancy.Infra.AzdoClient.Response;

public class ArtifactReference
{
    public string Type { get; set; }
    public string Alias { get; set; }
    public DefinitionReference DefinitionReference { get; set; }
}