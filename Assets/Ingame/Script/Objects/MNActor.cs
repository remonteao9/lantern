using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MNActor : MonoBehaviour{
    public Rigidbody2D rb;

    public virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void HitMosquito() {
        transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => Destroy(gameObject));
    }

    public virtual void HitGun() {
        this.transform.DOScale(Vector3.zero, 1f).OnComplete(() => Destroy(gameObject));
    }

    public virtual void HitMagnet() {
        Debug.Log(1233);
        rb.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);
    }

    public virtual void Gravity() {
        rb.gravityScale *= -1;
        StartCoroutine(ReverseGravity(2));
    }

    protected IEnumerator ReverseGravity(int second) {
        yield return new WaitForSeconds(second);
        rb.gravityScale *= -1;
    }

    protected IEnumerator DestroyWait(int second) {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

}
