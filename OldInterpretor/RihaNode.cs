using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OldInterpreter
{

    public class RihaNode
    {
        private ValueType _type = ValueType.auto;
        private object _value;

        public RihaNode(ValueType type, object value = null)
        {
            _type = type;
            _value = value;
        }

        public override string ToString() => GetString();

        public string GetString() => _type != ValueType.array ? _value.ToString() : ((List<RihaNode>) _value).Aggregate("", (current, node) => current + (node.GetString() + ","));

        public object GetValue() => _value;

        public void SetType(ValueType type) => this._type = type;

        public ValueType GetNodeType() => _type;

        public void Add(RihaNode addNode)
        {
            switch (_type)
            {
                case ValueType.number:
                {
                    var add = addNode.GetNodeType() == ValueType.number ? GetNumeric(addNode.GetValue()) : 0;
                    var newValue = GetNumeric(_value) + add;
                    _value = newValue.ToString();
                    break;
                }
                case ValueType.text:
                    _value += addNode.GetString();
                    break;
                case ValueType.array:
                    ((List<RihaNode>) _value).Add(addNode);
                    break;
            }
        }

        public float GetSize()
        {
            return _type switch
            {
                ValueType.number => float.Parse(GetValue().ToString()),
                ValueType.array => ((List<RihaNode>) _value).Count,
                ValueType.boolean => ((bool) _value ? 1 : 0),
                _ => GetString().Length
            };
        }

        public RihaNode get(RihaNode[] parameters)
        {
            var id = (int) parameters[0].GetSize();
            return ((List<RihaNode>) _value)[id];
        }

        public RihaNode set(RihaNode[] parameters)
        {
            var p = parameters[^1];
            _value = p._value;
            _type = p.GetNodeType();
            return this;
        }

        public RihaNode equal(RihaNode[] parameters)
        {
            var ret = false;
            var comparisionNode = parameters[^1];
            if (comparisionNode.GetNodeType() == GetNodeType())
                if (comparisionNode.GetString() == GetString())
                    ret = true;

            return new RihaNode(ValueType.boolean, ret);
        }

        public RihaNode equal_type(RihaNode[] parameters)
        {
            var ret = false;
            var comparisonNode = parameters[^1];
            if (comparisonNode.GetNodeType() == GetNodeType()) ret = true;
            return new RihaNode(ValueType.boolean, ret);
        }

        public RihaNode bigger_than(RihaNode[] parameters)
        {
            var ret = false;
            var comparisonNode = parameters[^1];
            if (comparisonNode.GetSize() < GetSize()) ret = true;
            return new RihaNode(ValueType.boolean, ret);
        }

        public RihaNode less_than(RihaNode[] parameters)
        {
            var ret = false;
            var comparisonNode = parameters[^1];
            if (comparisonNode.GetSize() > GetSize()) ret = true;
            return new RihaNode(ValueType.boolean, ret);
        }

        public RihaNode GlobalCall(string method, RihaNode[] par)
        {
            var functionMethod = _value.GetType().GetMethod(method);
            if (functionMethod == null) return null;
            object[] parameters = {par};
            var returnValue = (RihaNode) functionMethod.Invoke(_value, parameters);
            return returnValue;
        }

        private static float GetNumeric(object value)
        {
            var isNumeric = float.TryParse(value.ToString(), out var n);
            return isNumeric ? n : 0;
        }

        private static bool IsNumeric(object value)
        {
            var isNumeric = float.TryParse(value.ToString(), out var n);
            return isNumeric;
        }

        private static bool IsList(object value) => value is IList && value.GetType().IsGenericType;
    }
}