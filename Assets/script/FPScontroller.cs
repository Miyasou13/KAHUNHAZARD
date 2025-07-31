using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPScontroller: MonoBehaviour
{
    public static FPScontroller instanse;

    float x, z;
    public float speed;

    public GameObject cam;
    Quaternion cameraRot, characterRot;
    public float Xsensitivity=1.5f, Ysensitivity=1.5f;

    bool cursorLock = true;

    float minX=-90f,maxX=90f;

    public Animator animator;

    int ammunition=9999, maxAmmunition=9999,ammoClip=28,maxAmmoClip=28;

    int playerHP = 100, maxplayerHP = 100;
    public Slider hpBer;
    public Text ammoText;

    [SerializeField]
    private GameObject QuitButton;

    [SerializeField]
    private GameObject GamesettingButton;

    [SerializeField] private GameObject pauseUI;
    private GameObject pauseUIInstance;

    public AudioClip FireSound;
    public AudioClip ReloadSound;
    AudioSource audioSource;

    float sensi;
    // Start is called before the first frame update
    void Start()
    {
        GameState.GameOver = false;
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        GameState.canShoot = true;

        hpBer.value = playerHP;
        ammoText.text = ammoClip + "/" + ammunition;

  
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        if (playerHP > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUI) as GameObject;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(Time.timeScale == 0f&&Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (Time.timeScale != 0f)
        {
            float xRot = Input.GetAxis("Mouse X") * Ysensitivity;
            float yRot = Input.GetAxis("Mouse Y") * Xsensitivity;

            cameraRot *= Quaternion.Euler(-yRot, 0, 0);
            characterRot *= Quaternion.Euler(0, xRot, 0);

            cameraRot = ClampRotation(cameraRot);

            cam.transform.localRotation = cameraRot;
            transform.localRotation = characterRot;

            UpdateCursorLock();
        }

        if (Input.GetMouseButtonDown(0) && GameState.canShoot)
        {
            if (ammoClip > 0)
            {
                animator.SetTrigger("Fire");
                audioSource.PlayOneShot(FireSound);
                GameState.canShoot = false;

                ammoClip--;
                ammoText.text = ammoClip + "/" + ammunition;
            }
            else
            {
                Debug.Log("ÇÃÅ[Ç†Ç‡");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int amountNeed = maxAmmoClip - ammoClip;
            int ammoAvailable = amountNeed < ammunition ? amountNeed : ammunition;

            if (amountNeed != 0 && ammunition != 0)
            {
                audioSource.PlayOneShot(ReloadSound);
                animator.SetTrigger("Reload");
                ammunition -= ammoAvailable;
                ammoClip += ammoAvailable;
                ammoText.text = ammoClip + "/" + ammunition;
            }
        }


        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
        {
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
        }
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }


        if (z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            if (!animator.GetBool("Run"))
            {
                animator.SetBool("Run", true);
                speed = 0.18f;
            }
        }
        else if (animator.GetBool("Run"))
        {
            animator.SetBool("Run", false);
            speed = 0.1f;
        }


    }

    private void FixedUpdate()
    {

        //Rigidbody rb = this.GetComponent<Rigidbody>();
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal")*speed;
        z = Input.GetAxisRaw("Vertical")*speed;

       //transform.position += new Vector3(x, 0, z);
       transform.position+=cam.transform.forward * z+cam.transform.right* x;

       
    }


    public void UpdateCursorLock()
    {
        if (cursorLock == true && Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        if(cursorLock == false&& Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = true;
        }

        if (cursorLock == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (cursorLock == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
    public Quaternion ClampRotation(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX,maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    public void Takehit(float damage)
    {
        playerHP = (int)Mathf.Clamp(playerHP-damage,0,playerHP);
        hpBer.value = playerHP;
        if (playerHP<=0&&!GameState.GameOver)
        {
            GameState.GameOver = true;
            SceneManager.LoadScene("ResultScene");
        }
    }


}
