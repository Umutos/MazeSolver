using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float[] results;
    public float maxDistance = 30f;

    public float fitness = 0f;
    public float distLeft = 0f;
    public float distRight = 0f;
    public float distForward = 0f;
    public float distDiagLeft = 0f;
    public float distDiagRight = 0f;
    public float currentVelocity = 0.0f;
    public bool active = true;

    private Vector3 lastPosition;
    private Vector3 movDir = Vector3.zero;
    private float speed = 1.0f;
    private float rotSpeed = 500f;
    private float finalVelocity = 2.0f;
    private float initialVelocity = 0.0f;
    private float distanceTraveled = 0f;
    private float accelerationRate = 1.0f;

    private Transform transform2;


    void Update()
    {
        if (active)
        {
            CharacterController controller = gameObject.GetComponent<CharacterController>();
            if (results.Length != 0)
            {
                currentVelocity += (accelerationRate * Time.deltaTime) * results[0];
                currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, finalVelocity);

                movDir = new Vector3(0, 0, currentVelocity);
                movDir *= speed;
                movDir = transform.TransformDirection(movDir);

                controller.Move(movDir);
                transform.Rotate(0, results[1] * rotSpeed * Time.deltaTime, 0);
            }
            InteractRaycast();

            distanceTraveled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            fitness += distanceTraveled / 1000;
            fitness -= 0.05f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            active = false;

        }

        if (other.gameObject.tag == "Point")
        {
            fitness += 2f;      
        }
    }

    void InteractRaycast()
    {

        transform2 = GetComponent<Transform>();
        Vector3 playerPosition = transform.position;

        Vector3 forwardDirection = transform2.forward;
        Vector3 leftDirection = -transform2.right;
        Vector3 rightDirection = transform2.right;
        Vector3 diagLeft = transform2.TransformDirection(new Vector3(maxDistance / 5f, 0f, maxDistance / 5f));
        Vector3 diagRight = transform2.TransformDirection(new Vector3(-maxDistance / 5f, 0f, maxDistance / 5f));

        Ray myRay = new Ray(playerPosition, forwardDirection);
        Ray leftRay = new Ray(playerPosition, leftDirection);
        Ray rightRay = new Ray(playerPosition, rightDirection);
        Ray diagLeftRay = new Ray(playerPosition, diagLeft);
        Ray diagRightRay = new Ray(playerPosition, diagRight);

        RaycastHit hit;
        if (Physics.Raycast(myRay, out hit, maxDistance))
        {
            if (hit.transform.tag == "Wall")
            {
                 distForward = hit.distance;
            }
        }
        if (Physics.Raycast(leftRay, out hit, maxDistance))
        {
            if (hit.transform.tag == "Wall")
            {
                distLeft = hit.distance;
            }
        }
        if (Physics.Raycast(rightRay, out hit, maxDistance))
        {
            if (hit.transform.tag == "Wall")
            {
                distRight = hit.distance;
            }
        }
        if (Physics.Raycast(diagLeftRay, out hit, maxDistance))
        {
            if (hit.transform.tag == "Wall")
            {
                distDiagLeft = hit.distance;
            }
        }
        if (Physics.Raycast(diagRightRay, out hit, maxDistance))
        {
            if (hit.transform.tag == "Wall")
            {
                distDiagRight = hit.distance;
            }
        }
    }
}