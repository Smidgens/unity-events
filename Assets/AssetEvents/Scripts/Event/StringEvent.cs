namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	[CreateAssetMenu(menuName="Asset Events/String")]
	internal class StringEvent : ValueEvent<string>
	{
		protected sealed override UnityEvent<string> GetPersistentListeners()
		{
			return _persistentListeners;
		}

		[Serializable] private class ValueEvent : UnityEvent<string>{}
		[SerializeField] private ValueEvent _persistentListeners = null;
	}
}