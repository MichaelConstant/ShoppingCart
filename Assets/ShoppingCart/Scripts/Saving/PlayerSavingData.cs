using System.Collections.Generic;

namespace HackMan.Scripts.Systems
{
    public class PlayerSavingData
    {
        public Dictionary<string, float> PlayerName2Score = new Dictionary<string, float>();
        public Dictionary<string, int> PlayerName2MeshIndex = new Dictionary<string, int>();
    }
}