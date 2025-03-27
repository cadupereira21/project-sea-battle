using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle {
    public class EnemyController : MonoBehaviour {
        
        public static EnemyController Instance { get; private set; }

        private bool _canAttack = false;

        private BattleController _battleController;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
            
            _battleController = BattleController.Instance;
        }
        
        private void Start() {
            _battleController.enemyTurn.AddListener(StartEnemyTurn);
        }

        private void StartEnemyTurn() {
            Debug.Log("[EnemyController] Enemy turn started!");

            if (_canAttack == false) {
                this.StartCoroutine(Wait());
            }
        }

        private IEnumerator Wait() {
            yield return new WaitForSeconds(5);
            _canAttack = true;
        }

        private void Attack() {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            
            _battleController.EnemyAttack(x, y);
            _canAttack = false;
        }
    }
}