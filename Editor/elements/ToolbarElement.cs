using System;
// using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public abstract class ToolbarElement : ScriptableObject
	{
		[field: SerializeField]
		public bool CanHide { get; private set; } = true;

		[field: SerializeField]
		public bool VisibleByDefault { get; private set; } = true;

		// [ShowInInspector]
		public bool Visible
		{
			get => GetVisible(Name, VisibleByDefault);
			set => SetVisible(Name, value);
		}

		public Action DrawCallback => Draw;
		public abstract string Name { get; }

		public static bool GetVisible(string name, bool defaultValue)
		{
			return EditorPrefs.GetBool(name, defaultValue);
		}

		public static void SetVisible(string name, bool value)
		{
			EditorPrefs.SetBool(name, value);
		}

		private void Draw()
		{
			DrawImpl();
		}

		protected abstract void DrawImpl();
	}
}