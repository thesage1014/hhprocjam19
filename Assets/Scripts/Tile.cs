using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    List<Transform> objects;
    List<Vector3> originalScales;
    // Start is called before the first frame update
    bool scaling = true;
    float scaleSpeed = .0001f;
    void Start() {
        objects = new List<Transform>(GetComponentsInChildren<Transform>());
        originalScales = new List<Vector3>();
        for (int i = 0; i < objects.Count; i++) {
            Transform obj = objects[i];
            originalScales.Add(obj.transform.localScale);
            obj.transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update() {
        if (objects[0].localScale == originalScales[0]) {
            scaling = false;
        } else {
            for (int i = 0; i < objects.Count; i++) {
                Transform obj = objects[i];
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, originalScales[i], scaleSpeed*Time.deltaTime);
            }
        }
    }
    public void explore(Vector2 agentOffset) {
        scaling = true;
        //print(agentOffset);
        float mag = agentOffset.magnitude * agentOffset.magnitude - .3f;
        if (mag != 0) {
            scaleSpeed = 1.5f / (mag);
        }
    }
}
