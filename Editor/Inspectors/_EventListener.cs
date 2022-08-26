// smidgens @ github

namespace Smidgenomics.Unity.Events.Editor
{
	using UnityEditor;
	using SP = UnityEditor.SerializedProperty;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(EventListener), true)]
	internal class _EventListener : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.UpdateIfRequiredOrScript();
			PF(_event);
			PF(_onEvent);
			serializedObject.ApplyModifiedProperties();
		}

		private SP _event, _onEvent;

		private void OnEnable()
		{
			_event = serializedObject.FindProperty(EL_int._FN.EVENT);
			_onEvent = serializedObject.FindProperty(EL_int._FN.ON_EVENT);
		}

		private static void PF(SP p) => EditorGUILayout.PropertyField(p);

	}

}