﻿using System.Linq;
using System.Management.Automation;
using Cognifide.PowerShell.Extensions;
using Cognifide.PowerShell.PowerShellIntegrations.Commandlets;
using Sitecore;
using Sitecore.Security.Accounts;

namespace Cognifide.PowerShell.Security
{
    [Cmdlet(VerbsCommon.Get, "Role", DefaultParameterSetName = "Id")]
    [OutputType(new[] {typeof (Role)})]
    public class GetRoleCommand : BaseCommand
    {
        [Alias("Name")]
        [Parameter(ParameterSetName = "Id", ValueFromPipeline = true, Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public AccountIdentity Identity { get; set; }

        [Parameter(ParameterSetName = "Filter", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Filter { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Filter")
            {
                var filter = Filter;
                if (!filter.Contains('?') && !filter.Contains('*')) return;

                var managedRoles = Context.User.Delegation.GetManagedRoles(true);
                WildcardWrite(filter, managedRoles, role => role.Name);
            }
            else
            {
                if (!this.CanFindAccount(Identity, AccountType.Role)) { return; }

                WriteObject(Role.FromName(Identity.Name));
            }
        }
    }
}