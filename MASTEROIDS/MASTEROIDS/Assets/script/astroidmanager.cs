using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class astroidmanager : MonoBehaviour
{

    public Animator camerashake;
    public Image[] Img_buttons;
    public Button[] buttons;

    public GameObject[] missilearray;
    public GameObject[] bombarray;

    public GameObject gameover_panel;
    public GameObject game_panel;
    public GameObject player;


    private float ammo_time;
    private float bomb_time;
    private float missile_time;
    private float laser_time;
    private float current_missile_time=0;
    private float current_bomb_time=0;
    private float current_laser_time=0;
    private float current_ammo_time=0;
    public static float score;


    [SerializeField]
    private float coins;


    public List<GameObject> astroid = new List<GameObject>();

    private int missilecount;

    private int index;

   
    private bool isammo=false;
    private bool ismissile=false;
    private bool isbomb=false;
    private bool islaser=false;

    public TextMeshProUGUI score_text;
    public TextMeshProUGUI coin_text;

    public wavemanager wavemanager;


    public void Start()
    {
        ammo_time = PlayerPrefs.GetFloat("ammotime", 5);
        bomb_time = PlayerPrefs.GetFloat("bombtime", 6);
        missile_time = PlayerPrefs.GetFloat("missiletime", 10);
        laser_time = PlayerPrefs.GetFloat("lasertime", 10);
        coins = PlayerPrefs.GetFloat("coins", 0);
        score = 0;
    }

    // Update is called once per frame
    void  Update()
    {

        score= Mathf.Round(score);
        score_text.text = score.ToString();
        coin_text.text = coins.ToString();
        if(ismissile==false)
        {
            buttons[2].interactable = true;
        }
        else
        {
            buttons[2].interactable = false;
            current_missile_time += Time.deltaTime;
            var per = current_missile_time / missile_time;
            Img_buttons[2].fillAmount = per;
            if (Img_buttons[2].fillAmount == 1)
            {
                ismissile = false;
                current_missile_time = 0;
            }
        }
        if (isbomb == false)
        {
            buttons[1].interactable = true;
        }
        else
        {
            buttons[1].interactable = false;
            current_bomb_time += Time.deltaTime;
            var per = current_bomb_time / bomb_time;
            Img_buttons[1].fillAmount = per;
            if (Img_buttons[1].fillAmount==1)
            {
                isbomb = false;
                current_bomb_time = 0;
            }
        }
        if (isammo == true)
        {
            buttons[0].interactable = false;
            current_ammo_time += Time.deltaTime;
            var per = current_ammo_time / ammo_time;
            Img_buttons[0].fillAmount = per;
            if (Img_buttons[0].fillAmount == 1)
            {
                isammo = true;
                current_ammo_time = 0;
                buttons[0].interactable = true;
            }
        }

    }


    public void coincollect()
    {
        coins++;
        PlayerPrefs.SetFloat("coins", coins);
        
    }


    public void missile()
    {
        missilecount = PlayerPrefs.GetInt("missilecount", 5);
        StartCoroutine(firemissile());
        if (ismissile == false)
        {
            ismissile = true;
        }
    }

    public IEnumerator firemissile()
    {
        
        WaitForSeconds wait = new WaitForSeconds(.01f);

        for (int i = 0; i < missilecount; i++)
        {
            GameObject go = Instantiate(missilearray[0], player.transform.position, player.transform.rotation);
        }
            yield return wait;
    }
        
        
    
    public void bomb()
    {
        GameObject go = Instantiate(bombarray[0], player.transform.position, Quaternion.Euler(0, 0, 90));
        if (isbomb == false)
        {
            isbomb = true;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="astroid")
        {
            astroid.Add(collision.gameObject);            
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "astroid")
        {
            astroid.Remove(collision.gameObject);
        }
    }
    public void gameover(GameObject gameObject)
    {
        if(astroid.Contains(gameObject))
        {
            gameover_panel.SetActive(true);
            game_panel.SetActive(false);
        }
    }

    public void astroid_destroy(GameObject gameObject)
    {
        astroid.Remove(gameObject);
    }

    public void cameraanim_on()
    {
        camerashake.SetBool("bullet",true);
    }
    public void cameraanim_off()
    {
        camerashake.SetBool("bullet", false);
    }


}
