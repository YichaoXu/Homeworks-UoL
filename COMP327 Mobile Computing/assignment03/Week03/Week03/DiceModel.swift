//
//  DiceModel.swift
//  Week03Particial
//
//  Created by aoo' Mac Mini on 08/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation

class DiceModel{
    private let numberOfDices: Int
    
    init(numberOfDices num: Int) {
        assert(num>0,"DiceModel.init(numberOfDicesint) the parameter shall be number greater than 0.")
        numberOfDices = num
    }
    
    init() {
        numberOfDices = 1
    }
    
    func rollDices()->Int{
        var result = 0
        for _ in 0..<numberOfDices{
            result += Int.random(in: 1...6)
        }
        return result
    }
}
