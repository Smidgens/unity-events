namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	[CreateAssetMenu(menuName="Asset Events/Int")]
	internal class IntEvent : ValueEvent<int>
	{
		protected sealed override UnityEvent<int> GetPersistentListeners()
		{
			return _persistentListeners;
		}

		[Serializable] private class ValueEvent : UnityEvent<int>{}
		[SerializeField] private ValueEvent _persistentListeners = null;
	}
}