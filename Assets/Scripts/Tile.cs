using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    List<Transform> objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = new List<Transform>(GetComponentsInChildren<Transform>());
        objects.Add(transform);
        foreach (Transform obj in objects) {
            obj.transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform obj in objects) {
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, Vector3.one,.02f);
        }
    }
}
