using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBoundsCheck : MonoBehaviour {
    public float BedY = -20.0f;
    public GameObject checkpoint;
    private Vector3 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y <= BedY)
        {
            if(checkpoint != null)
            {
                transform.position = checkpoint.transform.position;
            }
            else
            {
                transform.position = startPos;
            }
           

        }


	}
}
