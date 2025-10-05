using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItemController : MNActor
{
    private Camera cam;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip SE;
    public GameObject Selected { get; private set; }

    public override void Awake() {
        base.Awake();
        cam = cam ? cam : Camera.main;
        Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(wp.x, wp.y);

        // クリック座標にある2Dコライダーを取得
        Collider2D col = Physics2D.OverlapPoint(point);
        Debug.Log(col);
        if (col) {
            var actor = col.GetComponent<MNActor>();
            if (actor != null) {
                actor.HitMagnet();
            }
            audioSource.clip = SE;
            audioSource.Play();
        }
        StartCoroutine(DestroyWait(1));
    }
}
