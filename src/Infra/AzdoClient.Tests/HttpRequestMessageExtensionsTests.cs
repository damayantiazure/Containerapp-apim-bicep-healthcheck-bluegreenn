using System;
using System.Net.Http;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Shouldly;
using Xunit;
using Rabobank.Compliancy.Infra.AzdoClient.Extensions;

namespace Rabobank.Compliancy.Infra.AzdoClient.Tests;

public class HttpRequestMessageExtensionsTests
{
    private readonly Fixture _fixture = new Fixture();
        
    [Fact]
    public void ExtMgtRequestShouldReturnTrue()
    {
        _fixture.Customize(new AutoNSubstituteCustomization());
            
        var org = "raboweb-test";
        var request = _fixture.Create<HttpRequestMessage>();
        request.RequestUri = new Uri($"https://extmgmt.dev.azure.com/{org}/blablabla");
        request.IsExtMgtRequest(org).ShouldBeTrue();
    }
        
    [Fact]
    public void OtherRequestShouldReturnFalse()
    {
        _fixture.Customize(new AutoNSubstituteCustomization());
            
        var org = "raboweb-test";
        var request = _fixture.Create<HttpRequestMessage>();
        request.RequestUri = new Uri($"https://dev.azure.com/{org}/blabla");
        request.IsExtMgtRequest(org).ShouldBeFalse();
    }
}