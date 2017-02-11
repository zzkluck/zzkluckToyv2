using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Zzkluck.Toy.Core.BirthdayMoudle
{
	public class BirthdayManager
	{
		private ObservableCollection<PersonInfoPlus> persons = new ObservableCollection<PersonInfoPlus>();

		private ObservableCollection<PersonInfoPlus> searchResult = new ObservableCollection<PersonInfoPlus>();

		private int nextID;
		public ObservableCollection<PersonInfoPlus> Persons
		{
			get { return persons; }
			set { persons = value; }
		}

		public ObservableCollection<PersonInfoPlus> SearchResult
		{
			get { return searchResult; }
			set { searchResult = value; }
		}

		public int NextID
		{
			get { return nextID; }
			set { nextID = value; }
		}

		public void WriteAllToFile(string path)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.NewLineOnAttributes = true;
			using (XmlWriter xw = XmlWriter.Create(path, settings))
			{
				xw.WriteStartElement("PersonInfo");
				xw.WriteAttributeString("NextID", NextID.ToString());
				foreach (PersonInfoPlus pip in Persons)
				{
					PersonInfoPlus.WriteToXML(xw, pip);
				}
				xw.WriteEndElement();
				xw.Flush();
			}
		}

		public void ReadAllFromFile(string path)
		{
			using (XmlReader xr = XmlReader.Create(path))
			{
				while (xr.Read())
				{
					if (xr.NodeType == XmlNodeType.Element && xr.Name == "PersonInfo")
					{
						NextID = Convert.ToInt32(xr.GetAttribute("NextID"));
					}
					if (xr.NodeType == XmlNodeType.Element && xr.Name == "Person")
					{
						XmlReader subElement = xr.ReadSubtree();
						Persons.Add(PersonInfoPlus.ReadFromXml(subElement));
					}
				}
			}
		}
	}
}
