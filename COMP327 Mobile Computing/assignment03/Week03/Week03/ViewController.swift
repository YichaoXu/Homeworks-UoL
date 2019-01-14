//
//  ViewController.swift
//  Week03Particial
//
//  Created by aoo' Mac Mini on 07/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    let dices = DiceModel(numberOfDices: 2)
    @IBOutlet weak var notificationTxtfield: UITextView!
    @IBOutlet weak var inputTxtfield: UITextField!
    
    @IBAction func clickGuessButton(_ sender: Any) {
        inputTxtfield.resignFirstResponder()
        guard inputTxtfield.hasText else{ return }
        let userInput = Int(inputTxtfield.text!) ?? 0
        let result = dices.rollDices()
        if (userInput == result){
            notificationTxtfield.text = "correct."
        }else{
            notificationTxtfield.text = "wrong. The number is \(result)"
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
    }


}

