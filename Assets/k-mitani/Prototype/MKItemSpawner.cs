using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MKItemSpawner : MonoBehaviour
{
    [SerializeField] private MKPlayer player;
    [SerializeField] private LifeUpItem yoshinoLifeUpItemPrefab;

    public event System.EventHandler<Transform> ItemSpawned;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnItemLoop());
    }

    private IEnumerator SpawnItemLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (MKUIManager.Instance.IsGameOver) continue;

            var anyDamaged =
                player.Kobuta.IsDamaged ||
                player.m_option1.Kobuta.IsDamaged ||
                player.m_option2.Kobuta.IsDamaged;
            if (anyDamaged)
            {
                yield return new WaitForSeconds(Random.value * 3);
                var item = Instantiate(yoshinoLifeUpItemPrefab);
                var x = player.m_xMax + 1;
                var y = Random.Range(player.m_yMin + 1, player.m_yMax - 1);
                item.transform.position = new Vector3(x, y, 0);
                ItemSpawned?.Invoke(this, item.transform);
                var xMin = player.m_xMin;
                while (xMin - 2 < item.transform.position.x)
                {
                    yield return null;
                    if (item.IsDestroyed()) break;
                }
                if (!item.IsDestroyed())
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}
