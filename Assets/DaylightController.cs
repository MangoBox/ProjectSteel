using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DaylightController : MonoBehaviour {

    //This is a class specifically intended for creating day-night cycles within the game.
    //Actually, since theres night as well, this class is kinda miss-named, but whatever.

    //The light to be rotated.
    public Light light;

    //The from and to rotations.
    Quaternion dawnRotation;
    Quaternion nightRotation;

    public Image barObject;

    public SunRotation startRot;
    public SunRotation destRot;

    //The value to increment every second to the time.
    public float incrementValue;

    //A global variable for showing the time of day.
    private float currentTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Time is in accordance to seconds.

        addTime(incrementValue * Time.deltaTime);
	}

    void setTime(float value)
    {
            float rotX = Mathf.Lerp(startRot.yawRotation, destRot.yawRotation, value);
            float rotY = Mathf.Lerp(startRot.pitchRotation, destRot.pitchRotation, value);
            float rotZ = Mathf.Lerp(startRot.tiltRotation, destRot.tiltRotation, value);

            light.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
            print(rotX + " " + rotY + " " + rotZ);

            currentTime = value;
            barObject.fillAmount = currentTime;
    }

    //Simply adds the current time to the parameter amount.
    void addTime(float addValue)
    {
        setTime(addValue + currentTime);
    }

}

[System.Serializable]
public struct SunRotation { 
    public float yawRotation;
    public float pitchRotation;
    public float tiltRotation;
}
