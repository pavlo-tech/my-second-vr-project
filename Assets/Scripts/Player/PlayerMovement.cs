using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;
    Vector3 Movement, lastSafeLocation, colliderLocation;
    bool colliding;
    Rigidbody PlayerRigidBody;
    int FloorMask;

    GameObject Player;
    public SteamVR_TrackedObject head;
    SteamVR_PlayArea area;
    private SteamVR_Controller.Device LeftController, RightController;

    void Awake()
    {
        FloorMask = LayerMask.GetMask("Floor");
        PlayerRigidBody = GetComponent<Rigidbody>();
        LeftController = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestLeft));
        RightController = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight));
        area = GetComponentInParent<SteamVR_PlayArea>();

        Player = GameObject.FindGameObjectWithTag("PlayerBody");

        colliding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliderLocation = collision.contacts[0].point;

        colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!colliding)
        {
            lastSafeLocation.x = head.transform.position.x;
            lastSafeLocation.y = area.transform.position.y;
            lastSafeLocation.z = head.transform.position.z;

            //lastSafeLocation = area.transform.position;

            Vector2 LeftHand = LeftController.GetAxis();
            Vector2 RightHand = RightController.GetAxis();

            if (LeftHand.magnitude > RightHand.magnitude)
            {
                float controllerRotation = -1 * LeftController.transform.rot.y * Mathf.PI;

                Move(LeftHand.x * Mathf.Cos(controllerRotation) - LeftHand.y * Mathf.Sin(controllerRotation),
                    LeftHand.x * Mathf.Sin(controllerRotation) + LeftHand.y * Mathf.Cos(controllerRotation));
            }
            else
            {
                float controllerRotation = -1 * RightController.transform.rot.y * Mathf.PI;

                Move(RightHand.x * Mathf.Cos(controllerRotation) - RightHand.y * Mathf.Sin(controllerRotation),
                    RightHand.x * Mathf.Sin(controllerRotation) + RightHand.y * Mathf.Cos(controllerRotation));
            }
        }
        else
        {
            //Vector3 playerOffset = new Vector3(
            //    lastSafeLocation.x - head.transform.position.x +1f, 
            //    0,
            //    lastSafeLocation.z - head.transform.position.z+ 1f);

            Vector3 direction = new Vector3();
            direction.x = head.transform.position.x - colliderLocation.x;
            direction.z = head.transform.position.z - colliderLocation.z;

            area.transform.position += direction.normalized *(.005f); 
            //area.transform.position = area.transform.position + playerOffset;
        }

        Vector3 pos = transform.position;
        pos.x = head.transform.position.x;
        pos.z = head.transform.position.z;
        transform.position = pos;
    }

    private void Move(float h, float v)
    {
        Movement.Set(h, 0f, v);
        Movement = Movement.normalized * Speed * Time.deltaTime;

        area.transform.position += Movement;



        //PlayerRigidBody.MovePosition(head.head.localPosition);

        //PlayerRigidBody.MovePosition(transform.position + Movement);
    }
}
