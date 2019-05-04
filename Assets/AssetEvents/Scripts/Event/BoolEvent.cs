namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	[CreateAssetMenu(menuName="Asset Events/Bool")]
	internal class BoolEvent : ValueEvent<bool>
	{
		protected sealed override UnityEvent<bool> GetPersistentListeners()
		{
			return _persistentListeners;
		}

		[Serializable] private class ValueEvent : UnityEvent<bool>{}
		[SerializeField] private ValueEvent _persistentListeners = null;
	}
}