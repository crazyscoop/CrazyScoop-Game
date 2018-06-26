using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class combineMesh : MonoBehaviour
{

    private MeshFilter myMeshFilter;

   

    void Start()
    {

    }



    public void AdvancedMerge()
    {


        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        MeshFilter myMeshFilter = GetComponent<MeshFilter>();
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(false);

        List<Material> materials = new List<Material>();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(false); // <-- you can optimize this
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.transform == transform)
                continue;
            Material[] localMats = renderer.sharedMaterials;
            foreach (Material localMat in localMats)
                if (!materials.Contains(localMat))
                    materials.Add(localMat);
        }

        // Each material will have a mesh for it.
        List<Mesh> submeshes = new List<Mesh>();
        MeshFilter[] him = GetComponentsInChildren<MeshFilter>();
        Vector3[] oldnorm = him[1].sharedMesh.normals;
        Vector2[] olduv = him[1].sharedMesh.uv;
        
        foreach (Material material in materials)
        {
                     
            // Make a combiner for each (sub)mesh that is mapped to the right material.
            List<CombineInstance> combiners = new List<CombineInstance>();
            foreach (MeshFilter filter in filters)
            {
                
                if (filter.transform == transform) continue;
                // The filter doesn't know what materials are involved, get the renderer.
                MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)
                if (renderer == null)
                {
                    Debug.LogError(filter.name + " has no MeshRenderer");
                    continue;
                }

                // Let's see if their materials are the one we want right now.
                Material[] localMaterials = renderer.sharedMaterials;
                for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
                {
                    if (localMaterials[materialIndex] == material)

                    // This submesh is the material we're looking for right now.
                    {
                        if (filter.sharedMesh.subMeshCount > 1)

                        {
                            // We will split the submeshes.   
                            CombineInstance ci2 = new CombineInstance(); 
                            List<CombineInstance> ci2com = new List<CombineInstance>();
                            Mesh ci2mesh = new Mesh();

                            ci2.mesh = filter.sharedMesh;
                            ci2.subMeshIndex = materialIndex;
                            ci2.transform = filter.transform.localToWorldMatrix;
                            ci2com.Add(ci2);

                            ci2mesh.CombineMeshes(ci2com.ToArray());   //ci2mesh is a submesh seperated from the parent mesh.
                            
                            Dictionary<int, Vector3> a = new Dictionary<int, Vector3>(); //Here we are storing the vertices of entire mesh in a dictionary with
                                                                                         // index in vertices[] as the key. We are doing so because all submeshes use
                                                                                         //same array of vertices.

                            for (int k = 0; k < filter.sharedMesh.vertices.Length; k++)
                            {
                                a[k] = filter.sharedMesh.vertices[k];
                            }

                            List<Vector3> resetvert = new List<Vector3>();
                            List<int> resettri = new List<int>();

                            List<int> fake = new List<int>();

                            Dictionary<int, int> b = new Dictionary<int, int>();
                            int rec = 0;

                            for (int k = 0; k < ci2mesh.triangles.Length; k++)  //here we have created our own array of vertices and included only those vertices that
                                                                                //are being used in triangles[]. Note triangle array for each submesh is different and 
                                                                                //is not same as that of parent mesh, but is a part of parent mesh.
                            {

                                if (fake.Contains(ci2mesh.triangles[k]) == false)
                                {
                                    resetvert.Add(a[ci2mesh.triangles[k]]);
                                    fake.Add(ci2mesh.triangles[k]);
                                    resettri.Insert(k, rec);
                                    b[ci2mesh.triangles[k]] = rec;
                                    rec++;
                                }
                                else
                                {
                                    resettri.Insert(k, b[ci2mesh.triangles[k]]);
                                }
                            }
   
                            Mesh mesh2 = new Mesh();


                            mesh2.vertices = resetvert.ToArray(); //created a mesh with our vertices[] and triangles[]
                            mesh2.triangles = resettri.ToArray();                      
                            mesh2.RecalculateNormals(); //recalcu
                            
                            CombineInstance ci = new CombineInstance();
                            // Mesh ci3mesh = new Mesh();
                            // ci3mesh = filter.sharedMesh;

                            // filter.sharedMesh = mesh2;
                            ci.mesh = mesh2;
                            ci.subMeshIndex = 0;
                            ci.transform = filter.transform.localToWorldMatrix;
                            combiners.Add(ci);

                           // filter.sharedMesh = ci3mesh;                      
                        }
                        else
                        {
                            CombineInstance ci = new CombineInstance(); //If there are no additional submesh . No need for creaing an additional mesh
                            ci.mesh = filter.sharedMesh;
                            ci.subMeshIndex = materialIndex;
                            ci.transform = filter.transform.localToWorldMatrix;
                            combiners.Add(ci);
                        }
                    }

                }
            }
       
            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combiners.ToArray(), true);
            submeshes.Add(mesh);         // All meshes are stored in together in a list.
        }

        // The final mesh: combine all the material-specific meshes as independent submeshes.
        List<CombineInstance> finalCombiners = new List<CombineInstance>();
        foreach (Mesh mesh in submeshes)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = mesh;
            ci.subMeshIndex = 0;
            ci.transform = Matrix4x4.identity;
            finalCombiners.Add(ci);
        }
        Mesh finalMesh = new Mesh();
        finalMesh.CombineMeshes(finalCombiners.ToArray(), false);
        //finalMesh.RecalculateNormals();
        
        myMeshFilter.sharedMesh = finalMesh;
      
        Debug.Log("Final mesh has " + submeshes.Count + " materials.");

        transform.rotation = oldRot;  //resetting position and rotation.
        transform.position = oldPos;

        MeshRenderer myRender = GetComponent<MeshRenderer>();
        myRender.sharedMaterials = materials.ToArray();  // setting the material to final mesh in ccorrect order.

        Renderer[] childRender = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in childRender)                                   // disabling children.
        {
            if (r.transform != transform)
            {
                r.gameObject.SetActive(false);
            }

        }
    }


    public void AdvancedMerge2()
    {
        MeshFilter myMeshFilter = GetComponent<MeshFilter>();

        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        // All our children (and us)
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(false);

        // All the meshes in our children (just a big list)
        List<Material> materials = new List<Material>();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(false); // <-- you can optimize this
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.transform == transform)
                continue;
            Material[] localMats = renderer.sharedMaterials;
            foreach (Material localMat in localMats)
                if (!materials.Contains(localMat))
                    materials.Add(localMat);
        }

        // Each material will have a mesh for it.
        List<Mesh> submeshes = new List<Mesh>();
        foreach (Material material in materials)
        {
            // Make a combiner for each (sub)mesh that is mapped to the right material.
            List<CombineInstance> combiners = new List<CombineInstance>();
            foreach (MeshFilter filter in filters)
            {
                if (filter.transform == transform) continue;
                // The filter doesn't know what materials are involved, get the renderer.
                MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)
                if (renderer == null)
                {
                    Debug.LogError(filter.name + " has no MeshRenderer");
                    continue;
                }

                // Let's see if their materials are the one we want right now.
                Material[] localMaterials = renderer.sharedMaterials;
                for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
                {
                    if (localMaterials[materialIndex] != material)
                        continue;
                    // This submesh is the material we're looking for right now.
                    CombineInstance ci = new CombineInstance();
                    ci.mesh = filter.sharedMesh;
                    ci.subMeshIndex = materialIndex;
                    ci.transform = filter.transform.localToWorldMatrix;
                    combiners.Add(ci);
                }
            }
            // Flatten into a single mesh.
            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combiners.ToArray(), true);
            submeshes.Add(mesh);
        }

        // The final mesh: combine all the material-specific meshes as independent submeshes.
        List<CombineInstance> finalCombiners = new List<CombineInstance>();
        foreach (Mesh mesh in submeshes)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = mesh;
            ci.subMeshIndex = 0;
            ci.transform = Matrix4x4.identity;
            finalCombiners.Add(ci);
        }
        Mesh finalMesh = new Mesh();
        finalMesh.CombineMeshes(finalCombiners.ToArray(), false);
        myMeshFilter.sharedMesh = finalMesh;
        Debug.Log("Final mesh has " + submeshes.Count + " materials.");

        transform.rotation = oldRot;
        transform.position = oldPos;


        MeshRenderer myRender = GetComponent<MeshRenderer>();
        myRender.sharedMaterials = materials.ToArray();

        Renderer[] childRender = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in childRender)
        {
            if (r.transform != transform)
            {
                r.gameObject.SetActive(false);
            }

        }

    }



    public void AdvancedMerge3()
    {


        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        MeshFilter myMeshFilter = GetComponent<MeshFilter>();
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(false);

        List<Material> materials = new List<Material>();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(false); // <-- you can optimize this
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.transform == transform)
                continue;
            Material[] localMats = renderer.sharedMaterials;
            foreach (Material localMat in localMats)
                if (!materials.Contains(localMat))
                    materials.Add(localMat);
        }



        // Each material will have a mesh for it.
        List<Mesh> submeshes = new List<Mesh>();
        MeshFilter[] jyothi = GetComponentsInChildren<MeshFilter>();
        MeshFilter him = new MeshFilter();
        foreach (MeshFilter hf in jyothi)
        {
            if (hf.transform != transform)
            {
                him = hf;
            }
        }
        int count = 0;



        foreach (Material material in materials)
        {
            //Debug.Log(him.sharedMesh.vertexCount);



            // Debug.Log(material.name);
            /*
            for(int hm = 0; hm < him.sharedMesh.vertexCount;hm++)
            {
                anshu.Add(him.sharedMesh.vertices[hm]);
            }*/

            // Make a combiner for each (sub)mesh that is mapped to the right material.
            List<CombineInstance> combiners = new List<CombineInstance>();
            foreach (MeshFilter filter in filters)
            {

                if (filter.transform == transform) continue;
                // The filter doesn't know what materials are involved, get the renderer.
                MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)
                if (renderer == null)
                {
                    Debug.LogError(filter.name + " has no MeshRenderer");
                    continue;
                }

                // Let's see if their materials are the one we want right now.
                Material[] localMaterials = renderer.sharedMaterials;
                for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
                {
                    if (localMaterials[materialIndex] != material)
                        continue;
                    // This submesh is the material we're looking for right now.
                    CombineInstance ci = new CombineInstance();
                    ci.mesh = filter.sharedMesh;
                    ci.subMeshIndex = materialIndex;
                    ci.transform = filter.transform.localToWorldMatrix;
                    combiners.Add(ci);
                }
            }


            // Flatten into a single mesh.
            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combiners.ToArray(), false);



            List<int> indices = new List<int>();
            List<int> tri = new List<int>();

            // Debug.Log(count + " Loop !!");


            //List<Vector3> trial = new List<Vector3>(him.sharedMesh.vertices);
            // Debug.Log(mesh.triangles.Length / 3);
            foreach (int vrt in mesh.triangles)
            {
                tri.Add(vrt);
                // Debug.Log(vrt);
                if (!indices.Contains(vrt))
                {
                    indices.Add(vrt);
                    //Debug.Log(vrt);
                }
            }

            Dictionary<int, Vector3> a = new Dictionary<int, Vector3>();

            for (int k = 0; k < him.sharedMesh.vertices.Length; k++)
            {
                a[k] = him.sharedMesh.vertices[k];
            }

            List<Vector3> resetvert = new List<Vector3>();
            List<int> resettri = new List<int>();




            List<int> fake = new List<int>();
            
            if (count == 1)
            {
                int rec = 0;
                Dictionary<int, int> b = new Dictionary<int, int>();
                for (int k = 0; k < mesh.triangles.Length; k++)
                {

                    if (fake.Contains(mesh.triangles[k]) == false)
                    {
                        resetvert.Add(a[mesh.triangles[k]]);
                        
                        fake.Add(mesh.triangles[k]);
                        resettri.Insert(k,rec );
                        b[mesh.triangles[k]] = rec;
                        rec++;
                       

                    }
                    else
                    {
                        resettri.Insert(k, b[mesh.triangles[k]]);

                    }



                }
                for (int i = 0; i < resetvert.Count; i++)
                {
                    Debug.Log(i);
                    Debug.Log(resettri[i]);
                    Debug.Log(mesh.triangles[i]);
                }

                //Debug.Log(him.sharedMesh.vertices[30]);
                // Debug.Log(him.sharedMesh.vertices[23]);




            }


            List<Vector3> allVerts = new List<Vector3>();


            for (int l = 0; l < him.sharedMesh.vertexCount; l++)
            {

                if (indices.Contains(l) == true)
                {
                    allVerts.Add(him.sharedMesh.vertices[l]);

                }
            }

            ///mesh.vertices = allVerts.ToArray();

            // mesh.triangles = tri;
            Mesh mesh2 = new Mesh();

            //mesh2.vertices = allVerts.ToArray();
            int fuck = 0;

            if (count == 1)
            {
                //    Debug.Log(resettri.Count);
                //  Debug.Log(resetvert.Count);
                
                mesh2.vertices = resetvert.ToArray();
                mesh2.triangles = resettri.ToArray();
                mesh2.RecalculateNormals();
                
            }
            if (count == 0)
            {
                //Debug.Log(allVerts.Count);
                
                mesh2.vertices = allVerts.ToArray();
                mesh2.triangles = tri.ToArray();
                mesh2.RecalculateNormals();
            }
            count++;






            // Debug.Log(mesh2.vertices.Length);
            //  Debug.Log(indices.Count);
            //  Debug.Log(allVerts.Count);



            /* for(int j = 0;j < mesh.vertexCount;j++)
             {
                 if(!allVertices.Contains(mesh.vertices[j]))
                 {
                     allVertices.Add(mesh.vertices[j]);
                     localVert.Add(mesh.vertices[j]);
                 }
             }

             mesh.vertices = localVert.ToArray();
             */
            submeshes.Add(mesh2);
        }

        // The final mesh: combine all the material-specific meshes as independent submeshes.
        List<CombineInstance> finalCombiners = new List<CombineInstance>();
        foreach (Mesh mesh in submeshes)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = mesh;
            ci.subMeshIndex = 0;
            ci.transform = Matrix4x4.identity;
            finalCombiners.Add(ci);
        }
        Mesh finalMesh = new Mesh();
        finalMesh.CombineMeshes(finalCombiners.ToArray(), false);

        myMeshFilter.sharedMesh = finalMesh;
        Debug.Log("Final mesh has " + submeshes.Count + " materials.");

        transform.rotation = oldRot;
        transform.position = oldPos;

        MeshRenderer myRender = GetComponent<MeshRenderer>();
        myRender.sharedMaterials = materials.ToArray();

        Renderer[] childRender = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in childRender)
        {
            if (r.transform != transform)
            {
                r.gameObject.SetActive(false);
            }

        }



    }


    






}