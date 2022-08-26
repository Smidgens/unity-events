// smidgens @ github

namespace Smidgenomics.Unity.Events.Editor
{
	using System;
	using UnityEditor;
	using UnityEditorInternal;
	using UnityEngine;
	using UnityObject = UnityEngine.Object;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(EventAsset), true)]
	internal class _EventAsset : Editor
	{
		public const string
		DYNAMIC_LISTENERS_LABEL = "Dynamic Listeners";

		public static readonly float
		LIST_ITEM_HEIGHT = EditorGUIUtility.singleLineHeight * 1.35f;

		public override void OnInspectorGUI()
		{
			serializedObject.UpdateIfRequiredOrScript();
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(_invokeOrder);

			GUILayout.Space(5f);
			EditorGUILayout.PropertyField(_persistentListeners);
			GUILayout.Space(5f);
			DrawStaticListeners();
			serializedObject.ApplyModifiedProperties();
		}

		private SerializedProperty
		_invokeOrder = null,
		_persistentListeners = null;

		private EventAsset _target = null;
		private ReorderableList _dynamicList = null;
		private string _dynamicListenersLabel = DYNAMIC_LISTENERS_LABEL;
		private (float, Delegate[]) _staticListeners = (-1f, null);

		private void OnEnable()
		{
			_target = (EventAsset)target;
			_invokeOrder = serializedObject.FindProperty(EventAsset._FN.INVOKE_ORDER);
			_persistentListeners = serializedObject.FindProperty(EventAsset._FN.PERSISTENT_LISTENERS);

			if(_target.ArgType != null && _target.ArgType != typeof(void))
			{
				_dynamicListenersLabel = $"{DYNAMIC_LISTENERS_LABEL} ({_target.ArgType.Name})";
			}
		}

		private void RefreshListeners()
		{
			if(_staticListeners.Item1 != _target.Modified)
			{
				_staticListeners = default;
			}
			_staticListeners = (_target.Modified, _target.GetDelegates());
		}

		// display list of serialized listeners
		private void DrawStaticListeners()
		{
			RefreshListeners();
			// regenerate list
			if (_dynamicList == null || _staticListeners.Item2.Length != _dynamicList.count)
			{
				_dynamicList = new ReorderableList(_staticListeners.Item2, typeof(Delegate), false, true, false, false);
				_dynamicList.drawElementCallback = DrawDynamicListener;
				_dynamicList.elementHeight = LIST_ITEM_HEIGHT;
				_dynamicList.drawHeaderCallback = DrawListHeader;
			}
			_dynamicList.DoLayoutList();
		}

		private void DrawListHeader(Rect pos)
		{
			EditorGUI.LabelField(pos, _dynamicListenersLabel);
		}

		private void DrawDynamicListener(Rect rect, int index, bool isActive, bool isFocused)
		{
			var l = _staticListeners.Item2[index];
			var o = l.Target;
			if (o is UnityObject)
			{
				var uo = (UnityObject)o;
				var center = rect.center;
				rect.height = EditorGUIUtility.singleLineHeight;
				rect.center = center;
				rect.y -= 1.2f;
				EditorGUI.ObjectField(rect, uo.name, uo, typeof(UnityObject), true);
			}
		}
	}
}