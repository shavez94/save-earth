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

    public GameObject gamepause;

    public Rigidbody rb;
    [SerializeField]
    public static int numberofbullet;

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
            resume();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ismovemnet = false;        
            
        }
        if(ismovemnet==false)
        {
            pausetime += Time.deltaTime;
            if (pausetime > pauseinterval)
            {
                pause();
                pausetime = 0;
            }
        }
    }

    public void pause()
    {
        Time.timeScale = 0;
        gamepause.SetActive(true);
    }
    public void resume()
    {
        Time.timeScale = 1;
        gamepause.SetActive(false);
    }
        // Update is called once per frame
    void FixedUpdate()
     {
        if (ismovemnet == true)
        {
            Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.AngleAxis(angle - 90, Vector3.forward),.3f);

        }
    }
    public void shoot()
    {
        if (ismovemnet == true)
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
