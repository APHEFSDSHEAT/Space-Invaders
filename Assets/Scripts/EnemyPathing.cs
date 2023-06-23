using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfiguration;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfiguration.GetWaypoints();
        //  at the start, go to the starting waypoint position
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig wave)
    {
        waveConfiguration = wave;
    }

    private void Move()
    {
        // if it is not at the last waypoint position 
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfiguration.GetMoveSpeed() * Time.deltaTime;
            // move towards the next waypoint
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
                // if we arrive at the next waypoint
            if(transform.position == targetPosition)
            {
                // increment the waypoint index (++)
                waypointIndex++;
            }

        }


        // if we are at the last waypoint position
        else
        {
            FindObjectOfType<EnemySpawner>().SubtractEnemy();
            // destroy this enemy 
            Destroy(gameObject);
        }

    }
}
