using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDesign : MonoBehaviour {

    [SerializeField]
    private List<Transform> location;

    [SerializeField]
    private List<GameObject> loc;


    void Start()
    {

    }

    void OnEnable()
    {
        for (int i = 0; i < 2; i++)
        {
            int randint = Random.Range(0 + 3 * i, 3 + 4 * i);
            GameObject obj = (GameObject)Instantiate(loc[randint]);
            obj.SetActive(true);
            Vector3 ls = obj.transform.localScale;
            obj.transform.parent = gameObject.transform.GetChild(6);
            obj.transform.position = location[i].position;
            obj.transform.localScale = ls;
        }

    }

    void OnDisable()
    {
        for(int i = 0;i < 2;i++)
        {
            Destroy(gameObject.transform.GetChild(6).transform.GetChild(i).gameObject);
        }
    }


    void Update ()
    {
	
	}
}
