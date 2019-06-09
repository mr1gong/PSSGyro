using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float lastStep = 0;
            if (Time.time - lastStep > 0.5f)

            {
                lastStep = Time.time;
                transform.LookAt(player.transform);
                transform.Translate(Vector3.right);



            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            float lastStep = 0;
            if (Time.time - lastStep > 0.5f)

            {
                lastStep = Time.time;
                transform.LookAt(player.transform);
                transform.Translate(Vector3.left);



            }
        }
        else
        {

        }

    }
}
