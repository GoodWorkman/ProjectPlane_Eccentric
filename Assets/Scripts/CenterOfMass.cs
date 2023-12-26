using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private Rigidbody _rigidbody;

    private float _radius = 0.1f;
    private void Awake()
    {
        _rigidbody.centerOfMass = Vector3.Scale(_centerOfMass.localPosition, transform.localScale);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        
        Gizmos.DrawSphere(_rigidbody.worldCenterOfMass, _radius);
    }
}
