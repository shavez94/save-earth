using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombexplostion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void destoyit()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "astroid")
        {
            Destroy(collision.gameObject);
        }

    }
}
