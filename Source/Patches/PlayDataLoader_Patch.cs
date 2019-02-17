using Verse;

namespace Toggles.Patches
{
    // Hooks into game load where defs have been loaded.
    internal class PlayDataLoader_Patch : Patch
    {
        internal PlayDataLoader_Patch() : base(
            patchType: typeof(PlayDataLoader_Patch),
            originType: typeof(PlayDataLoader),
            originMethod: "DoPlayLoad"
            )
        { }

        static void Postfix()
        {
            Initializer.OnDefsLoaded();
        }
    }
}