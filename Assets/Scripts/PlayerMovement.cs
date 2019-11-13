using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    float startMovespeed;
    public float RotateSpeed = 30f;
    public float WarpCooldown = 0f;
    bool respawning = false;
    // Start is called before the first frame update
    void Start()
    {
        startMovespeed = moveSpeed;
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
        moveSpeed = startMovespeed + Input.GetAxis("Fire3")*1.5f;
        if(!respawning && Input.GetAxis("Jump")==1) {
            respawning = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetAxis("Cancel") == 1) {
            Application.Quit();
        }
    }

}