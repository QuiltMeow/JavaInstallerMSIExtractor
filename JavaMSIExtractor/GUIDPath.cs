using System;
using System.Runtime.InteropServices;

namespace JavaMSIExtractor
{
    public static class GUIDPath
    {
        public static string getLocalLowPath()
        {
            Guid localLowGUID = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");
            return getKnownGUIDDirectoryPath(localLowGUID);
        }

        public static string getKnownGUIDDirectoryPath(Guid knownDirectoryGUID)
        {
            IntPtr ppszPath = IntPtr.Zero;
            try
            {
                int hResult = SHGetKnownFolderPath(knownDirectoryGUID, 0, IntPtr.Zero, out ppszPath);
                if (hResult >= 0)
                {
                    return Marshal.PtrToStringAuto(ppszPath);
                }
                throw Marshal.GetExceptionForHR(hResult);
            }
            finally
            {
                if (ppszPath != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(ppszPath);
                }
            }
        }

        [DllImport("shell32.dll")]
        public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);
    }
}