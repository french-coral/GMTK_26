using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reset : MonoBehaviour
{
    private Vector3 playerOriginePosition;
    private Quaternion playerOrigineRotation;
    public GameObject body;
    public GameObject player;
    private Rigidbody rbPlayer;
    public Timer timer;

    private List<GameObject> bodies;
    private GameObject lastBodie;

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

    private void Start()
    {
        rbObjects = new List<Rigidbody>();
        objectsOriginesPositions = new List<Vector3>();
        objectsOriginesRotations = new List<Quaternion>();
        bodies = new List<GameObject>();

        rbPlayer = player.GetComponent<Rigidbody>();
        playerOriginePosition = rbPlayer.position;
        playerOrigineRotation = rbPlayer.rotation;

        foreach (GameObject O in objects)
        {
            currentRb = O.GetComponent<Rigidbody>();
            rbObjects.Add(currentRb);
        }

        foreach (Rigidbody R in rbObjects)
        {
            currentObjectPosition = R.transform.position;
            currentObjectRotation = R.transform.rotation;
            objectsOriginesPositions.Add(currentObjectPosition);
            objectsOriginesRotations.Add(currentObjectRotation);
        }
    }

    public void ResetPlayer()
    {
        timer.StopCountDown();

        Vector3 position = rbPlayer.position;
        Quaternion rotation = rbPlayer.rotation;
        lastBodie = Instantiate(body, position, rotation);
        bodies.Add(lastBodie);

        rbPlayer.position = playerOriginePosition;
        rbPlayer.rotation = playerOrigineRotation;
        rbPlayer.linearVelocity = Vector3.zero;
        rbPlayer.angularVelocity = Vector3.zero;
        totalBodies++;
        onScreenBodies++;

        timer.time = timer.origineTime;
        timer.countDown = StartCoroutine(timer.CountDown());
    }

    public void ResetScene()
    {
        rbCount = 0;
        foreach (Rigidbody R in rbObjects)
        {
            R.position = objectsOriginesPositions[rbCount];
            R.rotation = objectsOriginesRotations[rbCount];
            rbCount++;
        }
    }

    public void ResetBodies()
    {
        ResetPlayer();
        ResetScene();
        totalBodies--;
        onScreenBodies = 0;

        foreach(GameObject G in bodies)
        {
            Destroy(G);
        }

        bodies.Clear();
    }
}