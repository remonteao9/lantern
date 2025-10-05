using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerController : Default {
    private Vector2 pos;
    private Vector2 newPos;
    private bool isActive = true;

    private void Awake() {
        Dao.player = this;
    }

    private void OnDestroy() {
        Dao.player = null;
    }

    private void Update() {

        if (isActive) {
            pos = transform.position;

            if (Input.GetKey(KeyCode.A)) {
                newPos = pos;
                newPos.x -= 0.008f;
                transform.position = newPos;
            }
            else if (Input.GetKey(KeyCode.D)) {
                newPos = pos;
                newPos.x += 0.008f;
                transform.position = newPos;
            }
        }
    }

    public void ChengeActive(bool wat) {
        isActive = wat;
    }

    public override IEnumerator Mosquito() {
        this.transform.DOScale(new Vector3(0, 0, 0), 1f);
        yield return new WaitForSeconds(1.1f);
        Destroy(this);
    }

    public override IEnumerator Gun() {
        this.transform.DOScale(new Vector3(0, 0, 0), 1f);
        yield return new WaitForSeconds(1.1f);
        Destroy(this);
    }

    public override IEnumerator Ufo() {
        throw new System.NotImplementedException();
    }

    public override void Magunet() {
        transform.position += Vector3.up * 7f;
    }

    public override IEnumerator Gravity() {
        throw new System.NotImplementedException();
    }
}
