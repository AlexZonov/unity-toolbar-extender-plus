using UnityEditor;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	[CreateAssetMenu(fileName = "SettingsElement", menuName = ToolbarUtilities.PACKAGE_MENU_PATH + "Elements/Settings")]
	public class ToolbarSettingsElement : ToolbarElement
	{
		[field: SerializeField]
		public Texture2D Icon { get; private set; }

		public override string Name => "Toolbar settings";

		protected override void DrawImpl()
		{
			ToolbarLayout.ImageButton(Icon, 30f, OnButtonClick, "Toolbar settings");
		}

		private static void OnButtonClick()
		{
			ToolbarSettingsWindow popup = new ToolbarSettingsWindow();
			Rect rect = GUILayoutUtility.GetLastRect();
			rect.y += 30;

			PopupWindow.Show(rect, popup);
		}
	}
}