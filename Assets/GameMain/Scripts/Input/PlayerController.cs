using UnityEngine;
using Cinemachine;
using GameFramework;
using GameFramework.Event;

namespace Chameleon
{
    public class PlayerController : IPause, IReference
    {
        public Player Player;
        public static PlayerController Create(Player player)
        {
            PlayerController playerController = new PlayerController();
            playerController.Player = player;
            return playerController;
        }
        public void Enter()
        {
            GameEntry.Input.RegisterAction(EnumInput.Jump,EnumButtonState.Down, OnJump);
            GameEntry.Input.RegisterAction(EnumInput.Jump,EnumButtonState.Hover, OnJumpHover);
            GameEntry.Input.RegisterAction(EnumInput.Change,EnumButtonState.Down, OnChangeColor);
            Player.Resume();
            var vc= GameObject.Find("VC").GetComponent<CinemachineVirtualCamera>();
            vc.Follow = Player.transform;
            vc.LookAt = Player.transform;
        }
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if(Player!=null)
            Player.update(elapseSeconds, realElapseSeconds);
        }
        public void Quit()
        {
            GameEntry.Input.UnregisterAction(EnumInput.Jump,EnumButtonState.Down);
            GameEntry.Input.UnregisterAction(EnumInput.Jump,EnumButtonState.Hover);
            GameEntry.Input.UnregisterAction(EnumInput.Change,EnumButtonState.Down);
        }
        private void OnJump()
        {
            Player.Jump();
        }
        private void OnJumpHover()
        {
            Player.JumpHover();
        }
        private void OnChangeColor()
        {
            Player.ChangeColor();
        }
        public void Pause()
        {
            Player.Pause();
        }
        public void Resume()
        {
            Player.Resume();
        }
        public void Clear()
        {
            Player = null;
        }
    }
}