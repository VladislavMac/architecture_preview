
using Application.Ui.Panels.MainMenu;
using Containers;
using Core.EventsBuses.Abstract;
using Core.Services;
using GameState.Data;
using UnityEngine;

namespace Application.UI.EventsBuses.MainMenu
{
    public class UIMainMenuEventBus : EventBus
    {
        private GameObject _settingsPanel;
        private GameObject _savePanel;

        private async void NewGameButtonClick()
        {
            GlobalContainer.GetContainer("service").GetSystem<SceneService>("service_scene").RemoveScene("mainMenu");

            await GlobalContainer.GetContainer("service").GetSystem<SaveService>("service_save").NewGameStateData();

            GlobalContainer.GetContainer("service").GetSystem<SceneService>("service_scene").LoadScene("world");
            GlobalContainer.GetContainer("service").GetSystem<SceneService>("service_scene").LoadScene("mainCity");
        }

        private async void StartGameButtonClick()
        {
            await GlobalContainer.GetContainer("service").GetSystem<SaveService>("service_save").LoadSaveGameStateData();

            GlobalContainer.GetContainer("service").GetSystem<SceneService>("service_scene").RemoveScene("mainMenu");
            GlobalContainer.GetContainer("service").GetSystem<SceneService>("service_scene").LoadScene(GlobalContainer.GameStateProvider.TryGetData<SceneData>());
        }

        private void SaveButtonClick()
        {
            _settingsPanel.SetActive(false);

            if (!_savePanel.activeSelf)
                _savePanel.SetActive(true);
            else
                _savePanel.SetActive(false);
        }

        private void SettingsButtonClick()
        {
            _savePanel.SetActive(false);

            if (!_settingsPanel.activeSelf)
                _settingsPanel.SetActive(true);
            else
                _settingsPanel.SetActive(false);
        }

        private void ExitButtonClick()
        {
            UnityEngine.Application.Quit();
        }

        public UIMainMenuEventBus(GameObject settingsPanel, GameObject savePanel)
        {
            _settingsPanel = settingsPanel;
            _savePanel = savePanel;

            UIMainMenuPanel.OnNewGameButtonClicked += NewGameButtonClick;
            UIMainMenuPanel.OnStartGameButtonClicked += StartGameButtonClick;
            UIMainMenuPanel.OnSaveButtonClicked += SaveButtonClick;
            UIMainMenuPanel.OnSettingsButtonClicked += SettingsButtonClick;
            UIMainMenuPanel.OnExitButtonClicked += ExitButtonClick;
        }
    }
}
