﻿using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public class Edge :ComponentList<IEdgeComponent>, IHaveId
{
    public Guid Id { get; init; }

    public Node NodeA { get; private set; }
    public Node NodeB { get; private set; }

    public Edge(Node nodeA, Node nodeB)
    {
        NodeA = nodeA;
        NodeB = nodeB;
    }

    public void ChangeNodes(Node a, Node b)
    {
        NodeA = a;
        NodeB = b;
    }
}
