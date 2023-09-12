﻿using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public class Node :ComponentList, IHaveId
{
    public Guid Id { get; init; }

    public Node()
    {
        Id = Guid.NewGuid();
    }
}
