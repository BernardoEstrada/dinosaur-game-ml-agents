using UnityEngine;

public class DestroyOnLeftEdge : MonoBehaviour {
    public GameObject ground;

    private void Update() {
        if (transform.position.x > -ground.transform.localScale.x / 2) return;
        Destroy(gameObject);
    }
}