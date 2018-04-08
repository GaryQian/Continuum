using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationChanger : MonoBehaviour {

	public NavMeshAgent agent;
	public List<GameObject> DestinationList;
	private int count, index;
	// Use this for initialization
	void Start () {
		index = 0;
		agent.SetDestination(DestinationList [index].transform.position);
		count = DestinationList.Count;
	}
	
	// Update is called once per frame
	void Update () {
		if ((gameObject.transform.position - DestinationList [index].transform.position).magnitude < 1f) {			
			agent.SetDestination (DestinationList [calculateIndex()].transform.position);
		}

	}

	int calculateIndex(){
		if (index++ >= DestinationList.Count - 1)
			index = 0;
		return index;
	}


}
