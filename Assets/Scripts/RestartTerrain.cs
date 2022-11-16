using UnityEngine;
using UnityEngine.Serialization;

public class RestartTerrain : MonoBehaviour {
    [FormerlySerializedAs("renderer")] public SpriteRenderer terrainSpriteRenderer;
    public int terrainOffset;

    private void Start() {
        transform.position += terrainOffset * terrainSpriteRenderer.bounds.size.x * Vector3.right;
    }
    public void Reset() {
        transform.position += terrainOffset * terrainSpriteRenderer.bounds.size.x * Vector3.right;
    }

    private void Update() {
        if (transform.position.x > -terrainSpriteRenderer.bounds.size.x) return;
        transform.position += 2 * terrainSpriteRenderer.bounds.size.x * Vector3.right;
    }
}