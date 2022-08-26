// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using UnityEngine;

	[CreateAssetMenu(
		menuName = Config.CreateAssetMenu.EA_GAMEOBJECT,
		order = Config.CreateAssetMenu.SORT_UNITY
	)]
	internal class EA_GameObject : EventAsset<GameObject> { }
}