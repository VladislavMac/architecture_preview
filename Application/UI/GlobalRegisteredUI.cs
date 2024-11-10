
using Application.Game;
using Application.UI.Abstract;
using Application.UI.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application.UI
{
    public class GlobalRegisteredUI : MonoBehaviour, IGlobalRegisteredUI
    {
        public IReadOnlyDictionary<string, RegisteredPanelUI> RegisteredPanelsUI => _registeredPanelsUI;

        private Dictionary<string, RegisteredPanelUI> _registeredPanelsUI = new();

        [SerializeField] private List<RegisteredPanel> _registeredPanels = new();

        public T GetPanelUI<T>(string name) where T : RegisteredPanelUI
        {
            if (_registeredPanelsUI.TryGetValue(name, out RegisteredPanelUI registeredPanel)) return registeredPanel as T;

            throw new System.Exception($"The required RegisteredPanelUI: [{name}] is missing");
        }

        [Serializable]
        public struct RegisteredPanel
        {
            public string name;
            public RegisteredPanelUI registeredPanel;
        }

        private void Reset()
        {
            foreach (RegisteredPanel panel in _registeredPanels)
            {
                _registeredPanelsUI[panel.name] = panel.registeredPanel;
            }
        }

        private void Awake()
        {
            GameManager.SetGlobalRegisteredUI(this);
        }
    }
}
