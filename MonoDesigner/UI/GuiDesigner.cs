using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDesigner.Command;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;

namespace MonoDesigner.UI
{
	class GuiDesigner: IGuiDesigner {
		private GuiSystem _guiSystem;
		public GuiDesigner(GraphicsDevice graphicsDevice, BitmapFont font) {
			Skin.CreateDefault(font);
			_guiSystem = CreateUI(graphicsDevice);
			CommandManager.GroupAdded += CommandManagerOnGroupAdded;
		}
		private void CommandManagerOnGroupAdded(CommandGroup obj) {
			var panel = _guiSystem.ActiveScreen.FindControl<StackPanel>("CommandPanel");
			panel.Items.Clear();
			panel.Items.AddRange(GetControlPanelContent());
		}
		public GuiSystem CreateUI(GraphicsDevice graphicsDevice) {
			var viewportAdapter = new DefaultViewportAdapter(graphicsDevice);
			var guiRenderer = new GuiSpriteBatchRenderer(graphicsDevice, () => Matrix.Identity);
			return new GuiSystem(viewportAdapter, guiRenderer) { ActiveScreen = CreateScreen() };
		}
		private Screen CreateScreen() {
			return new Screen {
				Content = new DockPanel {
					LastChildFill = true,
					Items = {
						new StackPanel {
							AttachedProperties = {{DockPanel.DockProperty, Dock.Right}},
							Items = {
								GetCommandPanel(),
								GetPropertiesPanel()
							}
						},
						new StackPanel {
							AttachedProperties = {{DockPanel.DockProperty, Dock.Left}},
							Items = {
								GetGameObjectPanel()
							}
						},
						GetViewPortContent()
					}
				}
			};
		}
		protected virtual Control GetViewPortContent() {
			var panel = new ViewPortControl {
				Name = "ViewPortPanel"
			};
			return panel;
		}
		protected virtual Control GetGameObjectPanel() {
			var panel = new StackPanel {
				Name = "GameObjectPanel",
				VerticalAlignment = VerticalAlignment.Stretch,
				HorizontalAlignment = HorizontalAlignment.Right,
			};
			panel.Items.Add(new GameObjectControl());
			return panel;
		}
		protected virtual Control GetCommandPanel() {
			var panel = new StackPanel {
				Name = "CommandPanel",
				VerticalAlignment = VerticalAlignment.Stretch,
				HorizontalAlignment = HorizontalAlignment.Right,
			};
			panel.Items.AddRange(GetControlPanelContent());
			return panel;
		}
		protected virtual Control GetPropertiesPanel() {
			var panel = new StackPanel {
				Name = "PropertiesPanel",
				VerticalAlignment = VerticalAlignment.Stretch,
				HorizontalAlignment = HorizontalAlignment.Right,
			};
			
			return panel;
		}
		protected virtual ControlCollection GetControlPanelContent() {
			var collection = new ControlCollection();
			foreach (var group in CommandManager.GetItems()) {
				collection.Add(CreateCommandGroupControl(group));
			}
			return collection;
		}
		protected virtual Control CreateCommandGroupControl(CommandGroup commandGroup) {
			var commands = commandGroup.GetItems().Select(CreateCommandItemControl);
			var list = new StackPanel();
			list.Items.AddRange(commands);
			return new StackPanel {
				Items = {
					new Label(commandGroup.Name),
					list
				}
			};
		}
		protected virtual Control CreateCommandItemControl(CommandItem commandItem) {
			var button = new Button {
				Content = commandItem.Name
			};
			button.Clicked += (sender, args) => {
				commandItem.Click?.Invoke(commandItem, EventArgs.Empty);
			};
			return button;
		}
		public void Draw(GameTime gameTime) {
			_guiSystem.Draw(gameTime);
		}
		public void Update(GameTime gameTime) {
			_guiSystem.Update(gameTime);
		}
		public void Resize() {
			_guiSystem.ClientSizeChanged();
		}
	}
}
