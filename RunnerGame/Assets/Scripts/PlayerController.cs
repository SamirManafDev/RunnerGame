using Cinemachine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject finishBall;
    [SerializeField] private List<GameObject> soccerDoor;
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineVirtualCamera awayCam;
    [SerializeField] private CinemachineVirtualCamera trackingCam;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] public GameObject showAdds;
    [SerializeField] private GameObject Pause;
    [SerializeField] private GameObject gameWin;
    //[SerializeField] private GameObject SettingPanel;
    [SerializeField] private AudioSource AudioVolume;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] GameObject game;

    private bool isJumped;
    //private Vector3 _direction;
    private Rigidbody _physics;
    private float ballCount = 0f;

    void Start()
    {
        VolumeSlider.value = 1f;
        _physics = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        //_direction = new Vector3(Input.GetAxis("Horizontal"), 0, verticalSpeed);
        PlayerJump();
        PauseMenu();
        
    }
    private void PauseMenu()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause.SetActive(true);
            Time.timeScale = 0f;

        }

    }
    public void PausePanel()
    {
        //SettingPanel.SetActive(false);
        Pause.SetActive(true); 
    }
    public void Resume()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }
    //public void Settings()
    //{
    //    SettingPanel.SetActive(true);
        
    //}
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void ClosePauseMenu()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ChangeVolume()
    {
        AudioVolume.volume=VolumeSlider.value;
    }


    private void PlayerJump()
    {
        if (Physics.Raycast(transform.position, -1 * transform.up, 1f, layer))
        {
            if (floatingJoystick.Vertical > 0.7f && !isJumped)
            {
                isJumped = true;
                animator.SetTrigger("Jump");
                _physics.AddForce(Vector3.up * jumpForce);
            }
        }
        else
        {
            isJumped = false;
        }

    }

    void FixedUpdate()
    {

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.5f, 4.5f), transform.position.y, transform.position.z);
        //_physics.MovePosition(transform.position + _direction * speed * Time.deltaTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        //float yPos = 0;
        if (collision.gameObject.tag == "Ball")
        {
            //collision.gameObject.transform.position= new Vector3(transform.position.x,transform.position.y+yPos,transform.position.z);
            //yPos += 0.5f;
            //collision.gameObject.transform.parent= transform;
            Destroy(collision.gameObject);
            ballCount++;
            UIManager.Instance.UpdateBallValue(ballCount);
            Debug.Log("Coins:" + ballCount);
        }

        if (collision.gameObject.tag == "Barier")
        {
            
            showAdds.SetActive(true);
            Time.timeScale = 0;
            //Ads.Instance.RequestBanner();
            //Restart();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            awayCam.Priority = 10;

            //speed = 0;
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            animator.SetTrigger("Goal");

        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Goal()
    {
        GameObject door = soccerDoor[UnityEngine.Random.Range(0, soccerDoor.Count)];

        finishBall.AddComponent<Rigidbody>();
        finishBall.transform.DOJump(door.transform.position, 2f, 1, 0.8f).OnComplete(() =>
        {
            gameWin.SetActive(true);
            Debug.Log("Oyun bItti");
            UIManager.Instance.OpenRestartPanel();
        });

        GetComponent<TimeLine>().PlayTimeLine();// basqa skripti cagirmaq
        
    }


}
