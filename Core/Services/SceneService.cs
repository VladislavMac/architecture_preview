
using Application.Game;
using Configs;
using Containers;
using Containers.Interfaces;
using GameState.Data;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class SceneService : IRegisteredSystem
    {
        public Dictionary<string, SceneConfig> _CurrentsScenesConfigs => _currentsScenesConfigs;

        private Dictionary<string, SceneConfig> _currentsScenesConfigs = new();

        public SceneService()
        {
            this.Initialize();
        }

        #region [ Public Methods ]

        public bool LoadScene(string id)
        {
            if (!_currentsScenesConfigs.ContainsKey(id))
            {
                _currentsScenesConfigs[id] = GlobalContainer.GetContainer("scenes").GetSystem<SceneConfig>(id);
                SceneManager.LoadSceneAsync(GlobalContainer.GetContainer("scenes").GetSystem<SceneConfig>(id).Scene.name, new LoadSceneParameters(LoadSceneMode.Additive));

                GlobalContainer.GameStateProvider.TryUpdateData(id, new SceneData(id));

                UnityEngine.Debug.Log($"Service [Scene]: The scene: [{id}] is loaded");
                return true;
            }

            UnityEngine.Debug.LogWarning($"Service [Scene]: The scene: [{id}] cannot be loaded. This scene already loaded");
            return false;
        }
        public bool LoadScene(SceneData[] scenesData)
        {
            foreach (SceneData dataScene in scenesData)
            {
                if (!this.LoadScene(dataScene.Id)) return false;
            }

            return true;
        }

        public bool RemoveScene(string id)
        {
            if (_currentsScenesConfigs.TryGetValue(id, out SceneConfig currentScene))
            {
                _currentsScenesConfigs.Remove(currentScene.Name);
                SceneManager.UnloadSceneAsync(currentScene.Name);

                if (!GlobalContainer.GameStateProvider.TryRemoveData<SceneData>(id)) return false;

                UnityEngine.Debug.Log($"Service [Scene]: The scene: [{id}] is deleted");
                return true;
            }

            throw new System.Exception($"Service [Scene]: The required scene for remove: [{id}] is missing");
        }

        #endregion

        #region [ Private ]

        private void Initialize()
        {
            foreach (SceneConfig config in GameManager.ScenesConfigs)
            {
                GlobalContainer.GetContainer("scenes").RegistrationSystem(config.Name, config);
            }
        }

        #endregion
    }
}
