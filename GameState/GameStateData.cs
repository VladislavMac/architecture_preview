
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GameState
{
    [Serializable]
    public class GameStateData
    {
        [JsonProperty("saveData")] public Dictionary<string, Dictionary<string, object>> SaveData;
        [JsonProperty("settingsData")] public Dictionary<string, List<object>> SettingsData;
    }
}
