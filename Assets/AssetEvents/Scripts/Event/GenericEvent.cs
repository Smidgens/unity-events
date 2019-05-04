namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	[CreateAssetMenu(menuName="Asset Events/Void")]
	internal class GenericEvent : EventAsset
	{
		public delegate void StaticCallback();
		public override int StaticListenerCount { get { return _staticListeners != null ? _staticListeners.GetInvocationList().Length : 0; } }
		public override Delegate[] StaticDelegates { get { return _staticListeners != null ? _staticListeners.GetInvocationList() : base.StaticDelegates; } }

		public void Register(StaticCallback listener)
		{
			_staticListeners += listener;
		}

		public void Unregister(StaticCallback listener)
		{
			_staticListeners -= listener;
		}

		public void Invoke()
		{
			if(!_staticFirst)
			{
				_persistentListeners.Invoke();
				if(_staticListeners != null){ _staticListeners.Invoke(); }
			}
			else
			{
				if(_staticListeners != null){ _staticListeners.Invoke(); }
				_persistentListeners.Invoke();
			}
		}

		[SerializeField] private UnityEvent _persistentListeners = null;
		private event StaticCallback _staticListeners = null;
	}
}