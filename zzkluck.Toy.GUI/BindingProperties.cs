using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzkluck.Toy.GUI
{
	public class BindingProperties
	{
		private string _nowDateTime;

		public string NowDateTime
		{
			get { return _nowDateTime; }
			set { _nowDateTime = value; }
		}

		public BindingProperties(string now)
		{
			_nowDateTime = now;
		}
	}
}
