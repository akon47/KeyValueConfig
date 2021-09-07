![logo](https://raw.githubusercontent.com/akon47/KeyValueConfig/master/KeyValueConfig/logo.png)

# KeyValueConfig

<p>
  <img alt="Hits" src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fakon47%2FKeyValueConfig&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false" />
  <img alt="MIT license" src="https://img.shields.io/badge/License-MIT-green.svg">
  <img alt="Nuget version" src="https://img.shields.io/nuget/v/KeyValueConfig">
  <img alt="Nuget downloads" src="https://img.shields.io/nuget/dt/KeyValueConfig">
  <img alt="GitHub starts" src="https://img.shields.io/github/stars/akon47/KeyValueConfig">
</p>

This is a library that allows you to save or load key-value data as a file

## 🎨 Features
- A key-value store class that can be saved as a file.
- A key-value store class that can be loaded from a file.

## 🐾 Examples

```cs
public enum DummyEnum
{
    DummyType1,
    DummyType2,
    DummyType3,
    DummyType4,
    DummyType5,
}
public class DummyClass : IConfigurable
{
    public string StringData { get; set; }
    public byte ByteData { get; set; }
    public short ShortData { get; set; }
    public ushort UnsignedShortData { get; set; }
    public int IntData { get; set; }
    public uint UnsignedIntData { get; set; }
    public long LongData { get; set; }
    public ulong UnsignedLongData { get; set; }
    public double DoubleData { get; set; }
    public float FloatData { get; set; }
    public DummyEnum EnumData { get; set; }

    public ConfigStore CreateConfigStore()
    {
        ConfigStore configStore = new ConfigStore();
        
        configStore.SetValue(nameof(StringData), StringData);
        configStore.SetValue(nameof(ByteData), ByteData);
        configStore.SetValue(nameof(ShortData), ShortData);
        configStore.SetValue(nameof(UnsignedShortData), UnsignedShortData);
        configStore.SetValue(nameof(IntData), IntData);
        configStore.SetValue(nameof(UnsignedIntData), UnsignedIntData);
        configStore.SetValue(nameof(LongData), LongData);
        configStore.SetValue(nameof(UnsignedLongData), UnsignedLongData);
        configStore.SetValue(nameof(DoubleData), DoubleData);
        configStore.SetValue(nameof(FloatData), FloatData);
        configStore.SetValue(nameof(EnumData), EnumData);
        
        return configStore;
    }

    public void LoadFromConfigStore(ConfigStore configStore)
    {
        if (configStore != null)
        {
            StringData = configStore.GetString(nameof(StringData), "");
            ByteData = configStore.GetByte(nameof(ByteData), 0);
            ShortData = configStore.GetInt16(nameof(ShortData), 0);
            UnsignedShortData = configStore.GetUInt16(nameof(UnsignedShortData), 0);
            IntData = configStore.GetInt32(nameof(IntData), 0);
            UnsignedIntData = configStore.GetUInt32(nameof(UnsignedIntData), 0);
            LongData = configStore.GetInt64(nameof(LongData), 0);
            UnsignedLongData = configStore.GetUInt64(nameof(UnsignedLongData), 0);
            DoubleData = configStore.GetDouble(nameof(DoubleData), 0);
            FloatData = configStore.GetFloat(nameof(FloatData), 0);
            EnumData = configStore.GetEnum<DummyEnum>(nameof(EnumData), DummyEnum.DummyType1);
        }
    }
}

DummyClass dummyClassA = new DummyClass();
// set data ...
a.StringData = "ABC";
a.ByteData = (byte)100;
// save to file
dummyClassA.CreateConfigStore().SaveToFile(tmpFilePath);

// create new dummy class
DummyClass dummyClassB = new DummyClass();
// load from file
dummyClassB.LoadFromConfigStore(ConfigStore.CreateFromFile(tmpFilePath));

// dummyClassB.StringData => "ABC", dummyClassB.ByteData => 100 
// float, double values may not match perfectly. 
```

## 🐞 Bug Report
If you find a bug, please report to us posting [issues](https://github.com/akon47/KeyValueConfig/issues) on GitHub.
