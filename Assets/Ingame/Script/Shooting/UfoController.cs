using UnityEngine;
using DG.Tweening;

public class UfoController : MonoBehaviour {
    [SerializeField] private float moveDuration = 3f;

    void Start() {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float leftX = cam.transform.position.x - camWidth / 2f;
        float rightX = cam.transform.position.x + camWidth / 2f;

        // 左端から右端へ移動 → 終わったらDestroy
        transform.position = new Vector3(leftX, transform.position.y, transform.position.z);

        transform.DOMoveX(rightX, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Destroy(gameObject); // 移動が終わったら削除
            });
    }
}
