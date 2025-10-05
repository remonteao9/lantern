using System.Collections;
using TMPro;
using UnityEngine;

public class GravityGameController : MonoBehaviour {
    [SerializeField] private GameObject iron;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject IronUpPanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private MagnetController magnetController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private float checkDistance = 10f; // 下方向にRayを飛ばす距離
    private float fallSpeed = 3f;      // 落下速度
    private float riseSpeed = 3f;      // 上昇速度
    private float moveSpeed = 8f;     // 横移動速度
    private float stopHeight = 10f;    // 上昇上限（越えない）

    private float moveDirection = 1f; // 1=右, -1=左

    private float time = 15;

    private bool isActive = false;

    // 画面のワールド座標境界
    private float minX, maxX, minY, maxY;

    private void Awake() {
        // 画面サイズ（スクリーン座標）をワールド座標に変換
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(940, 560, 0));

        minX = screenBottomLeft.x;
        minY = screenBottomLeft.y;
        maxX = screenTopRight.x;
        maxY = screenTopRight.y;

        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        IronUpPanel.SetActive(false);
        StartCoroutine(GameStart());
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private IEnumerator GameStart() {
        yield return new WaitForSeconds(3);
        gameStartPanel.SetActive(false);
        isActive = true;
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

            // ===== 画面内にClamp =====
            Vector3 pos = iron.transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            if (Mathf.Approximately(pos.y, minY)) {
                GameOver(); // メソッド呼び出し
                isActive = false;
            }

            if (Mathf.Approximately(pos.y, maxY)) {
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
