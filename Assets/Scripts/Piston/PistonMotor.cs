using System.Collections;
using UnityEngine;

public class PistonMotor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform head;

    [Header("Movement")]
    [SerializeField] private float extendDistance = 2.0f;
    [SerializeField] private float extendSpeed = 8.0f;
    [SerializeField] private float retractSpeed = 3.0f;

    [Header("Timing")]
    [SerializeField] private float extendedWait = 0.5f;
    [SerializeField] private float retractedWait = 1.0f;

    private Vector3 retractedLocalPos;
    private Vector3 extendedLocalPos;

    private Vector3 lastWorldPos;

    public bool IsExtending { get; private set; }
    public Vector3 LaunchDirection => transform.up;
    public float CurrentSpeed { get; private set; }

    private void Awake()
    {
        retractedLocalPos = head.localPosition;
        extendedLocalPos = retractedLocalPos + Vector3.up * extendDistance;

        lastWorldPos = head.position;
    }

    private void Start()
    {
        StartCoroutine(Cycle());
    }

    private void FixedUpdate()
    {
        CurrentSpeed = Vector3.Distance(head.position, lastWorldPos) / Time.fixedDeltaTime;
        lastWorldPos = head.position;
    }

    private IEnumerator Cycle()
    {
        while (true)
        {
            yield return MoveTo(extendedLocalPos, extendSpeed, true);

            yield return new WaitForSeconds(extendedWait);

            yield return MoveTo(retractedLocalPos, retractSpeed, false);

            yield return new WaitForSeconds(retractedWait);
        }
    }

    private IEnumerator MoveTo(Vector3 target, float speed, bool extending)
    {
        IsExtending = extending;

        while (Vector3.Distance(head.localPosition, target) > 0.001f)
        {
            head.localPosition = Vector3.MoveTowards(head.localPosition, target, speed * Time.deltaTime);
            yield return null;
        }

        head.localPosition = target;
        IsExtending = false;
    }
}