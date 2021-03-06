﻿using System.Data.Objects;
using System.Management.Automation;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Analytics
{
    [Cmdlet(VerbsCommon.Get, "AnalyticsVisitorClassification")]
    [OutputType(new[] {typeof (VisitorClassifications)})]
    public class GetAnalyticsVisitorClassificationsCommand : AnalyticsBaseCommand
    {
        protected override void ProcessRecord()
        {
            ObjectQuery<VisitorClassifications> visitorClassifications = Context.VisitorClassifications;
            PipeQuery(visitorClassifications);
        }
    }
}