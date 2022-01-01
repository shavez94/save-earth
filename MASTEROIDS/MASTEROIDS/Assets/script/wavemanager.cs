using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class wavemanager : MonoBehaviour
{
    public enum spawnrate { SPAWNING,WAITING,COUNTING};
    [System.Serializable]
    public class wave
    {
        public string name;
        public GameObject[] astroid;
        public int count;
        public float rate;
    }



    public wave[] waves;

    private int nextwave = 0;
    private int randomnumber_X;
    private int randomnumber_Y;

    public float timebetweenwaves = 5f;
    private float totalastroid;
    private float astroid_killed;
    private float wavecountdown = 0;
    private float seachCountdown=1f;

    private string wavename;

    public GameObject wave_panel;
    public GameObject[] game;
    public GameObject levelcomplete;
    public GameObject indicator;
    public TextMeshProUGUI wavepanel_text;
    public TextMeshProUGUI wavename_toptext;

    [SerializeField]
    private float maxastroid_size;
    [SerializeField]
    private float minastroid_size;
    [SerializeField]
    private float maxastroid_speed;
    [SerializeField]
    private float minastroid_speed;
    public float movementSpeed;

    public spawnrate state = spawnrate.COUNTING;


    public Slider progressslider;
    private void Start()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            totalastroid = waves[i].count + totalastroid;
        }
        progressslider.maxValue = totalastroid;
        wavecountdown = timebetweenwaves;
    }

    private void Update()
    {
        progressslider.value = astroid_killed;
        if(state==spawnrate.WAITING)
        {
 
                if(!EnemyIsAlive())
                {
                    WaveCompleted();
                    return;
                }
                else
                {
                    return;
                }
            
        }
        if(wavecountdown<=0)
        {
            wave_panel.SetActive(false);
            for (int i = 0; i < game.Length; i++)
            {
                game[i].SetActive(true);
            }
            var plus = nextwave + 1;
            wavename_toptext.text= "Wave " + plus;
            if (state!=spawnrate.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextwave]));
            }
        }
        else
        {
            wavecountdown -= Time.deltaTime;
            wave_panel.SetActive(true);
            for (int i = 0; i < game.Length; i++)
            {
                game[i].SetActive(false);
            }
            var plus = nextwave + 1;
            wavepanel_text.text = "Wave "+  plus;
        }
    }
    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = spawnrate.COUNTING;
        wavecountdown = timebetweenwaves;

        if(nextwave+1>waves.Length-1)
        {
            levelcomplete.SetActive(true);
            for (int i = 0; i < game.Length; i++)
            {
                game[i].SetActive(false);
            }
        }
        else
        {
            nextwave++;
        }

    }

    bool EnemyIsAlive()
    {
        seachCountdown -= Time.deltaTime;
        if(seachCountdown<=0f)
        {
            seachCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("astroid") == null)
            {
                return false;
            }
        }
        return true;

    }
    IEnumerator SpawnWave(wave _wave)
    {
        Debug.Log("spawning wave: " + _wave.name);
        wavename = _wave.name;
        state = spawnrate.SPAWNING;

        for(int i=0;i<_wave.count;i++)
        {
            SpawnEnemy(_wave.astroid[Random.Range(0,_wave.astroid.Length)]);
            yield return new WaitForSeconds(_wave.rate);
        }

        state = spawnrate.WAITING;

        yield break;
    }

    public void astroidkilled()
    {
        astroid_killed++;
    }
    void SpawnEnemy(GameObject _enmy)
    {
        X_value();
        Y_value();
        Vector3 point = new Vector3(randomnumber_X, randomnumber_Y, 0);
        GameObject go = Instantiate(_enmy, point, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10), 0);
        go.transform.localScale = new Vector3(UnityEngine.Random.Range(minastroid_size, maxastroid_size), UnityEngine.Random.Range(minastroid_size, maxastroid_size), UnityEngine.Random.Range(minastroid_size, maxastroid_size));
        PlayerPrefs.SetFloat("currentspeed", UnityEngine.Random.Range(minastroid_speed, maxastroid_speed));
        Debug.Log("spawning enemy: " + _enmy.name);
        GameObject ind = Instantiate(indicator, Vector3.zero, Quaternion.identity);
       // Vector3 dis = _enmy.transform.position - ind.transform.position;
        ind.transform.position += transform.forward * Time.deltaTime * movementSpeed;
        ind.transform.LookAt(_enmy.transform);

    }

    private void Y_value()
    {
        int n = UnityEngine.Random.Range(0, 100);
        if (n > 50)
        {
            randomnumber_Y = UnityEngine.Random.Range(-11, -15);

        }
        else
        {
            randomnumber_Y = UnityEngine.Random.Range(12, 15);
        }

    }
    private void X_value()
    {
        int n = UnityEngine.Random.Range(0, 100);
        if (n > 50)
        {
            randomnumber_X = UnityEngine.Random.Range(-10, -8);

        }
        else
        {
            randomnumber_X = UnityEngine.Random.Range(9, 12);
        }

    }

}
