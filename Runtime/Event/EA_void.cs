// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	[CreateAssetMenu(
		menuName = Config.CreateAssetMenu.EA_VOID,
		order = Config.CreateAssetMenu.SORT_SYSTEM - 20
	)]
	internal partial class EA_void : EventAsset, IEvent
	{
		public void AddListener(Action l)
		{
#if UNITY_EDITOR
			SetModified();
#endif
			_staticListeners += l;
		}
		public void RemoveListener(Action l)
		{
#if UNITY_EDITOR
			SetModified();
#endif
			_staticListeners -= l;
		}

		public void Invoke()
		{
			Action f1 = InvokePersistent, f2 = InvokeDynamic;
			if (DynamicFirst) { Helpers.Swap(ref f1, ref f2); }
			f1.Invoke();
			f2.Invoke();
		}

#if UNITY_EDITOR
		internal override Delegate[] GetDelegates()
		{
			return _staticListeners != null
			? _staticListeners.GetInvocationList()
			: base.GetDelegates();
		}
#endif

		[SerializeField] private UnityEvent _persistentListeners = null;
		private event Action _staticListeners = null;

		private void InvokePersistent() => _persistentListeners.Invoke();
		private void InvokeDynamic() => _staticListeners?.Invoke();

	}
}