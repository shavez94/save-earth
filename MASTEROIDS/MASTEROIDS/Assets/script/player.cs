using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Experimental.GraphView;

public class player : MonoBehaviour
{
    private bool ismovemnet = false;

    public Transform spawnpoints;

    public GameObject[] bulletprefab;
    public GameObject[] playerprefab;
    public Camera Camera;

    public Rigidbody rb;
    [SerializeField]
    public static int numberofbullet;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float cameraspeed;
    private float angle;
    private float previousangle;
    private float shoottime = 0;
    private float pausetime = 0;
    private float pauseinterval=3;
    [SerializeField]
    private float shootinginterval;

   

    public void Start()
    {
        numberofbullet = PlayerPrefs.GetInt("bullet", 1);
    }
    public void Update()
    {

        shoottime += Time.deltaTime;
        if (shoottime > shootinginterval)
        {
            shoot();
            shoottime = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ismovemnet = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ismovemnet = false;
            previousangle = angle;
        }

    }


        // Update is called once per frame
    void FixedUpdate()
     {

        if (ismovemnet == true)
        {
            Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            float div = Mathf.Abs(angle - previousangle);
            Debug.Log(div);
            if (div > 70)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), .2f);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), 1f);
            }
            playerprefab[0].transform.Rotate(Vector3.forward * angle / 20);
            Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), cameraspeed);
        }
    }
    public void shoot()
    {
        if(ismovemnet)
        {
            for (int i = 0; i < numberofbullet; i++)
            {
                GameObject go = Instantiate(bulletprefab[0], spawnpoints.position, transform.rotation);
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
                go.GetComponent<Rigidbody>().velocity = transform.up * 10;
            }

        }


    }
}
