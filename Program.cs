using System;
using System.Collections.Generic;
using System.Linq;

class SimplesIA
{
    private Dictionary<string, string> baseDeDados;

    public SimplesIA()
    {
        baseDeDados = new Dictionary<string, string>
        {
            { "Qual é a capital do Brasil?", "A capital do Brasil é Brasília." },
            { "Como funciona a gravidade?", "A gravidade é a força que atrai objetos para o centro da Terra." },
            { "Quem descobriu o Brasil?", "O Brasil foi descoberto por Pedro Álvares Cabral em 1500." },
            { "O que é inteligência artificial?", "Inteligência artificial é a capacidade de uma máquina realizar tarefas que normalmente requerem inteligência humana." },
            { "Qual é a fórmula da água?", "A fórmula química da água é H₂O." }
        };
    }

    public string Pesquisar(string consulta)
    {
        var resultado = baseDeDados.Keys
            .OrderBy(pergunta => CalcularDistanciaLevenshtein(pergunta, consulta))
            .FirstOrDefault();

        if (resultado != null && CalcularDistanciaLevenshtein(resultado, consulta) < consulta.Length / 2) // Ajuste para definir um limiar de similaridade
        {
            return baseDeDados[resultado];
        }
        else
        {
            return null; // Não encontrou resposta
        }
    }

    public void AdicionarPergunta(string pergunta, string resposta)
    {
        if (!baseDeDados.ContainsKey(pergunta))
        {
            baseDeDados.Add(pergunta, resposta);
            Console.WriteLine("Pergunta e resposta adicionadas à base de conhecimento!");
        }
        else
        {
            Console.WriteLine("Essa pergunta já existe na base de conhecimento.");
        }
    }

    private int CalcularDistanciaLevenshtein(string a, string b)
    {
        int[,] distancias = new int[a.Length + 1, b.Length + 1];

        for (int i = 0; i <= a.Length; i++)
            distancias[i, 0] = i;
        for (int j = 0; j <= b.Length; j++)
            distancias[0, j] = j;

        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int custo = (a[i - 1] == b[j - 1]) ? 0 : 1;
                distancias[i, j] = Math.Min(
                    Math.Min(distancias[i - 1, j] + 1, distancias[i, j - 1] + 1),
                    distancias[i - 1, j - 1] + custo);
            }
        }

        return distancias[a.Length, b.Length];
    }

    static void Main()
    {
        SimplesIA ia = new SimplesIA();
        while (true)
        {
            Console.WriteLine("Digite sua pergunta (ou digite 'sair' para encerrar):");
            string entrada = Console.ReadLine();

            if (entrada?.Trim().ToLower() == "sair")
                break;

            string resposta = ia.Pesquisar(entrada);

            if (resposta != null)
            {
                Console.WriteLine(resposta);
            }
            else
            {
                Console.WriteLine("Não encontrei uma resposta. Por favor, digite a resposta para essa pergunta:");
                string novaResposta = Console.ReadLine();
                ia.AdicionarPergunta(entrada, novaResposta);
            }
        }
    }
}