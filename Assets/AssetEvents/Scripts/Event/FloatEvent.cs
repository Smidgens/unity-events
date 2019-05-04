namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	[CreateAssetMenu(menuName="Asset Events/Float")]
	internal class FloatEvent : ValueEvent<float>
	{
		protected sealed override UnityEvent<float> GetPersistentListeners()
		{
			return _persistentListeners;
		}

		[Serializable] private class ValueEvent : UnityEvent<float>{}
		[SerializeField] private ValueEvent _persistentListeners = null;
	}
}