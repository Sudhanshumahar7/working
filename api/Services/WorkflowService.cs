using api.models;

namespace api.Services
{
    public class WorkflowService
    {
        private readonly Dictionary<string, WorkflowDefinition> _definitions = new();
        private readonly Dictionary<string, WorkflowInstance> _instances = new();

        public bool CreateWorkflowDefinition(WorkflowDefinition definition, out string error)
        {
            error = string.Empty;

            if (_definitions.ContainsKey(definition.Id))
            {
                error = "Workflow definition with this ID already exists.";
                return false;
            }

            if (definition.States.Count(s => s.IsInitial) != 1)
            {
                error = "Workflow must contain exactly one initial state.";
                return false;
            }

            var stateIds = definition.States.Select(s => s.Id).ToHashSet();
            foreach (var action in definition.Actions)
            {
                if (!stateIds.Contains(action.ToState) || !action.FromStates.All(s => stateIds.Contains(s)))
                {
                    error = $"Action '{action.Id}' contains invalid state references.";
                    return false;
                }
            }

            _definitions[definition.Id] = definition;
            return true;
        }

        public WorkflowInstance? StartWorkflowInstance(string definitionId)
        {
            if (!_definitions.TryGetValue(definitionId, out var definition))
                return null;

            var initialState = definition.States.First(s => s.IsInitial);

            var instance = new WorkflowInstance
            {
                Id = Guid.NewGuid().ToString(),
                WorkflowDefinitionId = definitionId,
                CurrentStateId = initialState.Id,
                History = new List<TransitionRecord>()
            };

            _instances[instance.Id] = instance;
            return instance;
        }

        public bool ExecuteAction(string instanceId, string actionId, out string error)
        {
            error = string.Empty;

            if (!_instances.TryGetValue(instanceId, out var instance))
            {
                error = "Instance not found.";
                return false;
            }

            if (!_definitions.TryGetValue(instance.WorkflowDefinitionId, out var definition))
            {
                error = "Definition not found.";
                return false;
            }

            var currentState = instance.CurrentStateId;
            var action = definition.Actions.FirstOrDefault(a => a.Id == actionId);

            if (action == null)
            {
                error = "Action not found.";
                return false;
            }

            if (!action.Enabled)
            {
                error = "Action is disabled.";
                return false;
            }

            if (!action.FromStates.Contains(currentState))
            {
                error = $"Action '{action.Id}' cannot be executed from current state '{currentState}'.";
                return false;
            }

            var targetState = definition.States.FirstOrDefault(s => s.Id == action.ToState);
            if (targetState == null || !targetState.Enabled)
            {
                error = $"Target state '{action.ToState}' is invalid or disabled.";
                return false;
            }

            if (definition.States.First(s => s.Id == currentState).IsFinal)
            {
                error = "Cannot perform action on a final state.";
                return false;
            }

            instance.CurrentStateId = targetState.Id;
            instance.History.Add(new TransitionRecord
            {
                ActionId = action.Id,
                Timestamp = DateTime.UtcNow
            });

            return true;
        }

        public WorkflowInstance? GetInstance(string instanceId)
        {
            _instances.TryGetValue(instanceId, out var instance);
            return instance;
        }

        public WorkflowDefinition? GetDefinition(string id)
        {
            _definitions.TryGetValue(id, out var def);
            return def;
        }
    }
}