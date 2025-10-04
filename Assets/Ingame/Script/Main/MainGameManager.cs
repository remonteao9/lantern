using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    [SerializeField] private GameObject mosquito;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject ufo;

    public static Dictionary<string, GameObject> itemNameToItemDict = new Dictionary<string, GameObject>();



    // 生成モードで対応シーン名を受け取りselectedItemNameDictの値を参照してitemNametoItemする

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        itemNameToItemDict["蚊"] = mosquito;
        itemNameToItemDict["銃"] = gun;
        itemNameToItemDict["UFO"] = ufo;
    }

    public void StartEditMode(string scenenName) {
        if (EditMode.instance == null) return;
        var newObjName = GameItems.selectedItemNameDict[scenenName];
        var newObj = itemNameToItemDict[newObjName];

        EditMode.instance.editObject = newObj;
    }
}
