
using Application.Behaviours;
using Containers;
using Containers.Interfaces;
using GameState.Logic;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SaveService : IRegisteredSystem, IDataService
    {
        private GameStateProvider _gameStateProvider;


        #region [ Public Methods ]

        public void SetCurrentGameStateProvider(GameStateProvider provider)
        {
            this._gameStateProvider = provider;
        }

        #region ===== Save =====
        public async Task NewGameStateData()
        {
            this._gameStateProvider.TryRemoveSaveFile(GlobalContainer.START_NAME_SAVE_FILE);

            // TODO: Copy "NewGame"
            await _gameStateProvider.SaveGameData(GlobalContainer.START_NAME_SAVE_FILE + "1");
            await this.LoadSaveGameStateData(GlobalContainer.START_NAME_SAVE_FILE);
        }
        public async Task SaveCurrentGameStateData()
        {
            await this.BeforeSave();

            await _gameStateProvider.SaveGameData(GlobalContainer.LAST_NAME_SAVE_FILE);
        }
        public async Task SaveCurrentGameStateData(string fileName)
        {
            await this.BeforeSave();

            await _gameStateProvider.SaveGameData(fileName);
        }
        #endregion

        #region ===== Load =====
        public async Task LoadSaveGameStateData()
        {
            await _gameStateProvider.LoadGameData(GlobalContainer.LAST_NAME_SAVE_FILE);
        }        
        public async Task LoadSaveGameStateData(string fileName)
        {
            await _gameStateProvider.LoadGameData(fileName);
        }
        #endregion

        #endregion

        #region [ Private ]

        private async Task BeforeSave()
        {
            if (_gameStateProvider == null)
            {
                throw new System.Exception("Service [Save]: GameStateProvider is null");
            }

            foreach (DataBehaviour data in GlobalContainer.RegisteredDataBehaviour)
            {
                data.UpdateData();
            }

            await Task.Yield();
        }

        #endregion
    }
}
