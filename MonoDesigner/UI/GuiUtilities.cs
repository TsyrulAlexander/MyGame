using System.Collections.Generic;
using MonoGame.Extended.Gui.Controls;

namespace MonoDesigner.UI
{
	static class GuiUtilities {
		public static void AddRange(this ControlCollection collection, IEnumerable<Control> items) {
			foreach (var control in items) {
				collection.Add(control);
			}
		}
	}
}
