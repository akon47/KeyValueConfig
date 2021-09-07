using System;
using KeyValueConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
	[TestClass]
	public class SaveLoadTest
	{
		static public bool IsDoubleEqual(double a, double b, double approximation = 0.0001d) // 0.001%
		{
			double difference = a * 0.0001d;
			return Math.Abs(a - b) <= difference;
		}

		static public bool IsFloatEqual(float a, float b, float approximation = 0.0001f) // 0.001%
		{
			float difference = a * 0.0001f;
			return Math.Abs(a - b) <= difference;
		}

		static public bool IsDataEqual(DummyClass a, DummyClass b)
		{
			

			if (a.StringData == b.StringData &&
				a.ByteData == b.ByteData &&
				a.ShortData == b.ShortData &&
				a.UnsignedShortData == b.UnsignedShortData &&
				a.IntData == b.IntData &&
				a.UnsignedIntData == b.UnsignedIntData &&
				a.LongData == b.LongData &&
				a.UnsignedLongData == b.UnsignedLongData &&
				IsDoubleEqual(a.DoubleData, b.DoubleData) &&
				IsFloatEqual(a.FloatData, b.FloatData) &&
				a.EnumData == b.EnumData)
			{
				return true;
			}
			return false;
		}

		[TestMethod]
		public void SaveToFileAndLoadFromFileTest()
		{
			Random random = new Random();
			for (int i = 0; i < 10; i++)
			{
				string tmpFilePath = System.IO.Path.GetTempFileName();

				DummyClass a = new DummyClass();
				a.StringData = DateTime.Now.ToString();
				a.ByteData = (byte)random.Next();
				a.ShortData = (short)random.Next();
				a.UnsignedShortData = (ushort)random.Next();
				a.IntData = (int)random.Next();
				a.UnsignedIntData = (uint)random.Next();
				a.LongData = (long)random.Next();
				a.UnsignedLongData = (ulong)random.Next();
				a.DoubleData = (double)random.NextDouble();
				a.FloatData = (float)random.NextDouble();
				a.EnumData = DummyEnum.DummyType3;

				a.CreateConfigStore().SaveToFile(tmpFilePath);

				DummyClass b = new DummyClass();
				b.LoadFromConfigStore(ConfigStore.CreateFromFile(tmpFilePath));

				Assert.IsTrue(IsDataEqual(a, b), "SaveToFile / LoadFromFile");

				System.IO.File.Delete(tmpFilePath);
			}
		}
	}
}
