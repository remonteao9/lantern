using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MNActor : MonoBehaviour{
    public CircleCollider2D col;
    public Rigidbody2D rb;

    public virtual void Awake() {
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void HitMosquito() {
        Destroy(gameObject);
    }

    public virtual void HitGun() {
        Destroy(gameObject);
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
