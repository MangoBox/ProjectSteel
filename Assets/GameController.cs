using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    //The target player. Must be in scene.
    public GameObject player;
    //The players camera. Must be in scene.
    public Camera playerCamera;
    //The target prefab for the checkpoint. Must be prefab.
    public GameObject checkpointPrefab;
    //Scene object for parenting.
    public Transform checkpointParent;

    public MapGenerator mapGenerator;
    private MeshData meshData;
    public Dictionary<string, Checkpoint> checkpointDictonary = new Dictionary<string, Checkpoint>();
    

	// Use this for initialization
	void Start () {
        PlaceRandomCheckpoint();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlaceRandomCheckpoint();
        }
	}

    //Places a checkpoint randomly
    void PlaceRandomCheckpoint()
    {
        Checkpoint checkpoint = new Checkpoint(checkpointParent, this, checkpointPrefab, 700, 100, 30, 180, player);
        float height = checkpoint.height;
        GameObject instCheckpoint = (GameObject) Instantiate(checkpoint.checkpointObject, new Vector3(checkpoint.coords.x, height + 50, checkpoint.coords.y), Quaternion.identity);
        print(checkpoint.height);

        //test statements
    }

}

//A custom class + constructor for creating a checkpoint.
public class Checkpoint
{
    //The checkpoint object.
    public GameObject checkpointObject;
    //The coordinates for the spawned checkpoint. Vector3 is not used due to raycasting functions later.
    public Vector2 coords;
    public float height;
    int id;


    //May not work, may require an explicit definition later.
    public GameController gameController;

    public Checkpoint(Transform parent, GameController gameContr, GameObject checkpoint, float minPlayerSpawnRadius, float maxPlayerSpawnRadius, float angleDeviation, float angle, GameObject player)
    {
        //Creating the final angle to feed into the placement math.
        float finalAngle = Random.Range(angle - angleDeviation, angle + angleDeviation);

        //Sets a global reference to gameController, allowing us to access to it.
        gameController = gameContr;
        checkpointObject = checkpoint;

        id = gameController.checkpointDictonary.Count + 1;
        checkpointObject.transform.SetParent(parent, true);
        
        //Creates a ray facing downwards over the object, casting to find the terrain.
        RaycastHit hit;

        float x = (Mathf.Cos(finalAngle) * Random.Range(minPlayerSpawnRadius, maxPlayerSpawnRadius)) + player.transform.position.x;
        float y = (Mathf.Sin(finalAngle) * Random.Range(minPlayerSpawnRadius, maxPlayerSpawnRadius)) + player.transform.position.z;
        coords = new Vector2(x, y);

        Physics.Raycast(new Vector3(coords.x, 200, coords.x), gameController.gameObject.transform.up * -1, out hit);
        height = 200 - hit.distance;

        

        checkpointObject.name = id.ToString();

        //Passes in a copy of this class to add to the dictonary. Useful for managing these later.
        gameController.checkpointDictonary.Add(id.ToString(), this);
    }

}
