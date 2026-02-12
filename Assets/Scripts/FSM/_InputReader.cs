// using UnityEngine;
//
// public class _InputReader: MonoBehaviour
// {
//     public KeyCode FirstKey = KeyCode.W;
//     public KeyCode SecondKey = KeyCode.Space;
//
//     private bool _isFirstPressed;
//     private bool _isSecondPressed;
//
//     public void Update()
//     {
//         if (Input.GetKeyDown(FirstKey))
//         {
//             Debug.Log("First Pressed");
//             _isFirstPressed = true;
//         }
//
//         if (Input.GetKeyDown(SecondKey))
//         {
//             Debug.Log("Second Pressed");
//             _isSecondPressed = true;
//         }
//     }
//
//     public bool IsFirstRequested()
//     {
//         if (_isFirstPressed)
//         {
//             _isFirstPressed = false;
//             return true;
//         }
//
//         return false;
//     }
//
//     public bool IsSecondRequested()
//     {
//         if (_isSecondPressed)
//         {
//             _isSecondPressed = false;
//             return true;
//         }
//
//         return false;
//     }
// }