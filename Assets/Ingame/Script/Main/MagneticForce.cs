using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticForce : MonoBehaviour
{
    public void Awake() {
        GetComponent<SpriteRenderer>().enabled = false;
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(wp.x, wp.y);

        // クリック座標にある2Dコライダーを取得
        Collider2D col = Physics2D.OverlapPoint(point);
        if (col) {
            var actor = col.gameObject.GetComponent<MNActor>();
            if (actor) {
                actor.Gravity();

            }
        }
    }
}

