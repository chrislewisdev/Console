﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Layouts;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Presentation
{
    [Cmdlet(VerbsCommon.Remove, "Rendering")]
    [OutputType(new[] {typeof (void)})]
    public class RemoveRenderingCommand : BaseRenderingCommand
    {

        protected override void ProcessRenderings(Item item, LayoutDefinition layout, DeviceDefinition device, IEnumerable<RenderingDefinition> renderings)
        {
            foreach (var rendering in renderings)
            {
                var instanceRendering =
                    device.Renderings.Cast<RenderingDefinition>().FirstOrDefault(r => r.UniqueId == rendering.UniqueId);
                if (instanceRendering != null)
                {
                    device.Renderings.Remove(instanceRendering);
                }
            }

            item.Edit(p =>
            {
                string outputXml = layout.ToXml();
                Item["__Renderings"] = outputXml;
            });

        }

    }
}