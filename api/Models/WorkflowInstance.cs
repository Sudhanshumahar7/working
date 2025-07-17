using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class WorkflowInstance
    {
        public required string Id { get; set; }
        public required string WorkflowDefinitionId { get; set; }
        public required string CurrentStateId { get; set; }
        public List<TransitionRecord> History { get; set; } = new();
    }
    
    public class TransitionRecord
    {    
        public required string ActionId { get; set; }        
        public required DateTime Timestamp { get; set; }   
    }
}