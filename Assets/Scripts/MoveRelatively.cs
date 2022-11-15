using UnityEngine;

public class MoveRelatively : MonoBehaviour {
    public Level level;
    public float speedMultiplier = 1;
    public float speedOffset;
    private float actualSpeedOffset;

    private void Start() {
        actualSpeedOffset = Random.value < 0.5 ? speedOffset : -speedOffset;
    }

    private void Update() {
        transform.position += Vector3.left * Time.deltaTime * (level.mainSpeed * speedMultiplier + actualSpeedOffset);
    }
}