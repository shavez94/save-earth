using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 2 * Time.deltaTime);
       transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if(transform.position==Vector3.zero)
        {
            Debug.Log("coin collect");
            FindObjectOfType<astroidmanager>().coincollect();
            Destroy(gameObject);
        }
    }

}
