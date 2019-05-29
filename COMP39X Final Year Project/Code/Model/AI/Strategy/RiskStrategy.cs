using System.Collections;
using System.Collections.Generic;

namespace RiskSrc.Model.AI.Strategy {
    
    public class RiskStrategy<T>: IEnumerable<T> where T:IRiskAction  {
        
        private int currentIndex;
        private readonly Dictionary<string, T> strategyCore;

        public RiskStrategy() {
            strategyCore = new Dictionary<string, T>();
        }
        public void Add(T newAction) {
            if (strategyCore.ContainsKey(newAction.ActionKey)) 
                newAction.ArmiesSize += strategyCore[newAction.ActionKey].ArmiesSize;
            strategyCore[newAction.ActionKey] = newAction;   
        }

        public IEnumerator<T> GetEnumerator() {
            return strategyCore.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}