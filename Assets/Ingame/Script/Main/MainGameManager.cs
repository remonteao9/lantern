using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    [SerializeField] private static GameObject mosquito;
    [SerializeField] private static GameObject gun;
    [SerializeField] private static GameObject ufo;

    public static Dictionary<string, GameObject> itemNameToItemDict = new Dictionary<string, GameObject>() {
        { "蚊", mosquito },
        { "銃", gun },
        { "UFO", ufo },
    };



    // 生成モードで対応シーン名を受け取りselectedItemNameDictの値を参照してitemNametoItemする

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartEditMode(string scenenName) {
        if (EditMode.instance == null) return;
        var newObjName = GameItems.selectedItemNameDict[scenenName];
        var newObj = itemNameToItemDict[newObjName];

        EditMode.instance.isEditMode = true;
    }
}
