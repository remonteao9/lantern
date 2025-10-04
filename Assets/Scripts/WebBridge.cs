using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebBridge : MonoBehaviour {
    [DllImport("__Internal")] private static extern void SetSceneName(string name, string imgPath, string title, string summary);
    [DllImport("__Internal")] private static extern void UpdateContent(string activeSceneName);
    [DllImport("__Internal")] private static extern void UpdateGameItems();
    [DllImport("__Internal")] private static extern void UpdateItemText(string itemName, string itemId, string sceneName);

    public static WebBridge instance = null;

    public void OnReady() {
        if (instance != null) Destroy(gameObject);
#if UNITY_WEBGL && !UNITY_EDITOR
        SetSceneName("MainGameScene", "Main.png", "ランタン", "");
        SetSceneName("ShootGameScene", "Shoot.png", "蚊シューティング", "UFOは無視して、<br>蚊を銃で撃ち殺しましょう。");

        UpdateContent("MainGameScene");
        UpdateGameItems();
#endif

        DontDestroyOnLoad(gameObject);
        instance = this;

        SceneManager.LoadScene("MainGameScene");
    }
    public void LoadScene(string sceneName) {
        if (!SceneExists(sceneName)) return;

        SceneManager.LoadScene(sceneName);
#if UNITY_WEBGL && !UNITY_EDITOR
        UpdateContent(sceneName);
#endif
    }

    public void SetGameItem(string itemCodePlusSceneName) {
        string[] parts = itemCodePlusSceneName.Split('+');
        Debug.Log(parts[0]);
        Debug.Log(parts[1]);
        //GameItems.sceneToItemDict[parts[1]] = itemCode;
    }

    public static void GetGameItem(string itemName) {
        UpdateItemText(itemName, GameItems.itemNameToCode[itemName].ToString(), SceneManager.GetActiveScene().name);
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
