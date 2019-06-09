using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour {
    //DO TIME OFFSET

    public List<GameObject> Waypoints;
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
        

        if (timer >= TimeOffset ) {

            GameObject waypoint = Waypoints[index];
            Transform target = waypoint.transform;
            float step = Speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {

                if ( /*some case  */ !beingHandled)
                {
                    beingHandled = true;
                    coroutine = IncIndex();
                    StartCoroutine(coroutine);

                }

            }

        }
        else
        {
            timer += Time.deltaTime;
        }
    }

   private IEnumerator IncIndex()
    {

        yield return new WaitForSeconds(WaitTimeBeforeGo);


        if (index != (Waypoints.Count - 1))
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

