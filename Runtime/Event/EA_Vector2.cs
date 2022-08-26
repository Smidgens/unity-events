// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using UnityEngine;

	[CreateAssetMenu(
		menuName = Config.CreateAssetMenu.EA_VECTOR2, 
		order = Config.CreateAssetMenu.SORT_UNITY
	)]
	internal class EA_Vector2 : EventAsset<Vector2> { }
}