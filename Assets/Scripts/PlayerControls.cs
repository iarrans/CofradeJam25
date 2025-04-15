using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    public float speed, senstivity;
    public Vector2 move;
    public float maxForce;
    public bool isPlaying = false;
    public ControlSituations currentStatus = ControlSituations.NORMAL;
    public float controlDuration = 5;
    public int currentState = 0;
    public int currentRound = 0;


    public static PlayerControls Instance;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Start()
    {
        isPlaying = true;
        StartCoroutine(ControlsChanger());
        EnemiesController.Instance.StartSpawner();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("Move");
        move = context.ReadValue<Vector2>();

    }
    
    void Move()
    {

        //Target Velocity
        Vector3 currentVelocity = rb.velocity;

        //Vector velocidad por defecto
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);

        switch (currentState)
        {
            case 0:
                // NORMAL
                break;
            case 1:
                // PLUS90 - I DELANTE D ATRAS
                targetVelocity = new Vector3(-move.y, 0, -move.x);
                break;
            case 2:
                // MIRRORED
                targetVelocity = new Vector3(- move.x, 0, - move.y);
                break;
            case 3:
                // MINUS90
                targetVelocity = new Vector3(move.y, 0, move.x);
                break;
            default:
                // code block
                break;
        }

        targetVelocity *= speed;

        //Dieccion
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate Forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public IEnumerator ControlsChanger()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(controlDuration);
            if (currentState == 3)
            {
                UpdateRound();
            }
            else
            {
                currentState++;
            }
            UIManager.Instance.ChangeState(currentState);
            Debug.Log("ControlChange");
        }
    }

    public void UpdateRound()
    {
        currentState = 0;
        currentRound++;
    }



}

public enum ControlSituations{
    NORMAL = 0, PLUS90 = 1, MIRRORED = 2, MINUS90 = 3
}