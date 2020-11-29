using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using MalbersAnimations;

namespace ActionController
{
    public class ACInputController : MonoBehaviour
    {
        [Tooltip("Inputs won't work on Time.Scale = 0")]
        public bool IgnoreOnPause = true;

        public UnityEvent OnInputEnabled = new UnityEvent();
        public UnityEvent OnInputDisabled = new UnityEvent();

        public List<InputRow> inputs = new List<InputRow>();                                        //Used to convert them to dictionary
        public Dictionary<KeyActions, List<InputRow>> DInputs = new Dictionary<KeyActions, List<InputRow>>();        //Shame it cannot be Serialided :(

        public static Vector2 m_LeftJoystickInput {get; private set;}
        public static Vector2 m_RightJoystickInput {get; private set;}

        private ICharacterMove m_CharacterMove;

        protected void InitializeCharacter() => m_CharacterMove = GetComponent<ICharacterMove>();

        private void Awake()
        {
            InitializeCharacter();
            DInputs = new Dictionary<KeyActions, List<InputRow>>();

            foreach (var item in inputs)
            {
                if(DInputs.ContainsKey(item.key))
                {
                    DInputs[item.key].Add(item);
                }
                else
                {
                    DInputs[item.key] = new List<InputRow>();
                }
            }
        }

        private void Update()
        {
             m_CharacterMove.SetInputAxis(new Vector3(m_LeftJoystickInput.x, 0, m_LeftJoystickInput.y));
        }

        public void OnActionLJoystick(InputValue input)
        {
            m_LeftJoystickInput = input.Get<Vector2>();
        }

        public void OnActionRJoystick(InputValue input)
        {
            m_RightJoystickInput = input.Get<Vector2>();
        }

        public void OnActionButtonA()
        {
             HandleInput(KeyActions.ButtonA);
        }

        public void OnActionButtonB()
        {
             HandleInput(KeyActions.ButtonB);
        }

        public void OnActionButtonY()
        {
             HandleInput(KeyActions.ButtonY);
        }

        public void OnActionButtonX()
        {
            HandleInput(KeyActions.ButtonX);
        }

        private void HandleInput(KeyActions keyAction)
        {
            List<InputRow> inputs  = DInputs[keyAction];
            foreach(InputRow member in inputs)
            {
           
            }
        }


        /// <summary>Input Class to change directly between Keys and Unity Inputs </summary>
        [System.Serializable]
        public class InputRow
        {
            public string name = "InputName";
            public bool active = true;
            public KeyActions key = KeyActions.ButtonA;

            /// <summary>Type of Button of the Row System</summary>
            public ButtonAction GetPressed = ButtonAction.Press;
            /// <summary>Current Input Value</summary>
            public bool InputValue = false;
            public bool ToggleValue = false;

            public UnityEvent OnInputDown = new UnityEvent();
            public UnityEvent OnInputUp = new UnityEvent();
            public UnityEvent OnLongPress = new UnityEvent();
            public UnityEvent OnDoubleTap = new UnityEvent();


            public string Name { get => name; set => name = value; }
            public bool Active { get => active; set => active = value; }
            public ButtonAction Button => GetPressed;

            public UnityEvent InputDown => this.OnInputDown;

            public UnityEvent InputUp => this.OnInputUp;

            #region Constructors

            public InputRow(KeyActions k)
            {
                active = true;
                key = k;
                GetPressed = ButtonAction.Down;
            }

            public InputRow(KeyActions k, ButtonAction pressed)
            {
                active = true;
                key = k;
                GetPressed = ButtonAction.Down;
            }

            public InputRow(string name, KeyActions k, ButtonAction pressed)
            {
                this.name = name;
                active = true;
                key = k;
                GetPressed = pressed;
            }

            public InputRow(bool active, string name, KeyActions k, ButtonAction pressed)
            {
                this.name = name;
                this.active = active;
                key = k;
                GetPressed = pressed;
            }

            public InputRow()
            {
                active = true;
                name = "InputName";
                key = KeyActions.ButtonA;
                GetPressed = ButtonAction.Press;
            }

            #endregion
        }
    }
}
