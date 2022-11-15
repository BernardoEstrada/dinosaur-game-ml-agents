using UnityEngine;

public class DoAnimationTrigger : MonoBehaviour {
    public string trigger;

    private void Start() {
        GetComponent<Animator>().SetBool(trigger, true);
    }
}