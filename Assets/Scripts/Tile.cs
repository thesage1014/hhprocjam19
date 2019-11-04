using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
     List<Transform> objects;
     List<Vector3> originalScales;
    // Start is called before the first frame update
    
    void Start()
    {
        objects = new List<Transform>(GetComponentsInChildren<Transform>());
        originalScales = new List<Vector3>();
        for (int i = 0; i < objects.Count; i++) {
            Transform obj = objects[i];
            originalScales.Add(obj.transform.localScale);
            obj.transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<objects.Count;i++) {
            Transform obj = objects[i];
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, originalScales[i], .02f);
        }
    }
}
