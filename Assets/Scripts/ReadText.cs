using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadText : MonoBehaviour {

    public TextAsset file;
    public GameObject text;
    public GameObject btnClose;
    public GameObject panelQuestions;
    public GameObject message;
    // public string[] texto;

    string[] texto;


    //TextAsset file = Resources.Load("pesquisa") as TextAsset;

    int cdPlayer;


    public int fimDaLinha;
    int linhaAtual = 0;

    bool ok = false;

    int[] playerAnswer = new int[28];



    string urlPost = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/InsertPlayerQuestions.php";

    // Use this for initialization
    void Start() {
        btnClose.SetActive(false);
        panelQuestions.SetActive(true);
        message.SetActive(false);


        texto = new string[] {
    "Voce utiliza com muita frequencia os meios de comunicacao dos jogos (chat escrito ou voz, emotes, etc)?",
	"Seus jogos preferidos sao do genero RPG, ficcao e/ou realidade alternativa?",
	"Voce costuma comprar ou desbloquear as skins caras e/ou mais cobicadas?",
	"Voce joga os jogos que sao lancamento, fase beta e/ou acesso antecipado?",
	"Voce termina os jogos que mais gosta?",
	"As historias dos jogos conseguem alterar seus sentimentos?",
	"Voce permanece motivado a jogar mesmo com diversas derrotas consecutivas?",
	"Voce frequentemente discute com outros jogadores e/ou faz provocacoes?",
	"Em um jogo competitivo em equipe, voce sente que na maioria das vezes seus parceiros mais te atrapalham do que ajudam?",
	"Voce possui horas reservadas apenas para jogar?",
	"Voce se considera um jogador habilidoso?",
	"Ao ficar frustrado com o jogo voce tende a abandona-lo e/ou criticar?",
	"Quando quer cumprir um objetivo no jogo, tem tendencia a explorar a deficiencia ou equilibrio do jogo?",
	"Nos jogos em equipe voce tenta assumir a lideranca?",
	"Voce costuma jogar com cautela quando comeca uma nova fase nos jogos?",
	"Voce completa o tutorial dos jogos?",
	"As artes e graficos sao determinantes para voce jogar um jogo?",
	"Voce compartilha com os outros suas tecnicas de jogo?",
	"Voce se sente desconfortavel quando seu desempenho e comparado com outros jogadores?",
	"Voce prefere jogar sozinho em vez de jogar em grupo?",
	"Voce prefere jogos de estrategia do que jogos de acao?",
	"Voce costuma ser paciente com erros, travamentos e/ou bugs nos jogos?",
	"Voce costuma ser paciente com criticas e/ou ofensas de outros jogadores?",
	"Voce prefere jogos simples e/ou casuais?",
	"Voce costuma deixar o inventario do jogo desorganizado?",
	"Em um jogo, voce prioriza a diversao do que a competicao?",
	"Seus jogos preferidos sao os classicos e/ou os lançados ha alguns anos?",
	"Voce prefere usar a skin padrao (inicial) de um personagem?"};

}

    // Update is called once per frame
    void Update() {

        print("playerAnswer.Length " + playerAnswer.Length);
        print("linhaAtual out" + linhaAtual);

        if (linhaAtual < playerAnswer.Length)
        {

            text.GetComponent<Text>().text = texto[linhaAtual];
        }
       
        if (linhaAtual > 27)
        {
            btnClose.SetActive(true);
            panelQuestions.SetActive(false);
            message.SetActive(true);
        }
    }





    public void Answer(int resp)
    {
        //guarda a resposta no vetor
        playerAnswer[linhaAtual] = resp;

        GameObject user = GameObject.Find("GetUser");
        CdPlayer getUser = user.GetComponent<CdPlayer>();

 
        cdPlayer = getUser.cdPlayer;

        WWWForm form = new WWWForm();

        form.AddField("cdPlayerPost", cdPlayer);
        form.AddField("questionPost", linhaAtual);
        form.AddField("answerPost", resp);

        WWW www = new WWW(urlPost, form);


        linhaAtual++;
    }




    public void LoadScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }


        
}

