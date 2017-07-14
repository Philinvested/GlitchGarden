using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public GameObject projectile, Gun;

    private GameObject projectileParent;
    private Animator animator;
    private AttackSpawner myLaneSpawner;

    void Start()
    {
        animator = GameObject.FindObjectOfType<Animator>();

        // Creates a Parent if Necessary
        projectileParent = GameObject.Find("Projectiles");
        if (!projectileParent)
        {
            projectileParent = new GameObject("Projectiles");
        }

        SetMyLaneSpawner();
    }

    void Update()
    {
        if (IsAttackerAheadInLane())
        {
            animator.SetBool("isAttacking", true);
;        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    // Look through all spawners, and set myLaneSpawner if found
    void SetMyLaneSpawner()
    {
        AttackSpawner[] spawnerArray = GameObject.FindObjectsOfType<AttackSpawner>();

        foreach (AttackSpawner spawner in spawnerArray)
        {
            if (spawner.transform.position.y == transform.position.y)
            {
                myLaneSpawner = spawner;
                return;
            }
        }

        Debug.LogError(name + " can't find spawner in lane");
    }


    bool IsAttackerAheadInLane()
    {
        // Exit if no Attackers in lane
        if(myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        
        // If there are attackers, are they ahead?
        foreach(Transform attackers in myLaneSpawner.transform)
        {
            if(attackers.transform.position.x > transform.position.x)
            {
                return true;
            }
        }

        // Attackers in lane, but behind us
        return false;
    }

    private void Fire()
    {
        GameObject newProjectile = Instantiate(projectile) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
        newProjectile.transform.transform.position = Gun.transform.position;
    }


}
