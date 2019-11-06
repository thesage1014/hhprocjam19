using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float RotateSpeed = 30f;
    public float WarpCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

        {
            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime);
        }

        if (WarpCooldown > 0)
        { 
            WarpCooldown = WarpCooldown - Time.deltaTime; 
        }
    }

}