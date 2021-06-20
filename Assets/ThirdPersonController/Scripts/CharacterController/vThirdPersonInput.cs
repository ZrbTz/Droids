using UnityEngine;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        private GameManager gameManager;

        #endregion

        private bool isShooting = false;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        protected virtual void Start()
        {
            inputManager = GetComponent<InputManager>();
            InitilizeController();
            InitializeTpCamera();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            if (!gameManager.IsPaused())
            {
                InputHandle();                  // update the input methods
                cc.UpdateAnimator();            // updates the Animator Parameters
            }
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput) && cc.isSprinting == false)
                cc.Strafe();
        }

        private InputManager inputManager;
        private bool sprintingWithController = false;
        private bool sprintDown = false;
        private bool sprinting = false;
        private bool sprintUp = false;

        protected virtual void SprintInput()
        {
            /*if (Input.GetButtonDown("Fire1")) isShooting = true;
            else if (Input.GetButtonUp("Fire1")) isShooting = false;
            Debug.Log(Input.GetKey(KeyCode.Joystick1Button8));
           
            if (Input.GetButtonDown("Sprint") && !isShooting) cc.Sprint(true);
            else if (cc.isSprinting && Input.GetButtonDown("Fire1")) cc.Sprint(false);
            else if (Input.GetButton("Sprint") && !cc.isSprinting && Input.GetButtonUp("Fire1")) cc.Sprint(true);
            else if (Input.GetButtonUp("Sprint") && cc.isSprinting) cc.Sprint(false);*/
            isShooting = inputManager.WeaponFire;
            sprintUp = sprintDown = false;

            if(Input.GetButtonDown("Sprint")
                && !sprintingWithController) 
                sprinting = sprintDown = true;

            if (!Input.GetButton("Sprint")
                && sprinting
                && !sprintingWithController) {
                sprinting = false;
                sprintUp = true;
            }

            if (Input.GetAxis("Sprint") != 0
                && !sprintingWithController
                && !sprinting)
                sprintingWithController = sprintDown = sprinting = true;

            if (Input.GetAxis("Sprint") == 0
                && sprintingWithController) {
                sprintingWithController = sprinting = false;
                sprintUp = true;
            }

            if (sprintDown && !isShooting) cc.Sprint(true);
            else if (cc.isSprinting && inputManager.WeaponFireDown) cc.Sprint(false);
            else if (sprinting && !cc.isSprinting && !isShooting) cc.Sprint(true);
            else if (sprintUp && cc.isSprinting) cc.Sprint(false);

            /*if ((Input.GetButtonDown("Sprint") || Input.GetAxis("Sprint") != 0) && !isShooting) cc.Sprint(true);
            else if (cc.isSprinting && Input.GetButtonDown("Fire1")) cc.Sprint(false);
            else if ((Input.GetButton("Sprint") || Input.GetAxis("Sprint") != 0) && !cc.isSprinting && Input.GetButtonUp("Fire1")) cc.Sprint(true);
            else if ((Input.GetButtonUp("Sprint")) && cc.isSprinting) cc.Sprint(false);*/
            //else if (Input.GetKeyUp(sprintInput) && !isShooting)
            //    cc.Sprint(false);
            //else if (cc.isSprinting == true && Input.GetButtonDown("Fire1")) {
            //    isShooting = true;
            //    cc.Sprint(false);
            //}
            //else if (isShooting = true && Input.GetButtonUp("Fire1")) {
            //    isShooting = false;
            //    cc.Sprint(true);
            //}
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetButtonDown("Jump") && JumpConditions())
            {
                cc.Jump();
                this.GetComponent<PlayerAnimationSounds>().Jump();
            }
        }

        #endregion       
    }
}