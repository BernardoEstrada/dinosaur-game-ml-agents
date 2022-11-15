using UnityEngine;

public class MoonPhases : MonoBehaviour {
    public TimeOfDay timeOfDay;
    public SpriteRenderer renderer;
    public Sprite[] sprites;
    private bool isNight;
    private int phase;

    private void Start() {
        if (sprites.Length > 0) {
            phase = sprites.Length - 1;
            renderer.sprite = sprites[phase];
        }
    }

    private void Update() {
        if (sprites.Length > 0)
            if (!isNight && timeOfDay.isNight()) {
                phase = (phase + 1) % sprites.Length;
                renderer.sprite = sprites[phase];
            }

        isNight = timeOfDay.isNight();
        renderer.color = new Color(0.7333333333f, 0.7333333333f, 0.7333333333f, timeOfDay.value());
    }
}