using System.Collections;
using TMPro;
using UnityEngine;

public class GravityGameController : MonoBehaviour {
    [SerializeField] private GameObject iron;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject IronUpPanel;
    [SerializeField] private MagnetController magnetController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private float checkDistance = 10f; // 下方向にRayを飛ばす距離
    private float fallSpeed = 2f;      // 落下速度
    private float riseSpeed = 4f;      // 上昇速度
    private float moveSpeed = 8f;     // 横移動速度
    private float stopHeight = 10f;    // 上昇上限（越えない）

    private float moveDirection = 1f; // 1=右, -1=左

    private float time = 15;

    private bool isActive = false;

    private void Awake() {

        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        IronUpPanel.SetActive(false);
        isActive = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Update() {

        if (isActive) {

            // iron の真下にRayを飛ばす
            RaycastHit2D hit = Physics2D.Raycast(iron.transform.position, Vector2.down, checkDistance);
            bool magnetBelow = hit.collider != null && hit.collider.CompareTag("Magnet");

            if (magnetBelow && iron.transform.position.y < stopHeight) {
                // 上昇 + 横移動を一括処理
                Vector3 move = Vector3.up * riseSpeed + Vector3.right * moveDirection * moveSpeed;
                iron.transform.position += move * Time.deltaTime;

                // ランダムで方向を切り替える（確率1%）
                if (Random.value < 0.01f)
                    moveDirection *= -1f;
            }
            else if (!magnetBelow) {
                // 落下
                iron.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            }


            Vector3 pos = iron.transform.position;

            if (pos.y < -6) {
                GameOver();
                isActive = false;
            }

            if (pos.y > 6) {
                IronUP();
                isActive = false;
            }

            iron.transform.position = pos;
            time -= Time.deltaTime;
            countText.text = time.ToString("F0");
            if (time <= 0) {
                GameClear();
                isActive = false;
            }
        }
    }

    private void GameClear() {
        magnetController.ChengeActive(false);
        clearPanel.SetActive(true);
        GameItems.SetItem("磁石");
    }

    private void GameOver() {
        magnetController.ChengeActive(false);
        gameOverPanel.SetActive(true);
        GameItems.SetItem("鉄");
    }

    private void IronUP() {
        magnetController.ChengeActive(false);
        IronUpPanel.SetActive(true);
        GameItems.SetItem("磁力");
    }
}
