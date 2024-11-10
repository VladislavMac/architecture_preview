
using Containers.Interfaces;
using GameState.Logic;
using GameState.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Behaviours;
using Services.Interfaces;
using UnityEngine;

namespace Containers
{
    public static class GlobalContainer
    {
        public static IGameStateProvider GameStateProvider => _gameStateProvider;
        public static IReadOnlyList<DataBehaviour> RegisteredDataBehaviour => _registeredDataBehaviour;

        public static string LAST_NAME_SAVE_FILE => PlayerPrefs.GetString("LAST_NAME_SAVE_FILE");
        public static string START_NAME_SAVE_FILE => "NewGame";
        public static string SAVE_DATA_PATH => UnityEngine.Application.persistentDataPath + "/Saves";

        private static GameStateProvider _gameStateProvider;

        private static Dictionary<string, IRegisteredContainer> _registeredContainers = new(); // future load DATA
        private static List<DataBehaviour> _registeredDataBehaviour = new();

        static GlobalContainer()
        {
            GlobalContainer._gameStateProvider = new GameStateProvider();
        }

        #region [ Public Methods ]

        public static async Task RegistrationContainer(string id, IRegisteredContainer container)
        {
            GlobalContainer._registeredContainers[id] = container;

            await Task.Yield();
            UnityEngine.Debug.Log($"GlobalContainer: Container [{id}] has been registered");
        }

        public static void RegistrationDataBehaviour<T>(T dataBehaviour) where T : DataBehaviour
        {
            _registeredDataBehaviour.Add(dataBehaviour);
        }

        public static IRegisteredContainer GetContainer(string id)
        {
            if (GlobalContainer._registeredContainers.TryGetValue(id, out IRegisteredContainer container)) return container;

            throw new System.Exception($"GlobalContainer: The required container: [{id}] is missing");
        }

        public static void SetGameStateProviderToSystem<T>(T system) where T : class, IRegisteredSystem, IDataService
        {
            system.SetCurrentGameStateProvider(_gameStateProvider);
        }

        public static void SetLastNameSaveFile(string name)
        {
            PlayerPrefs.SetString("LAST_NAME_SAVE_FILE", name);
            PlayerPrefs.Save();
        }

        #endregion

        #region [ Private Methods ]


        #endregion
    }
}
