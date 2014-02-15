﻿/*
 * ClamAV.Managed.PowerShell - Managed bindings for ClamAV - PowerShell cmdlets
 * Copyright (C) 2011, 2013-2014 Rupert Muchembled
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ClamAV.Managed.PowerShell
{
    /// <summary>
    /// Cmdlet wrapping the ClamEngine.ScanFile() method.
    /// </summary>
    [Cmdlet("Scan", "File")]
    public class ScanFileCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "ClamEngine created by New-ClamEngine.")]
        public ClamEngine Engine { get; set; }

        [Parameter(Mandatory = true, Position = 0, HelpMessage = "Path to the file to scan.")]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            string virusName;

            var scanResult = Engine.ScanFile(Path, out virusName);

            var fileScanResult = new FileScanResult
            {
                Path = Path,
                Infected = scanResult == ScanResult.Virus,
                VirusName = virusName
            };

            WriteObject(fileScanResult);
        }
    }
}