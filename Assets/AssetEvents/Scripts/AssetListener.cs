namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;

	[AddComponentMenu("Asset Events/Listener")]
	internal class AssetListener : MonoBehaviour
	{
		[Serializable] private class FloatEvent : UnityEvent<float>{}
		[Serializable] private class StringEvent : UnityEvent<string>{}
		[Serializable] private class BoolEvent : UnityEvent<bool>{}
		[Serializable] private class IntEvent : UnityEvent<int>{}
		[SerializeField] private EventAsset _eventAsset = null;
		[SerializeField] private UnityEvent _receivers = null;
		[SerializeField] private StringEvent _stringReceivers = null;
		[SerializeField] private FloatEvent _floatReceivers = null;
		[SerializeField] private BoolEvent _boolReceivers = null;
		[SerializeField] private IntEvent _intReceivers = null;

		private EventAsset _cachedAsset = null;

		private void OnEnable()
		{
			if(_eventAsset)
			{
				RegisterEvent();
				_cachedAsset = _eventAsset;
			}
		}

		private void OnDisable()
		{
			if(_cachedAsset)
			{
				UnRegisterEvent();
				_cachedAsset = null;
			}
		}

		private void RegisterEvent()
		{
			var t = _eventAsset.ArgumentType;
			if(t == typeof(string)) { RegisterType<string>(_eventAsset, OnString); }
			else if(t == typeof(float)) { RegisterType<float>(_eventAsset, OnFloat); }
			else if(t == typeof(bool)) { RegisterType<bool>(_eventAsset, OnBool); }
			else if(t == typeof(int)) { RegisterType<int>(_eventAsset, OnInt); }
			else { ((GenericEvent)_eventAsset).Register(OnNone); }
		}
		
		private void UnRegisterEvent()
		{
			var t = _eventAsset.ArgumentType;
			if(t == typeof(string)) { UnRegisterType<string>(_eventAsset, OnString); }
			else if(t == typeof(float)) { UnRegisterType<float>(_eventAsset, OnFloat); }
			else if(t == typeof(bool)) { UnRegisterType<bool>(_eventAsset, OnBool); }
			else if(t == typeof(int)) { UnRegisterType<int>(_eventAsset, OnInt); }
			else { ((GenericEvent)_cachedAsset).Unregister(OnNone); }
		}


		private void UnRegisterType<T>(EventAsset asset, ValueEvent<T>.StaticCallback handler)
		{
			((ValueEvent<T>)asset).Unregister(handler);
		}

		private void RegisterType<T>(EventAsset asset, ValueEvent<T>.StaticCallback  handler)
		{
			((ValueEvent<T>)asset).Register(handler);
		}

		private void OnNone() { _receivers.Invoke(); }
		private void OnString(string v) { _stringReceivers.Invoke(v); }
		private void OnFloat(float v) { _floatReceivers.Invoke(v); }
		private void OnBool(bool v) { _boolReceivers.Invoke(v); }
		private void OnInt(int v) { _intReceivers.Invoke(v); }
	}
}


#if UNITY_EDITOR
namespace Smidgenomics.AssetEvents
{

	using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.SceneManagement;
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(AssetListener))]
	internal class Editor_AssetListener: Editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(_eventAsset);
			if(_eventAsset.objectReferenceValue)
			{
				var asset = (EventAsset)_eventAsset.objectReferenceValue;
				SerializedProperty p = null;
				var t = asset.ArgumentType;
				if(t == typeof(string)) { p = _stringReceivers; }
				else if(t == typeof(float)) { p = _floatReceivers; }
				else if(t == typeof(bool)) { p = _boolReceivers; }
				else if(t == typeof(int)) { p = _intReceivers; }
				else { p = _receivers; }
				EditorGUILayout.PropertyField(p, new GUIContent("Receivers"));
			}
			else
			{
				EditorGUILayout.HelpBox("No Asset set.", MessageType.Info);
			}
			serializedObject.ApplyModifiedProperties();
		}

		private SerializedProperty _eventAsset = null;
		private SerializedProperty _receivers = null;
		private SerializedProperty _stringReceivers = null;
		private SerializedProperty _floatReceivers = null;
		private SerializedProperty _boolReceivers = null;
		private SerializedProperty _intReceivers = null;

		private void OnEnable()
		{
			_eventAsset = serializedObject.FindProperty("_eventAsset");
			_receivers = serializedObject.FindProperty("_receivers");
			_stringReceivers = serializedObject.FindProperty("_stringReceivers");
			_floatReceivers = serializedObject.FindProperty("_floatReceivers");
			_boolReceivers = serializedObject.FindProperty("_boolReceivers");
			_intReceivers = serializedObject.FindProperty("_intReceivers");
		}
	}
	
}

#endif