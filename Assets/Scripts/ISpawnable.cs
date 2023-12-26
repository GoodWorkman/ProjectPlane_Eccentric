using UnityEngine;

public interface ISpawnable
{
   GameObject CreateInstance(Vector3 position, Transform container);
}
