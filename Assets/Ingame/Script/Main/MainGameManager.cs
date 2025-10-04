using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    [SerializeField] private static GameObject mosquito;
    [SerializeField] private static GameObject gun;
    [SerializeField] private static GameObject ufo;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
