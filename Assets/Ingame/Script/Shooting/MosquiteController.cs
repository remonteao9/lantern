using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquiteController : MonoBehaviour {

    private bool isActive = true;     // ← これでON/OFF
    private float speed = 10f;                         // 移動速度
    private Vector2 pauseDuration = new(0.1f, 0.5f);   // 到着後に待つ時間の範囲
    private float arriveThreshold = 0.05f;             // 到着判定のしきい値
    private float edgeMargin = 0.1f;                   // 画面端からの余白（ワールド単位）
    private bool faceDirection = false;                // 進行方向を向くならON

    private Camera cam;
    private SpriteRenderer sr;
    private float minX, maxX, minY, maxY;
    private Vector3 target;
    private bool pausing;

    void Awake() {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        UpdateBounds();
        PickNewTarget();
    }

    void Update() {
        if (!isActive) return;        // ← ここで全体停止
        if (pausing) return;

        Vector3 pos = transform.position;
        if ((target - pos).sqrMagnitude <= arriveThreshold * arriveThreshold) {
            StartCoroutine(PauseAndPick());
            return;
        }

        Vector3 dir = (target - pos).normalized;
        transform.position = pos + dir * speed * Time.deltaTime;

        if (faceDirection && dir.sqrMagnitude > 0.0001f) {
            float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, ang);
        }
    }

    IEnumerator PauseAndPick() {
        pausing = true;
        float t = Random.Range(pauseDuration.x, pauseDuration.y);
        float elapsed = 0f;

        // 待機中に停止されたら即中断
        while (elapsed < t) {
            if (!isActive) { pausing = false; yield break; }
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (!isActive) { pausing = false; yield break; }

        UpdateBounds();
        PickNewTarget();
        pausing = false;
    }

    void PickNewTarget() {
        // 停止中に呼ばれても何もしない
        if (!isActive) return;

        target = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            transform.position.z
        );
    }

    void UpdateBounds() {
        Vector3 bl = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 tr = cam.ScreenToWorldPoint(new Vector3(940, 560, 0));

        float padX = edgeMargin + (sr ? sr.bounds.extents.x : 0f);
        float padY = edgeMargin + (sr ? sr.bounds.extents.y : 0f);

        minX = bl.x + padX;
        maxX = tr.x - padX;
        minY = bl.y + padY;
        maxY = tr.y - padY;
    }

    // --- 外部からON/OFFしたいとき用のヘルパ ---
    public void SetActive(bool active) {
        if (isActive == active) return;
        isActive = active;

        if (!isActive) {
            StopAllCoroutines();
            pausing = false;
        }
        else {
            // 再開時に目標を再設定（任意）
            PickNewTarget();
        }
    }

    public void Activate() => SetActive(true);
    public void Deactivate() => SetActive(false);
}
