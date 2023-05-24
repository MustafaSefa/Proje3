using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera c;
    void Start()
    {
        C();
    }

    void Update()
    {
        
    }

    private void C()
    {
        if (N.S == "a")
        {
            c.transform.position = new Vector3(978,56.6f,-473.5f);
            c.transform.Rotate(13.923f,27.378f,-0.851f);
        }
    }
}
