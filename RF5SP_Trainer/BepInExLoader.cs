using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using Object = UnityEngine.Object;
namespace RF5SP_Trainer
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepInExLoader : BepInEx.IL2CPP.BasePlugin
    {
        public const string
            MODNAME = "RF5Trainer",
            AUTHOR = "baoenzo",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.0.0";

        public static BepInEx.Logging.ManualLogSource log;

        public BepInExLoader()
        {
            log = Log;
        }

        public override void Load()
        {
            log.LogMessage("[Trainer] Registering TrainerComponent in Il2Cpp");

            try
            {
                // Register our custom Types in Il2Cpp
                ClassInjector.RegisterTypeInIl2Cpp<TrainerComponent>();

                var go = new GameObject("TrainerObject");
                go.AddComponent<TrainerComponent>();
                Object.DontDestroyOnLoad(go);
            }
            catch
            {
                log.LogError("[Trainer] FAILED to Register Il2Cpp Type: TrainerComponent!");
            }

            try
            {
                var harmony = new Harmony("baoenzo.trainer.il2cpp");

                // Our Primary Unity Event Hooks 

                // Awake
                //var originalAwake = AccessTools.Method(typeof(SOME_GAME_TYPE), "Awake"); // Change Type!
                //log.LogMessage("[Trainer] Harmony - Original Method: " + originalAwake.DeclaringType.Name + "." + originalAwake.Name);
                //var postAwake = AccessTools.Method(typeof(TrainerComponent), "Awake");
                //log.LogMessage("[Trainer] Harmony - Postfix Method: " + postAwake.DeclaringType.Name + "." + postAwake.Name);
                //harmony.Patch(originalAwake, postfix: new HarmonyMethod(postAwake));

                //// Start
                //var originalStart = AccessTools.Method(typeof(SOME_GAME_TYPE), "Start"); // Change Type!
                //log.LogMessage("[Trainer] Harmony - Original Method: " + originalStart.DeclaringType.Name + "." + originalStart.Name);
                //var postStart = AccessTools.Method(typeof(TrainerComponent), "Start");
                //log.LogMessage("[Trainer] Harmony - Postfix Method: " + postStart.DeclaringType.Name + "." + postStart.Name);
                //harmony.Patch(originalStart, postfix: new HarmonyMethod(postStart));

                // Update
                var originalUpdate = AccessTools.Method(typeof(UnityEngine.UI.CanvasScaler), "Update");
                log.LogMessage("[Trainer] Harmony - Original Method: " + originalUpdate.DeclaringType.Name + "." + originalUpdate.Name);
                var postUpdate = AccessTools.Method(typeof(TrainerComponent), "Update");
                log.LogMessage("[Trainer] Harmony - Postfix Method: " + postUpdate.DeclaringType.Name + "." + postUpdate.Name);
                harmony.Patch(originalUpdate, postfix: new HarmonyMethod(postUpdate));
                log.LogMessage("[Trainer] Harmony - Runtime Patch's Applied");
                

            }
            catch
            {
                log.LogError("[Trainer] Harmony - FAILED to Apply Patch's!");
            }
           
        }
    }
}