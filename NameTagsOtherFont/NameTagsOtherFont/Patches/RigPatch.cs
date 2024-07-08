using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;


namespace NameTagsOtherFont.Patches
{
    [HarmonyPatch(typeof(VRRig))]
    [HarmonyPatch("UpdateName", MethodType.Normal)]
    internal class UpdateInfoPatch
    {
        private static void Postfix(VRRig __instance)
        {
            if (!__instance.isOfflineVRRig)
            {
                if (!__instance.GetComponent<NameTag>())
                {
                    __instance.AddComponent<NameTag>();
                }
            }
        }
    }
}