
using Containers;
using GameState.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameState.Logic
{
    public class GameStateProvider : IGameStateProvider
    {
        private JsonGameDataSerializer _jsonGameDataSerializer;
        private GameStateData _gameStateData;

        public GameStateProvider()
        {
            this._jsonGameDataSerializer = new JsonGameDataSerializer();
            this.BuildGameStateData();
            this.SetLastNameSaveFile();
        }

        #region Public Methods

        public bool TryUpdateData<T>(string id, T data) where T : ISavedData
        {
            string correctId = GetСorrectId<T>(id);

            try
            {
                if (!this._gameStateData.SaveData.ContainsKey(GetCorrectСategory<T>())) 
                    this._gameStateData.SaveData.Add(GetCorrectСategory<T>(), new());

                this._gameStateData.SaveData[GetCorrectСategory<T>()][correctId] = data;
                UnityEngine.Debug.Log($"GameStateData: updated data [{correctId}]");
                return true;
            }
            catch
            {
                UnityEngine.Debug.LogWarning($"GameStateData: doesn't have this data: [{correctId}]");
                return false;
            }
        }

        #region ===== Get DATA =====

        public T TryGetData<T>(string id) where T : class, ISavedData
        {
            string correctId = GetСorrectId<T>(id);

            try
            {
                return JsonConvert.DeserializeObject<T>(this._gameStateData.SaveData[GetCorrectСategory<T>()][correctId].ToString());
            }
            catch
            {
                UnityEngine.Debug.LogWarning($"GameStateData: doesn't have this data: [{correctId}]");
                return null;
            }
        }
        
        public T[] TryGetData<T>() where T : class, ISavedData
        {
            try
            {
                List<T> data = new();

                foreach (var item in this._gameStateData.SaveData[GetCorrectСategory<T>()].Values)
                {
                    T deserializedItem = JsonConvert.DeserializeObject<T>(item.ToString());
                    data.Add(deserializedItem);
                }

                return data.ToArray();
            }
            catch
            {
                UnityEngine.Debug.LogWarning($"GameStateData: doesn't have this category: [{GetCorrectСategory<T>()}]");
                return new T[0];
            }
        }

        #endregion

        public bool TryRemoveData<T>(string id) where T : ISavedData
        {
            string correctId = GetСorrectId<T>(id);

            try
            {
                if (this._gameStateData.SaveData[GetCorrectСategory<T>()].ContainsKey(correctId))
                {
                    this._gameStateData.SaveData[GetCorrectСategory<T>()].Remove(correctId);
                    UnityEngine.Debug.Log($"GameStateData: remove data: [{correctId}]");
                    return true;
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"GameStateData: can't remove data: [{correctId}]");
                    return false;
                }
            }
            catch
            {
                UnityEngine.Debug.LogWarning($"GameStateData: SaveData is empty [{correctId}]");
                return false;
            }
        }

        #region ===== Json Data Serializer =====

        public async Task SaveGameData(string fileName)
        {
            await _jsonGameDataSerializer.SaveGameStateData(this._gameStateData, fileName);
        }

        public async Task LoadGameData(string fileName)
        {
            this._gameStateData = await _jsonGameDataSerializer.GetGameStateData(fileName);
        }

        #endregion

        #region ===== File system =====

        public FileInfo[] GetArraySaves()
        {
            DirectoryInfo dir = new DirectoryInfo(GlobalContainer.SAVE_DATA_PATH);
            FileInfo[] files = dir.GetFiles("*.json");

            return files;
        }

        public bool IsSaveFilePresent(string name)
        {
            if (GetSaveFile(name) == null) return false;
            return true;
        }

        public void TryRemoveSaveFile(string name)
        {
            if (!this.IsSaveFilePresent(name))
            {
                UnityEngine.Debug.LogWarning($"GameStateData: The file [{name}] cannot be deleted");
                return;
            }

            FileInfo file = this.GetSaveFile(name);

            try
            {
                file.Delete();
            }
            catch
            {
                UnityEngine.Debug.LogWarning($"GameStateData: The file [{name}] cannot be deleted");
            }
        }

        public int GetCountSaveFiles(string name)
        {
            DirectoryInfo dir = new DirectoryInfo(GlobalContainer.SAVE_DATA_PATH);
            FileInfo[] files = dir.GetFiles("*.json");

            int count = 0;

            foreach (FileInfo file in files)
            {
                if (file.Name.Contains(name)) count++;
            }

            return count;
        }

        #endregion

        #endregion

        #region Private

        private async void BuildGameStateData()
        {
            this._gameStateData = await _jsonGameDataSerializer.GetGameStateData(GlobalContainer.LAST_NAME_SAVE_FILE);
        }

        private void SetLastNameSaveFile()
        {
            FileInfo[] files = GetArraySaves();

            if (files.Length != 0)
            {
                FileInfo latestFile = files[0];

                foreach (FileInfo file in files)
                {
                    if (file.LastWriteTime > latestFile.LastWriteTime)
                    {
                        latestFile = file;
                    }
                }

                GlobalContainer.SetLastNameSaveFile(Path.GetFileNameWithoutExtension(latestFile.FullName));
            }
        }

        private FileInfo GetSaveFile(string name)
        {
            foreach (FileInfo file in GetArraySaves())
            {
                if (Path.GetFileNameWithoutExtension(file.FullName) == name)
                {
                    return file;
                }
            }
            return null;
        }

        private string GetСorrectId<T>(string id)
        {
            string idData = $"{GetCorrectСategory<T>()}_{id}";

            return idData;
        }
        private string GetCorrectСategory<T>()
        {
            string correctData = typeof(T).Name;

            return correctData;
        }

        #endregion
    }
}
