using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuPrincipal : MonoBehaviour {

    public GameObject menu;
    public GameObject controle;
    public GameObject sobre;
    public GameObject creditos;
    public GameObject idioma;
    public GameObject options;
    public GameObject selector;
    public GameObject music;
    public GameObject volume;
    public GameObject effects;
    GameObject list;
    int indice = 0;
    bool locked = false;
    bool lockedH = false;
    string state = "menu";

    public static float globalVolume = .5f;
    public static int globalMusic = 0;
    public static int initialLanguage = 0;
    float effectsVolume = .5f;

    public AudioClip axisSound;


    // Use this for initialization
    void Start () {
        //options.SetActive(false);
        clearScreen();

        musicControl.soundVolume = effectsVolume;

        initialLanguage = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (state.Equals("menu")) list = menu;
        if (state.Equals("options")) list = options.transform.GetChild(0).gameObject;

        globalVolume = volume.GetComponent<Slider>().value;
        effectsVolume = effects.GetComponent<Slider>().value;


        translateLangague();

        //navegação no menu 
        if (Input.GetAxis("Vertical") < 0 && !locked)
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            locked = true;
            indice++;
            if (indice > list.transform.childCount - 1) indice = 0;
            selection();
        }
        else if (Input.GetAxis("Vertical") > 0 && !locked)
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            locked = true;
            indice--;
            if (indice < 0) indice = list.transform.childCount - 1;
            selection();
        }
        
        //libera a trava dos eixos para seleção das opções
        if(Input.GetAxis("Vertical") == 0)
        {
            locked = false;
        }

        if (Input.GetAxis("Horizontal") == 0 && (state.Equals("music") || state.Equals("idioma")))
        {
            lockedH = false;
        }


        //navegacao no opcao musica do menu de opcoes
        if (Input.GetAxis("Horizontal") > 0 && state.Equals("music") && !lockedH)
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            lockedH = true;
            indice++;
            if(indice > music.transform.childCount -1)
            {
                indice = music.transform.childCount - 1;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }
        else if (Input.GetAxis("Horizontal") < 0 && state.Equals("music") && !lockedH)
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            lockedH = true;
            indice--;
            if (indice < 0)
            {
                state = "options";
                indice = 0;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }

        if (state.Equals("music") && Input.GetButtonDown("A"))
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            globalMusic = indice;
            music.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", false);
            music.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", false);
            music.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("select", false);
            music.transform.GetChild(indice).gameObject.GetComponent<Animator>().SetBool("select", true);
        }

        //navegacao no opcao idioma do menu de opcoes
        if (Input.GetAxis("Horizontal") > 0 && state.Equals("idioma") && !lockedH)
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            lockedH = true;
            indice++;
            if (indice > idioma.transform.childCount - 1)
            {
                indice = idioma.transform.childCount - 1;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }
        else if (Input.GetAxis("Horizontal") < 0 && state.Equals("idioma") && !lockedH)
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            lockedH = true;
            indice--;
            if (indice < 0)
            {
                state = "options";
                list = options.transform.GetChild(0).gameObject;
                indice = 3;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }

        if (state.Equals("idioma") && Input.GetButtonDown("A"))
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            initialLanguage = indice;
            idioma.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", false);
            idioma.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", false);
            idioma.transform.GetChild(indice).gameObject.GetComponent<Animator>().SetBool("select", true);
        }

        //seleciona as sublistas dentro do menu opçoes
        if (Input.GetAxis("Horizontal") > 0 && state.Equals("options"))
        {
            if(indice == 0)
            {
                GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
                list = music;
                indice = 0;
                selector.transform.position = list.transform.GetChild(indice).transform.position;
                state = "music";
                lockedH = true;
            }
            if(indice == 1)
            {
                volume.GetComponent<Slider>().value += 0.01f;
            }
            if(indice == 2)
            {
                effects.GetComponent<Slider>().value += 0.01f;
                musicControl.soundVolume = effectsVolume;
            }
            if (indice == 3)
            {
                GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
                list = idioma;
                indice = 0;
                selector.transform.position = list.transform.GetChild(indice).transform.position;
                state = "idioma";
                lockedH = true;
            }

        }

        //Controla o slider de volume no menu de opções
        if (Input.GetAxis("Horizontal") < 0 && state.Equals("options"))
        {
            if (indice == 1)
            {
                volume.GetComponent<Slider>().value -= 0.01f;
                
            }
            if (indice == 2)
            {
                effects.GetComponent<Slider>().value -= 0.01f;
                musicControl.soundVolume = effectsVolume;
            }
        }

        //controla a seleção das opções do menu
        if ((Input.GetButtonDown("A") || Input.GetButtonDown("Submit")) && state.Equals("menu"))
        {
            switch (indice)
            {
                case 0:
                    loadScene("Floor_1");
                    break;
                case 1:
                    indice = 0;
                    state = "options";
                    clearScreen();
                    options.SetActive(true);
                    if(globalMusic == 0)
                    {
                        music.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }
                    if (globalMusic == 1)
                    {
                        music.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }
                    if (globalMusic == 2)
                    {
                        music.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }
                    if(initialLanguage == 0)
                    {
                        idioma.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }
                    if(initialLanguage == 1)
                    {
                        idioma.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }

                    break;
                case 2:
                    clearScreen();
                    creditos.SetActive(true);
                    state = "others";
                    break;
                case 3:
                    clearScreen();
                    controle.SetActive(true);
                    state = "others";
                    break;
                case 4:
                    clearScreen();
                    sobre.SetActive(true);
                    state = "others";
                    break;
                case 5:
                    Application.Quit();
                    break;

            }
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
        }

        //retorna do menu opções para o menu principal
        if ((Input.GetButtonDown("B") || Input.GetButtonDown("Cancel")) && (state.Equals("options") || state.Equals("music") || state.Equals("idioma") || state.Equals("others")))
        {
            GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
            clearScreen();
            menu.SetActive(true);
            state = "menu";
            indice = 0;
        }
        
    }

    //realiza o controle de animações dos botões do menu principal
    public void selection()
    {
        if (state.Equals("menu"))
        {
            for (int i = 0; i < list.transform.childCount; i++)
            {
                list.transform.GetChild(i).GetComponent<Animator>().SetBool("hover", false);
            }

            list.transform.GetChild(indice).GetComponent<Animator>().SetBool("hover", true);
        }
        else if (state.Equals("options"))
        {
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        indice = 0;
        state = "options";
        clearScreen();
        options.SetActive(true);
        if (globalMusic == 0)
        {
            music.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", true);
        }
        if (globalMusic == 1)
        {
            music.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", true);
        }
        if (globalMusic == 2)
        {
            music.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("select", true);
        }
        if (initialLanguage == 0)
        {
            idioma.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", true);
        }
        if (initialLanguage == 1)
        {
            idioma.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", true);
        }
    }

    public void OpenCredits()
    {
        clearScreen();
        creditos.SetActive(true);
        state = "others";
    }

    public void OpenControls()
    {
        clearScreen();
        controle.SetActive(true);
        state = "others";
    }

    public void OpenAbout()
    {
        clearScreen();
        sobre.SetActive(true);
        state = "others";
    }

    public void BackToMainMenu()
    {
        GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
        clearScreen();
        menu.SetActive(true);
        state = "menu";
        indice = 0;
    }

    public void ChangeLanguage(int indice)
    {
        GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
        initialLanguage = indice;
        idioma.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", false);
        idioma.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", false);
        idioma.transform.GetChild(indice).gameObject.GetComponent<Animator>().SetBool("select", true);
    }

    public void ChangeMusic(int indice)
    {
        GetComponent<AudioSource>().PlayOneShot(axisSound, musicControl.soundVolume);
        globalMusic = indice;
        music.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", false);
        music.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", false);
        music.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("select", false);
        music.transform.GetChild(indice).gameObject.GetComponent<Animator>().SetBool("select", true);
    }

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    void clearScreen()
    {
        menu.SetActive(false);
        options.SetActive(false);
        creditos.SetActive(false);
        sobre.SetActive(false);
        controle.SetActive(false);
    }

    void translateLangague()
    {
        //tradução do menu
        if (initialLanguage == 0)//pt-br
        {
            menu.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "JOGAR";
            menu.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text = "OPCOES";
            menu.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Text>().text = "CREDITOS";
            menu.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Text>().text = "CONTROLES";
            menu.GetComponent<Transform>().GetChild(4).gameObject.GetComponent<Text>().text = "SOBRE";
            menu.GetComponent<Transform>().GetChild(5).gameObject.GetComponent<Text>().text = "SAIR";

            //opcoes++++++++++++++++++
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "OPCOES";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "MUSICA";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLUME";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(2).GetComponent<Text>().text = "EFEITOS";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(3).GetComponent<Text>().text = "IDIOMA";

            //btnVoltar
            options.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "VOLTAR";
            options.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLTAR";

            //btnSelecionar
            options.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "SELECIONAR";
            options.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "SELECIONAR";

            //creditos++++++++++++++++++++++
            creditos.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "CREDITOS";
            creditos.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text =
                "Andre Gueiros - Game Designer / Roteiro \n" +
                "Larissa Storck - Roteiro / Game Producer \n" +
                "Luis Felipe Carneiro - Game Designer / Programador\n" +
                "Victor Lopes - Arte / Level Designer";

            creditos.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Text>().text = "Prof Luiz Sergio de Souza - Orientador";
            creditos.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Text>().text = "Jogo desenvolvido como projeto SMAUG do " +
                "6 semestre e como trabalho de graduacao do curso de Tecnologia em Jogos Digitais da Fatec Carapicuiba";
            //btnVoltar
            creditos.GetComponent<Transform>().GetChild(4).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "VOLTAR";
            creditos.GetComponent<Transform>().GetChild(4).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLTAR";

            //Controle+++++++++++++++++++
            controle.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text = "CONTROLES";
            controle.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Text>().text = "1 -> Botao A : Botao de açao, avanca nos menus.\n" +
                "2->Botao B: Botao de voltar nos menus.\n" +
                "3->Analogico direito: Mira e atira para a direcao do correspondente.\n" +
                "4->Analogico esquerdo: Movimenta o personagem.\n" +
                "5->Botao X: Utiliza item.\n" +
                "6->Botao LB: Alterna entre os tiros equipados / Navegacao no menu de inventario.\n" +
                "7->Botao RB: Utiliza habilidade equipada / Navegacao no menu de inventario.\n" +
                "8->Botao Y: Acessa inventario.\n" +
                "9->Botao Start: Pausa o Jogo.";
            //btnVoltar
            controle.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "VOLTAR";
            controle.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLTAR";

            //sobre+++++++++++++++++++++++
            sobre.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "SOBRE";
            sobre.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text = "Projeto Blitz e um jogo rogue like onde o jogador controle um robo com diversas habilidades " +
                "para explorar dungeons em busca de uma poderosa fonte de energia que pode mudar o rumo da guerra. Durante a exploracao o jogador ira enfrentar armadilhas das dungeons " +
                "e os habitantes das cidades em ruinas que sofreram mutacoes devido a esposicao a radiacao e tambem robos cacadores de recompensa. Durante a busca o jogador nao estara " +
                "sozinho, um robo de codinome H6 vai te auxiliar aprendendo com o comportamento do jogador e se adaptando para prestar auxilio da melhor maneira possivel exclusivamente " +
                "para cada jogador.";
            //btnVoltar
            sobre.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "VOLTAR";
            sobre.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLTAR";



        }
        if (initialLanguage == 1)//en-us
        {
            menu.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "PLAY";
            menu.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text = "OPTIONS";
            menu.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Text>().text = "CREDITS";
            menu.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Text>().text = "CONTROLS";
            menu.GetComponent<Transform>().GetChild(4).gameObject.GetComponent<Text>().text = "ABOUT";
            menu.GetComponent<Transform>().GetChild(5).gameObject.GetComponent<Text>().text = "QUIT";

            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "OPTIONS";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "MUSIC";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLUME";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(2).GetComponent<Text>().text = "EFFECTS";
            options.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(3).GetComponent<Text>().text = "LANGUAGE";

            //btnVoltar
            options.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "BACK";
            options.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "BACK";

            //btnSelecionar
            options.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "SELECT";
            options.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "SELECT";

            //creditos++++++++++++++++++++++
            creditos.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "CREDITS";
            creditos.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text =
                "Andre Gueiros - Game Designer / Script\n" +
                "Larissa Storck - Script / Game Producer\n" +
                "Luis Felipe Carneiro - Game Designer / Programmer\n" +
                "Victor Lopes - Art / Level Designer";

            creditos.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Text>().text = "Prof Luiz Sergio de Souza - Leader";
            creditos.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Text>().text = "Game developed as SMAUG project of 6 " +
                "semester and as graduation work of the course of Technology in Digital Games of FATEC Carapicuíba";
            //btnVoltar
            creditos.GetComponent<Transform>().GetChild(4).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "BACK";
            creditos.GetComponent<Transform>().GetChild(4).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "BACK";

            //Controle+++++++++++++++++++
            controle.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text = "CONTROLS";
            controle.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Text>().text = "1->Button A: Action button, scroll through the menus.\n" +
                "2->Button B: Press to return to the menus.\n" +
                "3->Right Analog: Look and shoot towards the correspondent's direction.\n" +
                "4->Left Analog: Move the character.\n" +
                "5->Button X: Use item.\n" +
                "6->LB button: Switches between equipped shots / Navigation in the inventory menu.\n" +
                "7->Boot RB: Use equipped skill / Navigation in the inventory menu.\n" +
                "8->Boot Y: Accesses inventory.\n" +
                "9->Start button: Pause the game.";
            //btnVoltar
            controle.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "BACK";
            controle.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "BACK";

            //sobre+++++++++++++++++++++++
            sobre.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = "ABOUT";
            sobre.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().text = "Project Blitz and a rogue - like game where the player controls a robbery with " +
                "various abilities to explore dungeons in search of a powerful source of energy that can change the course of the war. During the exploration, the player will " +
                "encounter dungeon traps and the inhabitants of the ruined cities that have undergone mutations due to spellcasting and also bounty hunter robberies. During the " +
                "search the player will not be alone, a theft of code H6 will aid you learning with the behavior of the player and adapting to give aid in the best possible way " +
                "exclusively for each player.";
            //btnVoltar
            sobre.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "BACK";
            sobre.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "BACK";

        }
    }
}
