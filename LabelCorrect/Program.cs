using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;

namespace LabelCorrect
{
    class Program
    {
        enum FileType
        {
            XVC = 0,
            STRUCT = 1,
            ALERT = 2,
            LOV = 3
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Select the forms root folder");
            //var Path = "C:\\Users\\pedro.oliveira\\Documents\\Hammer - Pilot\\LabelCorrect\\Forms";
            var Path = args[0];
            var formDirectories = Directory.GetDirectories(Path);

            foreach (var form in formDirectories)
            {
                var resxFile = Directory.GetFiles(form, "strings.resx", SearchOption.AllDirectories).First();
                var resxKeys = GetResxKeys(resxFile);
                var xvcFiles = Directory.GetFiles(form, "*.xvc", SearchOption.AllDirectories);
                var structFiles = Directory.GetFiles(form, "*Struct.xml", SearchOption.AllDirectories);
                var alertFiles = Directory.GetFiles(form, "*Alerts.xml", SearchOption.AllDirectories);
                var lovFiles = Directory.GetFiles(form, "*Lovs.xml", SearchOption.AllDirectories);
                foreach (var file in xvcFiles)
                {
                    LocalizeProperties(file, resxKeys, FileType.XVC);
                }
                foreach (var file in structFiles)
                {
                    LocalizeProperties(file, resxKeys, FileType.STRUCT);
                }
                foreach (var file in alertFiles)
                {
                    LocalizeProperties(file, resxKeys, FileType.ALERT);
                }
                foreach (var file in lovFiles)
                {
                    LocalizeProperties(file, resxKeys, FileType.LOV);
                }
            }
        }

        private static bool ExistsInResxKeys(String resxFileName, String attr)
        {

            var xdoc = XDocument
            .Load(resxFileName)
            .Descendants()
            .Where(elem => elem.Name == "data")
            .Select(elem => $"{ elem.Attributes().First(a => a.Name == "name").Value}");

            return false;
        }

        private static IEnumerable<string> GetResxKeys(String resxFileName)
        {
            var xdoc = XDocument
            .Load(resxFileName)
            .Descendants()
            .Where(elem => elem.Name == "data")
            .Select(elem => $"{ elem.Attributes().First(a => a.Name == "name").Value}");
            Console.WriteLine("\nResx File key's: " + xdoc.Count());

            return xdoc;
        }

        private static void LocalizeProperties(string file, IEnumerable<string> resxKeys, FileType type)
        {
            var xmldoc = XDocument.Load(file, (LoadOptions.PreserveWhitespace | LoadOptions.SetBaseUri));
            Console.WriteLine("Current file: " + Path.GetFullPath(file));

            var root = xmldoc.Root;
            var filePath = Path.GetFileNameWithoutExtension(file);

            LocalizeChildElements(root, resxKeys, filePath, type);

            xmldoc.Save(file, SaveOptions.None);
            Console.WriteLine("File saved");
            Console.WriteLine("----------");
        }

        private static void LocalizeChildElements(XElement root, IEnumerable<string> resxKeys, String filename, FileType type)
        {
            switch (type)
            {
                case FileType.XVC:
                    {
                        LocalizeXvcElements(root, resxKeys);
                        break;
                    }
                case FileType.STRUCT:
                    {
                        LocalizeStructXMLElements(root, resxKeys);
                        break;
                    }
                case FileType.ALERT:
                    {
                        LocalizeAlertXMLElements(root, resxKeys);
                        break;
                    }
                case FileType.LOV:
                    {
                        LocalizeLovXMLElements(root, resxKeys);
                        break;
                    }
            }
        }

