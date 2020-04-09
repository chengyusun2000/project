using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class playerMovement : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rg;
    public Vector2 PlayerMove;
    public Tilemap tilemap;
    public Transform target;
    public Vector3Int playerMapPosition;
    public Vector3 PlayerCenterPos;
    public pathfinding pathfinding;
    public List<node> Path;

    public node NextStep;
    [Header("test step")]
    public float timer = 0;
    public float WaitTime = 10f;
    public Vector3 TargetMovement;
    public Vector3 Direction;
    public float Percent;
    public Vector3Int NextStepVector;
    private bool reach = true;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerMapPosition = tilemap.WorldToCell(transform.position);
        PlayerCenterPos = tilemap.CellToWorld(playerMapPosition);
        transform.position = PlayerCenterPos;
    }

    // Update is called once per frame
    void Update()
    {

        Path = pathfinding.PathTest;



        PlayerMove.x = Input.GetAxisRaw("Horizontal");
        PlayerMove.y = Input.GetAxisRaw("Vertical");


        WalkOnPath();
        
        
        
            
     
        
       

        //for(int i=0;i<Path.Count;i++)
        //{
        //    node NextStep = Path[i];



        //    Direction = TargetMovement - PlayerCenterPos;



        //    //if(timer<WaitTime)
        //    //{
        //    //    timer = timer + Time.deltaTime;

        //    //}
        //    //else
        //    //{
        //    //    timer = 0;
        //    //    transform.position = tilemap.CellToWorld(NextStepVector);
        //    //}

        //}
    }
    public void WalkOnPath()
    {
        if (reach)
        {
            NextStep = Path[i];

            NextStepVector = new Vector3Int(NextStep.position[0], NextStep.position[1], NextStep.position[2]);

            TargetMovement = tilemap.CellToWorld(NextStepVector);
            reach = false;
        }


        if (PlayerCenterPos != TargetMovement && reach == false)
        {
            //transform.position = Vector3.Lerp(PlayerCenterPos, TargetMovement, Time.deltaTime * speed);
            transform.position = Vector3.MoveTowards(PlayerCenterPos, TargetMovement, 0.03f);

        }
        else
        {
            reach = true;
            i++;
        }
        PlayerCenterPos = transform.position;
    }
}
