using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MemoryCleaner
{
    public static class BlacklistHandler
    {
        // Blacklist Globals
        private static string default_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Blacklist.xml";
        public static List<string> blacklisted_processes = new List<string>();

        // Initialize Blacklist
        public static void IniBL()
        {
            // Check & Create if needed
            if (!File.Exists(default_path))
                new XDocument(new XElement("Processes")).Save(default_path);
            // Load Blacklist
            LoadBL();
        }

        // Save Blacklist
        public static void SaveBL()
        {
            // Save Added Processes
            XDocument doc = new XDocument(new XElement("Processes"));
            XElement parentnode = doc.Element("Processes");
            foreach (string child_elements in blacklisted_processes)
                // Check before adding
                if (parentnode.Elements("Process").Where(x => x.Value == child_elements).Count() == 0)
                    // Add it
                    parentnode.Add(new XElement("Process", child_elements));
            // Replace Element
            doc.Element("Processes").ReplaceWith(parentnode);
            // Append to file
            doc.Save(default_path);
            // Reload Blacklist
            LoadBL();
        }

        // Load Blacklist
        private static void LoadBL()
        {
            // Load Processes into local list
            blacklisted_processes = new List<string>();
            XDocument doc = XDocument.Load(default_path);
            XElement parentnode = doc.Element("Processes");
            foreach (var ele in parentnode.Elements("Process").ToArray())
                blacklisted_processes.Add(ele.Value);
        }

    }
}
