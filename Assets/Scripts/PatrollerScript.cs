using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerScript : MonoBehaviour
{
    public List<WaypointScript> Waypoints = new List<WaypointScript>();
    public float Speed = 1.0f;
    public int DestinationWaypoint = 1;
    public PlayerController pc;
    public bool IsDetected = false;
    public bool HasCaught = false;

    private Vector3 Destination;
    private bool Forwards = true;
    private float TimePassed = 0f;

    Vector3 defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
        defaultPosition = this.gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();

        if (IsDetected)
        {
            ChasePlayer();
        }

        else
        {
            StopAllCoroutines();
            StartCoroutine(MoveTo());
        }
    }

    void CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(pc.transform.position, this.transform.position);
        if (distance < 3)
        {
            IsDetected = true;
        }
        else
        {
            IsDetected = false;
        }
    }

    void ChasePlayer()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, pc.transform.position, .01f);

        if (HasCaught)
        {
            Debug.Log("You have been caught!");
            pc.ResetPlayer();
            ResetPatroller();
            HasCaught = false;
            IsDetected = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            HasCaught = true;

        }
    }

    void ResetPatroller()
    {
        this.gameObject.transform.position = defaultPosition;
    }


    IEnumerator MoveTo()
    {
        while ((transform.position - this.Destination).sqrMagnitude > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                this.Destination, this.Speed * Time.deltaTime);
            yield return null;
        }
        if ((transform.position - this.Destination).sqrMagnitude <= 0.1f)
        {
            if (this.Waypoints[DestinationWaypoint].isSentry)
            {
                while (this.TimePassed < this.Waypoints[DestinationWaypoint].PauseTime)
                {
                    this.TimePassed += Time.deltaTime;
                    yield return null;
                }

                this.TimePassed = 0;
            }

            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (this.Waypoints[DestinationWaypoint].IsEndpoint)
        {
            if (this.Forwards)
                this.Forwards = false;
            else
                this.Forwards = true;
        }

        if (this.Forwards)
            ++DestinationWaypoint;
        else
            --DestinationWaypoint;
        if (DestinationWaypoint >= this.Waypoints.Count)
            DestinationWaypoint = 0;

        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

}
