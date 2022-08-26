// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using System;
	using UnityEngine;

	internal enum InvokeOrder
	{
		[InspectorName("Persistent -> Dynamic")]
		PersistentThenDynamic,
		[InspectorName("Dynamic -> Persistent")]
		DynamicThenPersistent,
	}

	public interface IEvent
	{
		public void AddListener(Action l);
		public void RemoveListener(Action l);
	}

	public interface IEvent<T>
	{
		public void AddListener(Action<T> l);
		public void RemoveListener(Action<T> l);
	}

	/// <summary>
	/// Base class for asset events
	/// </summary>
	public abstract class EventAsset : ScriptableObject
	{
		/// <summary>
		/// Value type of event parameter (default none)
		/// </summary>
		public virtual Type ArgType => typeof(void);

#if UNITY_EDITOR

		protected void SetModified() => Modified = Time.time;

		protected static readonly Delegate[] DELEGATES_0 = new Delegate[0];

		internal float Modified { get; private set; }
		internal virtual Delegate[] GetDelegates() => DELEGATES_0;

		internal static class _FN
		{
			public const string
			INVOKE_ORDER = nameof(_invokeOrder),
			PERSISTENT_LISTENERS = "_persistentListeners"; // defined in subclasses
		}
#endif

		internal bool DynamicFirst => _invokeOrder == InvokeOrder.DynamicThenPersistent;

		[SerializeField] private InvokeOrder _invokeOrder = default;

	}
}

namespace Smidgenomics.Unity.Events
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	/// <summary>
	/// Asset event that takes a single argument
	/// </summary>
	public abstract class EventAsset<T> : EventAsset, IEvent<T>
	{
		public sealed override Type ArgType => typeof(T);

		// add/rm dynamic listener
		public void AddListener(Action<T> l)
		{
#if UNITY_EDITOR
			SetModified();
#endif
			_staticListeners += l;
		}
		public void RemoveListener(Action<T> l)
		{
#if UNITY_EDITOR
			SetModified();
#endif
			_staticListeners -= l;
		}

		public void Invoke(T value)
		{
			Action<T> f1 = InvokePersistent, f2 = InvokeDynamic;
			if (DynamicFirst) { Helpers.Swap(ref f1, ref f2); }
			f1.Invoke(value);
			f2.Invoke(value);
		}

#if UNITY_EDITOR
		internal override Delegate[] GetDelegates()
		{
			return _staticListeners != null
			? _staticListeners.GetInvocationList()
			: base.GetDelegates();
		}
#endif

		private event Action<T> _staticListeners = default;
		[SerializeField] private UnityEvent<T> _persistentListeners = default;

		private void InvokePersistent(T v) => _persistentListeners.Invoke(v);
		private void InvokeDynamic(T v) => _staticListeners?.Invoke(v);

	}
}
