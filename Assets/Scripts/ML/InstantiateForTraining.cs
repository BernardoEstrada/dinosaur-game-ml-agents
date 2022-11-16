using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateForTraining : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public int w = 5;
    public int h = 6;
    private int n;
    void Awake() {
        n = 0;
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                var instance = Instantiate(
                    prefab, 
                    new Vector3(-8.39999962f + 100 * i,14.1650238f + 40 * j,-645.099976f),
                    Quaternion.identity
                    );
                instance.name = prefab.name + " (" + n + ")";
                n++;
            }
        }
    }
}
