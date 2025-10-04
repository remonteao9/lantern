
using DG.Tweening;
using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f; // 移動速度
    [SerializeField] private GameObject obj;

    private void Awake() {
        obj.transform.DOShakePosition(10, new Vector2(0.1f, 0.1f), 10, 90, fadeOut: false).SetLoops(-1, LoopType.Restart);
    }

    void Update() {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

}
