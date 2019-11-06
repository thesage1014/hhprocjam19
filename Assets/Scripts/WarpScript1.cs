using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript1 : MonoBehaviour{
    public Transform warpTarget;
    public GameObject thePlayer;

        void OnTriggerEnter(Collider other)
    {
       if(other.gameObject==thePlayer)
        thePlayer.transform.position = warpTarget.transform.position;
    }
}
