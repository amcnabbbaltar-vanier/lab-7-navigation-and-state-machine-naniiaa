using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public StateMachine StateMachine {get; private set;}
    public NavMeshAgent Agent {get; private set;}
    public Animator Animator {get; private set; } // Not needed 

    public Transform[] Waypoints;

    public Transform Player;

    public float SightRange = 10f;

    public float AttackRange = 2f; // New attack range

    public LayerMask PlayerLayer;

    public StateType currentState;

    public void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        Animator = GetComponent<Animator>(); // Commented out since we are not doing animations
    
        StateMachine = new StateMachine();

        StateMachine.AddState(new IdleState(this));

        StateMachine.AddState(new PatrolState(this));

        StateMachine.AddState(new ChaseState(this));

        StateMachine.AddState(new AttackState(this)); // Add the new AttackState

        StateMachine.TransitionToState(StateType.Idle);

    }

    public void Update()
    {
        StateMachine.Update();

        Animator.SetFloat("CharacterSpeed", Agent.velocity.magnitude);

        currentState = StateMachine.GetCurrentStateType();
    }

    public bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if(distanceToPlayer <= SightRange)
        {
            // Optionally, add a line of sight checks here using RayCast
            
            return true;
        }

        return false;
    }

    // New method to check if the AI is within the attack range
    public bool IsPlayerInAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        return distanceToPlayer <= AttackRange;
    }

}

/**

NAV MESH MODIFIER COMPONENT ON AN EMPTY GAMEOBJECT FOR OBJECT AVOIDANCE

-> IF THE SIZE IS UNREALISTICALLY SPECIFIED, HE WILL GO THROUGH THE PATH (CUTS THE NAV MESH) OR TRIES TO MOVE AROUND

OBSTACLES THAT DONT CARVE WROSK LESS EFFECTIVELY THAN AREAS, WE CAN HAVE MOVABLE AREA.

SMTHG THATS MINUTE TO MINUTE ITS OBSTACLES

LAYERED ANIMATIONS

*/
