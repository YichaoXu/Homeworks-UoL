//
//  ViewController.swift
//  Week05
//
//  Created by aoo' Mac Mini on 09/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

class ViewController: UIViewController, UITableViewDelegate, UITableViewDataSource {
    
    private var eventModel = EventModel(AppleDelegate: UIApplication.shared.delegate as! AppDelegate)

    @IBOutlet weak var inputTextfield: UITextField!
    @IBOutlet weak var eventTable: UITableView!
    @IBOutlet weak var outputTextfield: UITextField!
    
    @IBAction func clickGoButton(_ sender: Any) {
        inputTextfield.resignFirstResponder()
        guard inputTextfield.hasText else{return}
        eventModel.addUolEvent(with: inputTextfield.text!)
        eventTable.reloadData()
    }
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return eventModel.uolEventArr.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let tmpEvent = eventModel.uolEventArr[indexPath.row]
        let tmpCell = UITableViewCell()
        tmpCell.textLabel?.text = tmpEvent.place
        return tmpCell
    }
    
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }


}

