
using GameState.Interfaces;
using Newtonsoft.Json;
using System;

namespace GameState.Data
{
    [Serializable]
    public class SaveData : ISavedData
    {
        [JsonProperty("file")] public string Path => _path;

        private string _path;

        public SaveData(string path)
        {
            _path = path;
        }
    }
}
