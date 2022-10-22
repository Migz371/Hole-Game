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
    Collider planecol;
    RaycastHit hit;
    Ray ray;

    public bool GameIsPaused = false;
    public GameObject pausemenu;

    public void Move(BaseEventData myEvent)
    {
        if (((PointerEventData)myEvent).pointerCurrentRaycast.isValid)
        {
            //transform.position = ((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition; //------instantly moves hole to cursor?-------
            transform.position = Vector3.MoveTowards(transform.position, ((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition, Time.deltaTime * 5); // movement change number for speed
        }
    }

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
                Physics.IgnoreCollision(gameobj.GetComponent<Collider>(), GeneratedMeshCol, true);
            }
          }

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        planecol = GameObject.Find("Stage").GetComponent<Collider>();

    }

    private void Update()
    {
        //transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("working");
        //    ray = cam.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider == planecol)
        //            transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 2);
        //    }
        //}

        // -------Can't hold left button??------



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

    private void OnTriggerEnter(Collider other) // turn off col between ground and objects
    {
        Physics.IgnoreCollision(other,GroundCol, true);
        Physics.IgnoreCollision(other, GeneratedMeshCol, false); 
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, GroundCol, false);
        Physics.IgnoreCollision(other, GeneratedMeshCol, true);
    }

}
