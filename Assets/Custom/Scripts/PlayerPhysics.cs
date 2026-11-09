using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, -0.5f, hit.moveDirection.z);
        body.AddForceAtPosition(pushDir * 2.0f, hit.point, ForceMode.Impulse);
    }
}
