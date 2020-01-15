using System;
using Steamworks;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Tortellio.ModeCycle
{
    public class ModeCycle : RocketPlugin<Config>
    {
        public static ModeCycle Instance;
        public static string PluginName = "ModeCycle";
        public static string PluginVersion = " 1.0.0";
        protected override void Load()
        {
            Instance = this;
            Logger.Log("ModeCycle has been loaded!", ConsoleColor.Yellow);
            Logger.Log(PluginName + PluginVersion, ConsoleColor.Yellow);
            Logger.Log("Made by Tortellio", ConsoleColor.Yellow);

            if (!Instance.Configuration.Instance.EnablePlugin)
            {
                Logger.Log("ModeCycle is disabled in configuration.. unloading!");
                Unload();
                return;
            }
            LightingManager.onDayNightUpdated += OnDayNightUpdated;
            DamageTool.damagePlayerRequested += OnPlayerDamaged;
            DamageTool.damageAnimalRequested += OnAnimalDamaged;
            DamageTool.damageZombieRequested += OnZombieDamaged;
            VehicleManager.onDamageVehicleRequested += OnVehicleDamaged;
            VehicleManager.onDamageTireRequested += OnTireDamaged;
            BarricadeManager.onDamageBarricadeRequested += OnBarricadeDamaged;
            StructureManager.onDamageStructureRequested += OnStructureDamaged;
        }
        protected override void Unload()
        {
            Instance = null;
            Logger.Log("ModeCycle has been unloaded!");
            Logger.Log("Visit Tortellio Discord for more! https://discord.gg/pzQwsew", ConsoleColor.Yellow);

            LightingManager.onDayNightUpdated -= OnDayNightUpdated;
            DamageTool.damagePlayerRequested -= OnPlayerDamaged;
            DamageTool.damageAnimalRequested -= OnAnimalDamaged;
            DamageTool.damageZombieRequested -= OnZombieDamaged;
            VehicleManager.onDamageVehicleRequested -= OnVehicleDamaged;
            VehicleManager.onDamageTireRequested -= OnTireDamaged;
            BarricadeManager.onDamageBarricadeRequested -= OnBarricadeDamaged;
            StructureManager.onDamageStructureRequested -= OnStructureDamaged;
        }

        public void OnDayNightUpdated(bool isDayTime)
        {
            if (!Configuration.Instance.InvertCycle)
            {
                if (isDayTime)
                {
                    ChatManager.serverSendMessage(Configuration.Instance.PVEStartMessage.Replace("{","<").Replace("}",">"), Color.white, null, null, EChatMode.GLOBAL, Configuration.Instance.PVEStartMessageIconURL, true);
                    return;
                }
                ChatManager.serverSendMessage(Configuration.Instance.PVPStartMessage.Replace("{", "<").Replace("}", ">"), Color.white, null, null, EChatMode.GLOBAL, Configuration.Instance.PVPStartMessageIconURL, true);
                return;
            }
            else if (Configuration.Instance.InvertCycle) 
            {
                if (isDayTime)
                {
                    ChatManager.serverSendMessage(Configuration.Instance.PVPStartMessage.Replace("{", "<").Replace("}", ">"), Color.white, null, null, EChatMode.GLOBAL, Configuration.Instance.PVPStartMessageIconURL, true);
                    return;
                }
                ChatManager.serverSendMessage(Configuration.Instance.PVEStartMessage.Replace("{", "<").Replace("}", ">"), Color.white, null, null, EChatMode.GLOBAL, Configuration.Instance.PVEStartMessageIconURL, true);
                return;
            }
        }

        public void OnPlayerDamaged(ref DamagePlayerParameters parameter, ref bool allow)
        {
            if (Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    allow = false;
                }
            }
            else if (!Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    allow = false;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    allow = true;
                }
            }
        }

        public void OnAnimalDamaged(ref DamageAnimalParameters parameter, ref bool allow)
        {
            if (Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVPAnimalGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVEAnimalGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
            else if (!Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVEAnimalGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVPAnimalGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
        }

        public void OnZombieDamaged(ref DamageZombieParameters parameter, ref bool allow)
        {
            if (Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVPZombieGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVEZombieGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
            else if (!Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVEZombieGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVPZombieGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
        }
        public void OnVehicleDamaged(CSteamID steamID, InteractableVehicle vehicle, ref ushort pendingDamage, ref bool canRepair, ref bool allow, EDamageOrigin damageOrigin)
        {
            if (Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVPVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVEVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
            else if (!Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVEVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVPVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
        }

        public void OnTireDamaged(CSteamID steamID, InteractableVehicle vehicle, int tireIndex, ref bool allow, EDamageOrigin damageOrigin)
        {
            if (Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVPVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVEVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
            else if (!Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVEVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVPVehicleGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
        }

        public void OnBarricadeDamaged(CSteamID steamID, Transform barricadeTransform, ref ushort pendingDamage, ref bool allow, EDamageOrigin damageOrigin)
        {
            if (Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVPBarricadeGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVEBarricadeGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
            else if (!Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVEBarricadeGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVPBarricadeGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
        }

        public void OnStructureDamaged(CSteamID steamID, Transform structureTransform, ref ushort pendingDamage, ref bool allow, EDamageOrigin damageOrigin)
        {
            if (Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVPStructureGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVEStructureGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
            else if (!Configuration.Instance.InvertCycle)
            {
                if (LightingManager.isDaytime)
                {
                    if (Configuration.Instance.PVEStructureGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
                else if (LightingManager.isFullMoon || LightingManager.isNighttime)
                {
                    if (Configuration.Instance.PVPStructureGodMode)
                    {
                        allow = false;
                        return;
                    }
                    allow = true;
                }
            }
        }
    }
}
