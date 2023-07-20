using Source.Configs;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace Source.Scripts.Entities.Enemies.MovementComponent
{
    public class NavMeshEnemyMovementComponent : BaseEnemyMovementComponent
    {
        [Inject] private EnemyItem enemyItem;
        [SerializeField] private Transform demoGate;
        private NavMeshAgent navMeshAgent;

        protected void Awake()
        {
            InitComponentInGameObject(out navMeshAgent);
            Random.InitState(gameObject.GetHashCode());
            navMeshAgent.speed = Random.Range(enemyItem.minSpeed, enemyItem.speed);
            LoadTarget(demoGate);
        }

        public void LoadTarget(Transform transformTarget)
        {
            target = transformTarget;
        }

        protected override void Move()
        {
            ChangeStateMovement(true);
            navMeshAgent.SetDestination(target.position);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StateMovement += OnHandleStateMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.isStopped = true;   
            }
            StateMovement -= OnHandleStateMovement;
        }

        private void OnHandleStateMovement(bool state)
        {
            navMeshAgent.isStopped = !state;
        }
    }
}