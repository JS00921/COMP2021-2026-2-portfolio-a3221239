using System;
using System.Collections.Generic;

namespace PracBTask1
{
    class TodoApp
    {
        static void Main()
        {
            // List<string> holding the tasks
            List<string> tasks = new List<string>();

            Console.WriteLine("=== ToDo Manager ===");
            Console.WriteLine("Commands: add [item] | show | remove [index] | clear | exit");
            Console.WriteLine();

            // Loop entry
            bool running = true;
            while (running)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                // Ignore blank input rather than crashing
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please type a command.");
                    continue;
                }

                // Split into command + argument
                string[] parts = input.Trim().Split(' ', 2);
                string command = parts[0].ToLower();
                string argument = parts.Length > 1 ? parts[1].Trim() : "";

                // switch matching user input to commands
                switch (command)
                {
                    case "add":
                        // No item supplied -> ask the user again
                        if (string.IsNullOrWhiteSpace(argument))
                        {
                            Console.Write("What would you like to add? ");
                            argument = Console.ReadLine()?.Trim();
                        }

                        // Validate the item to add
                        if (string.IsNullOrWhiteSpace(argument))
                        {
                            Console.WriteLine("Error: cannot add an empty task.");
                        }
                        else
                        {
                            tasks.Add(argument);
                            Console.WriteLine($"Added: \"{argument}\"");
                        }

                        break;

                    case "show":
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("Your to-do list is empty.");
                        }
                        else
                        {
                            Console.WriteLine("Your tasks:");
                            for (int i = 0; i < tasks.Count; i++)
                            {
                                // Display a 1-based number to the user
                                Console.WriteLine($"  {i + 1}. {tasks[i]}");
                            }
                        }

                        break;

                    case "remove":
                        // No index supplied
                        if (string.IsNullOrWhiteSpace(argument))
                        {
                            Console.Write("Which task number do you want to remove? ");
                            argument = Console.ReadLine()?.Trim();
                        }

                        // Validate the index: must be a number AND in range
                        if (!int.TryParse(argument, out int index))
                        {
                            Console.WriteLine($"Error: \"{argument}\" is not a valid number.");
                        }
                        else if (tasks.Count == 0)
                        {
                            Console.WriteLine("Error: there are no tasks to remove.");
                        }
                        else if (index < 1 || index > tasks.Count)
                        {
                            Console.WriteLine($"Error: no task at number {index}. " +
                                              $"Valid range is 1 to {tasks.Count}.");
                        }
                        else
                        {
                            string removed = tasks[index - 1]; // 1-based -> 0-based
                            tasks.RemoveAt(index - 1);
                            Console.WriteLine($"Removed: \"{removed}\"");
                        }

                        break;

                    case "clear":
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("Nothing to clear - the list is already empty.");
                        }
                        else
                        {
                            tasks.Clear();
                            Console.WriteLine("All tasks cleared.");
                        }

                        break;

                    case "exit":
                    case "quit":
                        running = false;
                        Console.WriteLine("Goodbye.");
                        break;
                    
                    default:
                        Console.WriteLine($"Error: unknown command \"{command}\". " +
                                          "Try: add, show, remove, clear, or exit.");
                        break;
                }
            }
        }
    }
}