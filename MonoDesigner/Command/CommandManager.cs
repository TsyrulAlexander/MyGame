using System;
using System.Collections.Generic;

namespace MonoDesigner.Command {
	public static class CommandManager {
		public static event Action<CommandGroup> GroupAdded;
		private static List<CommandGroup> Groups { get; set; } = new List<CommandGroup>();
		public static IEnumerable<CommandGroup> GetItems() {
			return Groups;
		}
		public static void AddGroup(CommandGroup commandGroup) {
			Groups.Add(commandGroup);
			OnGroupAdded(commandGroup);
		}
		private static void OnGroupAdded(CommandGroup obj) {
			GroupAdded?.Invoke(obj);
		}
	}
}
