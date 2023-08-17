using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MKKingKobutaAIGreen : MonoBehaviour
{
    [SerializeField] private Vector3[] JunkaiPoints;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float m_fireballInterval = 3f;
    [SerializeField] private float m_updatePositionInterval = 3f;
    [SerializeField] private MKKingKobutaFace[] m_otherFaces;
    private MKKingKobutaFace m_face;
    [SerializeField] private Vector3 m_targetPosition;


    private Vector3[] m_cands = new Vector3[0];
    private void OnDrawGizmos()
    {
        for (int i = 0; i < m_cands.Length; i++)
        {
            Gizmos.DrawSphere(m_cands[i], 0.1f);
        }
    }

    void Start()
    {
        TryGetComponent(out m_face);
        StartCoroutine(FireFireball());
        StartCoroutine(UpdateTargetPosition());
    }

    private IEnumerator FireFireball()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_fireballInterval);
            if (m_face.ShouldPause) continue;

            if (!m_face.IsDead)
            {
                m_face.ShootFast();
            }
        }
    }

    private IEnumerator UpdateTargetPosition()
    {
        while (true)
        {
            // 移動可能範囲でランダムな点を選んで、一番他の顔から離れている場所を選ぶ。
            var xMin = 5f;
            var yMin = -4f;
            var xMax = 8.2f;
            var yMax = 1.3f;
            var cands = Enumerable.Range(0, 10)
                .Select(_ => new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), transform.position.z));
            m_cands = cands.ToArray();
            m_targetPosition = cands
                .OrderByDescending(pos => Vector3.Distance(m_targetPosition, pos) / 2f + m_otherFaces.Select(f => !f.IsDead ? Vector3.Distance(f.transform.position, pos) : 0).Sum())
                .First();
            yield return new WaitForSeconds(m_updatePositionInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_face.ShouldPause) return;
        if (m_face.IsDead) return;

        if (Vector3.Distance(m_targetPosition, transform.position) > 1)
        {
            var direction = (m_targetPosition - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
        }
    }
}
