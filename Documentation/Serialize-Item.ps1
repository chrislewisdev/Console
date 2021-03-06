<#
    .SYNOPSIS
        Serializes sitecore item to server disk drive.

    .DESCRIPTION
        Serializes sitecore item to server disk drive.
	The simplest command syntax is:
	Serialize-Item -path "master:\content"
	
        or
	
        Get-Item "master:\content" | Serialize-Item
	
	Both of them will serialize the content item in the master database. In first case we pass the path to the item as a parameter, in second case we serialize the items which come from the pipeline. 
        You can send more items from the pipeline to the serialize-item command, e.g. if you need to serialize all the descendants of the home item created by sitecore\admin, you can use:

	Get-Childitem "master:\content\home" -recurse | Where-Object { $_."__Created By" -eq "sitecore\admin" } | Serialize-Item

    .PARAMETER Item
        The item to be serialized.

    .PARAMETER Entry
        Serialization preset to be serialized. Obtain the preset through the use of Get-Preset commandlet

    .PARAMETER Path
        Path to the item to be processed - can work with Language parameter to narrow the publication scope.

    .PARAMETER Id
        You can pass the id of serialized item instead of path, e.g.
        Serialize-Item -id "{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}"

    .PARAMETER Recurse
        Process the item and all of its children - switch which decides if serialization concerns only the single item or the whole tree below the item, e.g.
        
        Serialize-Item -path "master:\content\articles" -recurse

        target - directory where the serialized files should be saved, e.g.
        Serialize-Item -path "master:\content" -target "c:\tmp"

    .PARAMETER ItemPathsAbsolute
        Works only with target parameter and decides if folder structure starting from "sitecore\content" should be created, e.g. if you want to serialize articles item in directory c:\tmp\sitecore\content you can use. For example:
        Serialize-Item -Path "master:\content\articles" -ItemPathsAbsolute -Target "c:\tmp"

    .PARAMETER Target
        Directory where the serialized files should be saved, e.g.
        
        Serialize-Item -Path "master:\content" -Target "c:\tmp"
    
    .INPUTS
        Sitecore.Data.Items.Item
    
    .OUTPUTS        

    .NOTES
        Help Author: Marek Musielak, Adam Najmanowicz, Michael West

    .LINK
        https://github.com/SitecorePowerShell/Console/

    .LINK
        Get-Preset

    .LINK
        Deserialize-Item

    .LINK
        http://www.cognifide.com/blogs/sitecore/serialization-and-deserialization-with-sitecore-powershell-extensions/

    .LINK
        https://www.youtube.com/watch?v=60BGRDNONo0&list=PLph7ZchYd_nCypVZSNkudGwPFRqf1na0b&index=7

    .LINK
        https://gist.github.com/AdamNaj/6c86f61510dc3d2d8b2f

    .LINK
        http://stackoverflow.com/questions/20266841/sitecore-powershell-deserialization

    .LINK
        http://stackoverflow.com/questions/20195718/sitecore-serialization-powershell

    .LINK
        http://stackoverflow.com/questions/20283438/sitecore-powershell-deserialization-core-db
        
    .EXAMPLE
        PS master:\> Serialize-Item -Path master:\content\home
#>
