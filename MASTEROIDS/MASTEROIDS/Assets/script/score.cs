using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class score : MonoBehaviour
{

    public GameObject[] bulleteffect;
    public GameObject[] missileeffects;
    public GameObject[] bombeffect;
    public GameObject[] powerups;
    public GameObject astroiddestory_effect;
    

    private int bulletpower;


    private float finalscore;
    private float maxspeed;

    private bool collided = false;

    astroidmanager astroidmanager;

    public void Start()
    {
        bulletpower = 50;
        finalscore = transform.localScale.magnitude / 20;
        finalscore = Mathf.Round(finalscore);
        maxspeed = PlayerPrefs.GetFloat("currentspeed");
        
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, maxspeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (collided == true)
        {

            var x = transform.localScale.x - bulletpower;
            var y = transform.localScale.y - bulletpower;
            var z = transform.localScale.z - bulletpower;
            var size = new Vector3(x, y, z);
            transform.localScale = Vector3.Lerp(transform.localScale, size, Time.deltaTime * 2);
            collided = false;
            astroidmanager.score += finalscore;
        }
        if (transform.localScale.x < 50 || transform.localScale.y < 50)
        {
            death();
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            FindObjectOfType<astroidmanager>().cameraanim_on();
            collided = true;
            for (int i = 0; i < bulleteffect.Length; i++)
            {
                Instantiate(bulleteffect[i], collision.gameObject.transform.position, Quaternion.identity);
            }

            if (transform.localScale.x < 0 || transform.localScale.y < 0 || transform.localScale.z<0)
            {
                Instantiate(astroiddestory_effect, gameObject.transform.position, Quaternion.identity);
                death();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "missile")
        {
            for (int i = 0; i < missileeffects.Length; i++)
            {
                Instantiate(missileeffects[i], collision.gameObject.transform.position, Quaternion.identity);
            }
            Instantiate(astroiddestory_effect, gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            death();

        }

        else if(collision.gameObject.tag=="earth")
        {
            FindObjectOfType<astroidmanager>().gameover(gameObject);
        }

    }

    public void death()
    {
        FindObjectOfType<wavemanager>().astroidkilled();
        var p = UnityEngine.Random.Range(0, 10);
        if(p<5)
        {
            for (int i = 0; i < p; i++)
            {
                GameObject go = Instantiate(powerups[0], gameObject.transform.position, Quaternion.identity);
            }

        }
        else if(p==6)
        {
            GameObject go = Instantiate(powerups[1], gameObject.transform.position, Quaternion.identity);
            go.transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, maxspeed * Time.deltaTime);
        }
        else if(p==7)
        {
            GameObject go = Instantiate(powerups[2], gameObject.transform.position, Quaternion.identity);
            go.transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, maxspeed * Time.deltaTime);
        }
        FindObjectOfType<astroidmanager>().astroid_destroy(gameObject);
        Destroy(gameObject);
    }
    public void bombhit()
    {
        for (int i = 0; i < bombeffect.Length; i++)
        {
            Instantiate(bombeffect[i], gameObject.transform.position, Quaternion.identity);
        }


        Instantiate(astroiddestory_effect, gameObject.transform.position, Quaternion.identity);
        death();
    }
}
