// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using System;
	using UnityEngine;

	/// <summary>
	/// Wrapper for void event
	/// </summary>
	[Serializable]
	public struct EventReference
	{
		internal EA_void Event => _asset;
		[SerializeField] private EA_void _asset;

#if UNITY_EDITOR
		// refactor helper
		internal static class _FN
		{
			public const string
			ASSET = nameof(_asset);
		}
#endif

	}

	/// <summary>
	/// Wrapper for event with 1 argument
	/// </summary>
	[Serializable]
	public struct EventReference<T>
	{
		internal EventAsset<T> Event => _asset;

		[SerializeField] private EventAsset<T> _asset;
	}
}