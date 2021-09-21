using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))] //Besoin d'un NavMeshAgent!!!
public class TobyManager : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<TargetPoint> targetPoints = new List<TargetPoint>(); 
    private int indexNextDestination = 0; // index de la destination de Toby qui va changer à chaque fois qu'on arrive à un point.
    private Vector3 actualDestination;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(1,100); // priorité aléatoire entre 1 et 100 non compris - permettant d'avoir plusieurs Toby mais pas même priorité
        agent.speed = Random.Range(1f,6f);
        NextDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            NextDestination();
        }

    }

    private void NextDestination()
    {   
        int oldIndexDestination = indexNextDestination;

        while(oldIndexDestination == indexNextDestination && targetPoints.Count >1)
        {
            indexNextDestination = Random.Range(0,targetPoints.Count);
        }

        actualDestination = targetPoints[indexNextDestination].GivePoint();  // on peut obtenir un point en Vector3 qui sera sa destination actuelle
        agent.SetDestination(actualDestination); // il va dire à l'agent, sa destination à chaque frame.
        
        
       
        if(indexNextDestination >= targetPoints.Count )
        {
            indexNextDestination = 0;
        }
        
        
    }

    private void OnDrawGizmos() 
    {
        if(agent != null) //si y a pas d'agent, tu ne le fais pas!
        {
            Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f + (100 - agent.avoidancePriority) * 0.01f);  // plus la priorité est grande, plus petite sera la taille de la sphère
        }
    }
}
