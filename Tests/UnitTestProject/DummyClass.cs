using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyValueConfig;

namespace UnitTestProject
{
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
}
