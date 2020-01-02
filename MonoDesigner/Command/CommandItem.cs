using System;

namespace MonoDesigner.Command
{
	public class CommandItem {
		public string Name { get; set; }
		public Action<object, EventArgs> Click { get; set; }
		public CommandItem(string name = null, Action<object, EventArgs> click = null) {
			Name = name;
			Click = click;
		}
	}
}
