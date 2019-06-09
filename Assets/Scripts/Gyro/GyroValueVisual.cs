using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroValueVisual : MonoBehaviour {

    
    public Slider visualX;
    public Slider visualY;
    public Slider visualZ;
    public Text numX;
    public Text numY;
    public Text numZ;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        visualX.value = GyroControl.Instance.RotX;
        visualY.value = GyroControl.Instance.RotY;
        visualZ.value = GyroControl.Instance.RotZ;
        numX.text = visualX.value+ "";
        numY.text = visualY.value + "";
        numZ.text = visualZ.value + "";

    }

    
}
