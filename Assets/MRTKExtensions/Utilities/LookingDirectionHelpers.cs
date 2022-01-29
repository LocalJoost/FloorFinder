using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace MRTKExtensions.Utilities
{
    public static class LookingDirectionHelpers
    {
        /// <summary>
        /// Get a position on the spatial map right ahead of the camera viewing direction
        /// </summary>
        /// <param name="maxDistance"></param>
        /// <param name="stabilizer"></param>
        /// <returns></returns>
        public static Vector3? GetPositionOnSpatialMap(float maxDistance = 2,
            BaseRayStabilizer stabilizer = null)
        {
            var transform = CameraCache.Main.transform;
            var headRay = stabilizer?.StableRay ?? new Ray(transform.position, transform.forward);
            if (Physics.Raycast(headRay, out var hitInfo, maxDistance, GetSpatialMeshMask()))
            {
                return hitInfo.point;
            }
            return null;
        }

        private static int _meshPhysicsLayer = 0;

        /// <summary>
        /// Determine the spatial mask(s) from the configuration
        /// </summary>
        /// <returns></returns>
        private static int GetSpatialMeshMask()
        {
            if (_meshPhysicsLayer == 0)
            {
                var spatialMappingConfig = Microsoft.MixedReality.Toolkit.CoreServices.SpatialAwarenessSystem.ConfigurationProfile as
                    MixedRealitySpatialAwarenessSystemProfile;
                if (spatialMappingConfig != null)
                {
                    foreach (var config in spatialMappingConfig.ObserverConfigurations)
                    {
                        var observerProfile = config.ObserverProfile
                            as MixedRealitySpatialAwarenessMeshObserverProfile;
                        if (observerProfile != null)
                        {
                            _meshPhysicsLayer |= (1 << observerProfile.MeshPhysicsLayer);
                        }
                    }
                }
            }

            return _meshPhysicsLayer;
        }
    }
}
