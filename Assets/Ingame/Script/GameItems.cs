
using System.Collections.Generic;

public　static class GameItems
{
    public static Dictionary<string, string> itemNameToCode = new Dictionary<string, string>() {
        { "銃", string.Empty },
        { "蚊", string.Empty },
        { "UFO", string.Empty},
        { "磁石", string.Empty },
        { "鉄", string.Empty },
        { "磁力", string.Empty },
    };


    // codeをタグにつける。タグについたコードが返ってきた(ボタンが押された)ら、対応するオブジェクトをメインに反映する。
    public static Dictionary<string, string> selectedItemNameDict = new(); // シーンごとの選択中アイテム名

    public static void SetItem(string itemName) {
        //sceneToItemDict[SceneManager.GetActiveScene().name] = ;
        WebBridge.GetGameItem(itemName);
    }

    public static void RandomizeValues() {
        var random = new System.Random();
        var keys = new List<string>(itemNameToCode.Keys);

        foreach (var key in keys) {
            itemNameToCode[key] = "item" + random.Next(10000, 99999);
        }
    }
}
