/*
 * CSE212 
 * (c) BYU-Idaho
 * 02-Prove - Problem 2
 * 
 * It is a violation of BYU-Idaho Honor Code to post or share this code with others or 
 * to post it online.  Storage into a personal and private repository (e.g. private
 * GitHub repository, unshared Google Drive folder) is acceptable.
 *
 */

public static class Priority {
    public static void Test() {
        // TODO Problem 2 - Write and run test cases and fix the code to match requirements
        // Example of creating and using the priority queue
        var priorityQueue = new PriorityQueue();
        Console.WriteLine(priorityQueue);

        // Test Cases

        // Test 1: Enqueue items with varying priorities and ensure the highest is dequeued first
        priorityQueue.Enqueue("Item1", 1);
        priorityQueue.Enqueue("Item2", 2);
        priorityQueue.Enqueue("Item3", 3);
        var dequeuedItem = priorityQueue.Dequeue();
        Console.WriteLine(dequeuedItem == "Item3" ? "Passed" : $"Failed: Expected 'Item3', got '{dequeuedItem}'");

        // Defect(s) Found: 

        Console.WriteLine("---------");

        // Test 2: Enqueue items with the same priority and ensure the FIFO order is preserved
        priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("ItemA", 1);
        priorityQueue.Enqueue("ItemB", 1);
        priorityQueue.Enqueue("ItemC", 1);
        dequeuedItem = priorityQueue.Dequeue();
        Console.WriteLine(dequeuedItem == "ItemA" ? "Passed" : $"Failed: Expected 'ItemA', got '{dequeuedItem}'");

        // Defect(s) Found: 

        Console.WriteLine("---------");

        // Test 3: Dequeue from an empty queue and expect an error
        priorityQueue = new PriorityQueue();
        try {
            dequeuedItem = priorityQueue.Dequeue();
            Console.WriteLine("Failed: Expected an error, but got an item.");
        }
        catch (InvalidOperationException ex) {
            Console.WriteLine("Passed: Caught expected error for empty queue.");
        }

        // Defect(s) Found: 

        // Add more Test Cases As Needed Below
    }
}
