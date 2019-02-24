//namespace Toggles
//{
//    // Class for creating instances of Toggles and saving these to memory.
//    internal static class ToggleFactory
//    {
//        internal static void Add(string label, string root, string group)
//        {
//            Toggle toggle = new Toggle(
//                    label: label,
//                    root: root,
//                    group: group
//                    );

//            ToggleManager.Add(toggle);

//        }

//        internal static void Remove(string label)
//        {
//            ToggleManager.Toggles.RemoveAll(x => x.Label.Equals(label));

//            ToggleManager.ToggleActive.Remove(label);
//        }
//    }
//}