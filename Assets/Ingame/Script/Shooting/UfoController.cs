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

        // 左端から右端へ移動
        transform.position = new Vector3(leftX, transform.position.y, transform.position.z);

        transform.DOMoveX(rightX * 2, moveDuration)
            .SetEase(Ease.Linear).SetLink(gameObject);
    }
}
