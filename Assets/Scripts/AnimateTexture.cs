using UnityEngine;

public class AnimateTexture : MonoBehaviour {
    public Level level;
    public Renderer renderer;
    public Vector2 direction = Vector2.up;
    public float speedMultiplier = 1;

    private void Update() {
        renderer.material.mainTextureOffset += direction.normalized * Time.deltaTime * level.mainSpeed *
            speedMultiplier * renderer.material.mainTextureScale.x / transform.localScale.x;
    }
}