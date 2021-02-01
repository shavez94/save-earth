using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class brickmanager : MonoBehaviour
{

    private score score;
    public GameObject[] blockprefab;

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            spawnrowsofblock();
        }
    }
    public int playwidth ;
    public float distanceBetweenBlock;
    public float distancebetweenrow;
    public int rowspaned;
    private List<GameObject> blockspaned = new List<GameObject>();

    public void spawnit()
    {

            spawnrowsofblock();
        
    }

    private void spawnrowsofblock()
    {
        foreach (var gameObject in blockspaned)
        {
            if(gameObject!=null)
            {
                gameObject.transform.position += Vector3.down * distanceBetweenBlock;
            }
        }

        for (int i = 0; i < playwidth; i++)
        {
            if (UnityEngine.Random.Range(0, 100) <= 60)
            {
                   var brick = Instantiate(blockprefab[UnityEngine.Random.Range(0, blockprefab.Length)], getpostion(i), Quaternion.identity);
                    brick.transform.parent = gameObject.transform;
                    blockspaned.Add(brick);
            }
        }
    }

    private Vector3 getpostion(int i)
    {
        Vector3 postion = transform.position;
        postion += Vector3.right * i * distanceBetweenBlock;
        return postion;
    }
}
