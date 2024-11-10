
using Containers.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/System/SceneConfig")]
    public class SceneConfig : ScriptableObject, IRegisteredSystem
    {
        public SceneAsset Scene => _scene;
        public string Name => _name;
        public bool IsDetailed => _isDetailed;

        [SerializeField] private SceneAsset _scene;
        [SerializeField] private string _name;
        [SerializeField] private bool _isDetailed;
    }
}
