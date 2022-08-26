// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	using UnityEngine;

	[CreateAssetMenu(
		menuName = Config.CreateAssetMenu.EA_OBJECT,
		order = Config.CreateAssetMenu.SORT_SYSTEM + 1
	)]
	internal class EA_object : EventAsset<object> { }
}