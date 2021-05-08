using System;
using System.Collections.Generic;

namespace LRU
{

    public class LRUCache<T>
    {
        private int capacity, size;
        private Dictionary<string, Node> hashmap;
        private DoublyLinkedList intenalQueue;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            hashmap = new Dictionary<string, Node>();
            intenalQueue = new DoublyLinkedList();
        }

        public T get(string key)
        {
            Node node;
            if(hashmap.TryGetValue(key, out node))
            {
                intenalQueue.MoveNodeToFront(node);
                return node.value;
            }
            else
            {
                return default(T); // null
            }
        }

        public void put(string key, T value)
        {
            Node currentNode;
            if (hashmap.TryGetValue(key, out currentNode))
            {
                currentNode.value = value;
                intenalQueue.MoveNodeToFront(currentNode);
            }
            else
            {
                if(size == capacity)
                {
                    string rearNodeKey = intenalQueue.GetRearKey();
                    intenalQueue.RemoveNodeFromRear();
                    hashmap.Remove(rearNodeKey);
                    size--;

                }

                Node node = new Node(key, value);
                intenalQueue.AddNodeToFront(node);
                hashmap.Add(key, node);
                size++;

            }
        }

        class DoublyLinkedList
        {
            private Node front, rear;
            public DoublyLinkedList()
            {
                front = rear = null;
            }

            public void AddNodeToFront(Node node)
            {
                if(rear == null)
                {
                    front = rear = node;
                    return;
                }
                node.next = front;
                front.prev = node;
                front = node;

            }

            public void MoveNodeToFront(Node node)
            {
                if(front == node)
                {
                    return;
                }

                if(node == rear)
                {
                    rear = rear.prev;
                    rear.next = null;
                }
                else
                {
                    node.prev.next = node.next;
                    node.next.prev = node.prev;
                }

                node.prev = null;
                node.next = front;
                front.prev = node;
                front = node;

            }

            public void RemoveNodeFromRear()
            {
                if(rear == null)
                {
                    return;
                }
                Console.WriteLine("Deleting key:" + rear.key);
                if(front == rear)
                {
                    front = rear = null;
                }
                else
                {
                    rear = rear.prev;
                    rear.next = null;
                }
            }

            public string GetRearKey()
            {
                return rear.key;
            }
        }

        class Node
        {
            public string key;
            public T value;
            public Node next, prev;
            public Node(string key, T value)
            {
                this.key = key;
                this.value = value;
                this.next = null;
                this.prev = null;
            }
        }

    }

    public class UseCache
    {
        static void Main(string[] args)
        {
            LRUCache<String> cache = new LRUCache<string>(3);
            int choice = 1;

            //choice = int.Parse(Console.ReadLine());

            while(choice != 0)
            {
                Console.WriteLine("1: Put");
                Console.WriteLine("2: Get");
                Console.WriteLine("0: Exit");
                choice = int.Parse(Console.ReadLine());
                string key;
                string value;
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter key");
                        key = Console.ReadLine();
                        Console.WriteLine("Enter Value");
                        value = Console.ReadLine();
                        cache.put(key, value);

                        Console.WriteLine("Inserted");
                        break;
                    case 2:
                        Console.WriteLine("Enter key");
                        key = Console.ReadLine();
                        Console.WriteLine("Value is: "+ cache.get(key));
                        break;
                    default:
                        Console.WriteLine("Stay safe! Wear mask! GOODBYE!!");
                        break;
                }
            }
        }
    }
}
