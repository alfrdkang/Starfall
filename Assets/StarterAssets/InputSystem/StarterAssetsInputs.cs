using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{

		[SerializeField] PauseMenu pauseMenu;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool shoot;
		public bool reload;
		public bool slide;
		public bool primary;
		public bool secondary;
		public bool pause;
		public bool crouch;
        public bool interact;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }
        public void OnReload(InputValue value)
        {
            ReloadInput(value.isPressed);
        }
        public void OnSlide(InputValue value)
        {
            SlideInput(value.isPressed);
        }
        public void OnPrimary(InputValue value)
        {
            PrimaryInput(value.isPressed);
        }
        public void OnSecondary(InputValue value)
        {
            SecondaryInput(value.isPressed);
        }
        public void OnPause(InputValue value)
        {
            PauseInput(value.isPressed);
        }
        public void OnCrouch(InputValue value)
        {
            CrouchInput(value.isPressed);
        }
        public void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }

#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
            if (!pauseMenu.IsPaused) move = newMoveDirection; else move = Vector2.zero;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
            if (!pauseMenu.IsPaused) look = newLookDirection; else look = Vector2.zero;
		}

		public void JumpInput(bool newJumpState)
		{
            if (!pauseMenu.IsPaused) jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
            if (!pauseMenu.IsPaused) sprint = newSprintState;
		}

        public void ShootInput(bool newShootState)
        {
            if (!pauseMenu.IsPaused) shoot = newShootState;
        }

        public void ReloadInput(bool newReloadState)
        {
            if (!pauseMenu.IsPaused) reload = newReloadState;
        }

        public void SlideInput(bool newSlideState)
        {
            if (!pauseMenu.IsPaused) slide = newSlideState;
        }

        public void PrimaryInput(bool newPrimaryState)
        {
            if (!pauseMenu.IsPaused) primary = newPrimaryState;
        }

        public void SecondaryInput(bool newSecondaryState)
        {
            if (!pauseMenu.IsPaused) secondary = newSecondaryState;
        }

        public void PauseInput(bool newPauseState)
        {
            pause = newPauseState;
        }

        public void CrouchInput(bool newCrouchState)
        {
            if (!pauseMenu.IsPaused) crouch = newCrouchState;
        }

        public void InteractInput(bool newInteractState)
        {
            if (!pauseMenu.IsPaused) interact = newInteractState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}