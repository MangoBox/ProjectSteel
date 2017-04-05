using UnityEngine;
using System.Collections;

public class ResetController : MonoBehaviour {

    //This is a class specifically resetting the player should they get stuck.
	//To be placed on the car object.

    public float maxHeightReset;
    public float additionalResultHeight;
    public Rigidbody carRigidbody;

	// Update is called once per frame
	void Update () {
        //If the user wishes to reset their car.
        if (Input.GetKey(KeyCode.R))
        {
            RaycastHit hit;
            Physics.Raycast(new Vector3(gameObject.transform.position.x, maxHeightReset, gameObject.transform.position.z), gameObject.transform.up * -1, out hit);

            //Resets the position of the gameobject to the required position.

            gameObject.transform.position = new Vector3(hit.point.x, hit.point.y + additionalResultHeight, hit.point.z);
            carRigidbody.velocity = Vector3.zero;
        }
	}
}
