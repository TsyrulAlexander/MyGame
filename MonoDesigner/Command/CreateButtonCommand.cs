using System;
using System.Collections.Generic;
using System.Text;

namespace MonoDesigner.Command
{
	public class CreateButtonCommand: CommandItem
	{
		public CreateButtonCommand(): base("Create button", OnClick) {
			
		}
		private static void OnClick(object arg1, EventArgs arg2) {
			
		}
	}
}
