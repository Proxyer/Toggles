using HugsLib;

namespace Toggles.Source.News
{
    class ModBase_Toggles : ModBase
    {
        public override string ModIdentifier => Constants.ModID;

        protected override bool HarmonyAutoPatch => false;
    }
}