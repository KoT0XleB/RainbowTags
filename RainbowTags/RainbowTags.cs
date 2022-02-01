using Qurre;
using Qurre.API;
using Qurre.Events;
using Qurre.API.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Round = Qurre.API.Round;
using Player = Qurre.API.Player;

namespace RainbowTags
{
    public class RainbowTags : Plugin
    {
        public override string Developer => "KoT0XleB#4663";
        public override string Name => "RainbowTags";
        public override int Priority => int.MinValue;
        public override Version Version => new Version(1, 0, 0);
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        public Config CustomConfig { get; set; }
        public void RegisterEvents()
        {
            CustomConfig = new Config();
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Round.Start += OnRoundStarted;

        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Round.Start -= OnRoundStarted;
        }
        public void OnRoundStarted()
        {
            Timing.RunCoroutine(RainbowCoroutine(), "tags");
        }
        public IEnumerator<float> RainbowCoroutine()
        {
            int count = 0;
            while (!Round.Ended && CustomConfig.Color.Count > 0)
            {
                if (count == CustomConfig.Color.Count) count = 0;

                foreach (Player player in Player.List)
                {
                    if (CustomConfig.AdminOnlyEnable)
                    {
                        if (player.ServerRoles) player.RoleColor = CustomConfig.Color[count];
                    }
                    else player.RoleColor = CustomConfig.Color[count];
                }

                count++;
                yield return Timing.WaitForSeconds(CustomConfig.TimeInterval);
            }
            yield break;
        }
    }
}
