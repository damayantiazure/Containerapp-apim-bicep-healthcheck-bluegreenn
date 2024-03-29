﻿using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json.Linq;
using System;

namespace Rabobank.Compliancy.Functions.ComplianceScanner.Batch.Model;

public class Orchestrator
{
    public string Name { get; set; }
    public string InstanceId { get; set; }
    public DateTime CreatedTime { get; set; }
    public OrchestrationRuntimeStatus RuntimeStatus { get; set; }
    public JToken CustomStatus { get; set; }
}