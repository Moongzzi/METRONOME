using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PivotAxis
{
    Free,
    Y,
    X,
    Z
}

namespace MET.Common
{
    public class Billboard : MonoBehaviour
    {
        [Tooltip("Specifies the axis about which the object will rotate.")]
        public PivotAxis PivotAxis = PivotAxis.Free;
        public bool isReverse = false;



        void Update()
        {
            var target = Camera.main.transform.forward;
            switch (PivotAxis)
            {
                case PivotAxis.X:
                    target.x = 0.0f;
                    break;

                case PivotAxis.Y:
                    target.y = 0.0f;
                    break;

                case PivotAxis.Z:
                    target.z = 0.0f;
                    break;

                case PivotAxis.Free:
                default:
                    break;
            }

            transform.forward = isReverse ? -target : target;

            //transform.LookAt(Camera.main.transform.position);
        }
    }
}