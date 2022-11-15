using UnityEngine;

public class RestartTerrain : MonoBehaviour {
    public SpriteRenderer renderer;
    public int terrainOffset;

    private void Start() {
        transform.position += terrainOffset * renderer.bounds.size.x * Vector3.right;
    }

    private void Update() {
        if (transform.position.x > -renderer.bounds.size.x) return;
        transform.position += 2 * renderer.bounds.size.x * Vector3.right;
    }
}