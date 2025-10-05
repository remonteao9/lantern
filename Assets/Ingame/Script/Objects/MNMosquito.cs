
using DG.Tweening;
using UnityEngine;

public class MNMosquito : MNActor
{
    [SerializeField] private float speed = 3f; // 移動速度

    void Update() {
        Vector2 direction = (Dao.player.transform.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var actor = collision.gameObject.GetComponent<MNActor>();
        if (actor != null){
            actor.HitMosquito();
            Destroy(gameObject);
        }
    }

}
