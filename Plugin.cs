using BepInEx;
using System;
using BepInEx.Configuration;
using Utilla;

namespace NameTags
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInIncompatibility("com.czangilos.gorillatag.nametagsotherfont")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<string> wyborczcionek;
        public static bool IsEnabled;
        private void Awake()
        {
            wyborczcionek = Config.Bind("Wybór czcionek",      
                "czcionka",  
                "1", 
                "Jaką czcionkę chciałbyś wybrać? 1 = Pixelowa, 2 = Nie pixelowa");
        }


        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/
            VRRig[] rigs = FindObjectsOfType<VRRig>();

            foreach (VRRig rig in rigs)
            {
                if (rig.GetComponent<NameTag>() == false && rig.isOfflineVRRig == false)
                {
                    rig.AddComponent<NameTag>();
                }
            }
            
            IsEnabled = true;
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/
            IsEnabled = false;
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
        }

    }
}
