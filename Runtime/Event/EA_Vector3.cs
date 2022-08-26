// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using UnityEngine;

	[CreateAssetMenu(
		menuName = Config.CreateAssetMenu.EA_VECTOR3,
		order = Config.CreateAssetMenu.SORT_UNITY
	)]
	internal class EA_Vector3 : EventAsset<Vector3> { }
}