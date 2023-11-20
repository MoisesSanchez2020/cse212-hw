using System.Collections;

public class LinkedList : IEnumerable<int> {
    private Node? _head;
    private Node? _tail;

    public void InsertHead(int value) {
        Node newNode = new Node(value);
        if (_head is null) {
            _head = newNode;
            _tail = newNode;
        }
        else {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    public void InsertTail(int value) {
        Node newNode = new Node(value);
        if (_head is null) {
            _head = newNode;
            _tail = newNode;
        }
        else {
            _tail.Next = newNode;
            newNode.Prev = _tail;
            _tail = newNode;
        }
    }

    public void RemoveHead() {
        if (_head == _tail) {
            _head = null;
            _tail = null;
        }
        else if (_head is not null) {
            _head.Next!.Prev = null;
            _head = _head.Next;
        }
    }

    public void RemoveTail() {
        if (_head == _tail) {
            RemoveHead();
        }
        else if (_tail is not null) {
            _tail.Prev!.Next = null;
            _tail = _tail.Prev;
        }
    }

    public void InsertAfter(int value, int newValue) {
        Node? curr = _head;
        while (curr is not null) {
            if (curr.Data == value) {
                if (curr == _tail) {
                    InsertTail(newValue);
                }
                else {
                    Node newNode = new Node(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    public void Remove(int value) {
        Node? curr = _head;
        while (curr is not null) {
            if (curr.Data == value) {
                if (curr == _head) {
                    RemoveHead();
                }
                else if (curr == _tail) {
                    RemoveTail();
                }
                else {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    public void Replace(int oldValue, int newValue) {
        Node? curr = _head;
        while (curr is not null) {
            if (curr.Data == oldValue) {
                curr.Data = newValue;
            }
            curr = curr.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
    }

    public IEnumerator<int> GetEnumerator() {
        Node? curr = _head;
        while (curr is not null) {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    public IEnumerable<int> Reverse() {
        Node? curr = _tail;
        while (curr is not null) {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString() {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    private class Node {
        public int Data { get; set; }
        public Node? Next { get; set; }
        public Node? Prev { get; set; }

        public Node(int data) {
            this.Data = data;
            this.Next = null;
            this.Prev = null;
        }
    }
}