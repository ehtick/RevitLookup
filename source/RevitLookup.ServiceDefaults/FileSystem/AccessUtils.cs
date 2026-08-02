// Copyright (c) Lookup Foundation and Contributors
// 
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
// 
// THIS PROGRAM IS PROVIDED "AS IS" AND WITH ALL FAULTS.
// NO IMPLIED WARRANTY OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE IS PROVIDED.
// THERE IS NO GUARANTEE THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.

using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace RevitLookup.ServiceDefaults.FileSystem;

/// <summary>
///     Provides methods to check file system access rights.
/// </summary>
public static class AccessUtils
{
    /// <summary>
    ///     Determines whether the current user has write access to the specified path.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns><see langword="true"/> if the current user has write access; otherwise, <see langword="false"/>.</returns>
    public static bool CheckWriteAccess(string path)
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        var accessControl = new DirectoryInfo(path).GetAccessControl();
        var accessRules = accessControl.GetAccessRules(true, true, typeof(NTAccount));
        var writeAccess = false;
        foreach (FileSystemAccessRule rule in accessRules)
        {
            if (principal.IsInRole(rule.IdentityReference.Value) && (rule.FileSystemRights & FileSystemRights.WriteData) == FileSystemRights.WriteData)
            {
                writeAccess = true;
                break;
            }
        }

        return writeAccess;
    }
}