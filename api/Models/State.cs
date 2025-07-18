using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class State
    {
        public required string Id { get; set; }       
        public bool IsInitial { get; set; }       
        public bool IsFinal { get; set; }         
        public bool Enabled { get; set; } = true;  
        public string? Description { get; set; }  
    }
}