using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UIElements;   
using UnityEngine.VFX;
using static UnityEngine.UI.Image;

public class Spawner : MonoBehaviour
{
    private GameObject newCube;
    private SpringJoint springJoint;
    private GameObject spawnerCube;
    private Rigidbody rb;
    private MeshFilter meshFilter;
    private static int numberOfCubes = 40;
    private GameObject[][] myCubs = new GameObject[numberOfCubes + 1][];
    private GameObject[] myCubeColumn = new GameObject[numberOfCubes + 1];
    private GameObject[] myPreCubeColumn = new GameObject[numberOfCubes + 1];
    [SerializeField] private int SpringPower = 900;
    [SerializeField] private int DistanceBetweenCubes = 2;
    [SerializeField] private Material trampolineMaterial;
    private bool isCubeOnSide;

    private void Awake()
    {
        spawnerCube = gameObject;

        // Creating Cubes
        for (int i = 0; i <= numberOfCubes; i++)
        {
            for (int z = 0; z <= numberOfCubes; z++)
            {
                if (z == 0 || z == numberOfCubes || i == 0 || i == numberOfCubes)
                {
                    isCubeOnSide = true;
                }
                else
                {
                    isCubeOnSide = false;
                }

                newCube = CreateCube(isCubeOnSide);
                newCube.transform.position = new Vector3(DistanceBetweenCubes * z, 0, DistanceBetweenCubes * i);

                myCubeColumn[z] = newCube;
                if (z != 0)
                {
                    ConnectWithSpring(myCubeColumn[z], myCubeColumn[z - 1]);
                }

                if (i != 0)
                {
                    ConnectWithSpring(myCubeColumn[z], myPreCubeColumn[z]);
                }
            }

            // initializing my previous CubeColumn
            for (int t = 0; t <= numberOfCubes; t++)
            {
                myPreCubeColumn[t] = myCubeColumn[t];
            }

            myCubs[i] = myCubeColumn;
        }
    }

    void Start()
    {
       
    }

    private void ConnectWithSpring(GameObject firstCube, GameObject secondCube)
    {
        springJoint = firstCube.AddComponent<SpringJoint>();
        springJoint.connectedBody = secondCube.GetComponent<Rigidbody>();
        springJoint.spring = SpringPower;
    }

    private GameObject CreateCube(bool freezePoisitionY)
    {
        newCube = new GameObject
        {
            name = "Cube ",// + sidewardCubeCounter + "-" + forwardCubeCounter,
        };

        rb = newCube.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        if (freezePoisitionY)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        rb.useGravity = false;
        meshFilter = newCube.AddComponent<MeshFilter>();
        meshFilter.mesh = spawnerCube.GetComponent<MeshFilter>().mesh; // Kodun Bulunduğu obje Cube
        meshFilter.mesh = spawnerCube.GetComponent<MeshFilter>().mesh; // Kodun Bulunduğu obje Cube
        // material = newCube.AddComponent<Material>();
        newCube.AddComponent<BoxCollider>();
        newCube.AddComponent<MeshRenderer>();
        newCube.GetComponent<MeshRenderer>().material = trampolineMaterial;

        newCube.transform.parent = spawnerCube.transform;

        return newCube;
    }
}