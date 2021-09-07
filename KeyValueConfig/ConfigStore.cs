using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace KeyValueConfig
{
    public class ConfigStore
    {
        #region Static Methods
        static public bool IsValidConfigStoreFile(string path)
        {
            try
            {
                bool validConfigFile = false;

                if (File.Exists(path))
                {
                    using (XmlTextReader xmlReadData = new XmlTextReader(path))
                    {
                        while (xmlReadData.Read())
                        {
                            if (xmlReadData.NodeType == XmlNodeType.Element && xmlReadData.Name == "Config")
                            {
                                if (xmlReadData.Depth <= 0)
                                {
                                    validConfigFile = true;
                                }
                                break;
                            }
                        }
                        xmlReadData.Close();
                    }
                }

                return validConfigFile;
            }
            catch
            {
                return false;
            }
        }

        static public ConfigStore CreateFromFile(string filePath, bool useBackupFile = false)
        {
            ConfigStore configStore = new ConfigStore();
            if (configStore.LoadFromFile(filePath, useBackupFile))
            {
                return configStore;
            }
            else
            {
                return null;
            }
        }

        static public ConfigStore CreateFromStream(Stream stream)
        {
            ConfigStore configStore = new ConfigStore();
            if (configStore.LoadFromStream(stream))
            {
                return configStore;
            }
            else
            {
                return null;
            }
        }

        static public ConfigStore CreateFromString(string config)
        {
            ConfigStore configStore = new ConfigStore();
            if (configStore.LoadFromString(config))
            {
                return configStore;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Private Members
        private Dictionary<string, string> store;
        #endregion

        #region Constructor
        public ConfigStore()
        {
            store = new Dictionary<string, string>();
        }
        #endregion

        #region Get Methods
        private string GetValue(string key, string defaultValue)
        {
            key = $"_{key}";

            if (store.ContainsKey(key))
            {
                return store[key];
            }
            else
            {
                return defaultValue;
            }
        }

        public byte GetByte(string key, byte defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (byte.TryParse(value, out byte result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public float GetFloat(string key, float defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (float.TryParse(value, out float result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public short GetInt16(string key, short defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (short.TryParse(value, out short result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public ushort GetUInt16(string key, ushort defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (ushort.TryParse(value, out ushort result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public int GetInt32(string key, int defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (int.TryParse(value, out int result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public uint GetUInt32(string key, uint defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (uint.TryParse(value, out uint result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public long GetInt64(string key, long defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (long.TryParse(value, out long result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public ulong GetUInt64(string key, ulong defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (ulong.TryParse(value, out ulong result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public double GetDouble(string key, double defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (double.TryParse(value, out double result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public string GetString(string key, string defaultValue)
        {
            string value = GetValue(key, null);
            if (value != null)
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public bool GetBool(string key, bool defaultValue)
        {
            string value = GetValue(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                if (bool.TryParse(value, out bool result))
                    return result;
                else
                    return defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public Guid GetGuid(string key, Guid defaultValue)
        {
            string value = GetValue(key, null);
            if (Guid.TryParse(value, out Guid result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public T GetEnum<T>(string key, T defaultValue) where T : System.Enum
        {
            try
            {
                T result;
                result = (T)Enum.Parse(typeof(T), GetValue(key, Enum.GetName(typeof(T), defaultValue)));
                return result;
            }
            catch
            {
                return defaultValue;
            }
        }
        #endregion

        #region Set Methods
        public void SetValue(string key, string value)
        {
            key = $"_{key}";
            if (store.ContainsKey(key))
            {
                store[key] = value;
            }
            else
            {
                store.Add(key, value);
            }
        }

        public void SetValue<T>(string key, T value) where T : struct
        {
            SetValue(key, value.ToString());
        }
        #endregion

        #region Save Methods
        public string SaveToString()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = Environment.NewLine;
            settings.NewLineHandling = NewLineHandling.Replace;
            settings.NewLineOnAttributes = true;
            settings.CheckCharacters = false;

            StringBuilder stringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Config");
                foreach (var pair in store)
                {
                    xmlWriter.WriteElementString(pair.Key, pair.Value);
                }
                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                xmlWriter.Close();
            }

            return stringBuilder.ToString();
        }

        public bool SaveToFile(string filePath, bool isWriteThrough = false)
        {
            try
            {
                string directoryPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(filePath));
                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.NewLineChars = Environment.NewLine;
                settings.NewLineHandling = NewLineHandling.Replace;
                settings.NewLineOnAttributes = true;
                settings.CheckCharacters = false;

                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 0x1000, isWriteThrough ? FileOptions.None : FileOptions.WriteThrough))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("Config");
                        foreach (var pair in store)
                        {
                            xmlWriter.WriteElementString(pair.Key, pair.Value);
                        }

                        xmlWriter.WriteEndDocument();

                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Load Methods
        public bool LoadFromFile(string path, bool useBackupFile = false)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    store.Clear();

                    using (XmlTextReader xmlReadData = new XmlTextReader(path))
                    {
                        while (xmlReadData.Read())
                        {
                            if (xmlReadData.NodeType == XmlNodeType.Element && xmlReadData.Name != "Config")
                            {
                                store.Add(xmlReadData.Name, xmlReadData.ReadString());
                            }
                        }
                        xmlReadData.Close();
                    }

                    if (useBackupFile)
                    {
                        try
                        {
                            System.IO.File.Copy(path, path + ".backup", true);
                        }
                        catch { }
                    }

                    return true;
                }
            }
            catch { }

            if (useBackupFile)
            {
                if (LoadFromFile(path + ".backup", false))
                {
                    System.IO.File.Copy(path + ".backup", path, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public bool LoadFromString(string config)
        {
            if (string.IsNullOrEmpty(config))
                return false;

            try
            {
                Dictionary<string, string> dicConfig = new Dictionary<string, string>();
                using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(config)))
                {
                    return LoadFromStream(memoryStream);
                }
            }
            catch
            {
                return false;
            }
        }

        public bool LoadFromStream(Stream stream)
        {
            try
            {
                using (XmlTextReader xmlReadData = new XmlTextReader(stream))
                {
                    while (xmlReadData.Read())
                    {
                        if (xmlReadData.NodeType == XmlNodeType.Element && xmlReadData.Name != "Config")
                        {
                            store.Add(xmlReadData.Name, xmlReadData.ReadString());
                        }
                    }
                    xmlReadData.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
