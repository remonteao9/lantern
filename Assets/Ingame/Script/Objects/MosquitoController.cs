
using DG.Tweening;
using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f; // 移動速度
    [SerializeField] private GameObject obj;

    void Update() {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Target")){
            collision.GetComponent<TargetController>().Mosquito();
        }
    }

}
