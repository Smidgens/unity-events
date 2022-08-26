// smidgens @ github

namespace Smidgenomics.Unity.Events.Editor
{
	using UnityEngine;
	using UnityEditor;
	using System;

	using SP = UnityEditor.SerializedProperty;

	internal static class EventFind
	{
		public static Type GetEventAssetType(Type vt)
		{
			if (vt == typeof(void) || vt == null) { return typeof(EA_void); }
			if (vt.IsPrimitive)
			{
				if (vt == typeof(int)) { return typeof(EA_int); }
				if (vt == typeof(bool)) { return typeof(EA_bool); }
				if (vt == typeof(float)) { return typeof(EA_float); }
			}
			else if (vt.IsValueType)
			{
				if (vt == typeof(Vector2)) { return typeof(EA_Vector2); }
				if (vt == typeof(Vector3)) { return typeof(EA_Vector3); }
			}
			if (vt == typeof(string)) { return typeof(EA_string); }
			if (vt == typeof(object)) { return typeof(EA_object); }
			if (vt == typeof(GameObject)) { return typeof(EA_GameObject); }
			return null;
		}
	}

	[CustomPropertyDrawer(typeof(EventReference))]
	[CustomPropertyDrawer(typeof(EventReference<>))]
	internal class _EventReference : PropertyDrawer
	{
		public static class FieldName
		{
			public const string
			ASSET = "_asset";
		}

		public override void OnGUI(Rect pos, SP prop, GUIContent l)
		{
			EnsureCache();

			// label not blank and item not inside array
			if (l != GUIContent.none && !fieldInfo.FieldType.IsArray)
			{
				pos = EditorGUI.PrefixLabel(pos, l);
			}

			using (new EditorGUI.PropertyScope(pos, l, prop))
			{
				var asset = prop.FindPropertyRelative(FieldName.ASSET);
				EventField(pos, asset);
			}
		}


		private bool _cacheInit = false;
		private Cache _cache = default;

		private void EventField(Rect pos, SP prop)
		{
			if (_cache.assetType != null)
			{
				EditorGUI.ObjectField(pos, prop, _cache.assetType, GUIContent.none);
			}
			else
			{
				// let unity enforce the type
				// (search won't show anything, manual dragging is required)
				// TODO: handle fallback better than this
				EditorGUI.ObjectField(pos, prop, GUIContent.none);
			}
		}

		private void EnsureCache()
		{
			if (_cacheInit) { return; }

			Type vtype = fieldInfo.FieldType.GenericTypeArguments.Length > 0
			? fieldInfo.FieldType.GenericTypeArguments[0]
			: typeof(void);

			_cacheInit = true;
			_cache = new Cache
			{
				valueType = vtype,
				assetType = EventFind.GetEventAssetType(vtype),
			};
		}

		private struct Cache
		{
			public Type assetType; // event asset type for lookup
			public Type valueType; // generic type argument
		}

	}
}