using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class changePos : MonoBehaviour
{
    public PolygonCollider2D hole2DCol;
    public PolygonCollider2D ground2DCol;
    public MeshCollider GeneratedMeshCol;
    public Collider GroundCol;
    public float initialScale = 0.5f; // col size
    Mesh GeneratedMesh;

    Camera cam;
    RaycastHit hit;
    Ray ray;
    Vector3 clickPos;

    public bool GameIsPaused = false;
    public GameObject pausemenu;

    public IEnumerator HoleGrow()
    {
        Vector3 StartScale = transform.localScale;
        Vector3 EndsScale = StartScale * 1.1f;

        float t = 0;
        while (t <= 0.4f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(StartScale, EndsScale, t);
            yield return null;
        }
    }





    private void Start()
    {
        GameObject[] allgameobjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var gameobj in allgameobjects)
        {
            if (gameobj.layer == LayerMask.NameToLayer("Objects"))
            {
                Physics.IgnoreLayerCollision(7,9,true);
                //Physics.IgnoreCollision(gameobj.GetComponent<Collider>(), GeneratedMeshCol, true); // will ignore any collider except stage
            }
          }

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            clickPos = hit.point;
            clickPos.y = 0;
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                    transform.position = Vector3.MoveTowards(transform.position, clickPos, Time.deltaTime * 5);
            }

            
        }


        // --------- Pause --------- //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }



    }

    public void Resume ()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    private void FixedUpdate()
    {
        if(transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2DCol.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCol.transform.localScale = transform.localScale * initialScale; //fixes col size to the object 
            MakeHole2D();
            Make3DMeshCol();

        }

    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCol.GetPath(0);

        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DCol.transform.TransformPoint(PointPositions[i]); 

        }

        ground2DCol.pathCount = 2;  
        ground2DCol.SetPath(1, PointPositions);

    }   

    private void Make3DMeshCol()
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2DCol.CreateMesh(true, true);
        GeneratedMeshCol.sharedMesh = GeneratedMesh;

    }



}
