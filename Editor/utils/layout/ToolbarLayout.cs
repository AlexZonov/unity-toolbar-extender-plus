using System;
using UnityEditor;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public static partial class ToolbarLayout
	{
		private static ContentColorScope _contentColorScope;
		private static VerticalGroupScope _fieldsVerticalScope;

		static ToolbarLayout()
		{
			_contentColorScope = new ContentColorScope();
			_fieldsVerticalScope = new VerticalGroupScope();
		}

		public static bool ImageButton(Texture2D texture, float width, Action clickCallback, string tooltip = null)
		{
			return Button(null, texture, width, clickCallback, tooltip);
		}

		public static bool LabelButton(string label, float width, Action clickCallback, string tooltip = null)
		{
			return Button(label, null, width, clickCallback, tooltip);
		}

		public static int Dropdown(int currentIndex, string[] values, float width, Action<int> changeCallback, string tooltip = null)
		{
			int selectedIndex = EditorGUILayout.Popup(currentIndex, values, ToolbarStyles.Dropdown, GUILayout.Width(width));
			if (currentIndex != selectedIndex)
			{
				changeCallback?.Invoke(selectedIndex);
			}

			return selectedIndex;
		}

		public static int IntField(int currentValue, float width, Action<int> changeCallback, string tooltip = null)
		{
			int newValue = currentValue;
			using (_fieldsVerticalScope.Start(-1, 0f))
			{
				newValue = EditorGUILayout.IntField(currentValue, ToolbarStyles.TextField, GUILayout.Width(width));
			}

			if (currentValue != newValue)
			{
				changeCallback?.Invoke(newValue);
			}

			return newValue;
		}

		private static bool Button(string label, Texture2D texture, float width, Action clickCallback, string tooltip = null)
		{
			bool isClick = false;
			using (_contentColorScope.Start())
			{
				_contentColorScope.SetColor(ToolbarStyles.ThemeColor);
				GUIContent content = new GUIContent(label, texture, tooltip ?? label);
				isClick = GUILayout.Button(content, ToolbarStyles.Button, GUILayout.Width(width));
			}

			if (isClick)
			{
				clickCallback();
			}

			return isClick;
		}
	}
}