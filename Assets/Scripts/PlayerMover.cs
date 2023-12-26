using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveForce = 50f;
    [SerializeField] private float _rotationForce = 5f;

    //[SerializeField] private float _rotationSpeed = 100f;
    //[SerializeField] private float _moveSpeed = 10f;

    private Rigidbody _rigidbody;
    private float _forwardInput;
    private float _sideInput;

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _forwardInput = Mathf.Clamp(Input.GetAxisRaw("Vertical"), 0f, 1f) * _moveForce * Time.fixedDeltaTime;
        _sideInput = Input.GetAxisRaw("Horizontal") * _rotationForce * Time.fixedDeltaTime;

        _rigidbody.AddRelativeForce(0f, 0f, _forwardInput, ForceMode.VelocityChange);
        _rigidbody.AddRelativeTorque(_sideInput, 0f, 0f, ForceMode.VelocityChange);


        //При такой записи, как мы делали с игроком, который ходит по земле - ничего не работает, голову сломал, почему так
        //управление искажено и полет совершенно некорректен

        /*Vector3 offcet = new Vector3( 0f, 0f, Mathf.Clamp(Input.GetAxisRaw("Vertical"),0f,1f)) * _moveSpeed; // Mathf.Clamp - и с ним и без него проблемы

       offcet.y = _rigidbody.velocity.y;

       _rigidbody.velocity = transform.TransformVector(offcet);

        _rigidbody.angularVelocity = new Vector3(0f,0f, Input.GetAxisRaw("Horizontal")) * _rotationSpeed; // вот здесь полный неадекват
*/
    }
}