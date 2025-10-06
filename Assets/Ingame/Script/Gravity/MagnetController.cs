using UnityEngine;

public class MagnetController : MonoBehaviour
{

    private Vector2 pos;
    private Vector2 newPos;
    private bool isActive = true;

    private void Update() {

        if (isActive) {
            pos = transform.position;
            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal < 0) {
                newPos = pos;
                newPos.x -= 10f * Time.deltaTime;
                transform.position = newPos;
            }
            else if (horizontal > 0) {
                newPos = pos;
                newPos.x += 10f * Time.deltaTime;
                transform.position = newPos;
            }
        }
    }

    public void ChengeActive(bool wat) {
        isActive = wat;
    }
}
