using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Rayc();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var tempY = rb.velocity.y;
        Vector3 movement = new Vector3(moveHorizontal * speed, 0f, moveVertical * speed);
        rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, moveVertical * speed);


        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
