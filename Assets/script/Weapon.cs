using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;

    GameObject Player;

    public Transform shotDirection;

    float time,oldtime;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void CanShoot()
    {
        GameState.canShoot = true;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(shotDirection.position, shotDirection.transform.forward * 50, Color.green);
    }


    public void Shooting()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out hitInfo, 300))
        {
            if (hitInfo.collider.gameObject.GetComponent<KahunController>() != null)
            {
                Debug.Log("HIT");

                KahunController hitKahun = hitInfo.collider.gameObject.GetComponent<KahunController>();
                hitKahun.DestroyKahun();
                ScoreController.instanse.TotalScore += 500;
            }
        }


    }
}
