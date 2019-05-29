using System.Collections.Generic;
using System.Linq;

namespace RiskSrc.View.Map {
    class PriorityOrderedList<T> {

        private readonly List<(T, int)> stackCore;
        
        public PriorityOrderedList() {
            stackCore = new List<(T, int)>();
        }

        public void Add(T newElement, int atPriority) {
            if (stackCore.Count == 0
                || stackCore.Last().Item2 < atPriority) {
                stackCore.Add((newElement, atPriority));
                return;
            }
            for (var i = 0; i < stackCore.Count; i++) {
                if (stackCore[i].Item2 > atPriority) {
                    stackCore.Insert(i,(newElement,atPriority));
                    break;
                } else if (stackCore[i].Item2 == atPriority) {
                    stackCore[i] = (newElement, atPriority);
                    break;
                }
            }
            
        }

        public (T,int)? GetHighestPriorityElement() {
            if (stackCore.Count == 0) 
                return null;
            return stackCore.Last();
        }

        public T GetElement(int atPriority) {
            return stackCore.Find(data=>data.Item2 == atPriority).Item1 ;
        }

        public bool Remove(int atPriority) {
            for (var index = 0; index < stackCore.Count; index++) {
                if (stackCore[index].Item2 != atPriority) continue;
                stackCore.RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}