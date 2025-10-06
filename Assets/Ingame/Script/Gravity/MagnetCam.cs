using UnityEngine;

public class MagnetCam : MonoBehaviour {
    [SerializeField] private Transform iron;
    [SerializeField] private Transform magnet;

    private void Update() {
        var newPosX = (iron.transform.position.x + magnet.transform.position.x) / 2;
        var newPos = transform.position;
        newPos.x = newPosX;
        transform.position = newPos;
    }
}
