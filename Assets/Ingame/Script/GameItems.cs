
using System.Collections.Generic;

public　static class GameItems
{
    public static Dictionary<string, int> itemNameToCode = new Dictionary<string, int>() {
        { "銃", 0 },
        { "蚊", 0 },
        { "UFO", 0 },
        { "磁石", 0 },
        { "鉄", 0 },
        { "磁力", 0 },
    };

    static GameItems() {
        RandomizeValues();
    }


    // codeをタグにつける。タグについたコードが返ってきた(ボタンが押された)ら、対応するオブジェクトをメインに反映する。
    public static Dictionary<string, string> selectedItemNameDict = new(); // シーンごとの選択中アイテム名

    public static void SetItem(string itemName) {
        //sceneToItemDict[SceneManager.GetActiveScene().name] = ;
        WebBridge.GetGameItem(itemName);
    }

    private static void RandomizeValues() {
        var random = new System.Random();
        var keys = new List<string>(itemNameToCode.Keys);

        foreach (var key in keys) {
            itemNameToCode[key] = random.Next(10000, 99999);
        }
    }
}
