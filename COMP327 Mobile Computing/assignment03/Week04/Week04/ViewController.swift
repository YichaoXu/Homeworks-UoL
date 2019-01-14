//
//  ViewController.swift
//  Week04
//
//  Created by aoo' Mac Mini on 08/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

class ViewController: UIViewController, UITableViewDelegate, UITableViewDataSource{
    
    let tableModel = TimesTableModel()
    
    var cellDataArray = [TimesTableCellData]()
    
    @IBOutlet weak var inputTextfield: UITextField!
    
    @IBOutlet weak var table: UITableView!
    
    @IBAction func clickGoButton(_ sender: Any) {
        guard inputTextfield.hasText else { return }
        inputTextfield.resignFirstResponder()
        if let inputNumber = Int(inputTextfield.text!){
            cellDataArray = tableModel.getCellDataArray(multipleBase: inputNumber)
            table.reloadData();
        }else{
            return
        }
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return tableModel.getCellNumber()
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        guard indexPath.row < cellDataArray.count else {return UITableViewCell()}
        let tmpCell = UITableViewCell()
        let tmpCellData = cellDataArray[indexPath.row]
        tmpCell.textLabel?.text = "\(tmpCellData.rowNumber) X \(tmpCellData.multipleBase) = \(tmpCellData.multiple)"
        return tmpCell
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }


}

