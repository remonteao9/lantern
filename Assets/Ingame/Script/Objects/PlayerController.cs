using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector2 pos;
    private Vector2 newPos;
    private bool isActive = true;

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
}
