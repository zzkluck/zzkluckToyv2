using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzkluck.Toy.GUI
{
	public class SomeInformation
	{
		private string _nowDateTime;

		public string NowDateTime
		{
			get { return _nowDateTime; }
			set { _nowDateTime = value; }
		}

		public SomeInformation(string now)
		{
			_nowDateTime = now;
		}
	}
}
