
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EditMode : MonoBehaviour
{
    [SerializeField] private SpriteRenderer editSprite;
    [SerializeField] private GameObject placementModeWindow;
    [SerializeField] private TMP_Text onOffText;

    [DllImport("__Internal")] private static extern void DisabledItemButton(string sceneName);

    public static EditMode instance = null;
    public string editObjectScene = string.Empty;
    public GameObject editObject = null;
    private int sortOrder = 10;
    private CircleCollider2D editCol;

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

            if (!editCol) {
                editCol = editObject.GetComponent<CircleCollider2D>();
            }
            if (editCol != null) {
                Vector2 center = (Vector2)worldPos + editCol.offset;
                bool isOverlap = Physics2D.OverlapCircle(center, editCol.radius) != null;
                if (isOverlap) {
                    editSprite.color = new Color(0, 0, 0, 0);
                    return;
                }
                else {
                    editSprite.color = new Color(1, 1, 1, 0.5f);
                }
            }


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
                editCol = null;
            }

        }
    }

    public void ChangeEdit(GameObject obj, string sceneName) {
        editObject = obj;
        editObjectScene = sceneName;
        if (obj) {
            onOffText.text = "PLACEMENT MODE\nON";
            onOffText.color = Color.yellow;
        }
        else {
            onOffText.text = "PLACEMENT MODE\nOFF";
            onOffText.color = Color.cyan;
        }
        placementModeWindow.SetActive(true);
        placementModeWindow.transform.DOKill();
        placementModeWindow.transform.localScale = Vector3.one;
        placementModeWindow.transform.DOScale(Vector3.zero, 0.5f).SetDelay(0.5f).OnComplete(() => placementModeWindow.SetActive(false));
    }
}
