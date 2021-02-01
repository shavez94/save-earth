using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody2D playerb;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerb = player.GetComponent<Rigidbody2D>();
        rb.GetComponent<Rigidbody>();
       
       // rb.AddForce(Vector3  .up * 30);
      //  transform.rotation = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
