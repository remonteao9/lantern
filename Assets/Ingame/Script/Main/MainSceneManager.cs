using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour {

    [SerializeField] private GroundLooper groundLooper;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject clearPanel;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip clearSE;

    private void Awake() {
        clearPanel.SetActive(false);
        groundLooper.endGame += GameClear;
        audioSource.clip = bgm;
        audioSource.Play();
    }

    private void GameClear() {
        clearPanel.SetActive(true);
        playerController.ChengeActive(false);
        audioSource.loop = false;
        audioSource.clip = clearSE;
        audioSource.Play();
    }


}
