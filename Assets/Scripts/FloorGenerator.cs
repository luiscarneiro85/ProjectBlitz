using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    public List<GameObject> leftDoor = new List<GameObject>();
    public List<GameObject> rightDoor = new List<GameObject>();
    public List<GameObject> upDoor = new List<GameObject>();
    public List<GameObject> downDoor = new List<GameObject>();

    public List<GameObject> noWayRoom = new List<GameObject>();

    public List<GameObject> leftNoWay = new List<GameObject>();
    public List<GameObject> rightNoWay = new List<GameObject>();
    public List<GameObject> upNoWay = new List<GameObject>();
    public List<GameObject> downNoWay = new List<GameObject>();

    public List<GameObject> floor = new List<GameObject>();

    int aux;
    public int numberOfRooms;

    GameObject currentMap;
    GameObject targetMap;

    GameObject objRoom;
    GameObject objChildRoom;

    GameObject obj;

    GameObject targetWarps;
    int deadCicle = 4;

    private void Awake()
    {
        aux = Mathf.RoundToInt(Random.Range(0, downDoor.Count));
        print(aux.ToString());
        currentMap = downDoor[aux];
        
        currentMap.GetComponent<MapConfig>().peek = true;

        currentMap.GetComponent<MapConfig>().enemysLeft =
                            Mathf.RoundToInt(Random.Range(currentMap.GetComponent<MapConfig>().enemySpot.Count / 2,
                            currentMap.GetComponent<MapConfig>().enemySpot.Count - 1));
        floor.Add(currentMap);
        print(floor[0].name);
        makeRoom(currentMap);

        
        for(int j = 1; j < numberOfRooms; j++)
        {
            print(floor[j].gameObject.name.ToString());   
            makeRoom(floor[j]);
        }

        
        obj = GameObject.Find("startRoom");
        //pega objeto warps
        objRoom = floor[0].GetComponent<Transform>().GetChild(0).gameObject;
        //pega o warp down
        objChildRoom = objRoom.GetComponent<Transform>().GetChild(2).gameObject;

        //liga a sala inicial com a primeira do andar
        obj.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().targetMap = floor[0];
        //liga sempre com o exit do warp down da sala
        obj.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().target = objChildRoom;
        
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        
		
	}

    void makeRoom(GameObject room)
    {
        int nTimes = 0;

        //pego o objeto warps
        objRoom = room.GetComponent<Transform>().GetChild(0).gameObject;
        for (int i = 0; i < 4; i++)
        {
            //pego o warp
            objChildRoom = objRoom.GetComponent<Transform>().GetChild(i).gameObject;

            if (objChildRoom.GetComponent<Warp>().isActive)
            {
                //verifico qual orientação do warp pego anteriormente e se ele ja naofoi setado
                if (objChildRoom.GetComponent<Warp>().info.Equals("up") && objChildRoom.GetComponent<Warp>().target == null)//0
                {

                    //escolho um mapa com warp oposto
                    Find_1:
                    aux = Mathf.RoundToInt(Random.Range(0, downDoor.Count));
                    targetMap = downDoor[aux];
                  
                    if (targetMap.GetComponent<MapConfig>().peek)
                    {
                        if(nTimes < deadCicle)
                        {
                            nTimes++;
                            goto Find_1;
                        }
                        else
                        {
                            objChildRoom.GetComponent<Warp>().target = null;
                            objChildRoom.GetComponent<Warp>().targetMap = null;
                        }

                    }
                    else
                    {
                        //marco o mapa como ja escolhido
                        targetMap.GetComponent<MapConfig>().peek = true;

                        // inseri o mapa selecionado na lista
                        floor.Add(targetMap);
                        targetMap.GetComponent<MapConfig>().enemysLeft =
                            Mathf.RoundToInt(Random.Range(targetMap.GetComponent<MapConfig>().enemySpot.Count / 2,
                            targetMap.GetComponent<MapConfig>().enemySpot.Count - 1));

                        //interligar os warps
                        //pego objeto warps detro do targetmap depois pego o warp oposto
                        objChildRoom.GetComponent<Warp>().target = targetMap.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(2).gameObject;
                        objChildRoom.GetComponent<Warp>().targetMap = targetMap;

                        //pega objeto warps do mapa target 
                        targetWarps = targetMap.GetComponent<Transform>().GetChild(0).gameObject;
                        //atribui ao warp do mapa alvo o retorno para mapa atual
                        targetWarps.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Warp>().target = objChildRoom;
                        //atribui o mapa alvo ao warp atual
                        targetWarps.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Warp>().targetMap = room;
                    }
                }
                if (objChildRoom.GetComponent<Warp>().info.Equals("right") && objChildRoom.GetComponent<Warp>().target == null)//1
                {
                    nTimes = 0;   
                    //escolho um mapa com warp oposto
                    Find_2:
                    aux = Mathf.RoundToInt(Random.Range(0, leftDoor.Count));
                    targetMap = leftDoor[aux];

                   if (targetMap.GetComponent<MapConfig>().peek)
                    {
                        if (nTimes < deadCicle)
                        {
                            nTimes++;
                            goto Find_2;
                        }
                        else
                        {
                            objChildRoom.GetComponent<Warp>().target = null;
                            objChildRoom.GetComponent<Warp>().targetMap = null;
                        }
                    }
                    else
                    {
                        //marco o mapa como ja escolhido
                        targetMap.GetComponent<MapConfig>().peek = true;

                        // inseri o mapa selecionado na fila
                        //currentFloor.Enqueue(targetMap);
                        floor.Add(targetMap);
                        targetMap.GetComponent<MapConfig>().enemysLeft =
                            Mathf.RoundToInt(Random.Range(targetMap.GetComponent<MapConfig>().enemySpot.Count / 2,
                            targetMap.GetComponent<MapConfig>().enemySpot.Count - 1));


                        //interligar os warps
                        //pego objeto warps detro do targetmap depois pego o warp oposto
                        objChildRoom.GetComponent<Warp>().target = targetMap.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(3).gameObject;
                        objChildRoom.GetComponent<Warp>().targetMap = targetMap;

                        //pega objeto warps do mapa target 
                        targetWarps = targetMap.GetComponent<Transform>().GetChild(0).gameObject;
                        //atribui ao warp do mapa alvo o retorno para mapa atual
                        targetWarps.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Warp>().target = objChildRoom;
                        //atribui o mapa alvo ao warp atual
                    }


                    targetWarps.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Warp>().targetMap = room;

                }
                if (objChildRoom.GetComponent<Warp>().info.Equals("down") && objChildRoom.GetComponent<Warp>().target == null)//2
                {
                    nTimes = 0;
                    //escolho um mapa com warp oposto
                    Find_3:
                    aux = Mathf.RoundToInt(Random.Range(0, upDoor.Count));
                    targetMap = upDoor[aux];

                    
                    if (targetMap.GetComponent<MapConfig>().peek)
                    {
                        if (nTimes < deadCicle)
                        {
                            nTimes++;
                            goto Find_3;
                        }
                        else
                        {
                            objChildRoom.GetComponent<Warp>().target = null;
                            objChildRoom.GetComponent<Warp>().targetMap = null;
                        }
                    }
                    
                    else
                    {
                        //marco o mapa como ja escolhido
                        targetMap.GetComponent<MapConfig>().peek = true;

                        // inseri o mapa selecionado na fila
                        //currentFloor.Enqueue(targetMap);
                        floor.Add(targetMap);
                        targetMap.GetComponent<MapConfig>().enemysLeft =
                            Mathf.RoundToInt(Random.Range(targetMap.GetComponent<MapConfig>().enemySpot.Count / 2,
                            targetMap.GetComponent<MapConfig>().enemySpot.Count - 1));

                        //interligar os warps
                        //pego objeto warps detro do targetmap depois pego o warp oposto
                        objChildRoom.GetComponent<Warp>().target = targetMap.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject;
                        objChildRoom.GetComponent<Warp>().targetMap = targetMap;

                        //pega objeto warps do mapa target 
                        targetWarps = targetMap.GetComponent<Transform>().GetChild(0).gameObject;
                        //atribui ao warp do mapa alvo o retorno para mapa atual
                        targetWarps.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().target = objChildRoom;
                        //atribui o mapa alvo ao warp atual
                        targetWarps.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().targetMap = room;
                    }
                }
                if (objChildRoom.GetComponent<Warp>().info.Equals("left") && objChildRoom.GetComponent<Warp>().target == null)//3
                {
                    nTimes = 0;
                    //escolho um mapa com warp oposto
                    Find_4:
                    aux = Mathf.RoundToInt(Random.Range(0, rightDoor.Count));
                    targetMap = rightDoor[aux];
                    
                    if (targetMap.GetComponent<MapConfig>().peek)
                    {
                        if (nTimes < deadCicle)
                        {
                            nTimes++;
                            goto Find_4;
                        }
                        else
                        {
                            objChildRoom.GetComponent<Warp>().target = null;
                            objChildRoom.GetComponent<Warp>().targetMap = null;
                        }
                    }
                    
                   else
                    {
                        //marco o mapa como ja escolhido
                        targetMap.GetComponent<MapConfig>().peek = true;

                        // inseri o mapa selecionado na fila
                        //currentFloor.Enqueue(targetMap);
                        floor.Add(targetMap);
                        targetMap.GetComponent<MapConfig>().enemysLeft =
                            Mathf.RoundToInt(Random.Range(targetMap.GetComponent<MapConfig>().enemySpot.Count / 2,
                            targetMap.GetComponent<MapConfig>().enemySpot.Count - 1));

                        //interligar os warps
                        //pego objeto warps detro do targetmap depois pego o warp oposto
                        objChildRoom.GetComponent<Warp>().target = targetMap.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(1).gameObject;
                        objChildRoom.GetComponent<Warp>().targetMap = targetMap;

                        //pega objeto warps do mapa target 
                        targetWarps = targetMap.GetComponent<Transform>().GetChild(0).gameObject;
                        //atribui ao warp do mapa alvo o retorno para mapa atual
                        targetWarps.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Warp>().target = objChildRoom;
                        //atribui o mapa alvo ao warp atual
                        targetWarps.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Warp>().targetMap = room;
                    }

                    
                }
            }

        }
    }
}
