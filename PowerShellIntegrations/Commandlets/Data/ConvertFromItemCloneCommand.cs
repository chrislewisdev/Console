﻿using System.Management.Automation;

using Sitecore.Data.Items;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Data
{
    [Cmdlet("ConvertFrom", "ItemClone")]
    [OutputType(new[] { typeof(Item) }, ParameterSetName = new[] { "Item from Pipeline", "Item from Path", "Item from ID" })]
    public class ConvertFromItemCloneCommand : BaseLanguageAgnosticItemCommand
    {
        protected override void ProcessItem(Item item)
        {
            if (!item.IsItemClone)
            {
                WriteError
                (
                    new ErrorRecord
                    (
                        new PSArgumentException("The supplied Item is not a clone!"),
                        "supplied_item_is_not_a_clone", 
                        ErrorCategory.InvalidArgument, 
                        null
                    )
                );
                return;
            }

            var clone = new CloneItem(item);
            WriteItem(clone.Unclone());
        }
    }
}