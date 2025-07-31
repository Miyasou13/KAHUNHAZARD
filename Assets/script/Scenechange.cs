using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechange : MonoBehaviour
{
    public static Scenechange instanse;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");

    }
    public void ChangeResultScene()
    {
    SceneManager.LoadScene("ResultScene");
    }

    public void ChangeTitleScene ()
    {
    SceneManager.LoadScene("Title");
    }

}
