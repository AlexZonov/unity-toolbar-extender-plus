using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	[InitializeOnLoad]
	internal class ToolbarExtenderPlus : AssetPostprocessor
	{
		private static ToolbarLayout.VerticalGroupScope _verticalGroupScope;
		private static ToolbarLayout.HorizontalGroupScope _horizontalGroupScope;
		private static bool _didDomainReload;
		private static bool _didPostprocess;
		private static bool _isInited;

#if UNITY_2021_1_OR_NEWER
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
		{
			_didPostprocess = true;

			if (didDomainReload)
			{
				_didDomainReload = true;
			}

			TryInit();
		}
#else
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			_didPostprocess = true;
			TryInit();
		}
#endif

		static ToolbarExtenderPlus()
		{
			_didDomainReload = true;
			TryInit();
		}

		private static void TryInit()
		{
			if (_didPostprocess && _didDomainReload && !_isInited)
			{
				_didPostprocess = false;
				_didDomainReload = false;
				_isInited = true;
				Init();
			}
		}

		private static void Init()
		{
			_verticalGroupScope = new ToolbarLayout.VerticalGroupScope();
			_horizontalGroupScope = new ToolbarLayout.HorizontalGroupScope();

			ToolbarExtender.LeftToolbarGUI.Add(OnLeftGUI);
			ToolbarExtender.RightToolbarGUI.Add(OnRightGUI);
		}

		private static void OnLeftGUI()
		{
			GUILayout.FlexibleSpace();
			DrawSide(ToolbarElements.Instance?.LeftElements);
		}

		private static void OnRightGUI()
		{
			DrawSide(ToolbarElements.Instance?.RightElements);
			GUILayout.FlexibleSpace();
		}

		private static void DrawSide(List<ToolbarElement> elements)
		{
			if (elements == null || elements.Count == 0)
			{
				return;
			}

			using (_verticalGroupScope.Start(1f, 0f))
			{
				using (_horizontalGroupScope.Start(0f, 0f))
				{
					foreach (ToolbarElement element in elements)
					{
						if (element != null && element.Visible)
						{
							element?.DrawCallback?.Invoke();
							GUILayout.Space(3f);
						}
					}
				}
			}
		}
	}
}