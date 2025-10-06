using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    [SerializeField] private GameObject mosquito;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject ufo;
    [SerializeField] private GameObject magnet;
    [SerializeField] private GameObject iron;
    [SerializeField] private GameObject magneticForce;


    // 生成モードで対応シーン名を受け取りselectedItemNameDictの値を参照してitemNametoItemする

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameItems.itemObjectDict[Item.Mosquite] = mosquito;
        GameItems.itemObjectDict[Item.Gun] = gun;
        GameItems.itemObjectDict[Item.Ufo] = ufo;
        GameItems.itemObjectDict[Item.Magnet] = magnet;
        GameItems.itemObjectDict[Item.Iron] = iron;
        GameItems.itemObjectDict[Item.MagneticForce] = magneticForce;
    }

    public void StartEditMode(string scenenName) {
        if (EditMode.instance == null) return;
        var newObjName = GameItems.selectedItemNameDict[scenenName];
        var newObj = GameItems.itemObjectDict[newObjName];

        EditMode.instance.ChangeEdit(newObj, scenenName);
    }

    public void EndEditMode(string sceneName) {
        if (EditMode.instance == null) return;
        EditMode.instance.ChangeEdit(null, string.Empty);
    }
}
