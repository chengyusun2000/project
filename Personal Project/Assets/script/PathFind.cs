using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFind : MonoBehaviour
{
    public Load load;
    public List<node> PathTest;
    public Transform player;
    
    public Transform target;
    
    public node Test;



    // Start is called before the first frame update
    void Start()
    {
        load = GetComponent<Load>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Target").transform;
       
    }
    
    void Update()
    {
        //Test=load.GetStartNode(PlayerMapPosition);
        FinDAPath(player.position, target.position);
        //for (int i = 0; i < Path.Count - 1; i++)
        //{
        //    Debug.Log("2333   "+Path[i].position[0] + "," + Path[i].position[1] + "," + Path[i].position[2] + "  " + Path[i].type + "  " + Path[i].walkable);




        //}
    }
    public void FinDAPath(Vector3 StartPoint,Vector3 EndPoint)
    {
        node StartNode = load.GetStartNode(StartPoint);
        node EndNode = load.GetEndNode(EndPoint);

        //Debug.Log("end" + EndNode.position[0] + "" + EndNode.position[1] + "" + EndNode.position[2]);
        List<node> openset = new List<node>();
        HashSet<node> Closed = new HashSet<node>();
        openset.Add(StartNode);

        while(openset.Count>0)
        {
            node currentNode = openset[0];
            for(int i=0;i<=openset.Count-1;i++)
            {
                if(openset[i].Fcost<currentNode.Fcost||(openset[i].Fcost==currentNode.Fcost))
                {
                    if( openset[i].Hcost < currentNode.Hcost)
                    {
                        currentNode = openset[i];
                    }
                    
                }
            }
            openset.Remove(currentNode);
            Closed.Add(currentNode);
            if(currentNode==EndNode)
            {
                retracePath(StartNode, EndNode);
                return;
            }

            foreach(node neighbour in load.NeighbourNodes(currentNode))
            {
                Debug.Log("n0"+neighbour.position[0] + "" + neighbour.position[1] + "" + neighbour.position[2]);
                Debug.Log("1"+currentNode.position[0] + " " + currentNode.position[1] + " " + currentNode.position[2]);
                if (!neighbour.walkable||Closed.Contains(neighbour))
                {
                    continue;
                }
                Debug.Log("2"+currentNode.position[0] + " " + currentNode.position[1] + " " + currentNode.position[2]);
                int MovementcostToNewNeighbour = currentNode.Gcost + GetDistance(currentNode, neighbour);
                if(MovementcostToNewNeighbour<neighbour.Gcost||!openset.Contains(neighbour))
                {
                    neighbour.Gcost = MovementcostToNewNeighbour;
                    //Debug.Log("end" + EndNode.position[0] + "" + EndNode.position[1] + "" + EndNode.position[2]);
                    neighbour.Hcost = GetDistance(neighbour, EndNode);
                    neighbour.parent = currentNode;
                    if(!openset.Contains(neighbour))
                    {
                        openset.Add(neighbour);
                    }
                }
            }
            
        }
    }
    public void retracePath(node StartNode,node EndNode)
    {
         List<node> Path = new List<node>();
        node currentnode = EndNode;
        while(currentnode!=StartNode)
        {
            Path.Add(currentnode);
            currentnode = currentnode.parent;
        }
        Path.Reverse();
        
    }
     int GetDistance(node A ,node B)
    {
        Debug.Log(A.position[0] +" "+ A.position[1] +" "+ A.position[2] /*+ "     " + B.position[0] + B.position[1] + B.position[2]*/ );
        int distanceX = Mathf.Abs(A.position[0] - B.position[0]);
        int distanceY = Mathf.Abs(A.position[1] - B.position[1]);
        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }

    }
}
