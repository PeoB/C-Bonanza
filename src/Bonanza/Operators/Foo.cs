using System;

namespace Operators
{
    public class Foo
    {
        public Foo(int i)
        {
            Value = i;
        }

        public Foo() { }

        public int Value { get; private set; }
        
        public static Foo operator &(Foo foo, Foo foo2)
        {
            return new Foo { Value = foo.Value & foo2.Value };
        }
        public static implicit operator int(Foo foo)
        {
            return foo.Value;
        } 

        public static implicit operator Foo(int i)
        {
            return new Foo(i);
        }

        public static explicit operator bool(Foo foo)
        {
            return !!foo;
        }
        public static explicit operator Foo(bool b)
        {
            return b ? new Foo() : null;
        }
        public static TimeSpan operator &(Foo foo, TimeSpan span)
        {
            return TimeSpan.FromSeconds(foo.Value) + span;
        }

        public static bool operator !(Foo foo)
        {
            return foo ? false : true;
        }

        public static bool operator true(Foo foo)
        {
            return foo != null;
        }

        public static bool operator false(Foo foo)
        {
            return foo == null;
        }
    }
}