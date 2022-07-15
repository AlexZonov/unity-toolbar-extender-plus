using System;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public static partial class ToolbarLayout
	{
		public class ContentColorScope : IDisposable
		{
			private Color _prevColor;

			public ContentColorScope()
			{
				Start();
			}

			public static ContentColorScope Create()
			{
				return new ContentColorScope().Start();
			}

			public ContentColorScope Start()
			{
				_prevColor = GUI.contentColor;
				return this;
			}

			public void SetColor(Color color)
			{
				GUI.contentColor = color;
			}

			public void Dispose()
			{
				SetColor(_prevColor);
			}
		}
	}
}