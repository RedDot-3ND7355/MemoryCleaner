using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MemoryCleaner
{
    public static class BlacklistHandler
    {
        private static readonly string default_path =
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? AppContext.BaseDirectory, "Blacklist.xml");

        /// <summary>Case-insensitive process file names, e.g. chrome.exe</summary>
        public static HashSet<string> blacklisted_processes { get; private set; } =
            new(StringComparer.OrdinalIgnoreCase);

        public static bool IsBlacklisted(string processFileName) =>
            !string.IsNullOrWhiteSpace(processFileName) &&
            blacklisted_processes.Contains(processFileName);

        public static void IniBL()
        {
            if (!File.Exists(default_path))
                new XDocument(new XElement("Processes")).Save(default_path);

            LoadBL();
        }

        public static void SaveBL()
        {
            var doc = new XDocument(new XElement("Processes"));
            XElement parent = doc.Element("Processes")!;

            foreach (string name in blacklisted_processes.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                string normalized = name.Trim();
                if (!parent.Elements("Process").Any(x =>
                        string.Equals(x.Value, normalized, StringComparison.OrdinalIgnoreCase)))
                {
                    parent.Add(new XElement("Process", normalized));
                }
            }

            doc.Save(default_path);
            LoadBL();
        }

        private static void LoadBL()
        {
            var loaded = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                XDocument doc = XDocument.Load(default_path);
                XElement? parent = doc.Element("Processes");
                if (parent != null)
                {
                    foreach (XElement ele in parent.Elements("Process"))
                    {
                        string value = ele.Value?.Trim() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(value))
                            loaded.Add(value);
                    }
                }
            }
            catch
            {
                // Keep empty set on corrupt file
            }

            blacklisted_processes = loaded;
        }
    }
}