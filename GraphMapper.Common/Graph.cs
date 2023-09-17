using GraphMapper.Common.Components;
using GraphMapper.Common.Conditions;
using System.Collections.Immutable;
using System.Reactive.Linq;
using System.Xml.Linq;

namespace GraphMapper.Common;
public class Graph : IHaveId
{
    private readonly List<Node> _nodes = new();
    private readonly List<Edge> _edges = new();
    private readonly List<ICondition> _conditions = new();

    public Guid Id { get; init; }
    public ImmutableList<Node> Nodes => _nodes.ToImmutableList();
    public ImmutableList<Edge> Edges => _edges.ToImmutableList();
    public ImmutableList<ICondition> Conditions => _conditions.ToImmutableList();

    public Graph() { }

    private Graph(IEnumerable<Node> nodes, IEnumerable<Edge> edges, IEnumerable<ICondition> conditions)
    {
        _nodes = nodes.ToList();
        _edges = edges.ToList();
        _conditions = conditions.ToList();
    }

    public void AddNode(Node node)
    {
        if (node is null)
            throw new ArgumentNullException(nameof(node));

        SimulateAndCheckConditions(g => g._nodes.Add(node));
        _nodes.Add(node);
        node.ComponentChanged
            .Subscribe(c => CheckConditionsAndCommitOrRollback(node));
    }

    public void RemoveNode(Node node)
    {
        EnsureNodesExist(new[] { node });

        SimulateAndCheckConditions(g => g._nodes.Remove(node));
        _nodes.Remove(node);
    }

    public void AddEdge(Node a, Node b)
    {
        EnsureNodesExist(a, b);

        SimulateAndCheckConditions(g => g._edges.Add(new(a, b)));
        Edge edge = new(a, b);
        _edges.Add(edge);
        edge.ComponentChanged
            .Subscribe(c => CheckConditionsAndCommitOrRollback(edge));
    }

    public void EditEdge(Edge edge, Node a, Node b)
    {
        EnsureEdgeExists(edge);
        EnsureNodesExist(a, b);

        SimulateAndCheckConditions(g => { g._edges.Remove(edge); var e = edge.Clone(); e.ChangeNodes(a, b); g._edges.Add(e); });
        edge.ChangeNodes(a, b);
    }

    public void RemoveEdge(Edge edge)
    {
        EnsureEdgeExists(edge);

        SimulateAndCheckConditions(g => g._edges.Remove(edge));
        _edges.Remove(edge);
    }

    public void AddCondition(ICondition condition)
    {
        if (condition is null)
            throw new ArgumentNullException(nameof(condition));

        SimulateAndCheckConditions(g => g._conditions.Add(condition));
        _conditions.Add(condition);
    }

    public void RemoveCondition(ICondition condition)
    {
        if (Conditions.Contains(condition))
        {
            SimulateAndCheckConditions(g => g._conditions.Remove(condition));
            _conditions.Remove(condition);
        }
        else
            throw new InvalidOperationException($"The condition '{condition}' is not contained in the graph");
    }

    private IEnumerable<ICondition> CheckConditions(Graph graph)
    {
        return Conditions.Where(c => !c.GetConditionValue(graph));
    }

    private void EnsureNodesExist(params Node[] nodes)
    {
        if (!nodes.All(node => Nodes.Contains(node)))
            throw new InvalidOperationException($"All nodes should be contained in the graph");
    }

    private void EnsureEdgeExists(Edge edge)
    {
        if (!Edges.Contains(edge))
            throw new InvalidOperationException($"The edge '{edge}' is not contained in the graph");
    }

    private void SimulateAndCheckConditions(Action<Graph> action)
    {
        var g = Clone();
        action(g);

        var notSuitable = CheckConditions(g);
        if (notSuitable.Any())
            throw new InvalidOperationException($"Conditions {string.Join(", ", notSuitable)} not satisfied");
    }

    private void CheckConditionsAndCommitOrRollback(Node node)
    {
        try
        {
            SimulateAndCheckConditions(g => 
            {
                Node n = node.Clone(); 
                n.CommitChanges(); 
                g._nodes.Remove(node);
                g._nodes.Add(n); 
            });
            node.CommitChanges();
        }
        catch
        {
            node.RollbackChanges();
            throw;
        }
    }

    private void CheckConditionsAndCommitOrRollback(Edge edge)
    {
        try
        {
            SimulateAndCheckConditions(g => 
            {
                Edge e = edge.Clone();
                e.CommitChanges();
                g._edges.Remove(edge);
                g._edges.Add(e);
            });
            edge.CommitChanges();
        }
        catch
        {
            edge.RollbackChanges();
            throw;
        }
    }

    public Graph Clone() => new(Nodes, Edges, Conditions);
}
