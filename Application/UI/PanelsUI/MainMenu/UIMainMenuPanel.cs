
using Application.UI.Abstract;
using Application.UI.EventsBuses.MainMenu;
using Core.EventsBuses.Abstract;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Ui.Panels.MainMenu
{
    public class UIMainMenuPanel : RegisteredPanelUI
    {
        public override EventBus EventBus => _eventBus;

        public static Action OnNewGameButtonClicked;
        public static Action OnStartGameButtonClicked;
        public static Action OnSaveButtonClicked;
        public static Action OnSettingsButtonClicked;
        public static Action OnExitButtonClicked;

        [SerializeField] private GameObject _savesPanel;
        [SerializeField] private GameObject _settingsPanel;
        [Space]
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = new UIMainMenuEventBus(_settingsPanel, _savesPanel);

            _newGameButton.onClick.AddListener(() => OnNewGameButtonClicked?.Invoke());
            _startGameButton.onClick.AddListener(() => OnStartGameButtonClicked?.Invoke());
            _saveButton.onClick.AddListener(() => OnSaveButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
        }
    }
}

