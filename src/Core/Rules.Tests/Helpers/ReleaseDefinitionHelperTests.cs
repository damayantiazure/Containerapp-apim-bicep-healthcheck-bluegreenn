﻿using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Rabobank.Compliancy.Core.Rules.Helpers;

namespace Rabobank.Compliancy.Core.Rules.Tests.Helpers;

public class ReleaseDefinitionHelperTests
{
    [Fact]
    public void GetArtifactAliases_GitAndBuildArtifactsPresent_OnlyTheseAreReturned()
    {
        // arrange
        var definition = JObject.FromObject(new
        {
            artifacts = new[]
            {
                new
                {
                    type = "Git",
                    alias = "_AzureRepo"
                },
                new
                {
                    type = "Build",
                    alias = "_SOx-compliant-demo-ASP.NET Core-CI"
                },
                new
                {
                    type = "SomeOtherType",
                    alias = "_SOx-compliant-demo-ASP.NET Core-CI"
                }
            }
        });

        // act
        var aliases = ClassicReleasePipelineHelper.GetArtifactAliases(definition).ToList();

        // assert
        aliases.ShouldBe(new[] {"_SOx-compliant-demo-ASP.NET Core-CI"});
    }

    [Fact]
    public void GetArtifactAliases_NoArtifactsAreDefined_NoneAreReturned()
    {
        // arrange
        var definition = JObject.FromObject(new
        {
            artifacts = Array.Empty<object>()
        });

        // act
        var aliases = ClassicReleasePipelineHelper.GetArtifactAliases(definition);

        // assert
        aliases.Count().ShouldBe(0);
    }

    [Fact]
    public void AddConditionToEnvironments_AddConditionToEmptyConditions_ShouldBePresent()
    {
        // arrange
        var definition = JObject.FromObject(new
        {
            environments = new[]
            {
                new
                {
                    id = 1,
                    conditions = Array.Empty<object>()
                }
            }
        });

        // act
        ClassicReleasePipelineHelper.AddConditionToEnvironments(definition, "_SOx-compliant-demo-ASP.NET Core-CI", "1");

        // assert
        definition.SelectTokens("environments[*].conditions[?(@.conditionType == 'artifact')]").ShouldNotBeEmpty();
    }

    [Fact]
    public void AddConditionToEnvironments_AddConditionToNonEmptyConditions_ShouldBePresent()
    {
        // arrange
        var definition = JObject.FromObject(new
        {
            environments = new[]
            {
                new
                {
                    id = "1",
                    conditions = new[]
                    {
                        new
                        {
                            name = "Condition1",
                            conditionType = "test"
                        }
                    }
                }
            }
        });

        // act
        ClassicReleasePipelineHelper.AddConditionToEnvironments(definition, "_SOx-compliant-demo-ASP.NET Core-CI", "1");

        // assert
        definition.SelectTokens("environments[*].conditions[?(@.conditionType == 'test')]").ShouldNotBeEmpty();
    }

    [Fact]
    public void AddConditionToEnvironments_AddConditionWithNonExistingEnvironmentId_ShouldNotBeAdded()
    {
        // arrange
        var definition = JObject.FromObject(new
        {
            environments = new[]
            {
                new
                {
                    id = 2,
                    conditions = new[]
                    {
                        new
                        {
                            name = "Condition1",
                            conditionType = "test"
                        }
                    }
                }
            }
        });

        // act
        ClassicReleasePipelineHelper.AddConditionToEnvironments(definition, "_SOx-compliant-demo-ASP.NET Core-CI", "1");

        // assert
        definition.SelectTokens("environments[*].conditions[?(@.conditionType == 'artifact')]").ShouldBeEmpty();
    }

    [Fact]
    public void AddConditionToEnvironments_ConditionAlreadyExists_AddWithSameName_ShouldBeAdded()
    {
        // arrange
        var definition = JObject.FromObject(new
        {
            environments = new[]
            {
                new
                {
                    id = "1",
                    conditions = new[]
                    {
                        new
                        {
                            name = "_SOx-compliant-demo-ASP.NET Core-CI",
                            conditionType = "artifact",
                            value =
                                "{\"sourceBranch\":\"master\",\"tags\":[],\"useBuildDefinitionBranch\":false,\"createReleaseOnBuildTagging\":false}"
                        }
                    }
                }
            }
        });

        // act
        ClassicReleasePipelineHelper.AddConditionToEnvironments(definition, "_SOx-compliant-demo-ASP.NET Core-CI", "1");

        // assert
        definition.SelectTokens("environments[*].conditions[?(@.conditionType == 'artifact')]").Count().ShouldBe(1);
    }

    [Fact]
    public void AddConditionToEnvironments_ConditionExistsOnDifferentBranch_AddOneWithSameName_ShouldBeAdded()
    {
        // arrange
        var definition = JObject.FromObject(new
        {
            environments = new[]
            {
                new
                {
                    id = 1,
                    conditions = new[]
                    {
                        new
                        {
                            name = "_SOx-compliant-demo-ASP.NET Core-CI",
                            conditionType = "artifact",
                            value =
                                "{\"sourceBranch\":\"test\",\"tags\":[],\"useBuildDefinitionBranch\":false,\"createReleaseOnBuildTagging\":false}"
                        }
                    }
                }
            }
        });

        // act
        ClassicReleasePipelineHelper.AddConditionToEnvironments(definition, "_SOx-compliant-demo-ASP.NET Core-CI", "1");

        // assert
        definition.SelectTokens("environments[*].conditions[?(@.conditionType == 'artifact')]").Count().ShouldBe(2);
    }
}