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
    [DllImport("__Internal")] private static extern void UnSelectedItemButtons();


    public static WebBridge instance = null;

    private readonly string mainSceneName = "MainGameScene";

    public void OnReady() {
        if (instance != null) Destroy(gameObject);
        GameItems.RandomizeValues();
        SetSceneName(mainSceneName, "Main.png", "運動会", "メイン種目！ゴールできない...");

        // シューティング
        var sceneName = "ShootGameScene";
        SetSceneName(sceneName, "Shoot.png", "蚊シューティング", "UFOは無視して、<br>蚊を銃で撃ち殺しましょう。");
        AddGameItem(sceneName);

        sceneName = "MagnetGameScene";
        SetSceneName(sceneName, "Magnet.png", "ふわふわキープ", "磁力が反転した。磁石を使って鉄を落とさないようにしよう。<br>Aで右、Dで左に移動");
        AddGameItem(sceneName);

        UpdateContent("MainGameScene");
        UpdateGameItems();

        DontDestroyOnLoad(gameObject);
        instance = this;

        SceneManager.LoadScene(mainSceneName);
    }
    public void LoadScene(string sceneName) {
        if (!SceneExists(sceneName)) return;

        SceneManager.LoadScene(sceneName);
        UpdateContent(sceneName);
        UnSelectedItemButtons();
    }

    public void SetGameItem(string itemCodePlusSceneName) {
        string[] parts = itemCodePlusSceneName.Split('+');
        var itemCode = parts[0];
        var sceneName = parts[1];
        // コードが一致するアイテム(名)をセット中アイテムにセット
        foreach (var itemName in GameItems.itemNameToCode.Keys) {
            if (GameItems.itemNameToCode[itemName] == itemCode) {
                GameItems.selectedItemNameDict[sceneName] = itemName;
                UpdateItemIcon(sceneName, itemName + ".png");
                break;
            }
        }

    }

    internal static void GetGameItem(string itemName) {
#if UNITY_WEBGL && !UNITY_EDITOR
        UpdateItemText(itemName, GameItems.itemNameToCode[itemName].ToString(), SceneManager.GetActiveScene().name);
#endif
    }

    public void SelectItem(string sceneName) {
        if (!MainGameManager.Instance) return;
        MainGameManager.Instance.StartEditMode(sceneName);
    }

    public void UnSelectItem(string sceneName) {
        if (!MainGameManager.Instance) return;
        MainGameManager.Instance.EndEditMode(sceneName);
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
