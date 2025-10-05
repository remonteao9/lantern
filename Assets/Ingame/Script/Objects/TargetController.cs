using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TargetController : Default {

    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f; // 移動速度


    private void Update() {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public override IEnumerator Gravity() {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Gun() {
        this.transform.DOScale(new Vector3(0, 0, 0), 1f);
        yield return new WaitForSeconds(1.1f);
        Destroy(this);
    }

    public override void Magunet() {
        transform.position += Vector3.up * 7f;
    }

    public override IEnumerator Mosquito() {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Ufo() {
        throw new System.NotImplementedException();
    }
}
