/***************************************************************************************
 * Author:CC
 * 
 * Ini文件管理类.
 * 
 * DateTime:2017-06-27
 * ************************************************************************************/
namespace CCSdk
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// Provides access to ini files for applications. This class cannot be inherited.
    /// </summary>
    public static class IniFileManager
    {
        /// <summary>
        /// Field IniDictionary.
        /// </summary>
        private static readonly Dictionary<string, IniFileEntry> IniDictionary = new Dictionary<string, IniFileEntry>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Field Lock.
        /// </summary>
        private static ReaderWriterLock Lock = new ReaderWriterLock();

        /// <summary>
        /// Opens the ini file for the current application.
        /// </summary>
        /// <param name="iniFile">Ini file for the current application; if null or string.Empty use the default AppDomain ini File.</param>
        /// <returns>IniEntry instance.</returns>
        public static IniFileEntry Open(string iniFile = null)
        {
            if (string.IsNullOrEmpty(iniFile))
            {
                iniFile = Path.ChangeExtension(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, "ini");
            }

            string key = Path.GetFullPath(iniFile);

            Lock.AcquireReaderLock(Timeout.Infinite);

            try
            {
                if (IniDictionary.ContainsKey(key))
                {
                    return IniDictionary[key];
                }
                else
                {
                    LockCookie lockCookie = Lock.UpgradeToWriterLock(Timeout.Infinite);

                    try
                    {
                        if (IniDictionary.ContainsKey(key))
                        {
                            return IniDictionary[key];
                        }
                        else
                        {
                            IniFileEntry result = new IniFileEntry(key);

                            IniDictionary.Add(key, result);

                            return result;
                        }
                    }
                    finally
                    {
                        Lock.DowngradeFromWriterLock(ref lockCookie);
                    }
                }
            }
            finally
            {
                Lock.ReleaseReaderLock();
            }
        }
    }
}
