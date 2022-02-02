using Qurre;
using Qurre.API;
using Qurre.Events;
using Qurre.API.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;

using Round = Qurre.API.Round;
using Player = Qurre.API.Player;

namespace RainbowTags
{
    public class RainbowTags : Plugin
    {
        public override string Developer => "KoT0XleB#4663";
        public override string Name => "RainbowTags";
        public override int Priority => int.MinValue;
        public override Version Version => new Version(2, 0, 0);
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        public static Config CustomConfig { get; set; }
        public void RegisterEvents()
        {
            CustomConfig = new Config();
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Player.Join += OnJoin;

        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Player.Join -= OnJoin;
        }
        public void OnJoin(JoinEvent ev)
        {
            if (CustomConfig.AdminOnlyEnable)
            {
                if (ev.Player.ServerRoles.RemoteAdmin)
                {
                    var component = ev.Player.ReferenceHub.GetComponent<RainbowController>();
                    if (component == null) component = ev.Player.GameObject.AddComponent<RainbowController>();
                }
            }
            else
            {
                var component = ev.Player.ReferenceHub.GetComponent<RainbowController>();
                if (component == null) component = ev.Player.GameObject.AddComponent<RainbowController>();
            }
        }
        public class RainbowController : MonoBehaviour
        {
            private ServerRoles _roles;
            private string _originalColor;

            private int _position = 0;
            private float _nextCycle = 0f;

            public static float interval { get; set; } = CustomConfig.TimeInterval;

            private void Start()
            {
                _roles = GetComponent<ServerRoles>();
                _nextCycle = Time.time;
                _originalColor = _roles.Network_myColor;
            }

            private void OnDisable()
            {
                _roles.Network_myColor = _originalColor;
            }

            private void Update()
            {
                if (Time.time < _nextCycle) return;
                _nextCycle += interval;

                _roles.Network_myColor = CustomConfig.Color[_position];

                if (++_position >= CustomConfig.Color.Count)
                    _position = 0;
            }
        }
    }
}
