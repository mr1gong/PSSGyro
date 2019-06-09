using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour {
    //DO TIME OFFSET
    public bool MovementEnabled = true;
    public List<Vector3> RotationSteps;
    private Vector3 target;
    public float WaitTimeBeforeGo = 1.0f;
    public float TimeOffset = 0.0f;
    public bool Repeat = true;
    public float Speed = 1.0f;
    private int index = 0;
    private bool beingHandled = false;
    private IEnumerator coroutine;
    private float timer;

    // Use this for initialization
    void Start () {
       


        timer = 0;
	}

    // Update is called once per frame
    void Update() {
        if (MovementEnabled)
        {

            target = RotationSteps[index];
            if (timer >= TimeOffset)
            {

             

                float step = Speed * Time.deltaTime; // calculate distance to move


                // Check if the position of the cube and sphere are approximately equal.
                if (Quaternion.Angle(transform.rotation, Quaternion.Euler(target)) == 0)
                {



                    if ( /*some case  */ !beingHandled)
                    {
                        beingHandled = true;
                        coroutine = IncIndex();
                        StartCoroutine(coroutine);

                    }

                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target), step);


                }

            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

   private IEnumerator IncIndex()
    {

        yield return new WaitForSeconds(WaitTimeBeforeGo);


        if (index != (RotationSteps.Count - 1))
        {

            index++;

           

        }
        else
        {
            if (Repeat)
            {
                index = 0;
            }
        }
        beingHandled = false;
    }
}
