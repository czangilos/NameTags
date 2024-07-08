using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace NameTagsOtherFont.Assets
{
    public class AssetRef
    {
        static Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("NameTagsOtherFont.Assets.nick");
        static AssetBundle bundle = AssetBundle.LoadFromStream(str);
        public static GameObject Tag = bundle.LoadAsset<GameObject>("NickNamePrefab");
    }
}
