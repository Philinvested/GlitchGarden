using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour {

    [Tooltip("Average # of seconds between appearances")]
    public float seenEverySeconds;

    private float currentSpeed;
    private GameObject currentTarget;
    private Animator animator;

	// Use this for initialization
	void Start () {
        //**This line of code creates the rigid body in Unity through script** Rigidbody2D myRigidbody = gameObject.AddComponent<Rigidbody2D>();
        //myRigidbody.isKinematic = true;

        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        if (!currentTarget)
        {
            animator.SetBool("IsAttacking", false);
        }
        //print(Button.selectedDefender);

	}

    void OnTriggerEnter2D ()
    {
        //Debug.Log(name + "Trigger enter");
    }

    public void SetSpeed (float speed)
    {
        currentSpeed = speed;
    }

    // Called from the animator at the time of the actual hit
    public void StrikeCurrentTarget (float damage)
    {
        if (currentTarget)
        {
            Health health = currentTarget.GetComponent<Health>();
            if (health)
            {
                health.DealDamage(damage);
            }
        }
           
    } 

    public void Attack(GameObject obj)
    {
        currentTarget = obj;

    }
}
