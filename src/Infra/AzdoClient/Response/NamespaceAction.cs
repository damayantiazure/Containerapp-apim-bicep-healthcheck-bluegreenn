using System;

namespace Rabobank.Compliancy.Infra.AzdoClient.Response;

public class NamespaceAction
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public Guid NamespaceId { get; set; }
    public int Bit { get; set; }
}