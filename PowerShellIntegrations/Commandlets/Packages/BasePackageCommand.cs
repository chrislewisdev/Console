﻿using System;
using System.IO;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;
using Sitecore;
using Sitecore.Data.Engines;
using Sitecore.Data.Proxies;
using Sitecore.Exceptions;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using Sitecore.Shell.Applications.Install;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Packages
{
    public class BasePackageCommand : BaseCommand
    {
        protected static string PackagePath
        {
            get { return ApplicationContext.PackagePath; }
        }

        protected static string PackageProjectPath
        {
            get { return ApplicationContext.PackageProjectPath; }
        }


        protected static string FullPackagePath(string packageFileName)
        {
            return Path.Combine(PackagePath, packageFileName);
        }

        protected static string FullPackageProjectPath(string packageFileName)
        {
            return Path.Combine(PackageProjectPath, packageFileName);
        }

        protected void PerformInstallAction(Action action)
        {
            if (!Directory.Exists(PackagePath))

            {
                WriteError(new ErrorRecord(
                    new ClientAlertException(
                        string.Format(
                            Translate.Text(
                                "Cannot access path '{0}'. Please check PackagePath setting in the web.config file."),
                            PackagePath)), "sitecore_package_folder_does_not_exist", ErrorCategory.ObjectNotFound, null));
                return;
            }
            if (action != null)
            {
                using (new SecurityDisabler())
                {
                    using (new ProxyDisabler())
                    {
                        using (new SyncOperationContext())
                        {
                            Context.SetActiveSite("shell");
                            action();
                            Context.SetActiveSite("website");
                        }
                    }
                }
            }
        }
    }
}