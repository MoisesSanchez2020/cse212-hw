/*
 * CSE212 
 * (c) BYU-Idaho
 * 04-Teach - Problem 2
 * 
 * It is a violation of BYU-Idaho Honor Code to post or share this code with others or 
 * to post it online.  Storage into a personal and private repository (e.g. private
 * GitHub repository, unshared Google Drive folder) is acceptable.
 *
 */

/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        _maxSize = maxSize > 0 ? maxSize : 10;
    }

    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        public string Name { get; }
        public string AccountId { get; }
        public string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId}): {Problem}";
        }
    }

    public void AddNewCustomer(string name, string accountId, string problem) {
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    public void ServeCustomer() {
        if (_queue.Count == 0) {
            Console.WriteLine("No customers to serve.");
            return;
        }
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine("Serving customer: " + customer);
    }

    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => {String.Join(", ", _queue)}]";
    }

    public static void Run() {
        Console.WriteLine("Test 1: Initialize with valid size");
        var customerServiceValid = new CustomerService(3);
        Console.WriteLine(customerServiceValid);
        Console.WriteLine("=================");

        Console.WriteLine("Test 2: Initialize with invalid size");
        var customerServiceInvalid = new CustomerService(-1);
        Console.WriteLine(customerServiceInvalid);
        Console.WriteLine("=================");

        Console.WriteLine("Test 3: Add customer to non-full queue");
        customerServiceValid.AddNewCustomer("John Doe", "12345", "Cannot log in");
        Console.WriteLine(customerServiceValid);
        Console.WriteLine("=================");

        Console.WriteLine("Test 4: Attempt to add customer to full queue");
        customerServiceValid.AddNewCustomer("Jane Doe", "23456", "Issue with account");
        customerServiceValid.AddNewCustomer("Bob Smith", "34567", "Question about billing");
        // Attempt to add a fourth customer to a queue that can only hold three
        customerServiceValid.AddNewCustomer("Alice Brown", "45678", "Unable to access service");
        Console.WriteLine(customerServiceValid);
        Console.WriteLine("=================");

        Console.WriteLine("Test 5: Serve a customer from a non-empty queue");
        customerServiceValid.ServeCustomer(); // Should serve John Doe
        Console.WriteLine(customerServiceValid);
        Console.WriteLine("=================");

        Console.WriteLine("Test 6: Attempt to serve customer from empty queue");
        var customerServiceEmpty = new CustomerService(1);
        customerServiceEmpty.ServeCustomer(); // Should show error message
        Console.WriteLine("=================");
    }
}
