using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzkluck.Toy.Core.BirthdayMoudle
{
	public class PersonInfoPlus : PersonInfo
	{
		public PersonInfoPlus(int id,string name, SexualEnum sexual, DateTime birthday, uint improtance, string icon) :
			base(name, sexual, birthday, improtance)
		{
			_id = id;
			_icon = icon;
		}

		public PersonInfoPlus(IList<string> Properties) :
			this(Convert.ToInt32(Properties[0]),
				Properties[1],
				StrToSexualEnum(Properties[2]),
				DateTime.Parse(Properties[3]),
				Convert.ToUInt32(Properties[4]),
				Properties[5])
		{}
		private int _id;

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}


		private string _icon;
		public string Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		public override object Clone()
		{
			return new PersonInfoPlus(ID,Name, Sexual, Birthday, Improtance, Icon);
		}

		public static SexualEnum StrToSexualEnum(string str)
		{
			switch (str)
			{
				case "male":
					return SexualEnum.male;
				case "female":
					return SexualEnum.female;
				case "rests":
					return SexualEnum.rests;
				default:
					throw new ArgumentException();
			}
		}

		/// <summary>
		/// 从一个XML文件里读取personInfo，接受一个可能由getSubtree()获得的名为Person的节点所对应的XmlReader
		/// </summary>
		/// <param name="PersonNode"></param>
		/// <returns></returns>
		public static PersonInfoPlus ReadFromXml(XmlReader PersonNode)
		{
			List<string> properties = new List<string>();
			PersonNode.Read();
			properties.Add(PersonNode.GetAttribute("ID"));
			while (PersonNode.Read())
			{
				if (PersonNode.NodeType == XmlNodeType.Text)
				{
					properties.Add(PersonNode.Value);
				}
			}
			return new PersonInfoPlus(properties);
		}

		public static void WriteToXML(XmlWriter xw, PersonInfoPlus pip)
		{
			xw.WriteStartElement("Person");
			xw.WriteAttributeString("ID", pip.ID.ToString());
			xw.WriteElementString("Name", pip.Name);
			xw.WriteElementString("Sexual", Enum.GetName(typeof(SexualEnum), pip.Sexual));
			xw.WriteElementString("Birthday", pip.Birthday.ToShortDateString());
			xw.WriteElementString("Improtance", pip.Improtance.ToString());
			xw.WriteElementString("IconPath", pip.Icon);
			xw.WriteEndElement();
		}
	}
}
