using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public bool isCursorHidden = true;
    public float minPitch = 20f, MaxPitch = 80f;
    public Vector2 speed = new Vector2(120f, 120f);

    private Vector3 euler;
    
    // Start is called before the first frame update
    void Start()
    {
        //Is the cursor supposed to be hidden
        if (isCursorHidden)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        euler = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the camera based on mouse movement
        euler.y += Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
        euler.x -= Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;

        //Clamp the value
        euler.x = Mathf.Clamp(euler.x, minPitch, MaxPitch);
        //Rotate the camera and player
        transform.parent.localEulerAngles = new Vector3(0, euler.y, 0);
        transform.localEulerAngles = new Vector3(euler.x, 0, 0);
    }
}
