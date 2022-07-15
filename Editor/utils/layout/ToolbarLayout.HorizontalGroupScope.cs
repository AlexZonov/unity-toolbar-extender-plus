using System;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public static partial class ToolbarLayout
	{
		public class HorizontalGroupScope : IDisposable
		{
			private float _paddingRight;

			public static HorizontalGroupScope Create(float paddingLeft, float paddingRight)
			{
				return new HorizontalGroupScope().Start(paddingLeft, paddingRight);
			}

			public HorizontalGroupScope Start(float paddingLeft, float paddingRight)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Space(paddingLeft);
				_paddingRight = paddingRight;
				return this;
			}

			public void Dispose()
			{
				GUILayout.Space(_paddingRight);
				GUILayout.EndHorizontal();
			}
		}
	}
}