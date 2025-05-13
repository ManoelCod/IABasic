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
        // Encontrar a melhor correspondência na base de dados
        var resultado = baseDeDados.Keys
            .OrderBy(pergunta => CalcularSimilaridade(pergunta, consulta))
            .LastOrDefault();

        return resultado != null ? baseDeDados[resultado] : "Não encontrei uma resposta. Você pode adicionar uma nova pergunta e resposta?";
    }

    private int CalcularSimilaridade(string a, string b)
    {
        return a.Intersect(b).Count(); // Simples métrica de similaridade
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
            Console.WriteLine(resposta);
        }
    }
}
