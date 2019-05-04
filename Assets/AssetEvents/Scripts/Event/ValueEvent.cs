namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	internal abstract class ValueEvent<T> : EventAsset
	{
		public delegate void StaticCallback(T v);
		public sealed override System.Type ArgumentType { get { return typeof(T); } }

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

		public void Invoke(T value)
		{
			if(!_staticFirst)
			{
				GetPersistentListeners().Invoke(value);
				if(_staticListeners != null){ _staticListeners.Invoke(value); }
			}
			else
			{
				if(_staticListeners != null){ _staticListeners.Invoke(value); }
				GetPersistentListeners().Invoke(value);
			}
		}

		protected abstract UnityEvent<T> GetPersistentListeners();

		private event StaticCallback _staticListeners = null;
	}
}