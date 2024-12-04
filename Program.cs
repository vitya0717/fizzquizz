using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Question> questions = new List<Question>();
        string[] lines = File.ReadAllLines("kerdesek.txt");

        for (int k = 0; k < lines.Length; k++)
        {
            string line = lines[k].Trim();

            if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("a)") && !line.StartsWith("b)") && !line.StartsWith("c)") && !line.StartsWith("d)"))
            {
                string questionText = line;  // Kérdés szövege
                List<string> answers = new List<string>();

                for (int j = 1; j <= 4; j++)
                {
                    if (k + j < lines.Length)
                    {
                        answers.Add(lines[k + j].Trim());
                    }
                }

                int correctAnswerIndex = -1;
                for (int j = 1; j <= 4; j++)
                {
                    if (lines[k + j].StartsWith("->"))
                    {
                        correctAnswerIndex = j - 1;
                        answers[j - 1] = answers[j - 1].Substring(2).Trim();
                        break;
                    }
                }

                questions.Add(new Question(questionText, answers.ToArray(), correctAnswerIndex));
                k += 4;
            }
        }

        int score = 0;

        Console.WriteLine("Üdvözöllek a kvízben! Válaszd ki a helyes választ (1-4):\n");

        int i = 0;

        Random random = new Random();

        while (questions.Count != 0)
        {
            Console.WriteLine(questions.Count);
            int num = random.Next(questions.Count - 1);
            Console.WriteLine(num);
            Console.WriteLine($"{questions[num].Text}");
            for (int j = 0; j < questions[num].Options.Length; j++)
            {
                Console.WriteLine($"{j + 1}. {questions[num].Options[j]}");
            }

            int userAnswer;
            while (true)
            {
                Console.Write("Válaszod (1-4): ");
                if (int.TryParse(Console.ReadLine(), out userAnswer) && userAnswer >= 1 && userAnswer <= 4)
                {
                    break;
                }
                Console.WriteLine("Érvénytelen válasz. Kérlek, adj meg egy számot 1 és 4 között!");
            }

            if (userAnswer - 1 == questions[num].CorrectOption)
            {
                Console.WriteLine("Helyes válasz!\n");
                questions.Remove(questions[num]);
                if (questions.Count == 0)
                {
                    Console.WriteLine("VÉGE VAN, INDÍTSD ÚJRA!");
                    break;
                }
                score++;
            }
            else
            {
                Console.WriteLine($"Rossz válasz. A helyes válasz: {questions[num].Options[questions[num].CorrectOption]}\n");
            }
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine($"Kvíz vége! Pontszámod: {score}/{52}");
    }
}

// Egy osztály a kérdések tárolására
class Question
{
    public string Text { get; }
    public string[] Options { get; }
    public int CorrectOption { get; }

    public Question(string text, string[] options, int correctOption)
    {
        Text = text;
        Options = options;
        CorrectOption = correctOption;
    }
}
