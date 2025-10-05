using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItemController : MonoBehaviour
{
    private Camera cam;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSE;
    public GameObject Selected { get; private set; }

    void Awake() {
        cam = cam ? cam : Camera.main;
        Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(wp.x, wp.y);

        // クリック座標にある2Dコライダーを取得
        Collider2D col = Physics2D.OverlapPoint(point);
        if (col) {
            if(col.GetComponent<TargetController>() != null) {
                col.GetComponent<TargetController>().HitGun();
            }
            audioSource.clip = shootSE;
            audioSource.Play();
        }
    }
}
