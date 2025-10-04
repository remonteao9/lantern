
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public　static class GameItems
{
    public static Dictionary<string, int> itemNameToCode = new Dictionary<string, int>() {
        { "銃", 11281 },
        { "蚊", 12832 },
        { "UFO", 10294 }
    };
    // codeをタグにつける。タグについたコードが返ってきた(ボタンが押された)ら、対応するオブジェクトをメインに反映する。
    public static Dictionary<string, GameObject> sceneToItemDict = new();
    public static Dictionary<int, GameObject> codeToItemDict = new();

    public static void SetItem(string itemName) {
        WebBridge.GetGameItem(itemName);
        //sceneToItemDict[SceneManager.GetActiveScene().name] = ;
    }
}
