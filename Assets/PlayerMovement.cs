using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float RotateSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        transform.Translate(0f, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

        {
            if (Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
            else if (Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        }
    }

}