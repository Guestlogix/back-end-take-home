using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRoutesApi.Models
{

    public class Node<T>
    {
        public T Value;

        public Node(T val)
        {
            Value = val;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
