
using GameState.Interfaces;
using Newtonsoft.Json;
using System;

namespace GameState.Data
{
    [Serializable]
    public class SceneData : ISavedData
    {
        [JsonIgnore] public string Id => _id;

        [JsonProperty("currentSceneConfigId")] private string _id;

        public SceneData(string id) 
        {
            _id = id;
        }  
    }
}
