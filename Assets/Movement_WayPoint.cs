using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_WayPoint : MonoBehaviour
{
   //Array de GameObject, que serve para guardar as referencias dos pontos
    public GameObject[] waypoints;
   //Para indetificar qual ponto tenho que ir, se faz um contador
    int currentWP = 0;
   //a velocidade para se locomover
    public float speed = 1.0f;
   //a distancia que eu quero que mude para proximo ponto, a variavel que decidi isso
    public float accuracy = 1.0f;
   // A velocidade de rotação
    public float rotSpeed = 0.8f;

    void Start()
    {
        //Buscando todos gameobject com a tag "waypoint", para guardar no array waypoints
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        //Apenas para identificar o proximo pronto, mudando o material
        waypoints[currentWP].GetComponent<MeshRenderer>().material = target;
    }

    void LateUpdate()
    {
        //Caso a quantidade de gameobjetcs no array for ZERO, ele vai retornar para não fazer ação indesajavel ou bugar
        if (waypoints.Length == 0) return;
      //A posição do Gameobject que eu tenho que seguir, so quero saber o eixo X e Z dele, eu pego o meu proprio eixo X
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y,
        waypoints[currentWP].transform.position.z);
      //A direção seria a posição que eu to olhando menos a minha posição
        Vector3 direction = lookAtGoal - this.transform.position;
       //Afrentando a minha rotação, para eu seguir a direção do objeto, ja que o script tem a logica de seguir o carro pela visão e andar reto(apenas no eixo Z)
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        Quaternion.LookRotation(direction),
        Time.deltaTime * rotSpeed);
       //a condição para mudar de ponto(waypoint) a seguir
        if (direction.magnitude < accuracy)
        {
            //a ação para mudar de ponto, tambem seguindo a logica para o meu contador não passar do tamanho do Array de objetos
            waypoints[currentWP].GetComponent<MeshRenderer>().material = padrao;
            currentWP++;
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
            waypoints[currentWP].GetComponent<MeshRenderer>().material = target;
        }
        //Para eu seguir em frente, no eixo Z, a indeferencia do waypoint, seria atraves da minha rotação e não minha movimentação no eixo de localização
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
    //Variavel de material para identificar o proximo alvo e tambem deixar o materia que estava
    [SerializeField] private Material target;
    [SerializeField] private Material padrao;
}
