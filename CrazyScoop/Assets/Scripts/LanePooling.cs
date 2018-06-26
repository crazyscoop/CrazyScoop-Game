using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LanePooling : MonoBehaviour {

	public static LanePooling current;

	[SerializeField]
	private GameObject pooledObject;

	[SerializeField]
	private GameObject obstacle;

	private float pooledAmount = 3;

	List<GameObject> pooledList;

	List<GameObject> obstacles;

	List<int> lanes;

	List<int> position;

	List<int> memory;

	List<int> storage;

	[SerializeField]
	List<Transform> laneList;

	void Awake()
	{
		current = this;
	}

	void Start () 
	{
		pooledList = new List<GameObject> ();

		//obstacles = new List<GameObject> ();

		lanes = new List<int> ();

		memory = new List<int> ();

		position = new List<int> ();

		storage = new List<int> ();

		for (int i = 0; i < 4; i++) 
		{
			lanes.Add (i);
			position.Add(i);
			memory.Add(i);
			storage.Add(i);
		}


		/*for (int i = 0; i < 3; i++)
		{
			GameObject obst = (GameObject)Instantiate(obstacle);
			obst.SetActive(false);
			obstacles.Add(obst);
		}*/



		for (int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledList.Add(obj);
		}
	}
	
	public GameObject Lanefunction()
	{
		for (int i = 0; i < 4; i++) 
		{
			memory.Add(i);
			//Debug.Log(lanes[i]);
		}

		for (int i = 0; i < pooledAmount; i++) 
		{
			if(!pooledList[i].activeInHierarchy)
			{
               
				int lanex = position[0]; //extracting first lane to be placed.
				lanes.Remove(storage.IndexOf(lanex)); // removing the position of lanex

				int laneloc = lanes[Random.Range(0,3)]; //index//position where lanex will be placed.
				Transform lanepos = pooledList[i].transform.GetChild(lanex).GetComponent<Transform>();
				Vector3 var = lanepos.position;
				var.x = laneList[laneloc].position.x;
				memory[laneloc] = lanex;
				lanepos.position = var;

				lanes.Add(position.IndexOf(lanex)); //inserting the lanex position as it is still empty.
				lanes.Remove(laneloc); //removing laneloc as it is filled---------1.

				int laneloc2 = lanes[Random.Range(0,3)];
				Transform lanepos2 = pooledList[i].transform.GetChild(storage[laneloc]).GetComponent<Transform>(); // laneloc is being places
				Vector3 var2 = lanepos2.position;
				var2.x = laneList[laneloc2].position.x; // laneloc is placed at laneloc2.
				memory[laneloc2] = storage[laneloc];
				lanepos2.position = var2;

				lanes.Remove(laneloc2); //laneloc2 is filled------------2.

				position.Remove (lanex); //lanex is already placed.
				position.Remove(storage[laneloc]); // laneloc is already placed.

				//count of both lanes and position must be 2.
				//Debug.Log (lanes.Count);
				//Debug.Log (position.Count);

	  			int truth = 0; // false = 0 

				int thirdlane = position[0];
				//Debug.Log (thirdlane);
				int fourthlane = position[1];
				///Debug.Log (fourthlane);

				for(int k =0; k < 2;k++)
				{
					//Debug.Log (lanes[k]);
				}

				for(int z = 0; z < 2 ; z++)
				{
					if(storage.IndexOf(thirdlane) == lanes[z])
					{
						int temp = lanes[z];
						lanes.Remove(lanes[z]);
						Transform lanepos3 = pooledList[i].transform.GetChild(thirdlane).GetComponent<Transform>(); // laneloc is being places
						Vector3 var3 = lanepos3.position;
						var3.x = laneList[lanes[0]].position.x; // laneloc is placed at laneloc2.
						memory[lanes[0]] = thirdlane;
						lanepos3.position = var3;

						lanes.Remove(lanes[0]);
						lanes.Add(temp);

						Transform lanepos4 = pooledList[i].transform.GetChild(fourthlane).GetComponent<Transform>(); // laneloc is being places
						Vector3 var4 = lanepos4.position;
						var4.x = laneList[lanes[0]].position.x; // laneloc is placed at laneloc2.
						memory[lanes[0]] = fourthlane;
						lanepos4.position = var4;
						lanes.Remove(temp);
						truth = 1;
						//Debug.Log("yo");
						break;
					}
				
					if(storage.IndexOf(fourthlane) == lanes[z])
					{
						int temp = lanes[z];
						lanes.Remove(lanes[z]);
						Transform lanepos3 = pooledList[i].transform.GetChild(fourthlane).GetComponent<Transform>(); // laneloc is being places
						Vector3 var3 = lanepos3.position;
						var3.x = laneList[lanes[0]].position.x; // laneloc is placed at laneloc2.
						memory[lanes[0]] = fourthlane;
						lanepos3.position = var3;
						
						lanes.Remove(lanes[0]);
						lanes.Add(temp);
						
						Transform lanepos4 = pooledList[i].transform.GetChild(thirdlane).GetComponent<Transform>(); // laneloc is being places
						Vector3 var4 = lanepos4.position;
						var4.x = laneList[lanes[0]].position.x; // laneloc is placed at laneloc2.
						memory[lanes[0]] = thirdlane;
						lanepos4.position = var4;
						lanes.Remove(temp);
						truth = 1;
						//Debug.Log ("yoyo");
						break;
					}
				}

				if(truth == 0)
				{
					int laneloc3 = lanes[Random.Range(0,2)];
					Transform lanepos3 = pooledList[i].transform.GetChild(thirdlane).GetComponent<Transform>(); // laneloc is being places
					Vector3 var3 = lanepos3.position;
					var3.x = laneList[laneloc3].position.x; // laneloc is placed at laneloc2.
					memory[laneloc3] = thirdlane;
					lanepos3.position = var3;

					lanes.Remove(laneloc3);
					int laneloc4 = lanes[0]; 

					Transform lanepos4 = pooledList[i].transform.GetChild(fourthlane).GetComponent<Transform>(); // laneloc is being places
					Vector3 var4 = lanepos4.position;
					var4.x = laneList[laneloc4].position.x; // laneloc is placed at laneloc2.
					memory[laneloc4] = fourthlane;
					lanepos4.position = var4;
					//Debug.Log ("yes");
				}

				/*
				 *backup from here
				for(int j = 0;j < 4;j++)
				{
					int k = Random.Range(0,4-j);
					int m = 0;

					Transform lanepos = pooledList[i].transform.GetChild(j).GetComponent<Transform>();
					Vector3 var = lanepos.position;
					var.x = laneList[k].position.x;
					lanepos.position = var;
					laneList.RemoveAt(k);
			    }

				for (int l = 0; l < 4; l++)
				{
					laneList.Add(pooledObject.transform.GetChild(l).transform);
				}
				*till here
				*/

				lanes.Clear();
				position.Clear();
				storage.Clear();

				for(int v = 0;v < 4;v++)
				{
					//Debug.Log(memory[v]);
					position.Insert(v,memory[v]);
					lanes.Insert(v,memory[v]);
					storage.Insert(v,memory[v]);
				}

				memory.Clear();
				return pooledList[i];
			}

		}
		return null;
	}



/*public GameObject obstaclefunction()
	{
		for (int i = 0; i < 3; i++)
		{
			if(obstacles[i].activeInHierarchy == false)
			{
				return obstacles[i];
			}
		}
		return null;
	}*/
}
