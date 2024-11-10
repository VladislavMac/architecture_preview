
using Application.UI;
using Application.UI.Interfaces;
using Behaviours.System;
using Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Application.Game
{
    public class GameManager : MonoBehaviour
    {
        public static IGlobalRegisteredUI GlobalRegisteredUI => _globalRegisteredUI;

        public static IReadOnlyList<SceneConfig> ScenesConfigs => _scenesConfigs;

        private static List<SceneConfig> _scenesConfigs = new();

        private static GlobalRegisteredUI _globalRegisteredUI;

        [SerializeField] private List<SceneConfig> _scenesConfigsSerialize = new();
        [SerializeField] private SystemBehaviour _system;

        private EntryPoint _entryPoint;

        public static void SetGlobalRegisteredUI(GlobalRegisteredUI globalRegistered)
        {
            GameManager._globalRegisteredUI = globalRegistered;
            UnityEngine.Debug.Log($"=== GameManager set GlobalRegisteredUI ===");
        }

        private async void initialize()
        {
            GameManager._scenesConfigs = _scenesConfigsSerialize;
            this._entryPoint = new EntryPoint();

            await this._entryPoint.BuildProject();
            UnityEngine.Debug.Log($"=== GameManager initialization is done ===");
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_system);

            UnityEngine.Debug.Log($"=== GameManager is working ===");

            this.initialize();
        }
    }
}
