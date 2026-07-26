using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reset : MonoBehaviour
{

    // Player
    private Vector3 playerOriginePosition;
    private Quaternion playerOrigineRotation;
    public GameObject body;
    public GameObject player;
    private Rigidbody rbPlayer;

    // Timer
    public Timer timer;

    // Body tracking
    private List<GameObject> bodies;
    private GameObject lastBodie;

    // Object tracking
    public List<GameObject> objects;
    private List<Rigidbody> rbObjects;

    private List<Vector3> objectsOriginesPositions;
    private List<Quaternion> objectsOriginesRotations;

    private int rbCount;
    
    private Rigidbody currentRb;
    private Vector3 currentObjectPosition;
    private Quaternion currentObjectRotation;

    [HideInInspector] public int totalBodies = 0;
    [HideInInspector] public int onScreenBodies = 0;

    public List<GameObject> conveyors;

    private void Start()
    {
        // Init tracking list
        rbObjects = new List<Rigidbody>();
        objectsOriginesPositions = new List<Vector3>();
        objectsOriginesRotations = new List<Quaternion>();
        bodies = new List<GameObject>();

        // Get Player infos
        rbPlayer = player.GetComponent<Rigidbody>();
        playerOriginePosition = rbPlayer.position;
        playerOrigineRotation = rbPlayer.rotation;

        foreach (GameObject Object in objects)
        {
            currentRb = Object.GetComponent<Rigidbody>();
            rbObjects.Add(currentRb);
        }

        foreach (Rigidbody RB in rbObjects)
        {
            currentObjectPosition = RB.transform.position;
            currentObjectRotation = RB.transform.rotation;
            objectsOriginesPositions.Add(currentObjectPosition);
            objectsOriginesRotations.Add(currentObjectRotation);
        }
    }

    public void ResetPlayer()
    {
        // Spawn Body in place
        Vector3 position = rbPlayer.position;
        Quaternion rotation = rbPlayer.rotation;
        lastBodie = Instantiate(body, position, rotation);
        bodies.Add(lastBodie);

        // Re-teleport player at spawn point
        rbPlayer.position = playerOriginePosition;
        rbPlayer.rotation = playerOrigineRotation;
        rbPlayer.linearVelocity = Vector3.zero;
        rbPlayer.angularVelocity = Vector3.zero;

        // Account for new body
        totalBodies++;
        onScreenBodies++;

        timer.Restart();
    }

    public void ResetScene()
    {
        // Clear all rb
        rbCount = 0;
        foreach (Rigidbody RB in rbObjects)
        {
            RB.position = objectsOriginesPositions[rbCount];
            RB.rotation = objectsOriginesRotations[rbCount];
            rbCount++;
        }
    }

    public void ResetBodies()
    {
        ResetPlayer();
        ResetScene();
        totalBodies--;
        onScreenBodies = 0;

        // Clear conveyor count
        foreach (GameObject B in conveyors)
        {
            B.GetComponent<ConveyourBelt>().onBelt.Clear();
        }

        // Delete all bodies
        foreach (GameObject G in bodies)
        {
            Destroy(G);
        }

        bodies.Clear();
    }
}