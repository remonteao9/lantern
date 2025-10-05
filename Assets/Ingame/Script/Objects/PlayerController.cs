using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MNActor {
    [SerializeField] private GameObject clearPanel;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clearSE;
    private Vector2 pos;
    private Vector2 newPos;
    private bool isActive = true;

    public override void Awake() {
        base.Awake();
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
                newPos.x -= 7f * Time.deltaTime;
                transform.position = newPos;
            }
            else if (Input.GetKey(KeyCode.D)) {
                newPos = pos;
                newPos.x += 7f * Time.deltaTime;
                transform.position = newPos;
            }
        }
    }

    public void ChengeActive(bool wat) {
        isActive = wat;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "GOAL") {
            GameClear();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "enemy") {
            Destroy(gameObject);
        }
    }

    private void GameClear() {
        clearPanel.SetActive(true);
        this.ChengeActive(false);
        audioSource.loop = false;
        audioSource.clip = clearSE;
        audioSource.Play();
    }
}
