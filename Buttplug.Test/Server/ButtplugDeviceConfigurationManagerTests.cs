﻿// <copyright file="ButtplugDeviceTests.cs" company="Nonpolynomial Labs LLC">
// Buttplug C# Source Code File - Visit https://buttplug.io for more info about the project.
// Copyright (c) Nonpolynomial Labs LLC. All rights reserved.
// Licensed under the BSD 3-Clause license. See LICENSE file in the project root for full license information.
// </copyright>

// Test file, disable ConfigureAwait checking.
// ReSharper disable ConsiderUsingConfigureAwait

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Buttplug.Devices;
using FluentAssertions;
using NUnit.Framework;

namespace Buttplug.Server.Test
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Test classes can skip documentation requirements")]
    [TestFixture]
    public class ButtplugDeviceConfigurationManagerTests
    {
        [Test]
        public void TestBuiltinProtocolLoading()
        {
            var buttplugAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(aAssembly => aAssembly.GetName().Name == "Buttplug");
            buttplugAssembly.Should().NotBeNull();
            var types = buttplugAssembly.GetTypes()
                .Where(aType => aType.IsClass && aType.Namespace == "Buttplug.Devices.Protocols" &&
                            typeof(IButtplugDeviceProtocol).IsAssignableFrom(aType)).ToList();
            types.Any().Should().BeTrue();

            // todo Create a derived version of the DeviceConfigurationManager and check against the protocols we add
            /*
            var b = new TestBluetoothSubtypeManager(new ButtplugLogManager());
            var d = b.GetDefaultDeviceInfoList();
            foreach (var t in types)
            {
                d.Any(aInfoObj => aInfoObj.GetType() == t).Should().BeTrue();
            }
            */
        }
    }
}
