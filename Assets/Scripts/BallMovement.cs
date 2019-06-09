using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    // Use this for initialization
    Rigidbody rigid;

    public float ForceModifier = 2;
	void Start () {
        rigid = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

        var d1 = Input.GetAxis("Horizontal")*ForceModifier;
        var d2 = Input.GetAxis("Vertical")*ForceModifier;



        rigid.AddForce(new Vector3(d1,0,d2));
        rigid.AddForce(new Vector3(GyroControl.Instance.RotX/90, 0, GyroControl.Instance.RotY/90));

	}
}
