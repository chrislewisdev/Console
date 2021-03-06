﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Globalization;
using Sitecore.Publishing;
using Sitecore.Publishing.Pipelines.PublishItem;
using Sitecore.Shell.Framework.Commands;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Data
{
    [Cmdlet(VerbsCommon.Get, "ItemField")]
    [OutputType(new[] {typeof (TemplateField), typeof (string), typeof (Field)},
        ParameterSetName = new[] {"Item from Pipeline", "Item from Path", "Item from ID"})]
    public class GetItemFieldCommand : BaseItemCommand
    {
        public enum ReturnValue
        {
            Name,
            Field,
            TemplateField
        }

        [Parameter]
        public SwitchParameter IncludeStandardFields { get; set; }

        [Parameter]
        public ReturnValue ReturnType { get; set; }

        [Parameter]
        [Alias("FieldName")]
        public string[] Name { get; set; }

        protected List<WildcardPattern> NameWildcardPatterns { get; private set; }

        protected override void BeginProcessing()
        {
            Language = null;
            base.BeginProcessing();

            if (Name != null && Name.Any())
            {
                NameWildcardPatterns =
                    Name.Select(
                        name =>
                            new WildcardPattern(name, WildcardOptions.IgnoreCase | WildcardOptions.CultureInvariant))
                        .ToList();
            }
        }

        protected override void ProcessItem(Item item)
        {
            item.Fields.ReadAll();
            Template template =
                TemplateManager.GetTemplate(Sitecore.Configuration.Settings.DefaultBaseTemplate,
                    item.Database);
            foreach (Field field in item.Fields)
            {
                if (NameWildcardPatterns != null)
                {
                    bool wildcardMatch = false;
                    foreach (var nameWildcardPattern in NameWildcardPatterns)
                    {
                        if (nameWildcardPattern.IsMatch(field.Name))
                        {
                            wildcardMatch = true;
                            break;
                        }
                    }
                    if (!wildcardMatch)
                    {
                        continue;
                    }

                }
                if (!IncludeStandardFields && template.ContainsField(field.ID))
                {
                    continue;
                }

                switch (ReturnType)
                {
                    case (ReturnValue.Field):
                        WriteObject(field);
                        break;
                    case (ReturnValue.TemplateField):
                        WriteObject(field.GetTemplateField());
                        break;
                    default:
                        WriteObject(field.Name);
                        break;
                }
            }
        }
    }
}