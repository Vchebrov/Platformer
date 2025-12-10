using UnityEngine;

public class Turnover : MonoBehaviour
{
        public void Flip(Transform objPosition)
        {
            objPosition.Rotate(0, 180, 0);
        }
}
