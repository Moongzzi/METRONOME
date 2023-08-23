using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Common
{
    public class ObjectRotator : MonoBehaviour
    {
        public enum RotateAxis
        {
            X = 0,
            Y = 2,
            Z = 3
        }


        public enum RotateDirection
        {
            Right = 0,
            Left = 1
        }

        Transform objectTrans;

        [Range(0f, 1f)]
        public float speed = 0.1f;

        public RotateDirection dir = RotateDirection.Right;

        public RotateAxis axis = RotateAxis.Y;

        // Start is called before the first frame update
        void Start()
        {
            if (objectTrans == null)
                objectTrans = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (objectTrans != null)
            {
                float dirValue = dir == RotateDirection.Right ? -1f : 1f;
                float rotateValue = dirValue * speed;

                switch (axis)
                {
                    case RotateAxis.X:
                        objectTrans.Rotate(new Vector3(rotateValue, 0, 0f));
                        break;
                    case RotateAxis.Y:
                        objectTrans.Rotate(new Vector3(0f, rotateValue, 0f));
                        break;
                    case RotateAxis.Z:
                        objectTrans.Rotate(new Vector3(0f, 0f, rotateValue));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}