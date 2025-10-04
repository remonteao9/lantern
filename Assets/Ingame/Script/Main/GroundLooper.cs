using System;
using UnityEngine;

public class GroundLooper : MonoBehaviour {
    [SerializeField] float slideSpeed = 3f;        // 左へ流れる速度
    [SerializeField] float seamOverlap = 0.05f;    // 2枚の重なり量（正で少し被せる）
    [SerializeField, Range(0f, 1f)] float stopPercentFromLeft = 0.7f; // 「左から何割の位置」で止めるか（7割＝0.7）

    Camera cam;
    Transform tileA, tileB;           // 子の2枚
    SpriteRenderer srA, srB;

    float screenLeft, screenRight;
    bool scrolling = true;
    const float EPS = 0.0005f;        // 浮動小数対策

    public Action endGame;

    void Awake() {
        cam = Camera.main;

        if (transform.childCount < 2) {
            Debug.LogError("GroundObject の子に地面スプライトを2枚置いてください。");
            enabled = false; return;
        }

        tileA = transform.GetChild(0);
        tileB = transform.GetChild(1);
        srA = tileA.GetComponent<SpriteRenderer>();
        srB = tileB.GetComponent<SpriteRenderer>();
        if (!srA || !srB) {
            Debug.LogError("子オブジェクトに SpriteRenderer が必要です。");
            enabled = false; return;
        }

        UpdateScreenEdges();
        AlignPairAtStart();
    }

    void Update() {
        if (!scrolling) return;

        // 親ごと左へスライド
        transform.position += Vector3.left * slideSpeed * Time.deltaTime;

        // 右側にいるタイルを特定（右端が大きい方）
        Transform rightTile; SpriteRenderer rightSR;
        if (GetRight(tileA, srA) >= GetRight(tileB, srB)) { rightTile = tileA; rightSR = srA; }
        else { rightTile = tileB; rightSR = srB; }

        // そのタイルの「左から stopPercent の位置」のワールドX
        float leftEdge = GetLeft(rightTile, rightSR);
        float width = rightSR.bounds.size.x;
        float anchorX = leftEdge + width * stopPercentFromLeft; // ← ここが画面左端に触れたら停止

        if (anchorX <= screenLeft + EPS) {
            // ピッタリ合わせて停止（わずかなズレ補正）
            float delta = screenLeft - anchorX;
            transform.position += new Vector3(delta, 0f, 0f);
            scrolling = false;
            endGame?.Invoke();
        }
    }

    // ---- ユーティリティ ----

    void UpdateScreenEdges() {
        // ゲームビュー 940x560 前提（スクリーン→ワールド）
        Vector3 bl = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 tr = cam.ScreenToWorldPoint(new Vector3(940, 560, 0));
        screenLeft = bl.x;
        screenRight = tr.x;
    }

    void AlignPairAtStart() {
        // Aの左端＝画面左端
        float wA = srA.bounds.size.x;
        var pa = tileA.position;
        pa.x = screenLeft + wA * 0.5f;
        tileA.position = pa;

        // BをAの右隣に、少し被せて配置
        float wB = srB.bounds.size.x;
        var pb = tileB.position;
        pb.x = GetRight(tileA, srA) + wB * 0.5f - seamOverlap;
        tileB.position = pb;
    }

    float GetLeft(Transform t, SpriteRenderer sr) => t.position.x - sr.bounds.size.x * 0.5f;
    float GetRight(Transform t, SpriteRenderer sr) => t.position.x + sr.bounds.size.x * 0.5f;
}
