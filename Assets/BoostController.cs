using UnityEngine;
using System.Collections;

public class BoostController : MonoBehaviour {
    //A class for controlling boost of the car.


    public ParticleSystem[] particleSystems;
    public Rigidbody carRigidbody;
	public Transform boostPos;
    public int XBoostOffset;

    public Transform boostTex;

    public float forceMultiplier;

    private bool isBoosting;

    void Update()
    {
        Vector2 carPos = Camera.main.WorldToScreenPoint(carRigidbody.transform.position);
        boostTex.transform.position = new Vector3(carPos.x + XBoostOffset, carPos.y, 0);
        //If the user is boosting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            //Enabling particles
            for (int i = 0; i < particleSystems.Length; i++)
            {
                //Looping through the selected particle systems.
                particleSystems[i].enableEmission = true;
                isBoosting = true;
                print("test");
       
            }

            //Adding velocity
			carRigidbody.AddForceAtPosition(carRigidbody.transform.forward * forceMultiplier, boostPos.position);
        }
        else
        {
            // If the player is not boosting
            for (int i = 0; i < particleSystems.Length; i++)
            {
                //Looping through the selected particle systems.
                particleSystems[i].enableEmission = false;
                isBoosting = false;
            }
        }
    }
}
