// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using UnityEngine;
	using UnityEngine.Events;

	/// <summary>
	/// Asset event listener
	/// </summary>
	[AddComponentMenu(Config.AddComponentMenu.EL_VOID)]
	internal class EL_void : EventListener
	{
		// event asset
		[SerializeField] private EventReference _event = default;
		[SerializeField] private UnityEvent _onEvent = default;

		// event currently subscribed to
		private EA_void _activeEvent = null;

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
			if (!_activeEvent) { return; } // nothing was used
			_activeEvent.RemoveListener(OnEvent);
			_activeEvent = null;
		}

		private void OnEvent() => _onEvent.Invoke();

	}
}
