﻿#region Settings
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

public class Settings
{
    #region Graphics Options

    public class GraphicsOptions
    {
        public enum Mode
        {
            Automatic = 0,
            OpenGL = 1,
            DirectX = 2
        }

        public enum Level
        {
            Automatic = 0,
            VeryLow = 1,
            Low = 2,
            Medium = 3,
            High = 4,
            Ultra = 5,
            Custom = 6
        }

        public enum ClientLoadOptions
        {
            Client_2007_NoGraphicsOptions = 0,
            Client_2007 = 1,
            Client_2008AndUp = 2,
            Client_2008AndUp_LegacyOpenGL = 3,
            Client_2008AndUp_QualityLevel21 = 4,
            Client_2008AndUp_NoGraphicsOptions = 5,
            Client_2008AndUp_ForceAutomatic = 6,
            Client_2008AndUp_ForceAutomaticQL21 = 7,
            Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL = 8
        }

        public static Mode GetModeForInt(int level)
        {
            switch (level)
            {
                case 1:
                    return Mode.OpenGL;
                case 2:
                    return Mode.DirectX;
                default:
                    return Mode.Automatic;
            }
        }

        public static int GetIntForMode(Mode level)
        {
            switch (level)
            {
                case Mode.OpenGL:
                    return 1;
                case Mode.DirectX:
                    return 2;
                default:
                    return 0;
            }
        }

        public static Level GetLevelForInt(int level)
        {
            switch (level)
            {
                case 0:
                    return Level.Automatic;
                case 1:
                    return Level.VeryLow;
                case 2:
                    return Level.Low;
                case 3:
                    return Level.Medium;
                case 4:
                    return Level.High;
                case 6:
                    return Level.Custom;
                case 5:
                default:
                    return Level.Ultra;
            }
        }

        public static int GetIntForLevel(Level level)
        {
            switch (level)
            {
                case Level.Automatic:
                    return 0;
                case Level.VeryLow:
                    return 1;
                case Level.Low:
                    return 2;
                case Level.Medium:
                    return 3;
                case Level.High:
                    return 4;
                case Level.Custom:
                    return 6;
                case Level.Ultra:
                default:
                    return 5;
            }
        }

        public static ClientLoadOptions GetClientLoadOptionsForInt(int level)
        {
            switch (level)
            {
                case 1:
                    return ClientLoadOptions.Client_2007;
                case 2:
                    return ClientLoadOptions.Client_2008AndUp;
                case 3:
                    return ClientLoadOptions.Client_2008AndUp_LegacyOpenGL;
                case 4:
                    return ClientLoadOptions.Client_2008AndUp_QualityLevel21;
                case 5:
                    return ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions;
                case 6:
                    return ClientLoadOptions.Client_2008AndUp_ForceAutomatic;
                case 7:
                    return ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21;
                case 8:
                    return ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL;
                default:
                    return ClientLoadOptions.Client_2007_NoGraphicsOptions;
            }
        }

        public static ClientLoadOptions GetClientLoadOptionsForBool(bool level)
        {
            switch (level)
            {
                case false:
                    return ClientLoadOptions.Client_2008AndUp;
                default:
                    return ClientLoadOptions.Client_2007_NoGraphicsOptions;
            }
        }

        public static int GetIntForClientLoadOptions(ClientLoadOptions level)
        {
            switch (level)
            {
                case ClientLoadOptions.Client_2007:
                    return 1;
                case ClientLoadOptions.Client_2008AndUp:
                    return 2;
                case ClientLoadOptions.Client_2008AndUp_LegacyOpenGL:
                    return 3;
                case ClientLoadOptions.Client_2008AndUp_QualityLevel21:
                    return 4;
                case ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions:
                    return 5;
                case ClientLoadOptions.Client_2008AndUp_ForceAutomatic:
                    return 6;
                case ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21:
                    return 7;
                case ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                    return 8;
                default:
                    return 0;
            }
        }

        public static string GetPathForClientLoadOptions(ClientLoadOptions level)
        {
            string localAppdataRobloxPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Roblox";
            string appdataRobloxPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Roblox";

            if (!Directory.Exists(localAppdataRobloxPath))
            {
                Directory.CreateDirectory(localAppdataRobloxPath);
            }

            if (!Directory.Exists(appdataRobloxPath))
            {
                Directory.CreateDirectory(appdataRobloxPath);
            }

            switch (level)
            {
                case ClientLoadOptions.Client_2008AndUp_QualityLevel21:
                case ClientLoadOptions.Client_2008AndUp_LegacyOpenGL:
                case ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions:
                case ClientLoadOptions.Client_2008AndUp_ForceAutomatic:
                case ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21:
                case ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                case ClientLoadOptions.Client_2008AndUp:
                    return localAppdataRobloxPath;
                default:
                    return appdataRobloxPath;
            }
        }
    }
    #endregion

    #region UI Options
    public static class UIOptions
    {
        public enum Style
        {
            None = 0,
            Extended = 1,
            Compact = 2
        }

        public static Style GetStyleForInt(int level)
        {
            switch (level)
            {
                case 1:
                    return Style.Extended;
                case 2:
                    return Style.Compact;
                default:
                    return Style.None;
            }
        }

        public static int GetIntForStyle(Style level)
        {
            switch (level)
            {
                case Style.Extended:
                    return 1;
                case Style.Compact:
                    return 2;
                default:
                    return 0;
            }
        }
    }
    #endregion

    #region Content Provider Options
    public class Provider
    {
        public string Name;
        public string URL;
        public string Icon;
    }

    [XmlRoot("ContentProviders")]
    public class ContentProviders
    {
        [XmlArray("Providers")]
        public Provider[] Providers;
    }

    public class OnlineClothing
    {
        public static Provider[] GetContentProviders()
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\ContentProviders.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ContentProviders));

                FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\ContentProviders.xml", FileMode.Open);
                ContentProviders providers;
                providers = (ContentProviders)serializer.Deserialize(fs);

                return providers.Providers;
            }
            else
            {
                return null;
            }
        }

        public static Provider FindContentProviderByName(Provider[] providers, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\ContentProviders.xml"))
            {
                return providers.SingleOrDefault(item => query.Contains(item.Name));
            }
            else
            {
                return null;
            }
        }
        
        public static Provider FindContentProviderByURL(Provider[] providers, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\ContentProviders.xml"))
            {
                return providers.SingleOrDefault(item => query.Contains(item.URL));
            }
            else
            {
                return null;
            }
        }
    }
    #endregion
}
#endregion
