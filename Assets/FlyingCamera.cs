using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCamera : MonoBehaviour
{

    public Camera cam;
    float xrot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(transform.position, cam.transform.forward, out RaycastHit hit))
            {

                if (hit.transform.tag == "Terrain")
                {
                    hit.transform.GetComponent<Marching>().PlaceTerrain(hit.point);
                    print("spaghetti");
                }
            }

        }

        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(transform.position, cam.transform.forward, out RaycastHit hit))
            {

                if (hit.transform.tag == "Terrain") 
                    hit.transform.GetComponent<Marching>().RemoveTerrain(hit.point);

            }

        }

    }

    private void FixedUpdate()
    {
        var movement = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("UpDown"), Input.GetAxisRaw("Vertical")) * 0.1f);
        transform.position += movement;

        transform.Rotate(0f, Input.GetAxisRaw("Mouse X") * 4, 0f);

        xrot -= Input.GetAxisRaw("Mouse Y") * 4;

        xrot = Mathf.Clamp(xrot, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xrot, 0f, 0f);
    }

}