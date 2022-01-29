using System.Runtime.CompilerServices;
using Microsoft.MixedReality.Toolkit.Utilities;
using MRTKExtensions.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace LocalJoost.Examples
{
    public class FloorFinder : MonoBehaviour
    {
        private float _delayMoment;
        
        private Vector3? foundPosition = null;

        [SerializeField]
        [Tooltip("Maximum distance to look for the floor")]
        private float maxDistance = 3.0f;
        
        [SerializeField]
        [Tooltip("Prompt to encourage the user to look at the floor")]
        private GameObject lookPrompt;

        [SerializeField]
        [Tooltip("Prompt to ask the user if this is indeed the floor")]
        private GameObject confirmPrompt;
        
        [SerializeField]
        [Tooltip("Sound that should be played when the conform prompt is displayed")]
        private AudioSource locationFoundSound;
        
        [SerializeField]
        [Tooltip("Triggered once when the location is accepted.")]
        private UnityEvent<Vector3> locationFound = new UnityEvent<Vector3>();


        private void OnEnable()
        {
            Reset();
        }

        private void Update()
        {
            CheckLocationOnSpatialMap();
        }

        public void Reset()
        {
            _delayMoment = Time.time + 2;
            foundPosition = null;
            lookPrompt.SetActive(true);
            confirmPrompt.SetActive(false);
        }

        public void Accept()
        {
            if (foundPosition != null)
            {
                locationFound?.Invoke(foundPosition.Value);
                lookPrompt.SetActive(false);
                confirmPrompt.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        
        private void CheckLocationOnSpatialMap()
        {
            if (foundPosition == null && Time.time > _delayMoment)
            {
                foundPosition = LookingDirectionHelpers.GetPositionOnSpatialMap(maxDistance);
                if (foundPosition != null)
                {
                    if (CameraCache.Main.transform.position.y - foundPosition.Value.y > 1f)
                    { 
                        lookPrompt.SetActive(false);
                        confirmPrompt.transform.position = foundPosition.Value;
                        confirmPrompt.SetActive(true);
                        locationFoundSound.Play();
                    }
                    else
                    {
                        foundPosition = null;
                    }
                }
            }
        }
    }
}