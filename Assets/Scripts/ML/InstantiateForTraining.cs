using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateForTraining : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public int instances = 10;
    void Awake() {
        for (int i=0; i<instances; i++) {
            var instance = Instantiate(prefab, new Vector3(-8.39999962f,14.1650238f,-645.099976f - i), Quaternion.identity);
            instance.name = prefab.name + " (" + i + ")";
        }
    }
}
