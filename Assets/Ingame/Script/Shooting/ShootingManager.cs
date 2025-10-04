using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetList;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject ufo;

    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioSource gunSorce;
    [SerializeField] private AudioSource clearSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip gunSound;
    [SerializeField] private AudioClip clearSound;


    private void Awake() {
        hitSource.clip = hitSound;
        gunSorce.clip = gunSound;
        clearSource.clip = clearSound;
        canvas.SetActive(false);

        foreach (GameObject target in targetList) {
            target.transform.DOShakePosition(10, new Vector2(0.1f,0.1f), 10, 90,fadeOut:false).SetLoops(-1, LoopType.Restart).SetLink(target.gameObject);
        }

        StartCoroutine(MakeUfo());
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            gunSorce.Play();
            if (hit.collider != null) {
                GameObject obj = hit.collider.gameObject;
                if (obj.tag == "UFO") {
                    Debug.Log("UFO");
                    GameItems.SetItem("UFO");
                }
                else {
                    targetList.Remove(obj);
                    // 蚊
                    GameItems.SetItem("蚊");
                }
                StartCoroutine(BreakObject(hit));
            }
        }
    }

    private IEnumerator BreakObject(RaycastHit2D hit) {
        GameObject obj = hit.collider.gameObject;
        obj.transform.DOScale(new Vector3(0, 0, 0), 1f);
        hitSource.Play();
        yield return new WaitForSeconds(1.1f);

        Destroy(obj);
        if (targetList == null || targetList.Count == 0) {
            Clear();
        }
    }

    private void Clear() {
        canvas.SetActive(true);
        clearSource.Play();
        GameItems.SetItem("銃");
        StartCoroutine(ClearCol());
    }

    private IEnumerator ClearCol() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainGameScene");
    }

    private IEnumerator MakeUfo() {
        yield return new WaitForSeconds(3f);
        Instantiate(ufo);
    }

}
