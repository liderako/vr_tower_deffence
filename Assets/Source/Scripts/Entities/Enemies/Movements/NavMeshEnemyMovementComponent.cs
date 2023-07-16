using Source.Configs;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Source.Scripts.Entities.Enemies.MovementComponent
{
    public class NavMeshEnemyMovementComponent : BaseEnemyMovementComponent
    {
        [SerializeField] private EnemyItem enemyItem;
        private NavMeshAgent navMeshAgent;

        protected void Awake()
        {
            InitComponentInGameObject(out navMeshAgent);
            Random.InitState(gameObject.GetHashCode());
            navMeshAgent.speed = Random.Range(enemyItem.minSpeed, enemyItem.speed);
        }

        protected override void Move()
        {
            ChangeStateMovement(true);
            navMeshAgent.SetDestination(player.transform.position);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.Stop();   
            }
        }
    }
}