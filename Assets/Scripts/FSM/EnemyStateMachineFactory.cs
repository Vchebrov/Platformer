using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM_for_test
{
    public class EnemyStateMachineFactory
    {
        public static StateMachine Create(
            Transform enemyTransform,
            TargetDetector targetDetector,
            Sword sword,
            Mover mover,
            Fliper fliper,
            ObstacleChecker obstacleChecker,
            EnemyAnimationHandler animationHandler,
            Attacker attacker,
            Health health,
            float attackRange = 2f)
        {
            if (targetDetector == null)
            {
                return null;
            }

            if (health == null)
            {
                return null;
            }

            var patrol = new PatrolState(mover, fliper, obstacleChecker, animationHandler);
            var chase = new ChaseState(mover, fliper, animationHandler, enemyTransform, targetDetector);
            var attack = new AttackState(sword, attacker, animationHandler);
            var death = new DeathState(animationHandler, health);

            var states = new Dictionary<Type, IState>
            {
                { typeof(PatrolState), patrol },
                { typeof(ChaseState), chase },
                { typeof(AttackState), attack },
                { typeof(DeathState), death }
            };

            var transitions = new List<Transition>
            {
                new Transition(
                    typeof(PatrolState), typeof(ChaseState),
                    () => targetDetector.GetTarget() != null
                ),
                
                new Transition(
                    typeof(ChaseState), typeof(PatrolState),
                    () => targetDetector.GetTarget() == null
                ),
                
                new Transition(
                    typeof(PatrolState), typeof(AttackState),
                    () => targetDetector.GetDistance() <= attackRange
                ),
                
                new Transition(
                    typeof(ChaseState), typeof(AttackState),
                    () => targetDetector.GetDistance() <= attackRange
                ),
                
                new Transition(
                    typeof(AttackState), typeof(ChaseState),
                    () => targetDetector.GetDistance() > attackRange
                ),
                
                new Transition(
                    typeof(PatrolState), typeof(DeathState),
                    () => health.HitPoints <= 0
                ),
                new Transition(
                    typeof(ChaseState), typeof(DeathState),
                    () => health.HitPoints <= 0
                ),
                new Transition(
                    typeof(AttackState), typeof(DeathState),
                    () => health.HitPoints <= 0
                ),
            };

            var stateMachine = new GameObject("EnemyStateMachine").AddComponent<StateMachine>();
            stateMachine.Initialize(transitions, states, typeof(PatrolState));

            return stateMachine;
        }
    }
}