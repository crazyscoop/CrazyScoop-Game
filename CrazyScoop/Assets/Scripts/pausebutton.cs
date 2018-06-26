using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class pausebutton : MonoBehaviour
{

    public static pausebutton pbs;

    private Animator anim;

    public bool pauseb;

    public bool gameover;

    public bool restart;

    public int score;

    [SerializeField]
    private List<GameObject> Fruitplayers;

    private GameObject ThePlayer;

    [SerializeField]
    private Text scorekeeper;

    [SerializeField]
    private GameObject game;

    public Dictionary<int, string> scorentry;

    [SerializeField]
    private InputField playername;

    [SerializeField]
    private Text hs;

    [SerializeField]
    private AudioSource theme;

    [SerializeField]
    private float rateofsec;

    [SerializeField]
    private float finalvol;

    [SerializeField]
    private float rateofinc;

    [SerializeField]
    private AudioSource clicksource;

    [SerializeField]
    private GameObject helpimg;

    [SerializeField]
    private GameObject helpimg2;

    private bool helpneed;

    [SerializeField]
    private float speed;

   

   
    void Awake()
    {
        pbs = this;

        if (LevelManager.musy == true)
        {
            StartCoroutine(StartTheme());
        }
    }

    IEnumerator StartTheme()
    {
        while (theme.volume < finalvol)
        {
            theme.volume += rateofinc;
            yield return null;
        }
        theme.volume = finalvol;
    }


    IEnumerator MyRoutine()
    {
        while (theme.volume > 0)
        {
            theme.volume -= rateofsec;
            yield return null;
        }
        theme.Stop();
    }


    void Start()
    {
        pauseb = false;
        gameover = false;
        restart = false;
        helpneed = false;
        helpimg.SetActive(false);
        helpimg2.SetActive(false);
        anim = game.GetComponent<Animator>();
        anim.enabled = false;

        int[] board = new int[5];

        //    PlayerPrefs.DeleteAll();

        for (int i = 0; i < 5; i++)
        {
            //        PlayerPrefs.SetInt("highscore" + i, 0);
            //          PlayerPrefs.SetString("highname" + i.ToString(),"");
            //  Debug.Log(PlayerPrefs.GetInt("highscore"+i));
        }

        //        PlayerPrefs.DeleteAll();



    }


    void FixedUpdate()
    {/*
        if(goright ==  true && goleft == false && Player.current.inair == false)
        {
            normalno += 0.2f;
            normalno = Mathf.Clamp(normalno, -1, 1);
        }
        if(goleft == true && goright == false && Player.current.inair == false)
        {
            normalno -= 0.2f;
            normalno = Mathf.Clamp(normalno, -1, 1);
        }

        if(goright == false && goleft == false && Player.current.inair == false)
        {
            normalno = 0;
        }

        Debug.Log(normalno);
        

        if(goleft == true && goright == false && Player.current.inair == false)
        {
            ActivePlayer();
            ThePlayer.GetComponent<Rigidbody>().velocity = Vector3.right * 0.8f * Time.deltaTime * speed * normalno;
        }

        if (goleft == false && goright == true && Player.current.inair == false)
        {
            ActivePlayer();
            ThePlayer.GetComponent<Rigidbody>().velocity = Vector3.right * 0.8f * Time.deltaTime * speed * normalno;
        }
        */
        


       // Debug.Log(normalno);
        //Mathf.Clamp(normalno, 1, 2);
        

    }
    

    void Update()
    {

        /*
        if (Player.current.inair == false && goleft == true)
        {
            ActivePlayer();
            ThePlayer.GetComponent<Rigidbody>().velocity = Vector3.right * -0.8f * Time.deltaTime * speed * normalno;
        }
        if (Player.current.inair == false && goright == true)
        {
            ActivePlayer();
            ThePlayer.GetComponent<Rigidbody>().velocity = Vector3.right * 0.8f * Time.deltaTime * speed * normalno;
        }

        if (Player.current.inair == false && jumping == true)
        {
            ActivePlayer();
            ThePlayer.GetComponent<Rigidbody>().AddForce(Vector3.up * 30);
            jumping = false;
        }
        */
    }
    
    public void Pause()
    {
        if (LevelManager.clickbool)
        {
            clicksource.Play();
        }
        if (gameover == false)
        {
            // Debug.Log(pauseb);
            pauseb = !pauseb;
            if (pauseb == true)
            {
                Time.timeScale = 0;
            }
            else if (pauseb == false)
            {
                Time.timeScale = 1;
            }
        }


    }

    public void GameOver()
    {

        gameover = true;
        Invoke("Stop", 1.7f);
        StartCoroutine(MyRoutine());
    }

    private void Stop()
    {

        Time.timeScale = 0;
        anim.enabled = true;
        scorekeeper.text = score + "";
        playername.text = PlayerPrefs.GetString("lastname");
        hs.text = PlayerPrefs.GetInt("highscore0") + "";
        anim.Play("Drop");
    }

    public void Restart()
    {
        if (LevelManager.clickbool)
        {
            clicksource.Play();
        }

        for (int i = 0; i < 5; i++)
        {
            if (score > PlayerPrefs.GetInt("highscore" + i))
            {
                int scored2 = PlayerPrefs.GetInt("highscore" + (i));
                string named2 = PlayerPrefs.GetString("highname" + (i));
                for (int j = i + 1; j < 5; j++)
                {
                    int scored = scored2;
                    string named = named2;
                    scored2 = PlayerPrefs.GetInt("highscore" + (j));
                    named2 = PlayerPrefs.GetString("highname" + (j));
                    PlayerPrefs.SetInt("highscore" + j, scored);
                    PlayerPrefs.SetString("highname" + j, named);


                }
                PlayerPrefs.SetInt("highscore" + i, score);
                PlayerPrefs.SetString("highname" + i, playername.text);
                break;
            }

        }


        restart = !restart;
        if (restart == true)
        {

            //Debug.Log(playername.text);
            //anim.enabled = true;
            //anim.Play("Drop 0");
            //Invoke("Re", 2f);
            PlayerPrefs.SetString("lastname", playername.text);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        // PlayerPrefs.SetString("lastname", playername.text);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void Re()
    {

        PlayerPrefs.SetString("lastname", playername.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        if (LevelManager.clickbool)
        {
            clicksource.Play();
        }
        for (int i = 0; i < 5; i++)
        {
            if (score > PlayerPrefs.GetInt("highscore" + i))
            {
                int scored2 = PlayerPrefs.GetInt("highscore" + (i));
                string named2 = PlayerPrefs.GetString("highname" + (i));
                for (int j = i + 1; j < 5; j++)
                {
                    int scored = scored2;
                    string named = named2;
                    scored2 = PlayerPrefs.GetInt("highscore" + (j));
                    named2 = PlayerPrefs.GetString("highname" + (j));
                    PlayerPrefs.SetInt("highscore" + j, scored);
                    PlayerPrefs.SetString("highname" + j, named);


                }
                PlayerPrefs.SetInt("highscore" + i, score);
                PlayerPrefs.SetString("highname" + i, playername.text);
                break;
            }

        }
        PlayerPrefs.SetString("lastname", playername.text);
        SceneManager.LoadScene(1);
    }

    public void HelpNeed()
    {
        if (gameover == false)
        {
            if (LevelManager.clickbool)
            {
                clicksource.Play();
            }
            Time.timeScale = 0;
            helpimg.SetActive(true);
        }
    }

    public void HelpNeed2()
    {
        if (gameover == false)
        {
            if (LevelManager.clickbool)
            {
                clicksource.Play();
            }
            helpimg2.SetActive(true);
        }
    }





    public void HelpNoNeed()
    {
        if (LevelManager.clickbool)
        {
            clicksource.Play();
        }
        helpimg2.SetActive(false);
        helpimg.SetActive(false);
        if (gameover == false)
        {
            Time.timeScale = 1;
        }
        if (gameover == true)
        {
            Stop();
        }

    }



}
