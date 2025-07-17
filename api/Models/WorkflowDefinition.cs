using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class WorkflowDefinition
    {
        public required string Id { get; set; }                       
        public required List<State> States { get; set; } = new();      
        public required List<Action> Actions { get; set; } = new(); 
    }
}