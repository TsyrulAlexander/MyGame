using System.Collections.Generic;

namespace MonoDesigner.Command {
	public class CommandGroup {
		public string Name { get; set; }
		private List<CommandItem> Items { get; set; } = new List<CommandItem>();
		public CommandGroup(string name = null, IEnumerable<CommandItem> items = null) {
			Name = name;
			if (items != null) {
				Items.AddRange(items);
			}
		}
		public IEnumerable<CommandItem> GetItems() {
			return Items;
		}
		public void Add(CommandItem item) {
			Items.Add(item);
		}
	}
}
