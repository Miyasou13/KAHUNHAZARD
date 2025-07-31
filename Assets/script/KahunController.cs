using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KahunController : MonoBehaviour
{
    Vector3 Size;
    int AddSize1,AddSize2,AddSize3;

    public GameObject target;
    public float runSpeed;
    private NavMeshAgent agent;

    public int attackDamage;
    float TotalTime;

    public AudioClip KahunDeathSound;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        Size = gameObject.transform.localScale;
        AddSize1 = Random.Range(-1,1);
        AddSize2 = Random.Range(-1,2);
        AddSize3 = Random.Range(-2,3);


        agent = GetComponent<NavMeshAgent>();
        if (TotalTime >= 10)
        {
            Size.x += AddSize1;
            Size.y += AddSize1;
            Size.z += AddSize1;
            agent.speed = Random.Range(5, 8);
        }
        else if (TotalTime >= 20)
        {
            Size.x += AddSize2;
            Size.y += AddSize2;
            Size.z += AddSize2;
            agent.speed = Random.Range(8, 10);
        }
        else if (TotalTime <= 20)
        {
            Size.x += AddSize3;
            Size.y += AddSize3;
            Size.z += AddSize3;
            agent.speed = Random.Range(9, 11);
        }

    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }

    public void DamagePlayer()
    {
        if (target != null)
        {
            target.GetComponent<FPScontroller>().Takehit(attackDamage);
        }
    }

    public void DestroyKahun()
    {
        //agent.enabled = false;
        //rigidBody.isKinematic = false;

        //agent.destination = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
        //agent.speed = Random.Range(15, 40);


        //Vector3 KahunVelocity = kahunRigidBody.velocity;
        //Vector3 force = new Vector3(0.0f,8.0f,10.0f) ;
        //rigidBody.AddForce(force * impulse, ForceMode.Impulse);
        //rigidBody.AddForce(Vector3.forward * -impulse, ForceMode.Impulse);
        audioSource.PlayOneShot(KahunDeathSound);

        Destroy(this.gameObject);
    }



    void Update()
    {
        TotalTime += Time.deltaTime;
        if (target&& agent.enabled == true)
        {
            agent.destination = target.transform.position;
        }

        if(DistanceToPlayer()<= 3)
        {
            DamagePlayer();
            DestroyKahun();
            ScoreController.instanse.TotalScore -= 300;
        }


    }
}