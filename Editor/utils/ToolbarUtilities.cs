using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public static class ToolbarUtilities
	{
		public const string PACKAGE_MENU_PATH = "Tools/Toolbar Extender+/";

		public static string GetProjectWindowFolder()
		{
			string projectPath = new DirectoryInfo(Application.dataPath).Parent.FullName;
			string objectProjectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			string objectAbsolutePath = string.IsNullOrEmpty(objectProjectPath) ? Application.dataPath : $"{projectPath}/{objectProjectPath}";
			string objectCorrectAbsolutePath = objectAbsolutePath.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
			string folderAbsolutePath = File.Exists(objectCorrectAbsolutePath) ? Path.GetDirectoryName(objectCorrectAbsolutePath) : objectCorrectAbsolutePath;
			return Path.GetRelativePath(projectPath, folderAbsolutePath);
		}

		public static T CreateScriptableObjectDialog<T>(string dialogName, string defaultName) where T : ScriptableObject
		{
			string prefferedPath = ToolbarUtilities.GetProjectWindowFolder();
			string settingsAbsolutePath = EditorUtility.SaveFilePanel(dialogName, prefferedPath, defaultName, "asset");
			if (!string.IsNullOrEmpty(settingsAbsolutePath) && settingsAbsolutePath.Contains(Application.dataPath))
			{
				string projectPath = new DirectoryInfo(Application.dataPath).Parent.FullName;
				string settingsProjectPath = Path.GetRelativePath(projectPath, settingsAbsolutePath);

				T settings = ScriptableObject.CreateInstance<T>();
				AssetDatabase.CreateAsset(settings, settingsProjectPath);
				AssetDatabase.Refresh();

				return settings;
			}
			else
			{
				return null;
			}
		}
	}
}
