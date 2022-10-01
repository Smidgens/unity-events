// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using UnityEngine;
	using UnityEngine.Events;

	public abstract class EventListener : MonoBehaviour
	{
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		protected void WarnMissingEvent()
		{
#if UNITY_EDITOR
			Debug.Log("Event not set, disabling", this);
#endif
		}

	}

	/// <summary>
	/// Base class for event listeners with single argument
	/// </summary>
	public abstract class EventListener<T> : EventListener
	{

#if UNITY_EDITOR

		internal static class _FN
		{
			public const string
			EVENT = nameof(_event),
			ON_EVENT = nameof(_onEvent);
		}

#endif
		[SerializeField] private EventReference<T> _event = default;
		[SerializeField] private UnityEvent<T> _onEvent = default;

		private EventAsset<T> _activeEvent = null;

		private void OnEnable()
		{
			_activeEvent = _event.Event;
			if (!_activeEvent)
			{
				WarnMissingEvent();
				enabled = false;
				return;
			}
			_activeEvent.AddListener(OnEvent);
		}

		private void OnDisable()
		{
			if (!_activeEvent) { return; }
			_activeEvent.RemoveListener(OnEvent);
			_activeEvent = null;
		}

		private void OnEvent(T v) => _onEvent.Invoke(v);
	}
}