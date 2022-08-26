// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	internal static class Config
	{
		public static class AddComponentMenu
		{
			public const string
			EL_VOID = _PF_EL,
			EL_INT = _PF_EL + " (int)",
			EL_BOOL = _PF_EL + " (bool)",
			EL_FLOAT = _PF_EL + " (float)",
			EL_STRING = _PF_EL + " (string)",
			EL_OBJECT = _PF_EL + " (object)",
			EL_GAMEOBJECT = _PF_EL + " (GameObject)",
			EL_V2 = _PF_EL + " (Vector2)",
			EL_V3 = _PF_EL + " (Vector3)";

			private const string _PF_EL = _PREFIX + "Event Listener";

			private const string _PREFIX = "Smidgenomics/Events/";
		}

		public static class CreateAssetMenu
		{
			public const int
			SORT_SYSTEM = 0,
			SORT_UNITY = 35;
			public const string
			EA_VOID = _PREFIX + "void",
			EA_BOOL = _PREFIX + "bool",
			EA_FLOAT = _PREFIX + "float",
			EA_INT = _PREFIX + "int",
			EA_STRING = _PREFIX + "string",
			EA_GAMEOBJECT = _PREFIX + "GameObject",
			EA_OBJECT = _PREFIX + "object",
			EA_VECTOR2 = _PREFIX + "Vector2",
			EA_VECTOR3 = _PREFIX + "Vector3";
			private const string
			_PREFIX = "Event/";
		}
	}
}