using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RiskSrc.Model.Data {
    public class RelationMatrix {
        
        private readonly byte[][] matrixCore;

        public RelationMatrix(int row, int column) {
            matrixCore = new byte[row][];
            for (var rowIndex = 0; rowIndex < matrixCore.Length; rowIndex++) 
                matrixCore[rowIndex] = new byte[column];
        }

        public void SetRelation(int row, int column, bool isExist) {
            matrixCore[row][column] = (byte)(isExist ? 1 : 0);
        }


        public List<int> GetColumnIndexesWhichExistRelation(int withRow) {
            var columnNumberList = new List<int>();
            var rowByteArr = matrixCore[withRow];
            for (var i = 0; i < rowByteArr.Length; i++) 
                if (rowByteArr[i] == 1) columnNumberList.Add(i);
            return columnNumberList;
        }

        public List<int> GetRowIndexesWhichExistRelation(int withColumn) {
            var columnNumberList = new List<int>();
            for (var i = 0; i < matrixCore.Length; i++) 
                if (matrixCore[i][withColumn] == 1) columnNumberList.Add(i);
            return columnNumberList;
        }

        public override string ToString() {
            var tmpResult = "\n";
            foreach (var bytesArray in matrixCore) {
                foreach (var b in bytesArray) tmpResult += b + " ";
                tmpResult += "\n";
            }
            return tmpResult;
        }
    }
}