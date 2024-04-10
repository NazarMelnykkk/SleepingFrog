using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGroupControl : MonoBehaviour
{

    [SerializeField] private AISpawnListConfig _spawnListConfig;
    [SerializeField] private List<GameObject> _spawnPoints;

   // [SerializeField] private int _waveCount = 5;
    [SerializeField] private float _spawnDelay = 1;

    private ScoreViewHandler _scoreViewHandler;
    private Agent _target;
    private List<Agent> _AIEnemies = new();

    private Coroutine _spawnCoroutine;

    

    private void Start()
    {
        if (_scoreViewHandler == null)
        {
            _scoreViewHandler = FindObjectOfType<ScoreViewHandler>();
        }

        if (_target == null)
        {
            _target = FindObjectOfType<CharacterManager>().Character;
        }

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true) 
        {
            SpawnAI();

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void SpawnAI()
    {
        PoolableObject agentObject = EnemyFactory.Instance.Create(transform);
        agentObject.gameObject.transform.position = GetRandomPosition();
        Agent agent = agentObject.gameObject.GetComponent<Agent>();
        _AIEnemies.Add(agent);
        AssignAIConfig(agent);
    }

    private void AssignAIConfig(Agent agent)
    {
        if (_spawnListConfig.AITypes.Count > 0)
        {
            AgentConfigBase config = _spawnListConfig.GetSpawn();
            agent.Init(config);
            AICommandHandler enemyMoveHandler = agent.GetComponent<AICommandHandler>();
            enemyMoveHandler.Init(_target);

            agent.OnDieEvent += AgentDie;
        }
    }

    private Vector2 GetRandomPosition()
    {
        int pointIndex = Random.Range(0, _spawnPoints.Count);
        Vector2 position = _spawnPoints[pointIndex].transform.position;
        return position;
    }

    private void AgentDie()
    {
        _scoreViewHandler.AddScore();
    }
}
