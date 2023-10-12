using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;
    [SerializeField] private int zOffset;
    [SerializeField] private bool followOnX;
    [SerializeField] private bool followOnY;
    [SerializeField] private bool followOnZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z + zOffset);
        
        if (followOnX)
        {
            transform.position = new Vector3(player.transform.position.x + xOffset, transform.position.y, transform.position.z);
        }

        if (followOnY)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + yOffset, transform.position.z);
        }

        if (followOnZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + zOffset);
        }
    }


}
