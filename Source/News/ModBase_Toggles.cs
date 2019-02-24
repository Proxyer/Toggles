using HugsLib;

namespace Toggles.News
{
    class ModBase_Toggles : ModBase
    {
        public override string ModIdentifier => Constants.ModName;

        protected override bool HarmonyAutoPatch => false;
    }
}