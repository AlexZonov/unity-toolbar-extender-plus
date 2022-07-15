using System;
using UnityEngine;

namespace com.flexford.packages.toolbar
{
	public static partial class ToolbarLayout
	{
		public class EnabledScope : IDisposable
		{
			private bool _prevEnabled;

			public EnabledScope()
			{
				Start(GUI.enabled);
			}

			public static EnabledScope Create(bool initialValue)
			{
				return new EnabledScope().Start(initialValue);
			}

			public EnabledScope Start(bool enabled)
			{
				_prevEnabled = GUI.enabled;
				SetEnabled(enabled);
				return this;
			}

			public void SetEnabled(bool value)
			{
				GUI.enabled = value;
			}

			public void Dispose()
			{
				SetEnabled(_prevEnabled);
			}
		}
	}
}