using System;
using System.Collections.Generic;
using System.Linq;
using RiskSrc.Controller.Command.Multiple;
using RiskSrc.Controller.Command.Unique;
using RiskSrc.View;
using RiskSrc.View.Component;
using UnityEngine;

namespace RiskSrc.Controller.Command {
    public class RiskCommandInvoker {
        private static RiskCommandInvoker _instance;

        private readonly RiskEventPanel eventPanel;

        private readonly Dictionary<Type, AbstractRiskExclusiveCommand> exclusiveCommandDictionary;
        private readonly LinkedList<AbstractRiskMultipleCommand> multipleCommandDeque;

        private RiskCommandInvoker() {
            multipleCommandDeque = new LinkedList<AbstractRiskMultipleCommand>();
            exclusiveCommandDictionary = new Dictionary<Type, AbstractRiskExclusiveCommand>();
            eventPanel = RiskViewSearcher.instance.GetComponentByName<RiskEventPanel>("EventDisplayPanel");
            eventPanel.Show();
        }

        public static RiskCommandInvoker instance => _instance ?? (_instance = new RiskCommandInvoker());

        public bool ExecuteMultipleCommand(string playerName, AbstractRiskMultipleCommand command) {
            if (command.Start()
                && command.Execute()) {
                multipleCommandDeque.AddLast(command);
                eventPanel.AddEventDescription(playerName + " " +command.Description);
                Debug.Log(playerName + " " +command.Description);
                return true;
            } else {
                command.Rollback();
                return false;
            }
        }

        public bool CommitAllTransactionAndUpdateCheckpoint() {
            while (multipleCommandDeque.Count != 0) {
                var transaction = multipleCommandDeque.First();
                if (!transaction.Commit()) 
                    return false;
                multipleCommandDeque.RemoveFirst();
            }
            return true;
        }

        public bool AbortLastMultipleCommand() {
            var transaction = multipleCommandDeque.Last();
            multipleCommandDeque.RemoveLast();
            return transaction.Rollback();
        }

        public bool ExecuteExclusiveCommand(AbstractRiskExclusiveCommand command) {
            var type = command.GetType();
            if(exclusiveCommandDictionary.ContainsKey(type))
                exclusiveCommandDictionary[type].Rollback();
            exclusiveCommandDictionary[type] = command;
            return command.Execute();
        }

        public bool RollbackExclusiveCommand<T>() {
            var type = typeof(T);
            return exclusiveCommandDictionary.ContainsKey(type)
                   && exclusiveCommandDictionary[type].Rollback()
                   && exclusiveCommandDictionary.Remove(type);
        }
    }
}