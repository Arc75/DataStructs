using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListAlg
{
    public class LinkedList<T> : IEnumerable<T>
    {
        //у связанного списка есть голова и хвост.
        //каждый элемент состоит из значения и ссылки на следующий элемент.
        Node<T> head;
        Node<T> tail;
        //и количество элементов.
        int _count;

        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            //если пусто - создаём голову.
            if (head == null)
                head = node;
            else
                tail.Next = node;

            tail = node;

            _count++;
        }

        public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> prev = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    //если узел не в начале(т.е. перед узлом что-то есть)
                    if (prev != null)
                    {
                        //устанавливаем предыдущему ссылку на следующий.
                        prev.Next = current.Next;

                        //если узел последний, то переустанавливаем хвост.
                        if (current.Next == null)
                            tail = prev;
                    }
                    else
                    {
                        //если узел первый, то переустановим голову на следующий элемент.
                        head = head.Next;
                        //если это был единственный элемент и мы его удалили, то очистим и хвост.
                        if (head == null)
                            tail = null;
                    }

                    _count--;
                    return true;
                }
                //перебираем вперёд.
                prev = current;
                current = current.Next;
            }
            return false;
        }

        public int Count { get { return _count; } }
        public bool IsEmpty { get { return _count == 0; } }

        public void Clear()
        {
            head = null;
            tail = null;
            _count = 0;
        }

        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }

            return false;
        }


        public void AppendFirst(T data)
        {
            //добавляем вначало. тупо прицепляемся спереди ссылкой на первый элемент.
            Node<T> node = new Node<T>(data)
            {
                Next = head
            };
            head = node;
            if (_count == 0)
            {
                tail = head;
            }
            _count++;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
