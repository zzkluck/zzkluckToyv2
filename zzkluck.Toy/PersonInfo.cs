using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzkluck.Toy
{
	public class PersonInfo:
		IComparable<PersonInfo>,ICloneable,IFormattable
	{
		#region Constructor
		public PersonInfo(string name,SexualEnum sexual,DateTime birthday,uint improtance)
		{
			Name = name;
			Sexual = sexual;
			Birthday = birthday;
			Improtance = improtance;
		}

		#endregion

		#region Fields
		private string _name;
		private SexualEnum _sexual;
		private DateTime _birthday;
		private uint _improtance;
		#endregion

		#region Properties
		public string Name
		{
			get { return _name; }
			private set { _name = value; }
		}

		public SexualEnum Sexual
		{
			get { return _sexual; }
			private set { _sexual = value; }
		}

		public DateTime Birthday
		{
			get { return _birthday; }
			private set { _birthday = value; }
		}

		public string BirthdayString
		{
			get { return Birthday.ToShortDateString(); }
		}

		public uint Improtance
		{
			get { return _improtance; }
			private set
			{
				if (value > 5 || value < 0)
					throw new IndexOutOfRangeException("Improtance must be between 0 and 5");
				_improtance = value;
			}
		}
		#endregion

		#region InterfaceAndOverrideMethod
		public int CompareTo(PersonInfo other)
		{
			if (ReferenceEquals(other,null)) return 1;
			int result=string.Compare(this.Name, other.Name);
			if (result == 0)
				result = DateTime.Compare(this.Birthday, other.Birthday);
			return result;
		}

		public virtual object Clone()
		{
			return new PersonInfo(_name, _sexual, _birthday, _improtance);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (format == null)
				return ToString();
			string upperFormat = format.ToUpper();
			switch (format)
			{
				case "N":
					return Name;
				case "S":
					return Enum.GetName(typeof(SexualEnum), Sexual);
				case "B":
					return Birthday.ToShortDateString();
				case "INFO":
					return string.Format("{0},{1},{2}",
						Name, Enum.GetName(typeof(SexualEnum), Sexual), Birthday.ToShortDateString());
				default:
					return ToString();
			}
		}
		public override string ToString()
		{
			return Name;
		}

		public static bool operator ==(PersonInfo lhs, PersonInfo rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(PersonInfo lhs, PersonInfo rhs)
		{
			return !(lhs == rhs);
		}

		public override bool Equals(object obj)
		{
			PersonInfo other = obj as PersonInfo;
			return this.Equals(other);	
		}

		public bool Equals(PersonInfo other)
		{
			if (ReferenceEquals(other, null)) return false;
			return (this.Name == other.Name) && (this.Sexual == other.Sexual) && (this.Birthday == other.Birthday);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}

	public class PersonInfoComparer : IComparer<PersonInfo>
	{
		PersonInfoComparer(PersonInfoCompareType compareType)
		{
			_compareType = compareType;
		}
		private PersonInfoCompareType _compareType;

		public PersonInfoCompareType CompareType
		{
			get { return _compareType; }
			set { _compareType = value; }
		}

		public int Compare(PersonInfo x, PersonInfo y)
		{
			if (x == null && y == null) return 0;
			else if (x == null) return -1;
			else if (y == null) return 1;

			switch (CompareType)
			{
				case PersonInfoCompareType.name:
					return string.Compare(x.Name, y.Name);
				case PersonInfoCompareType.sexual:
					return x.Sexual.CompareTo(y.Sexual);
				case PersonInfoCompareType.birthday:
					return DateTime.Compare(x.Birthday, y.Birthday);
				case PersonInfoCompareType.improtance:
					return x.Improtance.CompareTo(y.Improtance);
				default:
					throw new NotImplementedException();
			}
		}
	}

	public enum SexualEnum
	{
		male,
		female,
		rests
	}

	public enum PersonInfoCompareType
	{
		name,
		sexual,
		birthday,
		improtance
	}
}
