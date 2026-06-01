namespace Medisys.DataStructures;

public class Node<T>(T data)
{
    public T Data { get; set; } = data;

    public Node<T>? Next { get; set; } = null;
}
