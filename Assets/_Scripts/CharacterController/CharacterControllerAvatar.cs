using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using RootMotion.FinalIK;

namespace FireFighter.CharacterController
{
    public class CharacterControllerAvatar : MonoBehaviour, IEntityReceiver
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float animatorVelocitySmoothTime = 0.1f;
        [SerializeField] private LookAtIK lookAtIK;
        [SerializeField] private AimIK aimIK;
        
        private Entity entityToTrack = Entity.Null;
        private float desiredYRotation = 0.0f;
        private float velocityXSmoothDamp = 0.0f;
        private float velocityZSmoothDamp = 0.0f;
        private int animatorParameterIdle = Animator.StringToHash("Idle");
        private int animatorParameterVelocityX = Animator.StringToHash("VelocityX");
        private int animatorParameterVelocityY = Animator.StringToHash("VelocityY");
        private int animatorParameterVelocityZ = Animator.StringToHash("VelocityZ");
        private int animatorParameterAngle = Animator.StringToHash("Angle");
        private int animatorParameterJumping = Animator.StringToHash("Jumping");
        private int animatorTagTurn = Animator.StringToHash("Turn");
        private int animatorTransitionName = Animator.StringToHash("TurnTransition");
        private Coroutine alignToLookDirectionCoroutine;
        private bool Turning { get { return animator.GetCurrentAnimatorStateInfo(0).tagHash == animatorTagTurn ||
                    animator.GetAnimatorTransitionInfo(0).userNameHash == animatorTransitionName; } }

        #region Unity Life Cycle
        private void OnEnable()
        {
            alignToLookDirectionCoroutine = null;
        }

        private void Start()
        {
            lookAtIK.enabled = false;
            aimIK.enabled = false;
        }

        private void LateUpdate()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            CharacterControllerInternalComponentData internalComponentData = entityManager.GetComponentData<CharacterControllerInternalComponentData>(entityToTrack);

            animator.SetBool(animatorParameterJumping, internalComponentData.IsJumping);

            Vector3 localVelocity = transform.InverseTransformVector(internalComponentData.LinearVelocity);
            float smoothVelocityX = Mathf.SmoothDamp(animator.GetFloat(animatorParameterVelocityX), localVelocity.x,
                ref velocityXSmoothDamp, animatorVelocitySmoothTime);
            float smoothVelocityZ = Mathf.SmoothDamp(animator.GetFloat(animatorParameterVelocityZ), localVelocity.z,
                ref velocityZSmoothDamp, animatorVelocitySmoothTime);

            animator.SetFloat(animatorParameterVelocityX, smoothVelocityX);
            animator.SetFloat(animatorParameterVelocityY, internalComponentData.LinearVelocity.y);
            animator.SetFloat(animatorParameterVelocityZ, smoothVelocityZ);

            desiredYRotation = internalComponentData.CurrentRotationAngle * Mathf.Rad2Deg;

            float angle = Vector3.SignedAngle(transform.forward, Quaternion.Euler(0.0f, desiredYRotation, 0.0f) * Vector3.forward, Vector3.up);

            animator.SetFloat(animatorParameterAngle, angle);

            if(smoothVelocityX < 0.1f && smoothVelocityX > -0.1f && smoothVelocityZ < 0.1f && smoothVelocityZ > -0.1f)
            {
                animator.SetBool(animatorParameterIdle, true);

                if (alignToLookDirectionCoroutine != null)
                {
                    StopCoroutine(alignToLookDirectionCoroutine);
                    alignToLookDirectionCoroutine = null;
                }
            }
            else
            {
                animator.SetBool(animatorParameterIdle, false);

                if(alignToLookDirectionCoroutine == null && Mathf.Abs(angle) > 60.0f)
                {
                    alignToLookDirectionCoroutine = StartCoroutine(AlignToLookDirectionCoroutine());
                }
            }

            float aimYRotation = transform.eulerAngles.y + Mathf.Clamp(angle, -60.0f, 60.0f);
            Vector3 aimTargetPosition = aimIK.solver.transform.position +
                Quaternion.LookRotation(Quaternion.Euler(0.0f, aimYRotation, 0.0f) * Vector3.forward, aimIK.solver.transform.up) * Vector3.forward;

            aimIK.solver.target.position = aimTargetPosition;
            aimIK.solver.Update();

            Vector3 lookTargetPosition = transform.position + Quaternion.Euler(0.0f, desiredYRotation, 0.0f) * Vector3.forward * 2.0f;
            lookTargetPosition.y = lookAtIK.solver.head.transform.position.y;

            lookAtIK.solver.target.position = lookTargetPosition;
            lookAtIK.solver.Update();
        }

        private void OnAnimatorMove()
        {
            if (Turning)
            {
                transform.rotation = animator.rootRotation;
            }
        }
        #endregion

        public void SetReceivedEntity(Entity entity)
        {
            entityToTrack = entity;
        }

        private IEnumerator AlignToLookDirectionCoroutine()
        {
            float timer = 0.0f;
            Quaternion startingRotation = transform.rotation;

            while(timer < 0.5f)
            {
                timer += Time.deltaTime;

                transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(0.0f, desiredYRotation, 0.0f), timer / 0.5f);

                yield return new WaitForEndOfFrame();
            }

            transform.rotation = Quaternion.Euler(0.0f, desiredYRotation, 0.0f);

            alignToLookDirectionCoroutine = null;
        }
    }
}
