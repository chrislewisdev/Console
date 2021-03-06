﻿using System.Collections.Generic;
using System.Management.Automation;
using System.Web.UI;
using Sitecore.Data.Items;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Data
{
    [Cmdlet(VerbsCommon.Get, "ItemTemplate")]
    [OutputType(new[] {typeof (TemplateItem)})]
    public class GetItemTemplateCommand : BaseLanguageAgnosticItemCommand
    {
        [Parameter]
        public SwitchParameter Recurse { get; set; }

        protected override void ProcessItem(Item item)
        {
            if (Recurse)
            {
                Dictionary<string, TemplateItem> templates = new Dictionary<string, TemplateItem>();
                var template = item.Template;
                GetBaseTemplates(template, templates);
                WriteObject(templates.Values, true);
            }
            else
            {
                WriteObject(item.Template, true);
            }
        }

        private void GetBaseTemplates(TemplateItem template, Dictionary<string, TemplateItem> templates)
        {
            if (template != null && !templates.ContainsKey(template.FullName))
            {
                templates.Add(template.FullName, template);
                foreach (var baseTemplate in template.BaseTemplates)
                {
                    GetBaseTemplates(baseTemplate, templates);
                }
            }
        }
    }
}