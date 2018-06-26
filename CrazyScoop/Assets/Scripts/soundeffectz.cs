using UnityEngine;
using System.Collections;

public class soundeffectz : MonoBehaviour {

    [SerializeField]
    private AudioSource theme;

    [SerializeField]
    private float rateofsec;

    void Awake()
    {
        StartCoroutine(MyRoutine());
    }
	
    IEnumerator MyRoutine()
    {
        while(theme.volume > 0)
        {
            theme.volume -= rateofsec;
         //   Debug.Log(theme.volume);
            yield return null;
        }

        yield return new WaitForEndOfFrame();
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
