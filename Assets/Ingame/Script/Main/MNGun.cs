using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNGun : MNActor
{
    private Camera cam;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSE;

    public override void Awake() {
        GetComponent<SpriteRenderer>().enabled = false;
        cam = cam ? cam : Camera.main;
        Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(wp.x, wp.y);

        // クリック座標にある2Dコライダーを取得
        Collider2D col = Physics2D.OverlapPoint(point);
        if (col) {
            var actor = col.gameObject.GetComponent<MNActor>();
            if (actor) {
                actor.HitGun();

            }
        }

        audioSource.clip = shootSE;
        audioSource.Play();
        DestroyWait(2);
    }
}
