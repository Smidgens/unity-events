namespace Smidgenomics.AssetEvents
{
	using System;
	using UnityEngine;

	internal abstract class EventAsset : ScriptableObject
	{
		public virtual Type ArgumentType { get { return null; } }
		public abstract int StaticListenerCount { get; }
		[SerializeField] protected bool _staticFirst = false;
		public virtual Delegate[] StaticDelegates { get {  return new Delegate[0]; } }
	}
}



#if UNITY_EDITOR

namespace Smidgenomics.AssetEvents
{
	using UnityEngine;
	using UnityEditor;
	using UnityEditorInternal;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(EventAsset), true)]
	internal class Editor_EventAsset : Editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			_staticFirst.boolValue = EditorGUILayout.Popup("Execution Order", _staticFirst.boolValue ? 1 : 0, _options) == 0 ? false : true;
			EditorGUILayout.EndVertical();
			EditorGUILayout.PropertyField(_persistentListeners);

			_staticListeners = _script.StaticDelegates;

			if(_staticList == null || _staticListeners.Length != _staticList.count)
			{
				_staticList = new ReorderableList(_staticListeners, typeof(System.Delegate), false, true, false, false);
				_staticList.drawElementCallback = DrawStatic;
				_staticList.elementHeight = EditorGUIUtility.singleLineHeight * 1.35f;

				_staticList.drawHeaderCallback = r =>
				{
					var type = _script.ArgumentType != null ? _script.ArgumentType.Name : "";
					EditorGUI.LabelField(r, string.Format("Static Listeners ({0})", type));
				};
			}

			_staticList.DoLayoutList();

			serializedObject.ApplyModifiedProperties();
		}

		private SerializedProperty _staticFirst = null, _persistentListeners = null;

		private string[] _options = { "Persistent ➜ Static", "Static ➜ Persistent" };

		private EventAsset _script = null;
		
		private ReorderableList _staticList = null;
		private System.Delegate[] _staticListeners = {};

		private void OnEnable()
		{
			_script = (EventAsset)target;
			_staticFirst = serializedObject.FindProperty("_staticFirst");
			_persistentListeners = serializedObject.FindProperty("_persistentListeners");
			Undo.undoRedoPerformed += OnUndo;
		}

		private void OnDisable()
		{
			Undo.undoRedoPerformed -= OnUndo;
		}

		private void DrawStatic(Rect rect, int index, bool isActive, bool isFocused)
		{
			var l = _staticListeners[index];
			var o = l.Target;
			if(o is UnityEngine.Object)
			{
				var uo = (UnityEngine.Object)o;
				var center = rect.center;
				rect.height = EditorGUIUtility.singleLineHeight;
				rect.center = center;
				rect.y -= 1.2f;
				EditorGUI.ObjectField(rect, uo.name, uo, typeof(UnityEngine.Object), true);
			}
			
		}

		private void OnUndo()
		{
			serializedObject.Update();
			Repaint();
		}
	}
}
#endif