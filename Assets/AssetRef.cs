using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace NameTags.Assets
{
    public class AssetRef
    {
        static Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("NameTags.Assets.nick");
        static AssetBundle bundle = AssetBundle.LoadFromStream(str);
        public static GameObject Tag = bundle.LoadAsset<GameObject>("NickNamePrefab");
        public static TMP_FontAsset pixel = bundle.LoadAsset<TMP_FontAsset>("Pixel");
        public static TMP_FontAsset notpixel = bundle.LoadAsset<TMP_FontAsset>("NotPixel");
    }
}