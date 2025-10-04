using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebBridge : MonoBehaviour {
    [DllImport("__Internal")] private static extern void SetSceneName(string name, string imgPath, string title, string summary);
    [DllImport("__Internal")] private static extern void AddGameItem(string sceneName);
    [DllImport("__Internal")] private static extern void UpdateContent(string activeSceneName);
    [DllImport("__Internal")] private static extern void UpdateGameItems();
    [DllImport("__Internal")] private static extern void UpdateItemText(string itemName, string itemId, string sceneName);
    [DllImport("__Internal")] private static extern void UpdateItemIcon(string imgPath, string fileName);

    public static WebBridge instance = null;

    private readonly string mainSceneName = "MainGameScene";

    public void OnReady() {
        if (instance != null) Destroy(gameObject);
#if UNITY_WEBGL && !UNITY_EDITOR
        SetSceneName(mainSceneName, "Main.png", "ランタン", "");
        AddGameItem(mainSceneName);

        // シューティング
        var sceneName = "ShootGameScene";
        SetSceneName(sceneName, "Shoot.png", "蚊シューティング", "UFOは無視して、<br>蚊を銃で撃ち殺しましょう。");
        AddGameItem(sceneName);

        UpdateContent("MainGameScene");
        UpdateGameItems();
#endif

        DontDestroyOnLoad(gameObject);
        instance = this;

        SceneManager.LoadScene(mainSceneName);
    }
    public void LoadScene(string sceneName) {
        if (!SceneExists(sceneName)) return;

        SceneManager.LoadScene(sceneName);
#if UNITY_WEBGL && !UNITY_EDITOR
        UpdateContent(sceneName);
        // jsのボタンを全部オンに
#endif
    }

    public void SetGameItem(string itemCodePlusSceneName) {
        string[] parts = itemCodePlusSceneName.Split('+');
        var itemCode = parts[0];
        var sceneName = parts[1];
        // コードが一致するアイテム(名)をセット中アイテムにセット
        foreach (var itemName in GameItems.itemNameToCode.Keys) {
            if (GameItems.itemNameToCode[itemName].ToString() == itemCode) {
                GameItems.selectedItemNameDict[sceneName] = itemName;
                UpdateItemIcon(sceneName, itemName + ".png");
                break;
            }
        }

    }

    public static void GetGameItem(string itemName) {
#if UNITY_WEBGL && !UNITY_EDITOR
        UpdateItemText(itemName, GameItems.itemNameToCode[itemName].ToString(), SceneManager.GetActiveScene().name);
#endif
    }

    public void SelectItem(string sceneName) {
        Debug.Log("select" + sceneName);
        if (SceneManager.GetActiveScene().name != mainSceneName) return;
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
