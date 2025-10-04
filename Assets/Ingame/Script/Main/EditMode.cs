
using UnityEngine;
using static UnityEngine.Tilemaps.TilemapRenderer;

public class EditMode : MonoBehaviour
{
    public static EditMode instance = null;
    public bool isEditMode = false;
    GameObject editObject = null;
    private int sortOrder = 10;

    private void Awake() {
        instance = this;
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Update() {
        if (isEditMode && Input.GetMouseButtonDown(0)) {

            // クリック位置（スクリーン座標）→ ワールド座標に変換
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Zを0に（2D用）

            // オブジェクト生成
            GameObject obj = Instantiate(editObject, mousePos, Quaternion.identity);

            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null) {
                sr.sortingOrder = sortOrder;
                sortOrder++;
            }

            isEditMode = false;
            // jsの対象ボタンをオフにする
        }
    }
}
