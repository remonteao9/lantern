using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetList;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioSource gunSorce;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip gunSound;


    private void Awake() {
        hitSource.clip = hitSound;
        gunSorce.clip = gunSound;

        foreach (GameObject target in targetList) {
            target.transform.DOShakePosition(10, new Vector2(0.1f,0.1f), 10, 90,fadeOut:false).SetLoops(-1, LoopType.Restart);
        }
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            gunSorce.Play();
            if (hit.collider != null) {
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
    }
}
