using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebBridge : MonoBehaviour {
    [DllImport("__Internal")] private static extern void SetSceneName(string name, string imgPath, string title, string summary);
    [DllImport("__Internal")] private static extern void UpdateContent(string activeSceneName);
    [DllImport("__Internal")] private static extern void UpdateGameItems();

    public void OnReady() {
        SetSceneName("MainGameScene", "Main.png", "ランタン", "");
        SetSceneName("ShootGameScene", "Shoot.png", "蚊シューティング", "UFOは無視して、<br>蚊を銃で撃ち殺しましょう。");

        UpdateContent("MainGameScene");
        UpdateGameItems();

        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("MainGameScene");
    }
    public void LoadScene(string sceneName) {
        if (!SceneExists(sceneName)) return;

        SceneManager.LoadScene(sceneName);
        UpdateContent(sceneName);
    }

    private bool SceneExists(string sceneName) {
        int count = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < count; i++) {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName) return true;
        }
        return false;
    }
}
