using UnityEngine;

public class GatoFPS : MonoBehaviour
{
    public float velocidadeAndar = 3f;
    public float velocidadeCorrer = 5.5f;
    public float aceleracao = 12f;
    public float desaceleracao = 18f;
    public float sensibilidadeMouse = 2.5f;

    public Transform cameraJogador;
    public CharacterController controller;

    public float gravidade = -20f;
    public float alturaPulo = 1f;

    public float intensidadeBalanço = 0.04f;
    public float velocidadeBalanço = 8f;

    private float rotacaoX;
    private float velocidadeAtual;
    private float velocidadeVertical;
    private float tempoBalanço;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (controller == null)
            controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movimento();
        Camera();
    }

    void Movimento()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direcao = transform.right * horizontal + transform.forward * vertical;
        direcao = Vector3.ClampMagnitude(direcao, 1f);

        bool correndo = Input.GetKey(KeyCode.LeftShift) && vertical > 0;

        float velocidadeAlvo = correndo ? velocidadeCorrer : velocidadeAndar;

        if (direcao.magnitude > 0.1f)
        {
            velocidadeAtual = Mathf.MoveTowards(
                velocidadeAtual,
                velocidadeAlvo,
                aceleracao * Time.deltaTime
            );
        }
        else
        {
            velocidadeAtual = Mathf.MoveTowards(
                velocidadeAtual,
                0,
                desaceleracao * Time.deltaTime
            );
        }

        Vector3 movimento = direcao * velocidadeAtual;

        if (controller.isGrounded)
        {
            velocidadeVertical = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocidadeVertical = Mathf.Sqrt(alturaPulo * -2f * gravidade);
            }
        }

        velocidadeVertical += gravidade * Time.deltaTime;
        movimento.y = velocidadeVertical;

        controller.Move(movimento * Time.deltaTime);
    }

    void Camera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse;

        transform.Rotate(Vector3.up * mouseX);

        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -85f, 85f);

        cameraJogador.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);

        Vector3 posicaoOriginal = cameraJogador.localPosition;

        if (velocidadeAtual > 0.2f && controller.isGrounded)
        {
            tempoBalanço += Time.deltaTime * velocidadeBalanço * (velocidadeAtual / velocidadeAndar);

            float movimentoX = Mathf.Cos(tempoBalanço) * intensidadeBalanço;
            float movimentoY = Mathf.Sin(tempoBalanço * 2f) * intensidadeBalanço;

            cameraJogador.localPosition = posicaoOriginal + new Vector3(movimentoX, movimentoY, 0);
        }
    }
}