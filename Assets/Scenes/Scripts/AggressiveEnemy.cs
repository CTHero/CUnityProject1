using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : MonoBehaviour, Enemy
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float waypointTolerance = 1f;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float noticeDistance = 10f;
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private float stunnedDelay = 0.25f;

    private float oldX = 0;
    private SpriteRenderer sprite;
    private Rigidbody2D myRigidbody;
    private GameObject thePlayer = null;

    private float stunned = 0.0f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (stunned <= 0)
        {
            //If we do not yet have a reference to the Player object, get that object and set thePlayer object to that variable.
            if (thePlayer == null)
            {
                thePlayer = GameObject.FindGameObjectWithTag("Player");
            }

            //IF the player is within the notice range, chase the player.  If the player is hiding, don't chase.
            if (distanceTo(thePlayer) < noticeDistance && !DataManager.me.checkHiding())
            {

                if (transform.position.x < thePlayer.transform.position.x)
                {
                    myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);

                }
                else
                {
                    myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
                }

            }
            else
            {
                //If the aggressor gets closer to the waypoint than the waypointTolerance then switch to the next waypoint
                if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < waypointTolerance)
                {
                    currentWaypointIndex++;
                    if (currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;
                    }
                }


                //If the aggressor is to the left of the waypoint, move right.  If it is to the right, move left
                if (transform.position.x < waypoints[currentWaypointIndex].transform.position.x)
                {
                    myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);

                }
                else
                {
                    myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
                }

            }
        }
        stunned = stunned - Time.deltaTime;

        //Face the aggressor in the direction that it is moving
        if (oldX > transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        oldX = transform.position.x;
    }

    private float distanceTo(GameObject theObject)
    {
        float xDiff = theObject.transform.position.x - transform.position.x;
        float yDiff = theObject.transform.position.y - transform.position.y;
        float distance = Mathf.Sqrt( (xDiff * xDiff ) + (yDiff * yDiff) );

        return distance;
    }

    public int changeHitPoints(int changeAmount)
    {
        hitPoints = hitPoints + changeAmount;

        stunned = stunnedDelay;

        return hitPoints;
    }
}
