using UnityEngine;

public class CameraCollisionController : MonoBehaviour
{
    [Header("------COLLISION------")]
    public Camera _camera;
    public float SphereRadius;
    public float RayDistance;
    public LayerMask DetectLayers;

    public Vector3 RayDirection;



    private void Update()
    {
        // SET DIRECCTION TO CAMERA POSITION
        RayDirection = _camera.transform.position - transform.position;
        // SET DIRECCTION TO WORLD POSITION (transform is child of an object)
        transform.InverseTransformDirection(RayDirection);

        // SET SPHERE CAST
        RaycastHit hit;
        Debug.DrawRay(transform.position, RayDirection, Color.red);
        Physics.SphereCast(transform.position, SphereRadius, RayDirection, out hit, RayDistance, DetectLayers);
        if (Physics.SphereCast(transform.position, SphereRadius, RayDirection, out hit, RayDistance, DetectLayers))
        {

            Debug.DrawRay(transform.position, RayDirection, Color.green);

            _camera.transform.position = hit.point;

        }

    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, RayDirection);
        Gizmos.DrawWireSphere(_camera.transform.position, SphereRadius);
    }
}
