﻿namespace Rabobank.Compliancy.Infra.AzdoClient.Response;

public class RetentionSettings
{
    public int DaysToKeepDeletedReleases { get; set; }
    public RetentionPolicy DefaultEnvironmentRetentionPolicy { get; set; }
    public RetentionPolicy MaximumEnvironmentRetentionPolicy { get; set; }
}