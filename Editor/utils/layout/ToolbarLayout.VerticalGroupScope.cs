using System;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public static partial class ToolbarLayout
	{
		public class VerticalGroupScope : IDisposable
		{
			private float _paddingBottom;

			public static VerticalGroupScope Create(float paddingTop, float paddingBottom)
			{
				return new VerticalGroupScope().Start(paddingTop, paddingBottom);
			}

			public VerticalGroupScope Start(float paddingTop, float paddingBottom)
			{
				GUILayout.BeginVertical();
				GUILayout.Space(paddingTop);
				_paddingBottom = paddingBottom;
				return this;
			}

			public void Dispose()
			{
				GUILayout.Space(_paddingBottom);
				GUILayout.EndVertical();
			}
		}
	}
}