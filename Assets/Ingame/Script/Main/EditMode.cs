
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.Tilemaps.TilemapRenderer;

public class EditMode : MonoBehaviour
{
    [SerializeField] private SpriteRenderer editSprite;

    [DllImport("__Internal")] private static extern void DisabledItemButton(string sceneName);

    public static EditMode instance = null;
    public string editObjectScene = string.Empty;
    public GameObject editObject = null;
    private int sortOrder = 10;

    private void Awake() {
        instance = this;
        editSprite.enabled = false;
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Update() {
        if (editObject != null) {
            if (!editSprite.enabled) {
                editSprite.sprite = editObject.GetComponent<SpriteRenderer>().sprite;
                editSprite.enabled = true;
            }
            Vector3 mousePos = Input.mousePosition;

            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;

            transform.position = worldPos;

            if (Input.GetMouseButtonDown(0)) {                

                // オブジェクト生成
                GameObject obj = Instantiate(editObject, worldPos, Quaternion.identity);

                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null) {
                    sr.sortingOrder = sortOrder;
                    sortOrder++;
                }

                editObject = null;
#if UNITY_WEBGL && !UNITY_EDITOR
                DisabledItemButton(editObjectScene);
#endif
                editObjectScene = string.Empty;
                editSprite.enabled = false;
            }

        }
    }
}
