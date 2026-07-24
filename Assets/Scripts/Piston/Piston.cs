using System.Collections;
using UnityEngine;

public class Piston : MonoBehaviour
{

    [Header("Part Reference")]
    [SerializeField] private Rigidbody pistonHead;

    [Header("Cycle")]
    [SerializeField] private bool isCycle;

    [Header("Piston Movements")]
    [SerializeField] private float extendSpeed = 40.0f;
    [SerializeField] private float retractSpeed = 5.0f;
    [SerializeField] private float extendedWaitTime = 0.5f;
    [SerializeField] private float retractedWaitTime = 1.0f;
    [SerializeField] private float extendOffset = 2.0f;

    [SerializeField] private float minLaunchSpeed = 12.0f;


    private Vector3 extendedPos;
    private Vector3 retractedPos;
    private Vector3 targetPos;
    private float currentSpeed;
    private Vector3 lastPosition;

    public Vector3 Velocity { get; private set; }
    public Vector3 LaunchDirection => (extendedPos - retractedPos).normalized;
    public bool IsExtending { get; private set; }
    public float MinLaunchSpeed => minLaunchSpeed;
    public float LaunchForce => extendSpeed;

    void Start()
    {
        retractedPos = pistonHead.position;
        Vector3 endPoint = pistonHead.transform.position + pistonHead.transform.up * extendOffset;
        extendedPos = endPoint;
        targetPos = retractedPos;

        lastPosition = pistonHead.position;

        StartCoroutine(PistonCycle());
    }

    void FixedUpdate()
    {
        Vector3 targetVec = Vector3.MoveTowards(pistonHead.position, targetPos, Time.fixedDeltaTime * currentSpeed);
        pistonHead.MovePosition(targetVec);

        Velocity = (targetVec - lastPosition) / Time.fixedDeltaTime;
        lastPosition = targetVec;
    }

    private IEnumerator PistonCycle()
    {
        while (true)
        {
            targetPos = extendedPos;
            currentSpeed = extendSpeed;
            yield return new WaitUntil(() => Vector3.Distance(pistonHead.transform.position, extendedPos) < 0.1f);
            IsExtending = true;
            yield return new WaitForFixedUpdate();
            IsExtending = false;
            yield return new WaitForSeconds(extendedWaitTime);

            targetPos = retractedPos;
            currentSpeed = retractSpeed;
            yield return new WaitUntil(() => Vector3.Distance(pistonHead.transform.position, retractedPos) < 0.1f);
            yield return new WaitForSeconds(retractedWaitTime);
        }
    }

}
