using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public static class UpdateChecker
    {
        private const string RepoUrl = "https://api.github.com/repos/RedDot-3ND7355/MemoryCleaner/releases/latest";
        private const string ReleasesPage = "https://github.com/RedDot-3ND7355/MemoryCleaner/releases/latest";

        public static async Task CheckForUpdatesAsync(string currentVersion, bool manual = false)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("MemoryCleaner");
                client.Timeout = TimeSpan.FromSeconds(6);

                string json = await client.GetStringAsync(RepoUrl);

                using var doc = JsonDocument.Parse(json);
                string latestTag = doc.RootElement.GetProperty("tag_name").GetString() ?? "";

                string latestVersion = latestTag.TrimStart('v', 'V');

                if (IsNewerVersion(latestVersion, currentVersion))
                {
                    var result = MessageBox.Show(
                        $"A new version is available!\n\n" +
                        $"Current: v{currentVersion}\n" +
                        $"Latest: {latestTag}\n\n" +
                        "Would you like to open the download page?",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = ReleasesPage,
                            UseShellExecute = true
                        });
                    }
                }
                else if (manual)
                {
                    MessageBox.Show(
                        $"You are using the latest version (v{currentVersion}).",
                        "No Updates",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch
            {
                // Fail silently (no internet, rate limit, etc.)
            }
        }

        private static bool IsNewerVersion(string latest, string current)
        {
            try
            {
                var latestParts = latest.Split('.').Select(p => int.TryParse(p, out int n) ? n : 0).ToArray();
                var currentParts = current.Split('.').Select(p => int.TryParse(p, out int n) ? n : 0).ToArray();

                int max = Math.Max(latestParts.Length, currentParts.Length);

                for (int i = 0; i < max; i++)
                {
                    int l = i < latestParts.Length ? latestParts[i] : 0;
                    int c = i < currentParts.Length ? currentParts[i] : 0;

                    if (l > c) return true;
                    if (l < c) return false;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
