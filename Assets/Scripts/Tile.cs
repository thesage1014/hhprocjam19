using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    List<Transform> objects;
    List<Vector3> originalScales;
    List<Vector3> originalPoss;
    List<Quaternion> originalAngles;

    // Start is called before the first frame update
    bool scaling = true;
    float scaleSpeed = .0001f;
    public string tileType;
    public float rotRandomness = 360;
    public float posRandomness = .3f;
    public bool rot90Rand = true;
    void Start() {
        objects = new List<Transform>(GetComponentsInChildren<Transform>());
        originalScales = new List<Vector3>();
        originalPoss = new List<Vector3>();
        originalAngles = new List<Quaternion>();
        for (int i = 0; i < objects.Count; i++) {
            Transform obj = objects[i];
            if(rot90Rand) {
                obj.transform.localEulerAngles += Vector3.up * ((int)Random.Range(0, 3.99999f)) * 90;
            }
            originalScales.Add(obj.transform.localScale);
            originalPoss.Add(obj.transform.localPosition);
            originalAngles.Add(obj.transform.localRotation);
            obj.transform.localScale = Vector3.zero;
            obj.transform.localPosition += new Vector3(Random.Range(-posRandomness, posRandomness), Random.Range(-posRandomness, posRandomness), Random.Range(-posRandomness, posRandomness));
            obj.transform.localEulerAngles += new Vector3(Random.Range(-rotRandomness, rotRandomness), Random.Range(-rotRandomness, rotRandomness), Random.Range(-rotRandomness, rotRandomness));
        }
    }

    // Update is called once per frame
    void Update() {
        if (objects[0].localScale == originalScales[0]) {
            scaling = false;
        } else {
            for (int i = 0; i < objects.Count; i++) {
                Transform obj = objects[i];
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, originalScales[i], scaleSpeed * Time.deltaTime);
                obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition, originalPoss[i], scaleSpeed * Time.deltaTime);
                obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, originalAngles[i], scaleSpeed * Time.deltaTime);
                //obj.transform. originalAngles[i], scaleSpeed * Time.deltaTime);
            }
        }
    }
    public void beExplored(Vector2 agentOffset) {
        scaling = true;
        //print(agentOffset);
        float mag = agentOffset.magnitude * agentOffset.magnitude - .3f;
        if (mag != 0) {
            scaleSpeed = 1.5f / (mag); //1.5f
        }
    }
}
