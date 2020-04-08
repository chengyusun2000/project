using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
public class pathfinding : MonoBehaviour
{
    public Load load;
    public List<node> PathTest;
    public Transform player;

    public Transform target;

    public node Test;
    public node newnode;
    public int testnumber;
    public List<node> testlist;
    public List<node> openset;
    
    public List<node> Closed;
    //List<node> openset = new List<node>();
    //HashSet<node> Closed = new HashSet<node>();

    // Start is called before the first frame update
    void Start()
    {
        load = GetComponent<Load>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Target").transform;
        newnode = new node(new Vector3Int(0, 1, 0), true, "grass");
        

        
    }

    void Update()
    {
        FinDAPath(player.position, target.position);
        //Test = load.GetStartNode(PlayerMapPosition);


        //for (int i = 0; i < Path.Count - 1; i++)
        //{




        //}
    }
    public void FinDAPath(Vector3 StartPoint, Vector3 EndPoint)
    {
        node StartNode = load.GetStartNode(StartPoint);
        
        node EndNode = load.GetEndNode(EndPoint);
        openset = new List<node>();
         Closed = new List<node>();
        
        openset.Add(StartNode);
        
        while (openset.Count >0)
        {
            node currentNode = openset[0];
            
            for (int i = 1; i <openset.Count; i++)
            {
                
                if (openset[i].Fcost < currentNode.Fcost )
                {
                    //if( openset[i].Hcost < currentNode.Hcost)
                    //{
                        currentNode = openset[i];
                    //}
                        
                    

                }
            }
            
            
            
            if (currentNode == EndNode)
            {
                retracePath(StartNode, EndNode);
                return;
            }
            openset.Remove(currentNode);

            Closed.Add(currentNode);
            foreach (node neighbour in load.NeighbourNodes(currentNode))
            {
                
                if (!neighbour.walkable || Closed.Contains(neighbour))
                {
                    continue;
                }
                int MovementcostToNewNeighbour = currentNode.Gcost + GetDistance(currentNode, neighbour);
                
                if (MovementcostToNewNeighbour < neighbour.Gcost && !Closed.Contains(neighbour))
                {
                    neighbour.Gcost = MovementcostToNewNeighbour;
                    neighbour.Hcost = GetDistance(neighbour, EndNode);
                    neighbour.parent = currentNode;
                    
                    if (!Closed.Contains(neighbour))
                    {
                        openset.Add(neighbour);
                        
                    }
                        
                    
                    
                }
               
            }
            
        }
    }
    public void retracePath(node StartNode, node EndNode)
    {
       List<node> Path = new List<node>();
        node currentnode = EndNode;
        while (currentnode != StartNode)
        {
            Path.Add(currentnode);
            currentnode = currentnode.parent;
        }
        Path.Reverse();
        PathTest = Path;
    }
    int GetDistance(node A, node B)
    {
        //Debug.Log(A.position[0] + " " + A.position[1] + " " + A.position[2] /*+ "     " + B.position[0] + B.position[1] + B.position[2]*/ );
        int distanceX = Mathf.Abs(A.position[0] - B.position[0]);
        int distanceY = Mathf.Abs(A.position[1] - B.position[1]);

        return distanceX * distanceX + distanceY * distanceY;
        
        
        
            
        

    }
}
