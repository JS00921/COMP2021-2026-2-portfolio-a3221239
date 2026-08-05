// Task 2: Dictionary Lookups and Error Handling (30 m)
// Add a “tags” feature using Dictionary<string, List<int>> mapping tag → indices.
//
//     This tags an item on the todo list. As an example, by adding the "urgent" tag to the first and last item on your todo list.
//
//     Users can then retrieve the items tagged with "urgent".
//
//     Procedure
// Commands: tag [index] [name], get-tagged [tag].
//     Handle missing keys, duplicates, and out-of-range indices with exceptions caught gracefully.
//     Output
//     Updated source files.
//     Screenshot of Console showing tag and get-tagged commands working correctly.

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

            // Dictionary mapping a tag name
            Dictionary<string, List<int>> tags = new Dictionary<string, List<int>>();

            Console.WriteLine("=== ToDo Manager ===");
            Console.WriteLine("Commands: add [item] | show | remove [index] | clear | exit");
            Console.WriteLine("          tag [index] [name] | get-tagged [tag]");
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
                                // Collect any tags attached to this task
                                List<string> onThisTask = new List<string>();
                                foreach (KeyValuePair<string, List<int>> entry in tags)
                                {
                                    if (entry.Value.Contains(i))
                                    {
                                        onThisTask.Add(entry.Key);
                                    }
                                }

                                string tagLabel = onThisTask.Count == 0
                                    ? ""
                                    : $"  [{string.Join(", ", onThisTask)}]";

                                // Display a 1-based number to the user
                                Console.WriteLine($"  {i + 1}. {tasks[i]}{tagLabel}");
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
                            int gone = index - 1;              // 1-based -> 0-based
                            string removed = tasks[gone];
                            tasks.RemoveAt(gone);

                            // Keep tag indices correct after the removal
                            foreach (KeyValuePair<string, List<int>> entry in tags)
                            {
                                List<int> positions = entry.Value;
                                positions.Remove(gone);

                                // Everything after the removed task shifts down by one
                                for (int i = 0; i < positions.Count; i++)
                                {
                                    if (positions[i] > gone)
                                    {
                                        positions[i] = positions[i] - 1;
                                    }
                                }
                            }

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
                            tags.Clear();
                            Console.WriteLine("All tasks and tags cleared.");
                        }

                        break;

                    case "tag":
                        // No arguments supplied -> ask the user again
                        if (string.IsNullOrWhiteSpace(argument))
                        {
                            Console.Write("Which task number and tag name? (e.g. 1 urgent) ");
                            argument = Console.ReadLine()?.Trim();
                        }

                        // Split the argument into [index] and [name]
                        string[] tagParts = (argument ?? "").Split(' ', 2);

                        if (tagParts.Length < 2 || string.IsNullOrWhiteSpace(tagParts[1]))
                        {
                            Console.WriteLine("Error: tag needs both an index and a name, " +
                                              "e.g. \"tag 1 urgent\".");
                        }
                        else
                        {
                            string tagName = tagParts[1].Trim().ToLower();

                            try
                            {
                                // Out-of-range and non-numeric indices are raised as exceptions
                                if (!int.TryParse(tagParts[0], out int tagIndex))
                                {
                                    throw new FormatException(
                                        $"\"{tagParts[0]}\" is not a valid number.");
                                }

                                if (tagIndex < 1 || tagIndex > tasks.Count)
                                {
                                    throw new ArgumentOutOfRangeException(nameof(tagIndex),
                                        $"no task at number {tagIndex}. " +
                                        $"Valid range is 1 to {tasks.Count}.");
                                }

                                int target = tagIndex - 1;

                                // Missing key: create the entry the first time this tag is used
                                if (!tags.ContainsKey(tagName))
                                {
                                    tags[tagName] = new List<int>();
                                }

                                // Duplicate: this task already carries this tag
                                if (tags[tagName].Contains(target))
                                {
                                    Console.WriteLine($"Note: \"{tasks[target]}\" is already " +
                                                      $"tagged \"{tagName}\".");
                                }
                                else
                                {
                                    tags[tagName].Add(target);
                                    Console.WriteLine($"Tagged \"{tasks[target]}\" " +
                                                      $"as \"{tagName}\".");
                                }
                            }
                            catch (FormatException error)
                            {
                                Console.WriteLine($"Error: {error.Message}");
                            }
                            catch (ArgumentOutOfRangeException error)
                            {
                                Console.WriteLine($"Error: {error.Message}");
                            }
                        }

                        break;

                    case "get-tagged":
                        // No tag supplied -> ask the user again
                        if (string.IsNullOrWhiteSpace(argument))
                        {
                            Console.Write("Which tag do you want to look up? ");
                            argument = Console.ReadLine()?.Trim();
                        }

                        if (string.IsNullOrWhiteSpace(argument))
                        {
                            Console.WriteLine("Error: please supply a tag name.");
                        }
                        else
                        {
                            string lookup = argument.Trim().ToLower();

                            try
                            {
                                // Direct lookup: throws KeyNotFoundException if the tag is missing
                                List<int> tagged = tags[lookup];

                                if (tagged.Count == 0)
                                {
                                    Console.WriteLine($"No tasks are currently tagged " +
                                                      $"\"{lookup}\".");
                                }
                                else
                                {
                                    Console.WriteLine($"Tasks tagged \"{lookup}\":");
                                    foreach (int position in tagged)
                                    {
                                        Console.WriteLine($"  {position + 1}. {tasks[position]}");
                                    }
                                }
                            }
                            catch (KeyNotFoundException)
                            {
                                string known = tags.Count == 0
                                    ? "(none yet)"
                                    : string.Join(", ", tags.Keys);

                                Console.WriteLine($"Error: no tag named \"{lookup}\" exists. " +
                                                  $"Known tags: {known}");
                            }
                        }

                        break;

                    case "exit":
                    case "quit":
                        running = false;
                        Console.WriteLine("Goodbye.");
                        break;

                    default:
                        Console.WriteLine($"Error: unknown command \"{command}\". " +
                                          "Try: add, show, remove, clear, tag, get-tagged, or exit.");
                        break;
                }
            }
        }
    }
}