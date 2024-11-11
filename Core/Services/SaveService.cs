
using Application.Behaviours;
using Containers;
using Containers.Interfaces;
using GameState.Logic;
using Services.Interfaces;
using System.Diagnostics;
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

            await _gameStateProvider.SaveGameData(GlobalContainer.START_NAME_SAVE_FILE + _gameStateProvider.GetCountSaveFiles(GlobalContainer.START_NAME_SAVE_FILE));
            await this.LoadSaveGameStateData(GlobalContainer.START_NAME_SAVE_FILE);
        }
        public async Task SaveCurrentGameStateData()
        {
            await this.BeforeSave();

            int numberSave = 0;

            if (GlobalContainer.LAST_NAME_SAVE_FILE.Contains(GlobalContainer.FAST_NAME_SAVE_FILE))
            {
                if (_gameStateProvider.GetCountSaveFiles(GlobalContainer.FAST_NAME_SAVE_FILE) >= 3)
                {
                    if (GlobalContainer.NUMBER_FAST_SAVE_FILE >= 3)
                    {
                        numberSave = 0;
                        GlobalContainer.SetNumberFastSaveFile(numberSave);
                    }
                    else if (GlobalContainer.NUMBER_FAST_SAVE_FILE < 3)
                    {
                        numberSave = GlobalContainer.NUMBER_FAST_SAVE_FILE + 1;
                    }
                }
                else 
                    numberSave = _gameStateProvider.GetCountSaveFiles(GlobalContainer.FAST_NAME_SAVE_FILE);
            }
            else
                numberSave = 0;

            await _gameStateProvider.SaveGameData(GlobalContainer.FAST_NAME_SAVE_FILE + numberSave);
            GlobalContainer.SetNumberFastSaveFile(numberSave);
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
                await Task.Yield();
            }
        }

        #endregion
    }
}
