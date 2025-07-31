using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    public Text ResultScore;
    // Start is called before the first frame update
    void Start()
    {
        ResultScore.text = "TotalScore:" + ScoreController.instanse.TotalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
