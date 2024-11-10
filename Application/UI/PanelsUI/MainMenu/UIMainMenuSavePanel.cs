
using Application.UI.Abstract;
using Application.UI.EventsBuses.MainMenu;
using Core.EventsBuses.Abstract;

namespace Application.Ui.Panels.MainMenu
{
    public class UIMainMenuSavePanel : RegisteredPanelUI
    {
        public override EventBus EventBus => _eventBus;

        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = new UIMainMenuSaveEventBus();
        }
    }
}

