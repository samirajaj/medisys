using Medisys.Models.Common;

namespace Medisys.DataStructures;

public class SortedLinkedList<T> where T : IEntity
{
    private Node<T>? _head;

    public void InsertSorted(T item)
    {
        if (Find(item.Id) is not null)
        {
            throw new InvalidOperationException($"Item with id {item.Id} already exists.");
        }

        Node<T> newNode = new(item);

        if (_head is null || item.Id < _head.Data.Id)
        {
            newNode.Next = _head;
            _head = newNode;
            return;
        }

        Node<T> current = _head;

        while (current.Next is not null && current.Next.Data.Id < item.Id)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
    }

    public bool Delete(int id)
    {
        if (_head is null)
            return false;

        if (_head.Data.Id == id)
        {
            _head = _head.Next;
            return true;
        }

        Node<T> current = _head;

        while (current.Next is not null)
        {
            if (current.Next.Data.Id == id)
            {
                current.Next = current.Next.Next;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public T? Find(int id)
    {
        Node<T>? current = _head;

        while (current is not null)
        {
            if (current.Data.Id == id)
                return current.Data;

            current = current.Next;
        }

        return default;
    }

    public IEnumerable<T> GetAll()
    {
        Node<T>? current = _head;

        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    public bool Exists(int id)
    {
        return Find(id) is not null;
    }

    public int Count()
    {
        int count = 0;

        Node<T>? current = _head;

        while (current is not null)
        {
            count++;
            current = current.Next;
        }

        return count;
    }
}