        private static void LocalizeXvcElements(XElement root, IEnumerable<string> resxKeys)
        {
            var descendants = root.DescendantsAndSelf();
            Console.WriteLine("N of descendants: " + descendants.Count());

            var desTitles = descendants.Where(desc => desc.Attribute("title") != null);
            var desLabels = descendants.Where(desc => (desc.Attribute("label") != null) && !(desc.Name.LocalName.Equals("viewcolumn")));
            var desTooltip = descendants.Where(desc => (desc.Attribute("tooltip") != null) && !(desc.Name.LocalName.Equals("viewcolumn")));
            var descGridview = descendants.Where(desc => desc.Name.LocalName.Equals("gridview"));

            Console.WriteLine("Gridview Descendants: " + descGridview.Count());

            foreach (var desc in desTitles)
            {
                var tag = CreateTags(desc);
                var inResx = resxKeys.Contains(tag);
                if (inResx)
                {
                    Console.WriteLine("Title in resx: " + tag);
                    desc.SetAttributeValue("title", tag);
                    Console.WriteLine("Title in xvc : " + tag);
                }
            }

            foreach (var desc in desTooltip)
            {
                var tag = CreateTooltipTags(desc);
                var inResx = resxKeys.Contains(tag);
                if (inResx)
                {
                    Console.WriteLine("Tooltip in resx: " + tag);
                    desc.SetAttributeValue("tooltip", tag);
                    Console.WriteLine("Tooltip in xvc : " + tag);
                }
            }

            foreach (var desc in desLabels)
            {
                var tag = CreateTags(desc);
                var inResx = resxKeys.Contains(tag);
                if (inResx)
                {
                    Console.WriteLine("Label in resx: " + tag);
                    desc.SetAttributeValue("label", tag);
                    Console.WriteLine("Label in xvc : " + tag);
                }
                else
                {
                    if (tag.EndsWith("_label"))
                    {
                        Console.WriteLine("Label before replace: " + tag);
                        tag = tag.Replace("_label", "_prompttext");
                        Console.WriteLine("Label after replace : " + tag);
                    }
                    if (tag.EndsWith("_prompttext"))
                    {
                        Console.WriteLine("Label before replace: " + tag);
                        tag = tag.Replace("_prompttext", "_label");
                        Console.WriteLine("Label after replace : " + tag);
                    }
                    inResx = resxKeys.Contains(tag);
                    if (inResx)
                    {
                        Console.WriteLine("Label in resx: " + tag);
                        desc.SetAttributeValue("label", tag);
                        Console.WriteLine("Label in xvc : " + tag);
                    }
                    else
                    {
                        Console.WriteLine("Unset label: " + tag);
                    }
                }
            }

            foreach (var grid in descGridview)
            {
                var blockName = SnakeToCamelCase(grid.Attribute("block").Value);
                var viewColumns = descendants.Where(desc => desc.Name.LocalName.Equals("viewcolumn"));

                foreach (var viewColumn in viewColumns)
                {
                    var labelAttr = viewColumn.Attribute("label");
                    var memberAttr = viewColumn.Attribute("member");
                    var tooltipAttr = viewColumn.Attribute("tooltip");
                    string memberStr = string.Empty;
                    if(memberAttr != null)
                    {
                       var tag = CreateViewColumnLabelTags(viewColumn, blockName);

                        if (labelAttr != null)
                        {
                            var inResx = resxKeys.Contains(tag);
                            if (inResx)
                            {
                                Console.WriteLine("View Column Label in resx: " + tag);
                                viewColumn.SetAttributeValue("label", tag);
                                Console.WriteLine("View Column in xvc : " + tag);
                            }
                            else 
                            {
                                tag = tag.Replace("_label", "_prompttext");
                                inResx = resxKeys.Contains(tag);
                                if (inResx) 
                                {
                                    Console.WriteLine("View Column Label in resx: " + tag);
                                    viewColumn.SetAttributeValue("label", tag);
                                    Console.WriteLine("View Column in xvc : " + tag);
                                }
                                else
                                {
                                    Console.WriteLine("Unset View Column tooltip: " + tag);
                                }
                            }
                        }
                        if (tooltipAttr != null)
                        {
                            var tooltipTag = CreateViewColumnTooltipTags(viewColumn, blockName);
                            var inResx = resxKeys.Contains(tooltipTag);
                            if (inResx)
                            {
                                Console.WriteLine("View Column tooltip in resx: " + tooltipTag);
                                viewColumn.SetAttributeValue("tooltip", tooltipTag);
                                Console.WriteLine("View Column tooltip in xvc : " + tooltipTag);
                            }
                            else
                            {
                                Console.WriteLine("Unset View Column tooltip: " + tooltipTag);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Member does not exist: ");
                        continue;
                    }
                }
            }
        }


        private static void LocalizeStructXMLElements(XElement root, IEnumerable<string> resxKeys)
        {

            var descendants = root.DescendantsAndSelf();

            var windows = descendants.Where(desc => desc.Name.LocalName.Equals("Window"));
            var blocks = descendants.Where(desc => desc.Name.LocalName.Equals("Block"));

            foreach (var window in windows)
            {
                var fileUri = window.Attribute("Url").Value;
                var fileName = Path.GetFileNameWithoutExtension(fileUri);
                var viewName = char.ToLower(fileName[5]) + fileName.Substring(6);

                var inResx = resxKeys.Contains(viewName);
                var title = viewName + "_title";
                if (window.Attribute("Title") != null)
                {
                    if (inResx)
                    {
                        Console.WriteLine("Window Title in Url: " + fileName);
                        window.SetAttributeValue("Title", title);
                        Console.WriteLine("Window Title in struct.xml : " + title + "\n");
                    }
                    else
                    {
                        Console.WriteLine("Window Title: {0} not in Resx ", title);
                    }
                }
            }

            foreach (var block in blocks)
            {
                var blockId = SnakeToCamelCase(block.Attribute("Id").Value);

                var blockItems = block.Descendants();
                foreach (var item in blockItems)
                {
                    var itemId = SnakeToCamelCase(item.Attribute("Id").Value);

                    if (item.Attribute("Hint") != null)
                    {
                        var hint = blockId + "_" + itemId + "_hint";
                        if (resxKeys.Contains(hint))
                        {
                            Console.WriteLine("Hint in Item: " + itemId);
                            item.SetAttributeValue("Hint", hint);
                            Console.WriteLine("Hint in Item (Localized): " + hint);
                        }
                        else
                        {
                            Console.WriteLine("Hint: {0} not in Resx ", hint);
                        }
                    }
                }
            }
        }

        private static void LocalizeAlertXMLElements(XElement root, IEnumerable<string> resxKeys)
        {

            var descendants = root.DescendantsAndSelf();
            var alerts = descendants.Where(desc => desc.Name.LocalName.Equals("Alert"));

            foreach (var alert in alerts)
            {
                var alertName = SnakeToCamelCase(alert.Attribute("Name").Value);
                var title = alertName + "_title";
                if (alert.Attribute("Title") != null)
                {
                    if (resxKeys.Contains(title))
                    {
                        Console.WriteLine("Alert Title: " + title);
                        alert.SetAttributeValue("Title", title);
                    }
                    else
                    {
                        Console.WriteLine("Title: {0} not in Resx ", title);
                    }
                }

                var alertDescendents = alert.Descendants();
                foreach (var item in alertDescendents)
                {
                    var itemName = item.Name.LocalName;
                    var itemLabel = item.Attribute("Label");
                    var itemId = item.Attribute("Id");
                    if (itemName.Equals("Text"))
                    {
                        var text = alertName + "_text";
                        if (resxKeys.Contains(text))
                        {
                            Console.WriteLine("Alert Text Value: " + text);
                            item.SetValue(text);
                        }
                        else
                        {
                            Console.WriteLine("Alert Text Value: {0} not in Resx ", text);
                        }
                    }
                    else if (itemLabel != null)
                    {
                        if (itemId != null)
                        {
                            var label = alertName + "_" + SnakeToCamelCase(itemId.Value) + "_label";
                            if (resxKeys.Contains(label))
                            {
                                Console.WriteLine("Alert Label Value: " + label);
                                item.SetAttributeValue("Label", label);
                            }
                            else
                            {
                                Console.WriteLine("Alert Label Value: {0} not in Resx ", label);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Alert Label: {0} does not have ID", itemLabel);
                        }
                    }
                }
            }
        }

        private static void LocalizeLovXMLElements(XElement root, IEnumerable<string> resxKeys)
        {
            var descendants = root.DescendantsAndSelf();
            var lovs = descendants.Where(desc => desc.Name.LocalName.Equals("Lov"));

            foreach (var lov in lovs)
            {
                var lovName = SnakeToCamelCase(lov.Attribute("Name").Value);
                var title = lovName + "_title";
                if (lov.Attribute("Title") != null)
                {
                    if (resxKeys.Contains(title))
                    {
                        Console.WriteLine("Lov Title: " + title);
                        lov.SetAttributeValue("Title", title);
                    }
                    else
                    {
                        Console.WriteLine("Lov Title: {0} not in Resx ", title);
                    }
                }
                var lovColumns = lov.Descendants().Where(desc => desc.Name.LocalName.Equals("Column"));
                foreach (var lovColumn in lovColumns)
                {
                    var dataMember = SnakeToCamelCase(lovColumn.Attribute("DataMember").Value);
                    var columnTitle = lovName + "_" + dataMember + "_title";

                    if (lovColumn.Attribute("Title") != null)
                    {
                        if (resxKeys.Contains(columnTitle))
                        {
                            Console.WriteLine("Lov Column Title: " + columnTitle);
                            lovColumn.SetAttributeValue("Title", columnTitle);
                        }
                        else
                        {
                            Console.WriteLine("Lov Column Title: {0} not in Resx ", columnTitle);
                        }
                    }
                }
            }
        }
          
        private static string CreateTags(XElement element)
        {
            var elemName = element.Name.LocalName.ToLower();
            if (elemName.Equals("window"))
            {
                var fileUri = element.Document.BaseUri;
                var fileName = Path.GetFileNameWithoutExtension(fileUri);
                var viewName = char.ToLower(fileName[4]) + fileName.Substring(5);

                return viewName + "_title";
            }
            else if (elemName.Equals("tab"))
            {
                var name = element.Attribute("name").Value;
                return SnakeToCamelCase(name) + "_label";
            }
            
            else
            {
                var member = element.Attribute("member");
                var block = element.Attribute("block");
                string camelMember;
                string camelBlock;
                if (member != null && block != null)
                {
                    camelMember = SnakeToCamelCase(member.Value);
                    camelBlock = SnakeToCamelCase(block.Value);
                }
                else
                {
                    Console.WriteLine("CamelBlock_CamelMember: " + elemName + " | " + (block == null ? "null" : block.Value) + "_" + (member == null ? "null" : member.Value));
                    camelMember = camelBlock = "FIX ME, PLEASE!!";
                }

                var suffix = elemName switch
                {
                    "lovdropdown" => "_label",
                    "button" => "_label",
                    "checkbox" => "_label",
                    "radiobox" => "_label",
                    _ => "_prompttext"
                };

                Console.WriteLine("CamelBlock_CamelMember: " + camelBlock + "_" + camelMember);

                return camelBlock + "_" + camelMember + suffix;
            }
        }

        public static string CreateTooltipTags(XElement element)
        {
            var member = SnakeToCamelCase(element.Attribute("member").Value);
            var block = SnakeToCamelCase(element.Attribute("block").Value);
            string tooltip = string.Empty;
            if (!String.IsNullOrEmpty(member) || !String.IsNullOrEmpty(block))
            {
                tooltip = block + "_" + member + "_tooltip";
            }

            Console.WriteLine("Member: " + member);
            Console.WriteLine("Block: " + block);

            return tooltip;
        }

        public static string CreateViewColumnLabelTags(XElement element, string blockName)
        {
            var member = element.Attribute("member");
           
            string camelMember = string.Empty;

            if (member != null && !string.IsNullOrEmpty(blockName))
            {
                camelMember = SnakeToCamelCase(member.Value);
            }
            return blockName + "_" + camelMember + "_label";
        }

        public static string CreateViewColumnTooltipTags(XElement element, string blockName)
        {
            var member = element.Attribute("member");

            string camelMember = string.Empty;

            if (member != null && !string.IsNullOrEmpty(blockName))
            {
                camelMember = SnakeToCamelCase(member.Value);
            }
            return blockName + "_" + camelMember + "_tooltip";
        }
        private static string SnakeToCamelCase(String snakeText)
        {

            bool firstWord = true;
            var words = snakeText.Split("_");
            var camelCase = "";

            foreach (var word in words)
            {
                if (firstWord)
                {
                    camelCase += word.ToLower();
                    firstWord = false;
                }
                else
                {
                    camelCase += char.ToUpper(word[0]) + word.Substring(1).ToLower();
                }
            }
            return camelCase;
        }
    }
}
