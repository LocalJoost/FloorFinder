using UnityEngine;
using HoloToolkitExtensions.Utilities;

namespace HoloToolkitExtensions.Animation
{
    public class KeepInViewController : MonoBehaviour
    {

        public float MaxDistance = 2f;

        public float MoveTime = 0.8f;

        private bool _isActive = false;

        private bool _isBusy;

        public void SetActive(bool active)
        {
            _isActive = active;
        }

        void Start()
        {
            SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isActive || _isBusy)
            {
                return;
            }

            _isBusy = true;
            LeanTween.moveLocal(transform.gameObject,
                    LookingDirectionHelpers.GetPositionInLookingDirection(), MoveTime).
                setEase(LeanTweenType.easeInOutSine).
                setOnComplete(MovingDone);
        }

        private void MovingDone()
        {
            _isBusy = false;
        }
    }
}