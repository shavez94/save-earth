using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    [SerializeField]
    private float rotationforce = .1f;
    private float force = 10;

    public GameObject bombexplostion;
    private Rigidbody rb;
    private Transform target;
    public void Start()
    {
        closest_Astroid();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 10, ForceMode.Impulse);
    }
    private void Update()
    {
        if (target == null)
        {
            closest_Astroid();
        }
        rb.velocity = transform.forward * force;
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        Vector3 rotationamound = Vector3.Cross(transform.forward, direction);
        rb.angularVelocity = rotationamound * rotationforce;
        
    }

    public void closest_Astroid()
    {
        var distance = Mathf.Infinity;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("astroid");
        for (int i = 0; i < gos.Length; i++)
        {
            var diff = (gos[i].transform.position - transform.position).sqrMagnitude;
            if (diff < distance)
            {
                distance = diff;
                target = gos[i].transform;
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="astroid")
        {
            collision.gameObject.GetComponent<score>().bombhit();
            GameObject go = Instantiate(bombexplostion,transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
