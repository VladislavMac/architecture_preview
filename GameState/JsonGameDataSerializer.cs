
using Containers;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace GameState
{
    public class JsonGameDataSerializer
    {
        public async Task<GameStateData> GetGameStateData(string fileName)
        {
            string path = BuildSavePath($"{fileName}.json");

            try
            {
                using (var fileStream = new StreamReader(path))
                {
                    var json = await fileStream.ReadToEndAsync();
                    var data = JsonConvert.DeserializeObject<GameStateData>(json);

                    await Task.Yield();

                    if (data.SaveData == null) data.SaveData = new();
                    return data;
                }
            }
            catch
            {
                // TODO: If the path is wrong, a new game will start. It's bad. Fix it

                // If no saves before

                UnityEngine.Debug.LogWarning("JsonGameDataSerializer: NewGame start");

                GameStateData newGameStateData = new GameStateData();
                newGameStateData.SaveData = new();

                return newGameStateData;
            }
        }

        public async Task SaveGameStateData(GameStateData gameStateData, string fileName)
        {
            string path = BuildSavePath($"{fileName}.json");
            string json = JsonConvert.SerializeObject(gameStateData, Formatting.Indented);

            using (var fileStream = new StreamWriter(path))
            {
                try
                {
                    await fileStream.WriteAsync(json);
                    GlobalContainer.SetLastNameSaveFile(fileName);
                }
                catch
                {
                    throw new System.Exception("JsonGameDataSerializer: data saving has been interrupted");
                }
            }
        }

        public async Task SaveSettingsData(string fileName)
        {
            // TODO:

            /*
            string path = BuildPath($"Settings.json");
            string json = JsonConvert.SerializeObject(gameStateData, Formatting.Indented);

            using (var fileStream = new StreamWriter(path))
            {
                try
                {
                    await fileStream.WriteAsync(json);
                }
                catch
                {
                    throw new System.Exception("JsonGameDataSerializer data saving has been interrupted");
                }
            }
            */
        }

        private string BuildSavePath(string key)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(GlobalContainer.SAVE_DATA_PATH);

            if (!dirInfo.Exists)
                dirInfo.Create();

            return Path.Combine(GlobalContainer.SAVE_DATA_PATH, key);
        }

        //private string BuildSettingsPath(string key)
        //{
            //DirectoryInfo dirInfo = new DirectoryInfo(GlobalContainer.SAVE_SETTINGS_PATH);
            //if (!dirInfo.Exists)
            //    dirInfo.Create();

            //return Path.Combine(GlobalContainer.SAVE_SETTINGS_PATH, key);
        //}
    }
}
