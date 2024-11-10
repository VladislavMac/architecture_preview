

namespace GameState.Interfaces
{
    public interface IGameStateProvider
    {
        public T TryGetData<T>(string id) where T : class, ISavedData;
        public T[] TryGetData<T>() where T : class, ISavedData;
        public bool TryUpdateData<T>(string id, T data) where T : ISavedData;
        public bool TryRemoveData<T>(string id) where T : ISavedData;
    }
}
