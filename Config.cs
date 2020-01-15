using Rocket.API;

namespace Tortellio.ModeCycle
{
    public class Config : IRocketPluginConfiguration
    {
        public bool EnablePlugin;
        public bool InvertCycle;
        public string PVEStartMessage;
        public string PVEStartMessageIconURL;
        public string PVPStartMessage;
        public string PVPStartMessageIconURL;
        public bool PVEBarricadeGodMode;
        public bool PVEStructureGodMode;
        public bool PVEVehicleGodMode;
        public bool PVEAnimalGodMode;
        public bool PVEZombieGodMode;
        public bool PVPBarricadeGodMode;
        public bool PVPStructureGodMode;
        public bool PVPVehicleGodMode;
        public bool PVPAnimalGodMode;
        public bool PVPZombieGodMode;
        public void LoadDefaults()
        {
            EnablePlugin = true;
            InvertCycle = false;
            PVEStartMessage = "{color=green}PVE is enabled{/color}!";
            PVEStartMessageIconURL = "put image link here";
            PVPStartMessage = "{color=red}PVP is enabled{/color}!";
            PVPStartMessageIconURL = "put image link here";
            PVEBarricadeGodMode = true;
            PVEStructureGodMode = true;
            PVEVehicleGodMode = true;
            PVEAnimalGodMode = false;
            PVEZombieGodMode = false;
            PVPBarricadeGodMode = false;
            PVPStructureGodMode = false;
            PVPVehicleGodMode = false;
            PVPAnimalGodMode = false;
            PVPZombieGodMode = false;
        }
    }
}
