using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject KAHUN;
    public GameObject SPItem;
    public float TotalTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GenerateKahun1");
        StartCoroutine("GenerateKahun2");
    }

    // Update is called once per frame
    void Update()
    {
        TotalTime += Time.deltaTime;
    }

    IEnumerator GenerateKahun1()
    {
        while (true)
        {
            Instantiate(KAHUN,
                new Vector3(Random.Range(10, -25), 6.0f, -25f),
                transform.rotation
            );
            if (TotalTime <= 10)
            {
                yield return new WaitForSeconds(1);
            }
            else if (TotalTime <= 20)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
            }
            else if (TotalTime >= 20)
            {
                yield return new WaitForSeconds(Random.Range(0, 2));
            }
        }
    }

    IEnumerator GenerateKahun2()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1,2));
            Instantiate(KAHUN,
                new Vector3(Random.Range(10, -25), 6.0f, -25f),
                transform.rotation
            );
            if (TotalTime <= 10)
            {
                yield return new WaitForSeconds(Random.Range(1,2));
            }
            else if (TotalTime <= 20)
            {
                yield return new WaitForSeconds(1);
            }
            else if (TotalTime >= 20)
            {
                yield return new WaitForSeconds(1);
            }
        }
    }



}
