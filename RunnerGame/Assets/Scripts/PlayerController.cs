using Cinemachine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layer;
    [SerializeField] GameObject finishBall;
    [SerializeField] List<GameObject> soccerDoor;
    [SerializeField] Animator animator;
    [SerializeField] CinemachineVirtualCamera awayCam;
    [SerializeField] CinemachineVirtualCamera trackingCam;

  
    private Vector3 _direction;
    private Rigidbody _physics;
    float ballCount = 0f;
    void Start()
    {
        _physics = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        //_direction = new Vector3(Input.GetAxis("Horizontal"), 0, verticalSpeed);
        PlayerJump();

    }

    private void PlayerJump()
    {
        if (Physics.Raycast(transform.position, -1 * transform.up, 1f, layer))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
                
                _physics.AddForce(Vector3.up * jumpForce);
            }
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
            
            Restart();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            awayCam.Priority = 10;

            speed = 0;
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
        finishBall.transform.DOJump(door.transform.position, 2f, 1, 0.8f);
        animator.SetTrigger("FreeWorks");
    }

}
