using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager lm;

    private Animator animGui;

    private Animator animOpt;

    [SerializeField]
    private GameObject options;

    [SerializeField]
    private GameObject scorecard;

    [SerializeField]
    private GameObject credits;

    private Animator animCre;

    private Text[] allText;

    private bool displayscore;

    private bool displayoption;

    private bool displaycredits;

 // private bool musicbutton;

    [SerializeField]
    private AudioSource startmus;

    [SerializeField]
    private float rateofinc;

    [SerializeField]
    private float rateofdec;

    [SerializeField]
    private float maxvol;

    [SerializeField]
    private AudioSource clicksound;

    [SerializeField]
    private Toggle musicy;

    [SerializeField]
    private Toggle soundy;

    public static bool clickbool;

    public static bool musy ;

    public static int soundcount;

    void Awake()
    {
        soundcount += 1;
        lm = this;
       // Debug.Log(soundcount);

        if(soundcount != 1)
        {
            if (PlayerPrefs.GetInt("Musy") == 0)
            {
                musicy.isOn = true;
            }
            else if (PlayerPrefs.GetInt("Musy") == 1)
            {
                musicy.isOn = false;
            }

            if (PlayerPrefs.GetInt("Clickbool") == 0)
            {
                soundy.isOn = true;
            }
            else if (PlayerPrefs.GetInt("Clickbool") == 1)
            {
                soundy.isOn = false;
            }

        }
       
    }

    IEnumerator Startmusic()
    {
        while(startmus.volume < maxvol)
        {
            startmus.volume += rateofinc;
            yield return null;
        }

        startmus.volume = maxvol;
    }

    IEnumerator EndMusic()
    {
        while(startmus.volume > 0.008)
        {
            startmus.volume -= rateofdec;
            yield return null;
        }
        startmus.volume = 0;
       

    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        animGui = scorecard.GetComponent<Animator>();
        animOpt = options.GetComponent<Animator>();
        animCre = credits.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        animGui.enabled = false;
        animOpt.enabled = false;
        animCre.enabled = false;
        credits.SetActive(false);
        displayscore = false;
        displayoption = false;
        displaycredits = false;
       // musicbutton = false;
       // clickbool = true;
        credits.SetActive(false);

       

    
        for (int i = 0; i < 5; i++)
        {
           // Debug.Log(PlayerPrefs.GetString("highname" + i));
            scorecard.transform.GetChild(i).GetComponent<Text>().text = PlayerPrefs.GetString("highname" + i);
        }
        for (int i = 5; i < 10; i++)
        {
            //Debug.Log(PlayerPrefs.GetInt("highscore" + (i - 5)));
            scorecard.transform.GetChild(i).GetComponent<Text>().text = PlayerPrefs.GetInt("highscore" + (i - 5)).ToString();
        }

        /*
        for(int i = 0;i<10;i++)
        {
              allText[i] = scorecard.transform.GetChild(i).GetComponent<Text>();
        }
        */

    }

    void Update()
    {
        
        
            if (musicy.isOn)
            {
                StartCoroutine(EndMusic());
                PlayerPrefs.SetInt("Musy", 0);
                musy = false;
            }
            else if (!musicy.isOn)
            {
                StartCoroutine(Startmusic());
                PlayerPrefs.SetInt("Musy", 1);
                musy = true;
            }

            if (soundy.isOn)
            {
                PlayerPrefs.SetInt("Clickbool", 0);
                clickbool = false;

            }
            else if (!soundy.isOn)
            {
                PlayerPrefs.SetInt("Clickbool", 1);
                clickbool = true;
            }
        
      


        
    }


   

    public void DisplayScore()
    {
        if (clickbool)
        {
            clicksound.Play();
        }
        displayscore = !displayscore;
        animGui.enabled = true;
        
        if(displayscore == true)
        {

            //animGui.Play("ScoreBoard");
            
            for (int i = 0;i < 5;i++)
            {
              //  Debug.Log(PlayerPrefs.GetString("highname" + i));
                scorecard.transform.GetChild(i).GetComponent<Text>().text = PlayerPrefs.GetString("highname" + i);
            }

            for (int i = 5; i < 10; i++)
            {
                //Debug.Log(PlayerPrefs.GetInt("highscore" + (i-5)));
                scorecard.transform.GetChild(i).GetComponent<Text>().text = PlayerPrefs.GetInt("highscore" + (i-5)).ToString();
            }
            animGui.Play("ScoreBoard");


            //animGui.Play("ScoreBoard");
        }
        else if(displayscore == false)
        {
            animGui.Play("ScoreBoard 0");
        }

    }
  
    /*  
    public void MusicStop()
    {
        musicbutton = !musicbutton;
        if(musicbutton == true)
        {        
            StartCoroutine(EndMusic());
           
            ColorBlock cb = musicz.colors;
            cb.normalColor = new Color32(23, 23, 23,120);
            musicz.colors = cb;
            musicz.interactable = false;
            musicz.interactable = true;
            
        }
        if(musicbutton == false)
        {
            ColorBlock cb = musicz.colors;
            cb.normalColor = new Color32(255, 255, 255, 0);
            musicz.colors = cb;
            musicz.interactable = false;
            musicz.interactable = true;
            StartCoroutine(Startmusic());
        }
    }
    */

   
    public void OptionBox()
    {
        if (clickbool)
        {
            clicksound.Play();
        }
        displayoption = !displayoption;
        animOpt.enabled = true;
        if(displayoption == true)
        {
            animOpt.Play("OptionBox");
        }
        if(displayoption == false)
        {
            animOpt.Play("OptionBox 0");
        }


    }

     public void Credit()
     {
        if (clickbool)
        {
            clicksound.Play();
        }
        displaycredits = !displaycredits;
        animCre.enabled = true;
        if (displaycredits == true)
        {
            Screen.orientation = ScreenOrientation.Landscape;
            credits.SetActive(true);
            animCre.Play("creditz");
        }

        if(displaycredits == false)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            credits.SetActive(false);
            animCre.Stop();
        }
    }

    public void QuitGame()
    {
       
        startmus.Stop();
        if(clickbool)
        {
            clicksound.Play();
        }
        Application.Quit();
    }

    public void Facebook()
    {
        if (clickbool)
        {
            clicksound.Play();

        }
        Application.OpenURL("https://www.facebook.com/Dot0Code/");
    }

    public void Twiter()
    {
        if (clickbool)
        {
            clicksound.Play();
        }
        Application.OpenURL("https://twitter.com/himthegladiator");
    }

    public void Gogle()
    {
        if (clickbool)
        {
            clicksound.Play();
        }
        Application.OpenURL("https://plus.google.com/116951687223713558013");
    }


    public void LetsStart()
    {

        if (clickbool)
        {
            clicksound.Play();
        }
        StartCoroutine(EndMusic());
        SceneManager.LoadScene(2);
    }


}
