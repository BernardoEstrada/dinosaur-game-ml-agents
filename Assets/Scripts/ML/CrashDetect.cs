using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) { }
    }
}