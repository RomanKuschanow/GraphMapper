namespace GraphMapper.Common.Conditions;
public interface ICondition
{
    bool GetConditionValue(Graph graph);
}
