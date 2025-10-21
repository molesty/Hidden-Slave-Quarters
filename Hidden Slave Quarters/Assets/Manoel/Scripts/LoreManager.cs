using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreManager : MonoBehaviour
{
    public DialogueController dialogueController;

    void Start()
    {
        // Encontrar o DialogueController se não estiver atribuído
        if (dialogueController == null)
        {
            dialogueController = FindObjectOfType<DialogueController>();
        }

        // Configurar as linhas de diálogo com a lore
        if (dialogueController != null)
        {
            SetupLoreDialogue();
            
            // Iniciar o diálogo automaticamente
            dialogueController.StartDialogue();
        }
        else
        {
            Debug.LogError("DialogueController não encontrado na cena!");
        }
    }

    void SetupLoreDialogue()
    {
        List<DialogueLine> loreLines = new List<DialogueLine>();

        // Título
        loreLines.Add(new DialogueLine
        {
            speaker = "A SENZALA OCULTA",
            text = "Uma história de mistério e resistência"
        });

        // Parte 1 - Introdução
        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "1888? 1988? 2088? O tempo se confunde na imensidão verde da Amazônia, onde a floresta guarda segredos como uma sentinela implacável."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Entre cipós ancestrais e árvores que testemunharam séculos, existe um silêncio que não pertence à natureza — é um silêncio forjado pelo medo e pela opressão."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "A mata densa, com seus tons infinitos de verde, esconde mais que biodiversidade. Nas profundezas onde o sol mal consegue tocar o solo, os sons da floresta se transformam."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "O canto dos pássaros desaparece gradualmente, substituído por um vazio inquietante. O vento parece conter a respiração, como se a própria natureza temesse revelar o que esconde."
        });

        // Parte 2 - Abdias
        loreLines.Add(new DialogueLine
        {
            speaker = "Abdias",
            text = "Meu nome é Abdias Nascimento da Silva, historiador e ativista. Dediquei minha vida a desenterrar verdades que o Brasil prefere esquecer."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Abdias",
            text = "Carrego no nome e no sangue a herança de meus ancestrais. Meu corpo magro e resistente, marcado por cicatrizes de batalhas passadas, não demonstra o cansaço de quem luta contra o esquecimento."
        });

        // Parte 3 - Zumbi
        loreLines.Add(new DialogueLine
        {
            speaker = "Zumbi",
            text = "Eles nos mantêm acorrentados não apenas com ferro, mas com isolamento. A floresta é cúmplice involuntária."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Zumbi",
            text = "Os guardas têm armas modernas, mas seus métodos são antigos como o pecado. Crianças nascem já sabendo que são propriedade. Mulheres desaparecem na casa grande. Homens são quebrados até que esqueçam seus nomes."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Ninguém acreditou em Zumbi. Chamaram-no de louco, de mentiroso, de bêbado. Mas Abdias reconheceu nos olhos dele uma verdade que transcende palavras."
        });

        // Parte 4 - A Jornada
        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Munido apenas de um facão, uma bússola, um diário de anotações e a determinação que corre em suas veias, Abdias adentra a floresta."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "A cada quilômetro, a Amazônia se torna mais hostil, como se tentasse proteger seu terrível segredo. O ar fica mais denso, carregado de umidade e de presságios."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "O chão começa a contar histórias. Entre raízes e folhas em decomposição, Abdias encontra evidências: um pedaço de corrente enferrujada entrelaçada com uma raiz, como se a árvore tentasse digeri-la."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Pegadas antigas preservadas em lama seca; restos de tecido que não pertencem a nenhuma tribo indígena conhecida."
        });

        // Parte 5 - A Descoberta
        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Após três dias de caminhada, quando o desespero começava a se infiltrar em sua determinação, Abdias sente o cheiro. Não é apenas o odor da floresta — é fumaça, suor, sangue e medo."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "O som distante de metal contra metal confirma: ele está perto."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Escondido entre a vegetação, Abdias finalmente vê: uma clareira artificial onde o século XIX persiste como uma anomalia temporal."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Cercas altas de madeira reforçada com arame formam um perímetro guardado por homens armados — alguns com rifles modernos, outros com chicotes antigos."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Dentro, barracões de madeira escurecida pelo tempo abrigam dezenas de pessoas negras, trabalhando sob o sol inclemente ou se movendo cabisbaixas entre as construções."
        });

        // Parte 6 - A Realidade
        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Não são ruínas arqueológicas — é um sistema vivo, pulsante, alimentado por ganância e protegido pelo esquecimento."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Homens e mulheres se movem como sombras, seus corpos carregando o peso de gerações de cativeiro. Crianças que deveriam estar em escolas aprendem apenas a temer o estalar do chicote."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "No centro da propriedade, uma casa grande de estilo colonial contrasta com antenas parabólicas camufladas — o passado e o presente entrelaçados em uma perversão da história."
        });

        // Parte 7 - O Dilema
        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Abdias sente o sangue ferver em suas veias. A raiva ancestral se mistura com uma responsabilidade esmagadora."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Ele sabe que não pode enfrentar sozinho os guardas armados, mas também sabe que não pode simplesmente ir embora."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Cada respiração daquelas pessoas cativas é uma acusação contra um país que se recusa a encarar seus fantasmas."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Com mãos trêmulas, ele pega seu diário e começa a documentar tudo: nomes dos guardas que consegue ouvir, rotinas, pontos fracos na segurança."
        });

        // Parte 8 - O Clímax
        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Mas enquanto planeja sua retirada, um grito corta o ar. Uma criança foi descoberta tentando fugir. O castigo será exemplar."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "Abdias aperta o facão com força. A escolha agora é clara e terrível: permanecer escondido e garantir que a verdade venha à tona, ou agir imediatamente e arriscar tudo."
        });

        // Parte 9 - Conclusão
        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "O sol começa a se pôr, tingindo a floresta de vermelho-sangue. Seja qual for sua decisão, Abdias sabe que a partir deste momento, sua vida nunca mais será a mesma."
        });

        loreLines.Add(new DialogueLine
        {
            speaker = "Narrador",
            text = "A senzala oculta revelou seu segredo, e agora o passado e o presente se encontram em um confronto inevitável."
        });

        // Atribuir as linhas ao controlador de diálogo
        dialogueController.lines = loreLines;
    }
}