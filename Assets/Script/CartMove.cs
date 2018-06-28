using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CartMove : MonoBehaviour {

    /*카트 움직임이 오류나서 보류~*/

    public Transform Target;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(Target.position);
	}
}
