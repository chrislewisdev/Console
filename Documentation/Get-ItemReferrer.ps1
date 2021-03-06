<#
    .SYNOPSIS
        Get other items linking to the provided item.

    .DESCRIPTION
        Returns all items that link to the item specified with the commandlet parameters. if -ItemLink parameter is used the Commandlet will return links rather than items.


    .PARAMETER ItemLink
        Return ItemLink that define both source and target of a link rather than items linking to the specified item.

    .PARAMETER Item
        The item to be analysed.

    .PARAMETER Path
        Path to the item to be analysed - can work with Language parameter to narrow the publication scope.

    .PARAMETER Id
        Id of the item to be analysed - can work with Language parameter to narrow the publication scope.

    .PARAMETER Database
        Database containing the item to be analysed - can work with Language parameter to narrow the publication scope.

    .PARAMETER Language
        If you need the item in specific Language you can specify it with this parameter. Globbing/wildcard supported.    
    
    .INPUTS
        Sitecore.Data.Items.Item
    
    .OUTPUTS
        Sitecore.Data.Items.Item
        Sitecore.Links.ItemLink

    .NOTES
        Help Author: Adam Najmanowicz, Michael West

    .LINK
        Get-ItemReference

    .LINK
        https://github.com/SitecorePowerShell/Console/

    .EXAMPLE
        PS master:\>Get-ItemReferrer -Path master:\content\home
         
        Name                             Children Languages                Id                                     TemplateName
        ----                             -------- ---------                --                                     ------------
        Home                             True     {en, de-DE, es-ES, pt... {110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9} Sample Item
        Form                             False    {en, de-DE, es-ES, pt... {6D3B4E7D-FEF8-4110-804A-B56605688830} Webcontrol
        news                             True     {en, de-DE, es-ES, pt... {DB894F2F-D53F-4A2D-B58F-957BFAC2C848} Article
        learn-about-oms                  False    {en, de-DE, es-ES, pt... {79ECF4DF-9DB7-430F-9BFF-D164978C2333} Link

    .EXAMPLE
        PS master:\>Get-Item master:\content\home | Get-ItemReferrer -ItemLink
         
        SourceItemLanguage : en
        SourceItemVersion  : 1
        TargetItemLanguage :
        TargetItemVersion  : 0
        SourceDatabaseName : master
        SourceFieldID      : {F685964D-02E1-4DB6-A0A2-BFA59F5F9806}
        SourceItemID       : {110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}
        TargetDatabaseName : master
        TargetItemID       : {110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}
        TargetPath         : /sitecore/content/Home
#>
