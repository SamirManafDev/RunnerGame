using UnityEngine;

public class SpilineMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private FloatingJoystick floatingJoystick;


    private Vector3 _directions;


    void Start()
    {

    }

    private void Update()
    {
        _directions = new Vector3(floatingJoystick.Horizontal, 0, 0);

        transform.localPosition += _directions * speed * Time.deltaTime;

        transform.localPosition =
            new Vector3(
                Mathf.Clamp(transform.localPosition.x, -4f, 4f),
                transform.localPosition.y,
                transform.localPosition.z);
    }
}
