using System;
using System.Collections.Generic;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace AudicaModding
{
    public class AudicaMod : MelonMod
    {
        public static class BuildInfo
        {
            public const string Name = "ForcedHitSounds";  // Name of the Mod.  (MUST BE SET)
            public const string Author = "Alternity"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "0.1.0"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }
        public override void OnApplicationStart()
        {
            Config.RegisterConfig();
        }

        public override void OnModSettingsApplied()
        {
            Config.OnModSettingsApplied();
        }

        [HarmonyPatch(typeof(AudioDriver), "StartPlaying", new Type[0])]
        private static class PlayPatch
        {
            private static void Postfix(AudioDriver __instance)
            {
                if (!Config.Enabled) return;
                SongCues.Cue[] cues = SongCues.I.GetCues();

                for (int i = 0; i < cues.Length; i++)
                {
                    if (cues[i].behavior != Target.TargetBehavior.Dodge && cues[i].behavior != Target.TargetBehavior.Melee)
                    {
                        if (cues[i].velocity != 1 && cues[i].velocity != 2 && cues[i].velocity != 20 && cues[i].velocity != 60 && cues[i].velocity != 127)
                        {
                            cues[i].velocity = 2;
                        }
                    }
                    else if (cues[i].behavior == Target.TargetBehavior.Melee)
                    {
                        if (cues[i].velocity != 3)
                        {
                            cues[i].velocity = 3;
                        }
                    }
                }
            }
        }
    }
}



