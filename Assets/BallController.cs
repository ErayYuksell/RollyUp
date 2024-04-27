using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float gravityPower = 1f;
    [SerializeField] float range = 3f;
    [SerializeField] SphereCollider collisonCollider;
    [SerializeField] MainBallControl mainBallControl;
    [SerializeField] Rigidbody myRigidbody;

    Vector3 gravityDirection;
    Collider[] cubes;
    int maxCubeNumber = 15;
    private void FixedUpdate()
    {
        if (GameManager.Instance.hasTheGameStart)
        {
            transform.Rotate(Vector3.forward, 400 * Time.fixedDeltaTime);
            cubes = new Collider[maxCubeNumber];
            int totalCubeNumber = Physics.OverlapSphereNonAlloc(transform.position, range, cubes);
            // bu fizik fonksiyonu bu objenin gittigi yon boyunce range icerisine giren colliderlari algilar ve cubes dizisine ekler
            for (int i = 0; i < totalCubeNumber; i++)
            {
                Rigidbody rbs = cubes[i].GetComponent<Rigidbody>();

                if (rbs != null)
                {
                    gravityDirection = new Vector3(transform.position.x, 0, transform.position.z) - cubes[i].transform.position;
                    rbs.AddForceAtPosition(gravityPower * gravityDirection.normalized, transform.position);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
