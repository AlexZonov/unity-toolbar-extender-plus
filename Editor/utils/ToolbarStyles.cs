using UnityEditor;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public static class ToolbarStyles
	{
		public static readonly GUIStyle Button;
		public static readonly GUIStyle Dropdown;
		public static readonly GUIStyle TextField;

		public static Color ThemeColor => EditorGUIUtility.isProSkin ? DarkImageColor : LightImageColor;

		private static readonly Color DarkImageColor = new Color32(196, 196, 196, 255);
		private static readonly Color LightImageColor = new Color32(94, 94, 94, 255);

		static ToolbarStyles()
		{
			Button = CreateButtonStyle(TextAlignment.Center);
			Dropdown = CreateDropdownStyle();
			TextField = CreateTextFieldStyle();
		}

		private static GUIStyle CreateButtonStyle(TextAlignment alignment)
		{
			GUIStyle result = new GUIStyle(EditorStyles.toolbarButton)
			{
				fontSize = 16, 
				fixedHeight = 18, 
				padding = new RectOffset(3, 3, 4, 3), 
				margin = new RectOffset()
			};

			return result;
		}

		private static GUIStyle CreateDropdownStyle()
		{
			GUIStyle result = new GUIStyle(EditorStyles.toolbarPopup)
			{
				fixedHeight = 18, 
				margin = new RectOffset()
			};

			return result;
		}

		private static GUIStyle CreateTextFieldStyle()
		{
			GUIStyle result = new GUIStyle(EditorStyles.toolbarTextField)
			{
				fixedHeight = 20, 
				alignment = TextAnchor.MiddleCenter, 
				margin = new RectOffset()
			};

			return result;
		}
	}
}