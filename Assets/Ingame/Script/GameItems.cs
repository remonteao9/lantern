
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public　static class GameItems
{
    public static Dictionary<Item, string> itemNameDict = new Dictionary<Item, string>() {
        { Item.Gun, "銃" },
        { Item.Mosquite, "蚊" },
        { Item.Ufo, "UFO" },
        { Item.Magnet, "磁石" },
        { Item.Iron, "鉄" },
        { Item.MagneticForce, "磁力" }
    };

    public static Dictionary<string, Item> itemCodeDict = new();

    public static Dictionary<Item, GameObject> itemObjectDict = new Dictionary<Item, GameObject>();


    // codeをタグにつける。タグについたコードが返ってきた(ボタンが押された)ら、対応するオブジェクトをメインに反映する。
    public static Dictionary<string, Item> selectedItemNameDict = new(); // シーンごとの選択中アイテム名

    public static void SetItem(Item item) {
        WebBridge.GetGameItem(itemNameDict[item]);
    }

    public static void RandomizeItemKeys() {
        var rand = new System.Random();

        foreach (Item item in Enum.GetValues(typeof(Item))) {
            string key;
            do {
                key = "item" + rand.Next(1000000);
            } while (itemCodeDict.ContainsKey(key));

            itemCodeDict[key] = item;
        }

    }
    public static string GetCodeFromItemName(string itemName) {
        var item = itemNameDict.FirstOrDefault(x => x.Value.Trim() == itemName.Trim()).Key;
        var code = itemCodeDict.FirstOrDefault(x => x.Value.Equals(item)).Key;
        return code;
    }

}

public enum Item {
    Gun,
    Mosquite,
    Ufo,
    Magnet,
    Iron,
    MagneticForce
}
