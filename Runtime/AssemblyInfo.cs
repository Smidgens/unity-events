// smidgens @ github

using System.Runtime.CompilerServices;

#if UNITY_EDITOR
// expose internals to dev assemblies
[assembly:InternalsVisibleTo("Smidgenomics.Unity.Events.Editor")]
#endif