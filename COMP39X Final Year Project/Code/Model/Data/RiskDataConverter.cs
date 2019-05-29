using System;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;

namespace RiskSrc.Model.Data {
    
    public class RiskDataConverter<T> where T:AbstractData {
        
        private readonly JSONArray dataCore; 
        
        public RiskDataConverter(JSONArray jsonDataList) {
            dataCore = jsonDataList;
        }

        public List<T> GetTargetDataList() {
            var dataArray = new T[dataCore.Count];
            foreach (var rawData in dataCore) {
                var id = rawData.Value["ID"];
                var parameter = new object[]{rawData.Value};
                dataArray[id] = (T) Activator.CreateInstance(typeof(T), parameter);

            }
            return dataArray.ToList();
        }

        public RelationMatrix GetRelationMatrix(string with, int row, int column) {
            var matrix = new RelationMatrix(row:row, column:column);
            foreach (var rawData in dataCore) {
                var containedIdArray = rawData.Value[with].AsArray;
                foreach (var containedID in containedIdArray) {
                    matrix.SetRelation(
                        row: rawData.Value["ID"].AsInt, 
                        column: containedID.Value.AsInt,
                        isExist: true
                    );  
                } 
            }
            return matrix;
        }
    }
}