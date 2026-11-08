using UnityEngine;

public class ControllersClampPos : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    Vector3 leftLastSafePos;
    Vector3 rightLastSafePos;

    void Start()
    {

    }

    void Update()
    {
        // I wanted to limit the range of how far away the controllers can get from the camera, it did not work...
        
        // FIX: This does not work properly, ray detaches from controller and keeps moving

        //if (Vector3.Distance(leftHand.position, mainCamera.position) > 1)
        //{
        //    leftHand.position = leftLastSafePos;
        //}
        //else
        //{
        //    leftLastSafePos = leftHand.position;
        //}


        //if (Vector3.Distance(rightHand.position, mainCamera.position) > 1)
        //{
        //    rightHand.position = rightLastSafePos;
        //}
        //else
        //{
        //    rightLastSafePos = rightHand.position;
        //}
    }
}
