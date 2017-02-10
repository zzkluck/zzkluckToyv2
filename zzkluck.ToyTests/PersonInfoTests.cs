using Microsoft.VisualStudio.TestTools.UnitTesting;
using zzkluck.Toy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzkluck.Toy.Tests
{
	[TestClass()]
	public class PersonInfoTests
	{
		static PersonInfo p1 = new PersonInfo("zzk", SexualEnum.male, new DateTime(1997, 06, 24), 5);
		static PersonInfo p2 = new PersonInfo("ltt", SexualEnum.female, new DateTime(1997, 12, 20), 5);

		PersonInfo p11= new PersonInfo("zzk", SexualEnum.male, new DateTime(1997, 06, 25), 5);

		[TestMethod()]
		public void CompareToTest()
		{
			Assert.AreEqual(true, p1.CompareTo(null) > 0);
			Assert.AreEqual(true, p1.CompareTo(p2) > 0);
			Assert.AreEqual(true, p2.CompareTo(p1) < 0);
			Assert.AreEqual(true, p1.CompareTo(p1) == 0);
			Assert.AreEqual(true, p2.CompareTo(p2) == 0);
		}

		[TestMethod()]
		public void CloneTest()
		{
			PersonInfo p3 = p1.Clone() as PersonInfo;
			Assert.AreEqual(true, p1 == p3);
			Assert.AreEqual(true, p1.CompareTo(p3) == 0);
		}

		[TestMethod()]
		public void ToStringTest()
		{
			Assert.AreEqual("zzk", p1.ToString());
			Assert.AreEqual("ltt", p2.ToString());
		}

		[TestMethod()]
		public void ToStringWithFormatTest()
		{
			Assert.AreEqual("zzk", string.Format("{0}", p1));
			Assert.AreEqual("zzk", string.Format("{0:N}", p1));
			Assert.AreEqual("male", string.Format("{0:S}", p1));
			Assert.AreEqual(p1.Birthday.ToString(), string.Format("{0:B}", p1));
			Assert.AreEqual("zzk,male,"+p1.Birthday.ToString(), string.Format("{0:INFO}", p1));
		}

		[TestMethod()]
		public void EqualsTest()
		{
			Assert.AreEqual(false, p1.Equals(p2));
			Assert.AreEqual(false, p1.Equals(null));
			Assert.AreEqual(false, p1.Equals(2414));
			Assert.AreEqual(false, p1.Equals("zzk"));
			Assert.AreEqual(false, p1.Equals(p11));
			Assert.AreEqual(true, p1.Equals(p1.Clone()));
		}
	}
}