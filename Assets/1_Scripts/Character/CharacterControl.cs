using MET.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Character
{
    public class CharacterControl : MonoBehaviour
    {
        public CharacterController characterController;
        public Animator animator;
        public Transform CharacterBody;

        [SerializeField]
        private float moveSpeed = 3.0f;

        private bool isMoving = false;
        private Vector3 movement;
        private Vector3 lookDirection = Vector3.forward;
        private Vector3 lastmoving = Vector3.zero;

        public Transform cameraDirection;

        float mouseX = 0;
        float mouseY = 0;

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0 || vertical != 0)
            {
                isMoving = true;
                Vector3 offset = (cameraDirection.forward * vertical) + (cameraDirection.right * horizontal);
                offset.y = 0;
                CharacterBody.LookAt(CharacterBody.position + offset);
                CharacterBody.position += CharacterBody.forward * Time.deltaTime * moveSpeed;
            }
            else isMoving = false;

            //Vector3 dir = (CharacterBody.forward * vertical) + (CharacterBody.right * horizontal);
            //dir.y = 0;
            //Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;


            //movement = new Vector3(horizontal, 0f, vertical).normalized;

            //if (movement.magnitude > 0f)
            //{
            //    lookDirection = movement.normalized;
            //    isMoving = true;
            //}
            //else
            //{
            //    isMoving = false;
            //}

            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("xDirection", vertical);
            animator.SetFloat("zDirection", horizontal);


            //if (isMoving && lastmoving != movement)
            //{
            //    lastmoving = movement;
            //    StopAllCoroutines();
            //    StartCoroutine(TurnRightTo(-movement));
            //}

            if (Input.GetMouseButton(1))
            {
                mouseX = Input.GetAxis("Mouse X") * 3f;
                mouseY = Input.GetAxis("Mouse Y") * 3f;

                IngameManager.Instance.followCamera.wide += mouseX;
                IngameManager.Instance.followCamera.height += mouseY;
            }
        }

        private void FixedUpdate()
        {
            Vector3 nextPosition = transform.position + transform.TransformDirection(movement) * moveSpeed * Time.fixedDeltaTime;
            transform.position = nextPosition;
        }


        public IEnumerator TurnRightTo(Vector3 to)
        {
            Vector3 Begin = CharacterBody.right;

            Vector3 End = to;

            float time_flowed = 0;
            float total_time = 0.1f;

            while (time_flowed < total_time)
            {
                var current_light = Vector3.Slerp(Begin, End, time_flowed / total_time);
                time_flowed += Time.deltaTime;

                CharacterBody.right = current_light;

                yield return null;
            }

            CharacterBody.right = End;

            yield break;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.name.Equals("portal"))
                SceneLoadManager.Instance.LoadScene(2);
        }
    }
}