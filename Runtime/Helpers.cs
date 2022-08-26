// smidgens @ github

namespace Smidgenomics.Unity.Events
{
	internal static class Helpers
	{
		public static void Swap<T>(ref T f1, ref T f2)
		{
			var t = f1;
			f1 = f2;
			f2 = t;
		}
	}
}