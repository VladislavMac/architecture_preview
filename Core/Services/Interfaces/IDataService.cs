
using GameState.Logic;

namespace Services.Interfaces
{
    public interface IDataService
    {
        public void SetCurrentGameStateProvider(GameStateProvider provider);
    }
}
