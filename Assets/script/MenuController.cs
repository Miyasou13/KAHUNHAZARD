using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    float oldsensi;
    public bool changed;
    Slider sensitivitybar;
    void Start()
    {
        sensitivitybar = GetComponent<Slider>();
        float maxsensi=2f;
        float minsensi = 0f;

        sensitivitybar.maxValue = maxsensi;
        sensitivitybar.minValue = minsensi;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        oldsensi = sensitivitybar.value;
        if (oldsensi != sensitivitybar.value)
        {
            changed = true;
            PlayerPrefs.SetFloat("sensitivity", sensitivitybar.value);
            PlayerPrefs.Save();
        }
        changed = false;
    }
}
