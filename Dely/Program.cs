using System;
using System.Collections.Generic;

namespace DailyPlanner
{
    class Program
    {
        static Dictionary<DateTime, List<Note>> notes = new Dictionary<DateTime, List<Note>>()
        {
            { new DateTime(2024, 1, 6), new List<Note>
                {
                    new Note("Заметка 1", "Описание заметки 1", new DateTime(2024, 1, 6)),
                    new Note("Заметка 2", "Описание заметки 2", new DateTime(2024, 1, 6))
                }
            },
            { new DateTime(2024, 1, 8), new List<Note>
                {
                    new Note("Заметка 3", "Описание заметки 3", new DateTime(2024, 1, 8)),
                    new Note("Заметка 4", "Описание заметки 4", new DateTime(2024, 1, 8))
                }
            },
            { new DateTime(2024, 1, 13), new List<Note>
                {
                    new Note("Заметка 5", "Описание заметки 5", new DateTime(2024, 1, 13))
                }
            }
        };

        static int currentDateIndex = 0;
        static List<DateTime> dates = new List<DateTime>(notes.Keys);

        static int currentNoteIndex = -1;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ShowMenu();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (currentNoteIndex == -1)
                            currentDateIndex = (currentDateIndex + 1) % dates.Count;
                        ShowMenu();
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentNoteIndex == -1)
                            currentDateIndex = (currentDateIndex + dates.Count - 1) % dates.Count;
                        ShowMenu();
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentNoteIndex != -1)
                            currentNoteIndex = (currentNoteIndex + 1) % notes[dates[currentDateIndex]].Count;
                        ShowMenu();
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentNoteIndex != -1)
                            currentNoteIndex = (currentNoteIndex + notes[dates[currentDateIndex]].Count - 1) % notes[dates[currentDateIndex]].Count;
                        ShowMenu();
                        break;
                    case ConsoleKey.Enter:
                        if (currentNoteIndex == -1)
                        {
                            currentNoteIndex = 0;
                            ShowMenu();
                        }
                        else
                        {
                            ShowNoteDetails();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Ежедневник");

            if (currentNoteIndex == -1)
            {
                Console.WriteLine("Выберите дату:");
                Console.WriteLine("-> " + dates[currentDateIndex].ToString("yyyy-MM-dd"));
            }
            else
            {
                DateTime selectedDate = dates[currentDateIndex];
                List<Note> selectedNotes = notes[selectedDate];

                Console.WriteLine($"Ежедневник - {selectedDate:yyyy-MM-dd}");
                Console.WriteLine("Выберите заметку:");

                for (int i = 0; i < selectedNotes.Count; i++)
                {
                    if (i == currentNoteIndex)
                        Console.WriteLine("-> " + selectedNotes[i].Title);
                    else
                        Console.WriteLine("   " + selectedNotes[i].Title);
                }
            }
        }

        static void ShowNoteDetails()
        {
            DateTime selectedDate = dates[currentDateIndex];
            List<Note> selectedNotes = notes[selectedDate];
            Note selectedNote = selectedNotes[currentNoteIndex];

            Console.Clear();
            Console.WriteLine("Детали заметки:");
            Console.WriteLine($"Название: {selectedNote.Title}");
            Console.WriteLine($"Описание: {selectedNote.Description}");
            Console.WriteLine($"Срок выполнения: {selectedNote.Deadline:yyyy-MM-dd}");
            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey(true);
            currentNoteIndex = -1;
            ShowMenu();
        }
    }

    class Note
    {
        public string Title { get; }
        public string Description { get; }
        public DateTime Deadline { get; }

        public Note(string title, string description, DateTime deadline)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
        }
    }
}