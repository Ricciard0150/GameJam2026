using UnityEngine;
using UnityEngine.AI;

public class CachorroIA : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Patrulha")]
    public Transform[] pontosPatrulha;
    public float distanciaPonto = 1f;

    [Header("Visão")]
    public float distanciaVisao = 12f;
    public float anguloVisao = 100f;
    public LayerMask obstaculos;

    [Header("Perseguição")]
    public float distanciaPerda = 18f;

    private NavMeshAgent agent;
    private int pontoAtual = 0;
    private bool perseguindo = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (pontosPatrulha.Length > 0)
        {
            agent.SetDestination(pontosPatrulha[0].position);
        }
    }

    void Update()
    {
        if (player == null)
            return;

        if (PodeVerPlayer())
        {
            perseguindo = true;
        }

        if (perseguindo)
        {
            Perseguir();
        }
        else
        {
            Patrulhar();
        }
    }

    bool PodeVerPlayer()
    {
        Vector3 direcao = player.position - transform.position;
        float distancia = direcao.magnitude;

        if (distancia > distanciaVisao)
            return false;

        float angulo = Vector3.Angle(transform.forward, direcao);

        if (angulo > anguloVisao / 2f)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(
            transform.position + Vector3.up,
            direcao.normalized,
            out hit,
            distancia,
            ~0))
        {
            if (hit.transform == player)
                return true;
        }

        return false;
    }

    void Perseguir()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia <= distanciaPerda)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            perseguindo = false;
            VoltarParaPatrulha();
        }
    }

    void Patrulhar()
    {
        if (pontosPatrulha.Length == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance <= distanciaPonto)
        {
            pontoAtual++;

            if (pontoAtual >= pontosPatrulha.Length)
                pontoAtual = 0;

            agent.SetDestination(pontosPatrulha[pontoAtual].position);
        }
    }

    void VoltarParaPatrulha()
    {
        if (pontosPatrulha.Length > 0)
        {
            agent.SetDestination(pontosPatrulha[pontoAtual].position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaVisao);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaPerda);
    }
}