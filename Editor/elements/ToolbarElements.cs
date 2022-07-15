using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
// using Sirenix.OdinInspector;

namespace com.flexford.packages.toolbar
{
	[Serializable]
	public class ToolbarElements : ScriptableObject
	{
		private const string CREATE_SETTINGS_MENU_PATH = "Assets/Create/" + ToolbarUtilities.PACKAGE_MENU_PATH + "Create settings";

		internal static ToolbarElements Instance => GetInstance();
		private static ToolbarElements _instance;

		[field: SerializeField]
		// [field: InlineEditor(InlineEditorModes.GUIOnly)]
		// [field: ListDrawerSettings(AlwaysAddDefaultValue = true)]
		public List<ToolbarElement> LeftElements { get; private set; }

		[field: SerializeField]
		// [field: InlineEditor(InlineEditorModes.GUIOnly)]
		// [field: ListDrawerSettings(AlwaysAddDefaultValue = true)]
		public List<ToolbarElement> RightElements { get; private set; }

		private void OnEnable()
		{
			CleanInstance();
		}

		[MenuItem(CREATE_SETTINGS_MENU_PATH)]
		private static void MenuCreateSettings()
		{
			const string dialogName = "Save toolbar settings";
			const string defaultFileName = "ExtendedToolbarSettings";
			ToolbarElements settings = ToolbarUtilities.CreateScriptableObjectDialog<ToolbarElements>(dialogName, defaultFileName);
			if (settings != null)
			{
				CleanInstance();
			}
		}

		[MenuItem(CREATE_SETTINGS_MENU_PATH, isValidateFunction: true)]
		private static bool MenuCreateSettingsValidate()
		{
			return !AssetDatabase.GetAssetPath(_instance).StartsWith("Assets/");
		}

		private static ToolbarElements GetInstance()
		{
			if (_instance == null)
			{
				_instance = GetFromProject();
			}

			if (_instance == null)
			{
				_instance = GetFromPackage();
			}

			return _instance;
		}

		private static void CleanInstance()
		{
			_instance = null;
		}

		private static ToolbarElements GetFromProject()
		{
			string[] settingsPaths = AssetDatabase.FindAssets($"t:{nameof(ToolbarElements)}", new []{"Assets"})
			                                      .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
			                                      .ToArray();

			if (settingsPaths.Length > 1)
			{
				var pathsWithLinks = settingsPaths.Select((path, i) => $"<a href=\"{path}\" line=\"0\">{i+1}) {path}</a>");
				string message = "Found multiple toolbar settings, will use first! Need remove redundant settings!";
				string paths = $"\r\nSettings:\r\n{string.Join("\r\n", pathsWithLinks)}\r\n";
				Debug.LogWarning($"{message}\r\n{paths}");
			}

			foreach (var settingsPath in settingsPaths)
			{
				ToolbarElements settings = AssetDatabase.LoadAssetAtPath<ToolbarElements>(settingsPath);
				if (settings != null)
				{
					return settings;
				}
			}

			return null;
		}

		private static ToolbarElements GetFromPackage()
		{
			PackageInfo packageInfo = PackageInfo.FindForAssembly(typeof(ToolbarElements).Assembly);
			string settingsPath = AssetDatabase.FindAssets($"t:{nameof(ToolbarElements)}", new []{packageInfo.assetPath})
			                                      .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
			                                      .FirstOrDefault();

			return AssetDatabase.LoadAssetAtPath<ToolbarElements>(settingsPath);
		}
	}
}