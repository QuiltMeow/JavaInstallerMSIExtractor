using Microsoft.Win32;
using System.Linq;

namespace JavaMSIExtractor
{
    public static class WebViewChecker
    {
        public static bool isWebViewInstall()
        {
            string registryKey = @"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients";
            using (RegistryKey edgeKey = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (edgeKey != null)
                {
                    string[] productKey = edgeKey.GetSubKeyNames();
                    if (productKey.Any())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}