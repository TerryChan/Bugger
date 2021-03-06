﻿using BigEgg.Framework.Utils;
using Bugger.PlugIns;
using Bugger.PlugIns.Configs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Bugger.Domain.Test.PlugIns.Configs
{
    [TestClass]
    public class PlugInConfigDocumentTest
    {
        [TestMethod]
        public void SerializeTest()
        {
            var author = new PlugInAuthor()
            {
                Name = "BigEgg",
                EmailAddress = "bigegg@bigegg.com"
            };
            var info = new PlugInInfo()
            {
                PlugInGuid = new Guid("26e54ac9-6286-4991-a687-c8c6b7c50289"),
                Name = "TFS Click",
                Description = "When click the bug open related website",
                Authors = new List<PlugInAuthor>() { author },
                VersionStr = "0.5.0.0",
                MinimumSupportBuggerVersionStr = "0.5.0.0",
                MaximumSupportBuggerVersionStr = "0.5.0.0"
            };
            var dependencyPlugIn = new DependentPlugIn()
            {
                PlugInGuid = new Guid("1dc425b3-c27b-46ba-9623-a046d1acc754"),
                DependentType = DependentType.Mandatory,
                MinimumSupportPlugInVersionStr = "0.5.0.0",
                MaximumSupportPlugInVersionStr = "0.5.0.0"
            };
            var document = new PlugInConfigDocument()
            {
                PlugInInfo = info,
                PlugInType = PlugInType.Click,
                AssemblyNames = new List<string>() { "Bugger.PlugIn.Click.TFSClick" },
                DependentPlugIns = new List<DependentPlugIn>() { dependencyPlugIn }
            };
            var xml = document.ObjectToXElement();

            Assert.AreEqual("Configuration", xml.Name);
            Assert.AreEqual(6, xml.Element("Info").Elements().Count());
            Assert.AreEqual("Click", xml.Element("Type").Value);
            Assert.AreEqual("Name", xml.Element("Assemblies").Elements().First().Name);
            Assert.AreEqual("Bugger.PlugIn.Click.TFSClick", xml.Element("Assemblies").Elements().First().Value);
            Assert.AreEqual("Dependency", xml.Element("Dependencies").Elements().First().Name);
            Assert.AreEqual(1, xml.Element("Dependencies").Elements().Count());
        }

        [TestMethod]
        public void SerializeTest_EmptyDependencyList()
        {
            var author = new PlugInAuthor()
            {
                Name = "BigEgg",
                EmailAddress = "bigegg@bigegg.com"
            };
            var info = new PlugInInfo()
            {
                PlugInGuid = new Guid("26e54ac9-6286-4991-a687-c8c6b7c50289"),
                Name = "TFS Click",
                Description = "When click the bug open related website",
                Authors = new List<PlugInAuthor>() { author },
                VersionStr = "0.5.0.0",
                MinimumSupportBuggerVersionStr = "0.5.0.0",
                MaximumSupportBuggerVersionStr = "0.5.0.0"
            };
            var document = new PlugInConfigDocument()
            {
                PlugInInfo = info,
                PlugInType = PlugInType.Click,
                AssemblyNames = new List<string>() { "Bugger.PlugIn.Click.TFSClick" },
                DependentPlugIns = new List<DependentPlugIn>() { }
            };
            var xml = document.ObjectToXElement();

            Assert.AreEqual("Configuration", xml.Name);
            Assert.AreEqual(6, xml.Element("Info").Elements().Count());
            Assert.AreEqual("Click", xml.Element("Type").Value);
            Assert.AreEqual("Name", xml.Element("Assemblies").Elements().First().Name);
            Assert.AreEqual("Bugger.PlugIn.Click.TFSClick", xml.Element("Assemblies").Elements().First().Value);
            Assert.AreEqual(0, xml.Element("Dependencies").Elements().Count());
        }

        [TestMethod]
        public void SerializeTest_NoDependency()
        {
            var author = new PlugInAuthor()
            {
                Name = "BigEgg",
                EmailAddress = "bigegg@bigegg.com"
            };
            var info = new PlugInInfo()
            {
                PlugInGuid = new Guid("26e54ac9-6286-4991-a687-c8c6b7c50289"),
                Name = "TFS Click",
                Description = "When click the bug open related website",
                Authors = new List<PlugInAuthor>() { author },
                VersionStr = "0.5.0.0",
                MinimumSupportBuggerVersionStr = "0.5.0.0",
                MaximumSupportBuggerVersionStr = "0.5.0.0"
            };
            var document = new PlugInConfigDocument()
            {
                PlugInInfo = info,
                PlugInType = PlugInType.Click,
                AssemblyNames = new List<string>() { "Bugger.PlugIn.Click.TFSClick" }
            };
            var xml = document.ObjectToXElement();

            Assert.AreEqual("Configuration", xml.Name);
            Assert.AreEqual(6, xml.Element("Info").Elements().Count());
            Assert.AreEqual("Click", xml.Element("Type").Value);
            Assert.AreEqual("Name", xml.Element("Assemblies").Elements().First().Name);
            Assert.AreEqual("Bugger.PlugIn.Click.TFSClick", xml.Element("Assemblies").Elements().First().Value);
            Assert.AreEqual(0, xml.Elements("Dependencies").Count());
        }

        [TestMethod]
        public void DeserializeTest()
        {
            var xml = new XElement("Configuration",
                new XElement("Info",
                    new XAttribute("PlugInGuid", "26e54ac9-6286-4991-a687-c8c6b7c50289"),
                    new XElement("Name", "TFS Click"),
                    new XElement("Description", "When click the bug open related website"),
                    new XElement("Authors",
                        new XElement("Author",
                            new XAttribute("Name", "BigEgg"),
                            new XText("bigegg@bigegg.com")
                        )
                    ),
                    new XElement("Version", "0.5.0.0"),
                    new XElement("MinimumSupportBuggerVersion", "0.5.0.0"),
                    new XElement("MaximumSupportBuggerVersion", "0.5.0.0")
                ),
                new XElement("Type", "Click"),
                new XElement("Assemblies",
                    new XElement("Name", "Bugger.PlugIn.Click.TFSClick")
                ),
                new XElement("Dependencies",
                    new XElement("Dependency",
                        new XAttribute("Guid", "1dc425b3-c27b-46ba-9623-a046d1acc754"),
                        new XElement("Type", "Mandatory"),
                        new XElement("MinimumSupportPlugInVersion", "0.5.0.0"),
                        new XElement("MaximumSupportPlugInVersion", "0.5.0.0")
                    )
                )
            );
            var document = xml.XElementToObject<PlugInConfigDocument>();

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.PlugInInfo);
            Assert.AreEqual("26e54ac9-6286-4991-a687-c8c6b7c50289", document.PlugInInfo.PlugInGuid.ToString());
            Assert.AreEqual(PlugInType.Click, document.PlugInType);
            Assert.AreEqual("Bugger.PlugIn.Click.TFSClick", document.AssemblyNames.First());
            Assert.AreEqual(1, document.DependentPlugIns.Count);
            Assert.AreEqual("1dc425b3-c27b-46ba-9623-a046d1acc754", document.DependentPlugIns.First().PlugInGuid.ToString());
        }

        [TestMethod]
        public void DeserializeTest_EmptyDependencyList()
        {
            var xml = new XElement("Configuration",
                new XElement("Info",
                    new XAttribute("PlugInGuid", "26e54ac9-6286-4991-a687-c8c6b7c50289"),
                    new XElement("Name", "TFS Click"),
                    new XElement("Description", "When click the bug open related website"),
                    new XElement("Authors",
                        new XElement("Author",
                            new XAttribute("Name", "BigEgg"),
                            new XText("bigegg@bigegg.com")
                        )
                    ),
                    new XElement("Version", "0.5.0.0"),
                    new XElement("MinimumSupportBuggerVersion", "0.5.0.0"),
                    new XElement("MaximumSupportBuggerVersion", "0.5.0.0")
                ),
                new XElement("Type", "Click"),
                new XElement("Assemblies",
                    new XElement("Name", "Bugger.PlugIn.Click.TFSClick")
                ),
                new XElement("Dependencies")
            );
            var document = xml.XElementToObject<PlugInConfigDocument>();

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.PlugInInfo);
            Assert.AreEqual("26e54ac9-6286-4991-a687-c8c6b7c50289", document.PlugInInfo.PlugInGuid.ToString());
            Assert.AreEqual(PlugInType.Click, document.PlugInType);
            Assert.AreEqual("Bugger.PlugIn.Click.TFSClick", document.AssemblyNames.First());
            Assert.AreEqual(0, document.DependentPlugIns.Count);
        }

        [TestMethod]
        public void DeserializeTest_NoDependency()
        {
            var xml = new XElement("Configuration",
                new XElement("Info",
                    new XAttribute("PlugInGuid", "26e54ac9-6286-4991-a687-c8c6b7c50289"),
                    new XElement("Name", "TFS Click"),
                    new XElement("Description", "When click the bug open related website"),
                    new XElement("Authors",
                        new XElement("Author",
                            new XAttribute("Name", "BigEgg"),
                            new XText("bigegg@bigegg.com")
                        )
                    ),
                    new XElement("Version", "0.5.0.0"),
                    new XElement("MinimumSupportBuggerVersion", "0.5.0.0"),
                    new XElement("MaximumSupportBuggerVersion", "0.5.0.0")
                ),
                new XElement("Type", "Click"),
                new XElement("Assemblies",
                    new XElement("Name", "Bugger.PlugIn.Click.TFSClick")
                )
            );
            var document = xml.XElementToObject<PlugInConfigDocument>();

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.PlugInInfo);
            Assert.AreEqual("26e54ac9-6286-4991-a687-c8c6b7c50289", document.PlugInInfo.PlugInGuid.ToString());
            Assert.AreEqual(PlugInType.Click, document.PlugInType);
            Assert.AreEqual("Bugger.PlugIn.Click.TFSClick", document.AssemblyNames.First());
            Assert.AreEqual(0, document.DependentPlugIns.Count);
        }
    }
}
