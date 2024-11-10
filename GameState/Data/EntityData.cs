
using GameState.Interfaces;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace GameState.Data
{
    [Serializable]
    public class EntityData : ISavedData
    {
        [JsonProperty("position")] public EntityPosition Position => _position;

        private EntityPosition _position;

        public EntityData(Vector3 position) 
        { 
            _position = new EntityPosition(position.x, position.y, position.z);
        }

        [Serializable]
        public class EntityPosition
        {
            [JsonProperty("x")] public float X;
            [JsonProperty("y")] public float Y;
            [JsonProperty("z")] public float Z;

            public EntityPosition(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}
