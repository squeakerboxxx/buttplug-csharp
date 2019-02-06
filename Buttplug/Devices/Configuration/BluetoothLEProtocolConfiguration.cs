﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Buttplug.Devices.Configuration
{
    public class BluetoothLEProtocolConfiguration : IProtocolConfiguration
    {
        public readonly List<string> Names;
        public readonly Dictionary<Guid, Dictionary<string, Guid>> Services;

        public BluetoothLEProtocolConfiguration(IEnumerable<string> aNames,
            Dictionary<Guid, Dictionary<string, Guid>> aServices = null)
        {
            Names = aNames.ToList();

            Services = aServices ?? new Dictionary<Guid, Dictionary<string, Guid>>();

            // TODO Fail on similarly named characteristics

            // TODO Fail on devices with multiple services without characteristic lists.
            //
            // Should we really do this here though? Lovense has a ton of services but only one will
            // ever be live. Shouldn't this be handled elsewhere, maybe in endpoint setup for devices?
        }

        internal BluetoothLEProtocolConfiguration(BluetoothLEInfo aInfo)
            : this(aInfo.Names, aInfo.Services)
        {
        }

        public BluetoothLEProtocolConfiguration(string aName)
            : this(new[] { aName })
        {
        }

        public bool Matches(IProtocolConfiguration aConfig)
        {
            if (!(aConfig is BluetoothLEProtocolConfiguration btleConfig))
            {
                return false;
            }

            // Right now we only support asterisk as a final character, and treat this as a "starts
            // with" check.
            foreach (var name in Names)
            {
                if (btleConfig.Names.Contains(name))
                {
                    return true;
                }

                if (!name.EndsWith("*"))
                {
                    continue;
                }

                var tempName = name.Substring(0, name.Length - 1);
                foreach (var otherName in btleConfig.Names)
                {
                    if (otherName.StartsWith(tempName))
                    {
                        return true;
                    }
                }
            }

            // TODO Put in advertised service checking, but that hasn't really been needed so far.
            return false;
        }
    }
}