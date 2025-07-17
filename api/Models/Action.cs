using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Action
    {
        public required string Id { get; set; }               
        public bool Enabled { get; set; } = true;            
        public required List<string> FromStates { get; set; } 
        public required string ToState { get; set; }          
    }
}