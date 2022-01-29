using UnityEngine;

namespace LocalJoost.Examples
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToPlace;

        public void PlaceObject(Vector3 location)
        {
            var obj = Instantiate(objectToPlace, gameObject.transform);
            obj.transform.position = location;
        }
    }
}