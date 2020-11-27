using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OmniXaml
{
    public static class NodeExtensions
    {
        public static IEnumerable<ConstructionNode> GetAllChildren(this ConstructionNode node)
        {
            return node.Children.Concat(node.Assignments.SelectMany(assignment => assignment.Values));
        }

        public static ConstructionNode WithAssignments(this ConstructionNode node, IEnumerable<MemberAssignment> assignment)
        {
            foreach (var ass in assignment)
            {
                node.Assignments.Add(ass);
            }

            return node;
        }

        public static ConstructionNode WithAssignment<T>(this ConstructionNode<T> node, Expression<Func<T, object>> selector, string value)
        {
            node.Assignments.Add(new MemberAssignment<T>(selector, value));

            return node;
        }

        public static ConstructionNode WithAssignment<TParent, TSub>(this ConstructionNode<TParent, TSub> node, Expression<Func<TSub, object>> selector, string value) where TSub : TParent
        {
            node.Assignments.Add(new MemberAssignment<TSub>(selector, value));

            return node;
        }

        public static ConstructionNode WithAssignment<T>(this ConstructionNode<T> node, string memberName, string value)
        {
            node.Assignments.Add(new MemberAssignment<T>(memberName, value));

            return node;
        }

        public static ConstructionNode WithAssignment<TParent, TSub>(this ConstructionNode<TParent, TSub> node, string memberName, string value) where TSub : TParent
        {
            node.Assignments.Add(new MemberAssignment<TSub>(memberName, value));

            return node;
        }

        public static ConstructionNode WithAssignments<T>(this ConstructionNode<T> node, params (Expression<Func<T, object>>, string)[] assignments)
        {
            foreach (var ass in assignments)
            {
                node.Assignments.Add(new MemberAssignment<T>(ass.Item1, ass.Item2));
            }

            return node;
        }

        public static ConstructionNode WithChildren(this ConstructionNode node, params ConstructionNode[] children)
        {
            foreach (var a in children)
            {
                node.Children.Add(a);
            }

            return node;
        }

        public static ConstructionNode WithChildren(this ConstructionNode node, IEnumerable<ConstructionNode> children)
        {
            foreach (var a in children)
            {
                node.Children.Add(a);
            }

            return node;
        }

        public static ConstructionNode WithAssignment<T>(this ConstructionNode<T> node, Expression<Func<T, object>> selector, ConstructionNode childNode)
        {
            node.Assignments.Add(new MemberAssignment<T>(selector, childNode));

            return node;
        }

        public static ConstructionNode WithAttachedAssignment<TAttachedOwner>(this ConstructionNode node, string attachableMemberName, string value)
        {
            node.Assignments.Add(new MemberAssignment()
            {
                Member = Member.FromAttached<TAttachedOwner>(attachableMemberName),
                SourceValue = value,
            });

            return node;
        }

        public static ConstructionNode WithAttachedAssignment<TAttachedOwner>(this ConstructionNode node, string attachableMemberName, ConstructionNode childNode)
        {
            node.Assignments.Add(new MemberAssignment
            {
                Member = Member.FromAttached<TAttachedOwner>(attachableMemberName),
                Values = new List<ConstructionNode>() { childNode },
            });

            return node;
        }
    }
}