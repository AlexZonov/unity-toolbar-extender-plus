using UnityEditor;

namespace com.flexford.packages.toolbar
{
	internal class ToolbarPrefs
	{
		public static bool LeftElementsGroup
		{
			get { return EditorPrefs.GetBool(nameof(LeftElementsGroup), true); }
			set { EditorPrefs.SetBool(nameof(LeftElementsGroup), value); }
		}

		public static bool RightElementsGroup
		{
			get { return EditorPrefs.GetBool(nameof(RightElementsGroup), true); }
			set { EditorPrefs.SetBool(nameof(RightElementsGroup), value); }
		}
	}
}