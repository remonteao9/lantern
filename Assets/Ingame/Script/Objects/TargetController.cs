using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TargetController : MNActor {

    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f; // 移動速度


    private void Update() {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public override void HitGun() {
        this.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() => Destroy(this));
    }

}
