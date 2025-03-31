using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle {
    public class EnemyController : MonoBehaviour {
        
        public static EnemyController Instance { get; private set; }

        private BattleController _battleController;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }
        
        private void Start() {
            _battleController = BattleController.Instance;
            _battleController.OnEnemyTurn.AddListener(StartEnemyTurn);
        }

        private void StartEnemyTurn() {
            Debug.Log("[EnemyController] Enemy turn started!");

            this.StartCoroutine(WaitAndAttack());
        }

        private IEnumerator WaitAndAttack() {
            yield return new WaitForSeconds(5);
            Attack();
        }

        private void Attack() {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            
            _battleController.EnemyAttack(x, y);
        }
    }
}