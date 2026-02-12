// using UnityEngine;
//
// public class EnemyBehaviorHandler : MonoBehaviour
// {
//     
//     [SerializeField] private TargetDetector _targetDetector;
//     [SerializeField] private float _attackRange = 2f;
//
//     public Transform GetTargetPosition()
//     {
//         return _targetDetector.GetTarget();
//     }
//
//     public bool CanAttack()
//     {
//         return _targetDetector.GetDistance() <= _attackRange;
//     }
// }