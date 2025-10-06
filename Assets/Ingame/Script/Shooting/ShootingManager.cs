using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class ShootingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetList;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject clearCanvas;
    [SerializeField] private GameObject gameOverCanvas;

    [SerializeField] private GameObject ufo;

    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioSource gunSorce;
    [SerializeField] private AudioSource clearSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip gunSound;
    [SerializeField] private AudioClip clearSound;

    private Action clearAction;
    private Action gameOverAction;

    private float time = 10;
    [SerializeField] private TextMeshProUGUI timeText;
    private bool isActive = true;
    private static HashSet<Item> obtainedItems = new();

    private void Awake() {
        hitSource.clip = hitSound;
        gunSorce.clip = gunSound;
        clearSource.clip = clearSound;
        clearCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        StartCoroutine(MakeUfo());

        clearAction += Clear;
        gameOverAction += GameOver;

        foreach (var item in obtainedItems) {
            GameItems.SetItem(item);
        }
    }

    private void Update() {
        if(isActive) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                gunSorce.Play();
                if (hit.collider != null) {
                    GameObject obj = hit.collider.gameObject;
                    if (obj.tag == "UFO") {
                        obtainedItems.Add(Item.Ufo);
                        GameItems.SetItem(Item.Ufo);
                    }
                    else {
                        targetList.Remove(obj);
                        // 蚊
                        obtainedItems.Add(Item.Mosquite);
                        GameItems.SetItem(Item.Mosquite);
                    }
                    StartCoroutine(BreakObject(hit));
                }
            }
        }

        if(time <= 0) {
            gameOverAction?.Invoke();
            isActive = false;
        }
        else if(isActive){
            time -= Time.deltaTime;
            timeText.text = time.ToString("F0");
        }
    }

    private IEnumerator BreakObject(RaycastHit2D hit) {
        GameObject obj = hit.collider.gameObject;
        obj.transform.DOScale(new Vector3(0, 0, 0), 1f);
        hitSource.Play();
        yield return new WaitForSeconds(1.1f);

        Destroy(obj);
        if (targetList == null || targetList.Count == 0) {
            clearAction?.Invoke();
        }
    }

    private void Clear() {
        isActive = false;
        clearAction -= Clear;
        clearCanvas.SetActive(true);
        clearSource.Play();
        obtainedItems.Add(Item.Gun);
        GameItems.SetItem(Item.Gun);
    }

    private void GameOver() {
        gameOverAction -= GameOver;
        GameOverAction();
    }

    private void GameOverAction() {
        StopMosquite();
        gameOverCanvas.SetActive(true);
    }


    private IEnumerator MakeUfo() {
        yield return new WaitForSeconds(3f);
        if (isActive) {
            Instantiate(ufo);
        }
    }

    private void StopMosquite() {
        foreach (var obj in targetList) {
            obj.gameObject.GetComponent<MosquiteController>().Deactivate();
        }
    }

}
