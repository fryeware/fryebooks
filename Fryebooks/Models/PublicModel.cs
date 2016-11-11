using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class PublicModel
    {
    }

    public class Node
    {
        public string id { get; set; }
        public int group { get; set; }
    }

    public class Link
    {
        public string source { get; set; }
        public string target { get; set; }
        public int value { get; set; }
        public int distance { get; set; }
    }

    public class GraphObject
    {
        public List<Node> nodes { get; set; }
        public List<Link> links { get; set; }
    }
}