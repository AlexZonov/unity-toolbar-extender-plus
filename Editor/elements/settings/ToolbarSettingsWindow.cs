using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public class ToolbarSettingsWindow : PopupWindowContent
	{
		private Vector2 _scrollPosition;

		public override void OnGUI(Rect rect)
		{
			_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false, GUILayout.Height(rect.height), GUILayout.Width(rect.width));

			DrawHeader();
			DrawElementsSettings();

			EditorGUILayout.EndScrollView();
		}

		private static void DrawHeader()
		{
			EditorGUILayout.LabelField("Toolbar options", EditorStyles.boldLabel);
			EditorGUILayout.Separator();
		}

		private static void DrawElementsSettings()
		{
			ToolbarPrefs.LeftElementsGroup = DrawElementsVisibleGroup("Left elements", ToolbarPrefs.LeftElementsGroup, ToolbarElements.Instance.LeftElements);
			ToolbarPrefs.RightElementsGroup = DrawElementsVisibleGroup("Right elements", ToolbarPrefs.RightElementsGroup, ToolbarElements.Instance.RightElements);
		}

		private static bool DrawElementsVisibleGroup(string groupName, bool groupVisible, List<ToolbarElement> elements)
		{
			if (elements == null || !elements.Any(element => element != null))
			{
				return false;
			}

			groupVisible = EditorGUILayout.Foldout(groupVisible, groupName);

			if (groupVisible)
			{
				using (var scope = new EditorGUI.IndentLevelScope(1))
				{
					foreach (var element in elements)
					{
						if (element == null)
						{
							continue;
						}

						using (new EditorGUI.DisabledGroupScope(!element.CanHide))
						{
							element.Visible = EditorGUILayout.Toggle(element.Name, element.Visible);
						}
					}
				}
			}

			return groupVisible;
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(300, 450);
		}
	}
}