/*
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(combineMesh))]
public class combineMeshEditior : Editor {

	void OnSceneGUI()
    {
        combineMesh em = target as combineMesh;
        if(Handles.Button(em.transform.position + Vector3.up*5,Quaternion.identity,1,1,Handles.CubeCap))
        {
            em.AdvancedMerge();


        }


    }
}
*/