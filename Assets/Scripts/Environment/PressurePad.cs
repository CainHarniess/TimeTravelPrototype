using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Environment
{
    public interface IPressurePad
    {
        bool IsPressed { get; }

        void Press();
        void Release();
    }

    public class PressurePad : MonoBehaviour, IPressurePad
    {
        [SerializeField] private bool _isPressed;
        public bool IsPressed => _isPressed;

        public void Press()
        {
            Debug.Log("Switch has been pressed.");
        }

        public void Release()
        {
            Debug.Log("Switch has been released.");
        }
    }
}

