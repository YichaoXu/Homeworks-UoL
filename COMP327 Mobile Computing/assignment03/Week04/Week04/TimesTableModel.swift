//
//  CalculationModel.swift
//  Week04
//
//  Created by aoo' Mac Mini on 09/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation

class TimesTableModel{
    
    func getCellDataArray(multipleBase number:Int) -> [TimesTableCellData] {
        var cellArray = [TimesTableCellData]()
        for index in 0..<20 {
            cellArray.append(
                TimesTableCellData(rowNumber: index, multipleBase: number)
            )
        }
        return cellArray
    }
    
    func getCellNumber() -> Int {
        return 20
    }
}

struct TimesTableCellData{
    let rowNumber:Int
    let multipleBase:Int
    var multiple:Int{
        return rowNumber*multipleBase
    }
}
