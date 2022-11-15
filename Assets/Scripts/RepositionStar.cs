using UnityEngine;

public class RepositionStar : MonoBehaviour {
    public TimeOfDay timeOfDay;
    public float minimumStart = -500f;
    public float maximumStart = 500f;
    private bool isNight;

    private void Update() {
        if (!isNight && timeOfDay.isNight()) {
            transform.position = new Vector3(minimumStart + Random.value * (maximumStart - minimumStart),
                0.3333333333f + Random.value * 4.6666666667f, 0f);
            var animator = GetComponent<Animator>();
            var state = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(state.fullPathHash, -1, Random.value);
        }

        isNight = timeOfDay.isNight();
    }
}