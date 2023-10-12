using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangToRim : MonoBehaviour
{
    [SerializeField] private bool dontMove;
    [SerializeField] private GameObject parentPlayer;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = parentPlayer.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dontMove)
        {
            //transform.SetParent(rim.transform);
        }
    }

    void OnTriggerEnter()
    {
        // pota ile temas halinde olduğunda diye özellikle belirt.        
        dontMove = true;
        playerController.Hang();
    }

    void OnTriggerExit()
    {
        // pota ile temas halinde olduğunda diye özellikle belirt.
        dontMove = false;
        playerController.StopHanging();
    }
}
