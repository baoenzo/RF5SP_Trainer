using System;
using HarmonyLib;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace RF5SP_Trainer
{
    public class TrainerComponent : MonoBehaviour
    {
        public TrainerComponent(IntPtr ptr) : base(ptr)
        {
            BepInExLoader.log.LogMessage("[Trainer] Entered Constructor");
        }

        // Harmony Patch's must be static!
        [HarmonyPostfix]
        public static void Awake()
        {
            BepInExLoader.log.LogMessage("[Trainer] I'm Awake!");
        }

        [HarmonyPostfix]
        public static void Start()
        {
            BepInExLoader.log.LogMessage("[Trainer] I'm Starting Up...");
        }

        [HarmonyPostfix]
        public static void Update()
        {
            if (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.F1) && Event.current.type == EventType.KeyDown)

            {
                SV.AddPlayerGold(10000);
                BepInExLoader.log.LogMessage("Add 100000 Gold !!!");
                Event.current.Use();
            }
            else if (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.F2) && Event.current.type == EventType.KeyDown)
            {
                SV.AddLumber(100);
                SV.AddStone(100);
                BepInExLoader.log.LogMessage("Add 100 lumber and stones !!!");
                Event.current.Use();

            }
            else if (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.F3) && Event.current.type == EventType.KeyDown)
            {
                SkillManager.RandomSkillLvUp();
                BepInExLoader.log.LogMessage("Random LV Up Skills");
                Event.current.Use();
            }
            else if (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.F4) && Event.current.type == EventType.KeyDown)
            {
                SV.AddSeedPoint(10000);
                BepInExLoader.log.LogMessage("Add 10000 SEED Points ");
                Event.current.Use();
            }
            
        }
    }
}
