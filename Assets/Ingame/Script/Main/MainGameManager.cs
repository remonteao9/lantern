using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    [SerializeField] private static GameObject mosquito;
    [SerializeField] private static GameObject gun;
    [SerializeField] private static GameObject ufo;

    public static List<GameObject> gameObjects = new List<GameObject>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void GetMosquito() {
        gameObjects.Add(mosquito);
    }

    public static void GetGun() {
        gameObjects.Add (gun);
    }

    public static void GetUfo() {
        gameObjects.Add(ufo);
    }
}
