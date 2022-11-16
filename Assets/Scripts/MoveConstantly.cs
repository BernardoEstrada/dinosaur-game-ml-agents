using UnityEngine;

public class MoveConstantly : MonoBehaviour {
    public float speedMultiplier = 1;
    public float startAt = 5000;
    public float restartAt = -5000;

    private void Update() {
        transform.position +=  Time.deltaTime * speedMultiplier * Vector3.left;
        if (transform.position.x > restartAt) return;
        transform.position += Vector3.right * (startAt - restartAt);
    }

    public void Stop() {
        speedMultiplier = 0;
    }

    public void Restart() {
        speedMultiplier = 1;
    }
}