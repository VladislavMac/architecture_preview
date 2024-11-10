
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Application
{
    [InitializeOnLoad]
    public static class BootstrapSceneAutoLoader
    {
        private const string ACTIVE_SCENE = "ACTIVE SCENE";

        private static void onPlayModeStateChanged(PlayModeStateChange state)
        {
            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                    return;

                string activeScene = SceneManager.GetActiveScene().path;
                EditorPrefs.SetString(ACTIVE_SCENE, activeScene);

                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    string path = SceneUtility.GetScenePathByBuildIndex(0);

                    try
                    {
                        EditorSceneManager.OpenScene(path);
                    }
                    catch
                    {
                        UnityEngine.Debug.LogError($"Cannot load scene: {path}");
                        EditorApplication.isPlaying = false;
                    }
                }
                else
                {
                    EditorApplication.isPlaying = false;
                }
            }

            if(!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                string path = EditorPrefs.GetString(ACTIVE_SCENE);

                try
                {
                    EditorSceneManager.OpenScene(path);
                }
                catch
                {
                    UnityEngine.Debug.LogError($"Cannot load scene: {path}");
                }
            }
        }

        static BootstrapSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += onPlayModeStateChanged;
        }
    }
}
