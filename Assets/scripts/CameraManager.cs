using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    public bool isLeftMoving = false;
    public bool isRightMoving = false;
    public bool isUpMoving = false;
    public bool isDownMoving = false;
    public void Update()
    {
        if (isLeftMoving)
        {
            camera.transform.position += new Vector3(-0.1f, 0, 0);
        }

        if (isRightMoving)
        {
            camera.transform.position += new Vector3(0.1f, 0, 0);
        }

        if (isUpMoving)
        {
            camera.transform.position += new Vector3(0, 0, 0.1f);
        }

        if (isDownMoving)
        {
            camera.transform.position += new Vector3(0, 0, -0.1f);
        }
    }
    public void OnPointerExit()
    {
        if (gameObject.tag == "Left")
        {
            isLeftMoving = false;
            
        }
        if (gameObject.tag == "Right")
        {
            isRightMoving = false;
            
        }
        if (gameObject.tag == "Footer")
        {
            isDownMoving = false;
            
        }
        if (gameObject.tag == "Header")
        {
            isUpMoving = false;
            
        }
    }

    public void OnPointerEnter()
    {
        if (gameObject.tag == "Left")
        {
            isLeftMoving = true;
            
        }
        if (gameObject.tag == "Right")
        {
            isRightMoving = true;
            
        }
        if (gameObject.tag == "Footer")
        {
            isDownMoving = true;
            
        }
        if (gameObject.tag == "Header")
        {
            isUpMoving = true;
            
        }
    }
}
